using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using APP_MODEL.ModelData;
using APP_COMMON;
using APP_NOTIFICATION;
using APP_CORE;
using PAYROLLPORTAL.Models;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using PAYROLLPORTAL.Controllers;
using APP_CORE.GetData;
using System.Web.Script.Serialization;
using System.Data.Entity;
using System.IO;
using System.Globalization;

namespace PAYROLLPORTAL.Controllers
{
    public class BlogController : Controller
    {
        private ModelEntities db = new ModelEntities();
        public List<Global_Error_Code> errMessage = new List<Global_Error_Code>();

        #region Get Menu
        public PartialViewResult _Menu()
        {
            var path = UICommonFunction.GetParameter("URL_BLOG_HARIGAJIAN");
            ViewBag.pathBlog = path;
            List<tbl_Menu_Blog> listTblMenuBlog = new List<tbl_Menu_Blog>();
            try
            {
                listTblMenuBlog = db.tbl_Menu_Blog.OrderBy(o => o.Menu_Position).ToList();
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "BlogController.Menu");
            }
            return PartialView(listTblMenuBlog);
        }
        #endregion

        #region Get PageBlog
        public PartialViewResult _Page(int page, string search, string CategoryId)
        {
            List<tbl_Blog_Posts> listTblBlogPost = new List<tbl_Blog_Posts>();
            IQueryable<tbl_Blog_Posts> modelQuery = null;
            try
            {
                modelQuery = GeneralCore.BlogPost();
                if (!string.IsNullOrEmpty(CategoryId))
                {
                    modelQuery = modelQuery.Where(s => s.Category_Id.ToString() == CategoryId);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    modelQuery = modelQuery.Where(s => s.Title.Contains(search));
                }
                listTblBlogPost = modelQuery.Skip((page - 1) * 4).Take(4).ToList();
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "BlogController.Page");
            }
            return PartialView(listTblBlogPost);
        }
        #endregion

        // GET: Blog
        public ViewResult Index(string search, string category)
        {
            Global_Blog_Post globalBlogPostIndex = new Global_Blog_Post();
            try
            {
                ViewBag.categoryId = category;
                ViewBag.searchDetail = search;
                if (string.IsNullOrEmpty(search) && string.IsNullOrEmpty(category))
                {
                    globalBlogPostIndex.BlogArticlePopulerList = db.tbl_Blog_Posts.Where(p => (p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_ACTIVE)).OrderByDescending(o => o.Frequence).Take(6).ToList();
                    globalBlogPostIndex.BlogPostsList = db.tbl_Blog_Posts.Where(p => (p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_ACTIVE)).OrderByDescending(o => o.Created_DateTime).Take(4).ToList();
                }
                if (!string.IsNullOrEmpty(category))
                {
                    globalBlogPostIndex.BlogArticlePopulerList = db.tbl_Blog_Posts.Where(p => p.Category_Id.ToString() == category && (p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_ACTIVE)).OrderByDescending(o => o.Frequence).Take(6).ToList();
                    globalBlogPostIndex.BlogPostsList = db.tbl_Blog_Posts.Where(p => p.Category_Id.ToString() == category && (p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_ACTIVE)).OrderByDescending(o => o.Created_DateTime).Take(4).ToList();
                }
                if (!string.IsNullOrEmpty(search))
                {
                    globalBlogPostIndex.BlogArticlePopulerList = db.tbl_Blog_Posts.Where(p => (p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_ACTIVE)).OrderByDescending(o => o.Frequence).Take(6).ToList();
                    globalBlogPostIndex.BlogPostsList = db.tbl_Blog_Posts.Where(p => p.Title.Contains(search) && (p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_ACTIVE)).OrderByDescending(o => o.Created_DateTime).Take(4).ToList();
                }
                return View(globalBlogPostIndex);
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "BlogController.Index");
                return View(globalBlogPostIndex);
            }

        }

        // GET: Blog/Details/5

        public ActionResult Details(Guid? id)
        {
            Global_Blog_Post globalBlogPostDetail = new Global_Blog_Post();
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                #region DetailBlog and Update Frequence
                globalBlogPostDetail.BlogPostsModels = db.tbl_Blog_Posts.Find(id);
                int? sumFrequence = null;
                if (globalBlogPostDetail.BlogPostsModels.Frequence == null)
                {
                    sumFrequence = 1;
                }
                else
                {
                    sumFrequence = globalBlogPostDetail.BlogPostsModels.Frequence + 1;
                }
                globalBlogPostDetail.BlogPostsModels.Frequence = sumFrequence;
                db.SaveChanges();
                #endregion

                #region Blog Article Populer and Blog News Article
                globalBlogPostDetail.BlogArticlePopulerList = new List<tbl_Blog_Posts>();
                globalBlogPostDetail.BlogNewsArticleList = new List<tbl_Blog_Posts>();

                var path = UICommonFunction.GetParameter("URL_BLOG_HARIGAJIAN");
                globalBlogPostDetail.pathBlog = path;
                globalBlogPostDetail.BlogArticlePopulerList = db.tbl_Blog_Posts.Where(p => p.Category_Id == globalBlogPostDetail.BlogPostsModels.Category_Id).OrderByDescending(o => o.Frequence).Take(8).ToList();
                globalBlogPostDetail.BlogNewsArticleList = db.tbl_Blog_Posts.Where(p => p.Category_Id == globalBlogPostDetail.BlogPostsModels.Category_Id).OrderByDescending(o => o.Created_DateTime).Take(5).ToList();
                #endregion

                if (globalBlogPostDetail.BlogPostsModels == null)
                {
                    return RedirectToAction("Index");
                }

                return View(globalBlogPostDetail);
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "BlogController.Details");
                return View(globalBlogPostDetail);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
