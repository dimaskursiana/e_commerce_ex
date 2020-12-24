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
using PAYROLLPORTAL.Models;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using PAYROLLPORTAL.Controllers;
using APP_CORE.GetData;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Data.Entity;
using System.IO;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net;
using System.Security.Cryptography;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PAYROLLPORTAL.Controllers
{
    public class OrderPaymentController : Controller
    {
        #region Template_Notif
        //Manual
        private string TEMPLATE_MANUAL_PAYMENT_MIDTRANS = "1MP001-PAY01";
        //Automatic
        private string TEMPLATE_AUTOMATIC_PAYMENT_MIDTRANS = "1AP001-PAY01";
        //Manage
        private string TEMPLATE_NOTIF_MANAGE_PAYMENT_MIDTRANS = "1PN002-PNI02";
        //User Account
        private string TEMPLATE_NOTIF_USER_ACCOUNT_PAYMENT_MIDTRANS = "1US002-USI02";
        #endregion

        private ModelEntities db = new ModelEntities();
        public List<Global_Error_Code> errMessage = new List<Global_Error_Code>();

        #region Global Static String
        public static string ContentType = "application/json";
        public static string Accept = "application/json";
        #endregion

        #region EncodeBase64 OAuthToken
        public static string Base64Encode(string ParamEncodeBase64)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ParamEncodeBase64);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        #endregion EncodeBase64 OAuthToken

        #region Index
        // GET: OrderPayment
        public ActionResult Index()
        {
            Global_Payment_Order globalPaymentCalculationOrder = new Global_Payment_Order();
            globalPaymentCalculationOrder.orderPaymentModels = new tbl_Order();
            try
            {
                string urlLink = Request.Url.AbsoluteUri;
                Uri myUri = new Uri(urlLink);
                string paramPackage = HttpUtility.ParseQueryString(myUri.Query).Get("package");
                ViewBag.idcache = Guid.NewGuid().ToString();
                ViewBag.idcache = Guid.NewGuid().ToString();
                ViewBag.clientKey = Common.getSysParam("MIDTRANS_CLIENT_KEY");
                ViewBag.listPricePackage = new SelectList(UISelectlist.List_Price_Package_Midtrans(paramPackage), "Value", "Text");
                var selectedValParamPackagePrice = db.tbl_Package_Pricing.Where(q => q.Description.ToLower() == paramPackage).Select(s => s.Value).FirstOrDefault();
                if (selectedValParamPackagePrice != null)
                {
                    globalPaymentCalculationOrder.orderPaymentModels.Package = selectedValParamPackagePrice.ToString();
                }
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "OrderPaymentController.Index");
            }
            return View(globalPaymentCalculationOrder);
        }
        #endregion

        #region Json Result
        #region GetCalculationOrder
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetCalculationOrder(string getPackageVal, string getNumEmpVal, string getMonthVal, string getPeriodVal, string getPromotionVal, string getIdCache)
        {
            Global_Payment_Order globalPaymentCalculationOrder = new Global_Payment_Order();
            globalPaymentCalculationOrder.orderTieringDetailModels = new tbl_Order_Tiering_Detail();
            globalPaymentCalculationOrder.orderPromotionDetailModels = new tbl_Order_Promotion_Detail();
            globalPaymentCalculationOrder.orderPromotionDetailList = new List<tbl_Order_Promotion_Detail>();

            globalPaymentCalculationOrder.strPromotionCodeList = new List<Global_Promotion_Code_Order>();

            Global_Promotion_Code_Order getPromotionModel = new Global_Promotion_Code_Order();

            try
            {
                #region ValidasiErrorMessage
                errMessage = new List<Global_Error_Code>();
                if (getPromotionVal != "")
                {
                    var listPromotionCode = db.tbl_Promotion.Where(q => q.Promotion_Code == getPromotionVal).ToList();
                    if (listPromotionCode.Count == 0)
                    {
                        {
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "errNotFoundPromotionCode",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_NOTFOUND_PROMOTION_CODE, "EN")
                            });
                        }
                    }
                }
                #endregion

                #region SubPackagePrice

                #region GetPackagePrice
                globalPaymentCalculationOrder.strDetailsDescTotalPackagePrice = "-(" + getPackageVal + "x" + getNumEmpVal + ")x" + getMonthVal;
                globalPaymentCalculationOrder.fltDetailsTotalPackagePrice = (int.Parse(getPackageVal) * int.Parse(getNumEmpVal)) * int.Parse(getMonthVal);
                #endregion

                #region GetTiering
                var getTieringForEmp = db.tbl_Tiering.AsEnumerable().Where(q => q.Tiering_Employee <= int.Parse(getNumEmpVal)).OrderByDescending(o => o.Value).Take(1).FirstOrDefault();
                if (getTieringForEmp != null)
                {
                    globalPaymentCalculationOrder.strDetailsDescTiering = "-" + getTieringForEmp.Description;
                    globalPaymentCalculationOrder.fltDetailsTiering = globalPaymentCalculationOrder.fltDetailsTotalPackagePrice * getTieringForEmp.Value / 100;
                    globalPaymentCalculationOrder.orderTieringDetailModels.id = Guid.NewGuid();
                    globalPaymentCalculationOrder.orderTieringDetailModels.Tiering_Id = getTieringForEmp.id;
                    globalPaymentCalculationOrder.orderTieringDetailModels.Tiering_Code = getTieringForEmp.Tiering_Code;
                    globalPaymentCalculationOrder.orderTieringDetailModels.Reward_Type = getTieringForEmp.Reward_Type;
                    globalPaymentCalculationOrder.orderTieringDetailModels.Tiering_Type = getTieringForEmp.Tiering_Type;
                    globalPaymentCalculationOrder.orderTieringDetailModels.Description = getTieringForEmp.Description;
                    globalPaymentCalculationOrder.orderTieringDetailModels.Value = getTieringForEmp.Value;
                    globalPaymentCalculationOrder.orderTieringDetailModels.Cost_Tiering = globalPaymentCalculationOrder.fltDetailsTiering;
                    globalPaymentCalculationOrder.orderTieringDetailModels.Tiering_Employee = getTieringForEmp.Tiering_Employee;
                }
                #endregion

                #region GetTotalPackagePrice
                globalPaymentCalculationOrder.fltTotalPackagePrice = globalPaymentCalculationOrder.fltDetailsTotalPackagePrice - globalPaymentCalculationOrder.fltDetailsTiering;
                #endregion

                #region GetImplementation
                var getImplementationOrder = db.tbl_SysParam.Where(q => q.Param_Code == "IMPLEMENTATION_PAYMENT_ORDER").FirstOrDefault();
                if (getImplementationOrder != null)
                {
                    globalPaymentCalculationOrder.fltOnTimeImplementation = int.Parse(getImplementationOrder.Value);
                }

                #endregion

                #region GetSubtotalPayment
                globalPaymentCalculationOrder.fltSubtotal = globalPaymentCalculationOrder.fltTotalPackagePrice + globalPaymentCalculationOrder.fltOnTimeImplementation;
                #endregion

                #endregion

                #region SubPromotion
                if (!string.IsNullOrEmpty(getPromotionVal))
                {
                    globalPaymentCalculationOrder.CountPromotionList = db.tbl_Promotion.Where(q => q.Promotion_Code == getPromotionVal).ToList();
                    foreach (var item in globalPaymentCalculationOrder.CountPromotionList)
                    {
                        globalPaymentCalculationOrder.orderPromotionDetailModels = new tbl_Order_Promotion_Detail();
                        if (item.Promotion_Type.ToLower() == "percent")
                        {
                            getPromotionModel.strDetailsDescPromotion = "-" + item.Description;
                            getPromotionModel.strDetailsPromotion = globalPaymentCalculationOrder.fltSubtotal * item.Value / 100;
                            globalPaymentCalculationOrder.strPromotionCodeList.Add(getPromotionModel);
                        }
                        else
                        {
                            getPromotionModel.strDetailsDescPromotion = "-" + item.Description;
                            getPromotionModel.strDetailsPromotion = item.Value;
                            globalPaymentCalculationOrder.strPromotionCodeList.Add(getPromotionModel);

                        }

                        globalPaymentCalculationOrder.orderPromotionDetailModels.id = Guid.NewGuid();
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Promotion_Id = item.id;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Promotion_Code = item.Promotion_Code;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Name = item.Name;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Reward_Type = item.Reward_Type;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Description = item.Description;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Value = item.Value;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Cost_Promotion = getPromotionModel.strDetailsPromotion;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Promotion_Type = item.Promotion_Type;
                        globalPaymentCalculationOrder.orderPromotionDetailList.Add(globalPaymentCalculationOrder.orderPromotionDetailModels);
                    }
                    globalPaymentCalculationOrder.fltDiscount = globalPaymentCalculationOrder.strPromotionCodeList.Sum(s => s.strDetailsPromotion);
                }
                #endregion

                #region SubTax
                var getTaxPaymentOrder = db.tbl_SysParam.Where(q => q.Param_Code == "TAX_PAYMENT_ORDER").FirstOrDefault();
                if (getTaxPaymentOrder != null)
                {
                    globalPaymentCalculationOrder.strDetailsDescTax = "-" + getTaxPaymentOrder.Description;
                    if (string.IsNullOrEmpty(getPromotionVal))
                    {
                        globalPaymentCalculationOrder.fltTax = globalPaymentCalculationOrder.fltSubtotal * int.Parse(getTaxPaymentOrder.Value) / 100;
                    }
                    else
                    {
                        globalPaymentCalculationOrder.fltTax = (globalPaymentCalculationOrder.fltSubtotal - globalPaymentCalculationOrder.fltDiscount) * int.Parse(getTaxPaymentOrder.Value) / 100;
                    }
                }
                #endregion

                #region SubTotalPayment
                if (string.IsNullOrEmpty(getPromotionVal))
                {
                    globalPaymentCalculationOrder.fltTotalPayment = globalPaymentCalculationOrder.fltSubtotal + globalPaymentCalculationOrder.fltTax;
                    globalPaymentCalculationOrder.strTotalPayment = globalPaymentCalculationOrder.fltSubtotal + "+" + globalPaymentCalculationOrder.fltTax;
                }
                else
                {
                    globalPaymentCalculationOrder.fltTotalPayment = (globalPaymentCalculationOrder.fltSubtotal - globalPaymentCalculationOrder.fltDiscount) + globalPaymentCalculationOrder.fltTax;
                    globalPaymentCalculationOrder.strTotalPayment = "(" + globalPaymentCalculationOrder.fltSubtotal + "-" + globalPaymentCalculationOrder.fltDiscount + ")" + "+" + globalPaymentCalculationOrder.fltTax;
                }
                #endregion

                UICache.DataCache_RequestPayment_Set(globalPaymentCalculationOrder, getIdCache);

                globalPaymentCalculationOrder.errMessage = errMessage;
                return Json(globalPaymentCalculationOrder, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                UIException.LogException(ex, "CalculationOrderPayment.Index");
                return Json(globalPaymentCalculationOrder, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Request Api
        [HttpPost]
        public JsonResult GetRequestApiPayment(Global_Payment_Order globalPaymentRequestOrder)
        {
            try
            {
                #region GlobalRequest
                GlobalAPIOnlinePaymentMidtrans globalAPIPaymentMidtrans = new GlobalAPIOnlinePaymentMidtrans();
                globalAPIPaymentMidtrans.RequestWebToMidtrans = new RequestAPIPaymentMidtrans();
                globalAPIPaymentMidtrans.RequestWebToMidtrans.transaction_details = new RequestTransactionDetails();
                globalAPIPaymentMidtrans.RequestWebToMidtrans.item_details = new List<RequestItemDetails>();
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details = new RequestCustomerDetails();
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details.billing_address = new RequestBillingAddress();
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details.shipping_address = new RequestShippingAddress();
                globalAPIPaymentMidtrans.RequestWebToMidtrans.credit_card = new RequestCreditCard();
                globalAPIPaymentMidtrans.RequestWebToMidtrans.credit_card.installment = new RequestInstallment();
                globalAPIPaymentMidtrans.RequestWebToMidtrans.credit_card.installment.terms = new RequestTerms();
                globalAPIPaymentMidtrans.RequestWebToMidtrans.bca_va = new RequestBcaVa();
                globalAPIPaymentMidtrans.RequestWebToMidtrans.bca_va.free_text = new RequestFreeText();
                globalAPIPaymentMidtrans.RequestWebToMidtrans.bca_va.free_text.inquiry = new List<RequestInquiry>();
                globalAPIPaymentMidtrans.RequestWebToMidtrans.bca_va.free_text.payment = new List<RequestPayment>();
                globalAPIPaymentMidtrans.RequestWebToMidtrans.bni_va = new RequestBniVa();
                globalAPIPaymentMidtrans.RequestWebToMidtrans.permata_va = new RequestPermataVa();
                globalAPIPaymentMidtrans.RequestWebToMidtrans.callbacks = new RequestCallbacks();
                globalAPIPaymentMidtrans.RequestWebToMidtrans.expiry = new RequestExpiry();
                #endregion

                #region GlobalResponse
                globalAPIPaymentMidtrans.ResponseMidtransToWeb = new ResponseAPIPaymentMidtrans();
                #endregion

                #region IdChace
                globalAPIPaymentMidtrans.IdChace = globalPaymentRequestOrder.IdChace;
                #endregion

                var API_Midtrans_Payment = Common.getSysParam("MIDTRANS_API_HTTP_LINK");

                #region Request Header
                var AuthorizationSting = Common.getSysParam("MIDTRANS_SERVER_KEY_PAYMENT");
                var AuthorizationBase64 = "Basic" + " " + Base64Encode(AuthorizationSting);
                #endregion

                #region Request Body
                #region transaction_details
                var orderID = UICommonFunction.GetAutoGeneratePaymentOrderIDMidtrans("MIDTRANS_GENERATE_CODE_ID");
                int amountToConvert = Convert.ToInt32(globalPaymentRequestOrder.orderPaymentModels.Total_Payment);
                globalAPIPaymentMidtrans.RequestWebToMidtrans.transaction_details.order_id = orderID;
                globalAPIPaymentMidtrans.RequestWebToMidtrans.transaction_details.gross_amount = amountToConvert;
                #endregion
                #region item_details
                var orderDetailsID = UICommonFunction.GetAutoGeneratePaymentOrderIDDetailsMidtrans(globalPaymentRequestOrder.orderPaymentModels.Package);
                RequestItemDetails reqModelItemDetail = new RequestItemDetails();
                reqModelItemDetail.id = orderDetailsID;
                reqModelItemDetail.price = amountToConvert;
                reqModelItemDetail.quantity = 1;
                reqModelItemDetail.name = orderDetailsID;
                reqModelItemDetail.brand = "Benemica";
                reqModelItemDetail.category = "Package Payment";
                reqModelItemDetail.merchant_name = "Benemica";
                globalAPIPaymentMidtrans.RequestWebToMidtrans.item_details.Add(reqModelItemDetail);
                #endregion

                #region customer_details
                string[] splitValueName = globalPaymentRequestOrder.userOrderModels.Full_Name.Split(' ');
                string valLastName = splitValueName[splitValueName.Length - 1];
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details.first_name = splitValueName[0];
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details.last_name = valLastName;
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details.email = globalPaymentRequestOrder.userOrderModels.Email;
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details.phone = globalPaymentRequestOrder.userOrderModels.Number_Phone_1;
                #region billing_address
                string[] splitValueBillingName = globalPaymentRequestOrder.orderPaymentModels.Billing_To.Split(' ');
                string valBillingLastName = splitValueName[splitValueName.Length - 1];
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details.billing_address.first_name = splitValueBillingName[0];
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details.billing_address.last_name = valBillingLastName;
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details.billing_address.email = null;
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details.billing_address.phone = "";
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details.billing_address.address = globalPaymentRequestOrder.orderPaymentModels.Billing_Address;
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details.billing_address.city = "";
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details.billing_address.postal_code = "";
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details.billing_address.country_code = "";
                #endregion
                #region shipping_address
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details.shipping_address.first_name = splitValueBillingName[0];
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details.shipping_address.last_name = valBillingLastName;
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details.shipping_address.email = null;
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details.shipping_address.phone = "";
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details.shipping_address.address = globalPaymentRequestOrder.orderPaymentModels.Billing_Address;
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details.shipping_address.city = "";
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details.shipping_address.postal_code = "";
                globalAPIPaymentMidtrans.RequestWebToMidtrans.customer_details.shipping_address.country_code = "";
                #endregion
                #endregion

                #region enabled_payments
                globalAPIPaymentMidtrans.RequestWebToMidtrans.enabled_payments = new[] { "credit_card", "mandiri_clickpay", "cimb_clicks", "bca_klikbca", "bca_klikpay", "bri_epay", "echannel", "mandiri_ecash", "permata_va", "bca_va", "bni_va", "other_va", "gopay", "indomaret", "danamon_online", "akulaku" };
                #endregion

                #region credit_card
                globalAPIPaymentMidtrans.RequestWebToMidtrans.credit_card.secure = true;
                globalAPIPaymentMidtrans.RequestWebToMidtrans.credit_card.channel = "migs";
                globalAPIPaymentMidtrans.RequestWebToMidtrans.credit_card.bank = "bca";
                #region installment
                globalAPIPaymentMidtrans.RequestWebToMidtrans.credit_card.installment.required = false;
                globalAPIPaymentMidtrans.RequestWebToMidtrans.credit_card.installment.terms.bni = new[] { 3, 6, 12 };
                globalAPIPaymentMidtrans.RequestWebToMidtrans.credit_card.installment.terms.mandiri = new[] { 3, 6, 12 };
                globalAPIPaymentMidtrans.RequestWebToMidtrans.credit_card.installment.terms.cimb = new[] { 3 };
                globalAPIPaymentMidtrans.RequestWebToMidtrans.credit_card.installment.terms.bca = new[] { 3, 6, 12 };
                globalAPIPaymentMidtrans.RequestWebToMidtrans.credit_card.installment.terms.offline = new[] { 6, 12 };
                #endregion
                globalAPIPaymentMidtrans.RequestWebToMidtrans.credit_card.whitelist_bins = new[] { "48111111", "41111111" };
                #endregion

                #region bca_va
                globalAPIPaymentMidtrans.RequestWebToMidtrans.bca_va.va_number = "12345678911";
                globalAPIPaymentMidtrans.RequestWebToMidtrans.bca_va.sub_company_code = "00000";
                RequestInquiry inquiryModels = new RequestInquiry();
                inquiryModels.en = "text in English";
                inquiryModels.id = "text in Bahasa Indonesia";
                globalAPIPaymentMidtrans.RequestWebToMidtrans.bca_va.free_text.inquiry.Add(inquiryModels);

                RequestPayment requestModels = new RequestPayment();
                requestModels.en = "text in English";
                requestModels.id = "text in Bahasa Indonesia";
                globalAPIPaymentMidtrans.RequestWebToMidtrans.bca_va.free_text.payment.Add(requestModels);
                #endregion

                #region bni_va
                globalAPIPaymentMidtrans.RequestWebToMidtrans.bni_va.va_number = "12345678";
                //globalAPIPaymentMidtrans.RequestWebToMidtrans.bca_va.sub_company_code = "00000";
                //globalAPIPaymentMidtrans.RequestWebToMidtrans.bca_va.free_text.inquiry.en = "text in English";
                //globalAPIPaymentMidtrans.RequestWebToMidtrans.bca_va.free_text.inquiry.id = "text in Bahasa Indonesia";
                //globalAPIPaymentMidtrans.RequestWebToMidtrans.bca_va.free_text.payment.en = "text in English";
                //globalAPIPaymentMidtrans.RequestWebToMidtrans.bca_va.free_text.payment.id = "text in Bahasa Indonesia";
                #endregion

                #region permata_va
                globalAPIPaymentMidtrans.RequestWebToMidtrans.permata_va.va_number = "1234567890";
                globalAPIPaymentMidtrans.RequestWebToMidtrans.permata_va.recipient_name = "SUDARSONO";
                //globalAPIPaymentMidtrans.RequestWebToMidtrans.bca_va.free_text.inquiry.en = "text in English";
                //globalAPIPaymentMidtrans.RequestWebToMidtrans.bca_va.free_text.inquiry.id = "text in Bahasa Indonesia";
                //globalAPIPaymentMidtrans.RequestWebToMidtrans.bca_va.free_text.payment.en = "text in English";
                //globalAPIPaymentMidtrans.RequestWebToMidtrans.bca_va.free_text.payment.id = "text in Bahasa Indonesia";
                #endregion

                #region callbacks
                globalAPIPaymentMidtrans.RequestWebToMidtrans.callbacks.finish = "https://benemica.com";
                #endregion

                #region expiry
                #region Make expiry payment
                var hoursExpire = Common.getSysParam("MIDTRANS_HOURS_EXPIRY");
                DateTime dateTimeExpiry = DateTime.Now.AddHours(Convert.ToDouble(hoursExpire));
                string dateFormatNoZ = dateTimeExpiry.ToString("yyyy-MM-dd HH:mm:ss");
                string[] dateFormatZ = dateTimeExpiry.ToString("zzz").Split(':');
                string splitdateFormatZ = dateFormatZ[0] + dateFormatZ[1];
                string combineDateFormat = dateFormatNoZ + " " + splitdateFormatZ;
                #endregion
                globalAPIPaymentMidtrans.RequestWebToMidtrans.expiry.start_time = combineDateFormat;
                globalAPIPaymentMidtrans.RequestWebToMidtrans.expiry.unit = "minutes";
                globalAPIPaymentMidtrans.RequestWebToMidtrans.expiry.duration = 1;
                #endregion

                #region customfield
                globalAPIPaymentMidtrans.RequestWebToMidtrans.custom_field1 = "custom field 1 content";
                globalAPIPaymentMidtrans.RequestWebToMidtrans.custom_field2 = "custom field 2 content";
                globalAPIPaymentMidtrans.RequestWebToMidtrans.custom_field3 = "custom field 3 content";
                #endregion

                var jsonBodyRequest = JsonConvert.SerializeObject(globalAPIPaymentMidtrans.RequestWebToMidtrans);
                #endregion

                #region Response
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(API_Midtrans_Payment);
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    client.DefaultRequestHeaders.Add("Authorization", AuthorizationBase64);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var stringContent = new StringContent(jsonBodyRequest, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = new HttpResponseMessage();
                    response = client.PostAsync(client.BaseAddress, stringContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var resultString = response.Content.ReadAsStringAsync().Result;
                        globalAPIPaymentMidtrans.ResponseMidtransToWeb = new ResponseAPIPaymentMidtrans();
                        globalAPIPaymentMidtrans.ResponseMidtransToWeb = JsonConvert.DeserializeObject<ResponseAPIPaymentMidtrans>(resultString);
                    }
                }
                #endregion

                UICache.DataCache_RequestPayment_Set(globalPaymentRequestOrder, globalAPIPaymentMidtrans.IdChace + "payinfo");
                return Json(globalAPIPaymentMidtrans, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "APIOrderPayment.Index");
                return Json(globalPaymentRequestOrder, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region ResultAutomaticPaymentMidtrans
        public string SaveResultAutomaticPayment(ResponsePaymentMidtransResult response_payment_midtrans_result)
        {
            GlobalAPIOnlinePaymentMidtrans globalAPIResultPayment = new GlobalAPIOnlinePaymentMidtrans();
            globalAPIResultPayment.StatusPaymentMidtrans = new ResponseStatusPaymentMidtrans();
            globalAPIResultPayment.StatusPaymentMidtrans.response_payment_midtrans_result = new ResponsePaymentMidtransResult();
            try
            {
                Global_Payment_Order globalPaymentCalculationOrder = new Global_Payment_Order();
                globalPaymentCalculationOrder.userOrderModels = new tbl_User_Order();
                globalPaymentCalculationOrder.orderPaymentModels = new tbl_Order();
                globalPaymentCalculationOrder.orderTieringDetailModels = new tbl_Order_Tiering_Detail();
                globalPaymentCalculationOrder.orderPromotionDetailModels = new tbl_Order_Promotion_Detail();
                globalPaymentCalculationOrder.orderPromotionDetailList = new List<tbl_Order_Promotion_Detail>();

                var CachedModel = UICache.DataCache_RequestPayment_Get(response_payment_midtrans_result.IdChace);
                var CachedModelPayInfo = UICache.DataCache_RequestPayment_Get(response_payment_midtrans_result.IdChace + "payinfo");

                #region add table
                #region tbl_User_Order
                globalPaymentCalculationOrder.userOrderModels.id = Guid.NewGuid();
                globalPaymentCalculationOrder.userOrderModels.Company_Name = CachedModelPayInfo.userOrderModels.Company_Name;
                globalPaymentCalculationOrder.userOrderModels.Full_Name = CachedModelPayInfo.userOrderModels.Full_Name;
                var userNameVal = globalPaymentCalculationOrder.userOrderModels.Full_Name;
                var replaceUserName = userNameVal.Replace(" ", ".");
                globalPaymentCalculationOrder.userOrderModels.Username = CachedModelPayInfo.userOrderModels.Email;
                globalPaymentCalculationOrder.userOrderModels.Position = CachedModelPayInfo.userOrderModels.Position;
                globalPaymentCalculationOrder.userOrderModels.Email = CachedModelPayInfo.userOrderModels.Email;
                var encryptPass = UICommonFunction.Encrypt(CachedModelPayInfo.userOrderModels.Password);
                globalPaymentCalculationOrder.userOrderModels.Password = encryptPass;
                globalPaymentCalculationOrder.userOrderModels.Number_Phone_1 = CachedModelPayInfo.userOrderModels.Number_Phone_1;
                globalPaymentCalculationOrder.userOrderModels.Number_Phone_2 = CachedModelPayInfo.userOrderModels.Number_Phone_2;
                globalPaymentCalculationOrder.userOrderModels.Created_DateTime = DateTime.Now;
                db.tbl_User_Order.Add(globalPaymentCalculationOrder.userOrderModels);
                #endregion

                #region tbl_Order
                globalPaymentCalculationOrder.orderPaymentModels.id = Guid.NewGuid();
                globalPaymentCalculationOrder.orderPaymentModels.User_Order_Id = globalPaymentCalculationOrder.userOrderModels.id;
                globalPaymentCalculationOrder.orderPaymentModels.Billing_To = CachedModelPayInfo.orderPaymentModels.Billing_To;
                globalPaymentCalculationOrder.orderPaymentModels.Billing_Address = CachedModelPayInfo.orderPaymentModels.Billing_Address;
                globalPaymentCalculationOrder.orderPaymentModels.Package = CachedModelPayInfo.orderPaymentModels.Package;
                globalPaymentCalculationOrder.orderPaymentModels.Number_Employee = CachedModelPayInfo.orderPaymentModels.Number_Employee;
                globalPaymentCalculationOrder.orderPaymentModels.Month = CachedModelPayInfo.orderPaymentModels.Month;
                globalPaymentCalculationOrder.orderPaymentModels.Month_Period_Start = UICommonFunction.ConvertToDateTime(CachedModelPayInfo.strStartPeriod);
                var endperiodDate = globalPaymentCalculationOrder.orderPaymentModels.Month_Period_Start.Value.AddMonths(Convert.ToInt32(globalPaymentCalculationOrder.orderPaymentModels.Month));
                globalPaymentCalculationOrder.orderPaymentModels.Month_Period_End = endperiodDate;
                globalPaymentCalculationOrder.orderPaymentModels.Total_Package_Price = CachedModelPayInfo.orderPaymentModels.Total_Package_Price;
                globalPaymentCalculationOrder.orderPaymentModels.Cost_Implementation = CachedModelPayInfo.orderPaymentModels.Cost_Implementation;
                globalPaymentCalculationOrder.orderPaymentModels.Subtotal = CachedModelPayInfo.orderPaymentModels.Subtotal;
                globalPaymentCalculationOrder.orderPaymentModels.Total_Discount = CachedModel.fltDiscount;
                globalPaymentCalculationOrder.orderPaymentModels.Tax = CachedModelPayInfo.orderPaymentModels.Tax;
                globalPaymentCalculationOrder.orderPaymentModels.Payment_Date = Convert.ToDateTime(response_payment_midtrans_result.transaction_time);
                globalPaymentCalculationOrder.orderPaymentModels.Valid_Start_Payment = Convert.ToDateTime(response_payment_midtrans_result.transaction_time);
                globalPaymentCalculationOrder.orderPaymentModels.Valid_End_Payment = Convert.ToDateTime(response_payment_midtrans_result.transaction_time);
                globalPaymentCalculationOrder.orderPaymentModels.Payment_Status = Convert.ToInt32(response_payment_midtrans_result.status_code);
                globalPaymentCalculationOrder.orderPaymentModels.Created_DateTime = DateTime.Now;
                globalPaymentCalculationOrder.orderPaymentModels.Order_No = response_payment_midtrans_result.order_id;
                globalPaymentCalculationOrder.orderPaymentModels.Total_Payment = CachedModelPayInfo.orderPaymentModels.Total_Payment;
                globalPaymentCalculationOrder.orderPaymentModels.Payment_Status_Description = response_payment_midtrans_result.status_message;
                globalPaymentCalculationOrder.orderPaymentModels.Payment_Type = response_payment_midtrans_result.payment_type;
                db.tbl_Order.Add(globalPaymentCalculationOrder.orderPaymentModels);
                #endregion

                #region tbl_Order_Tiering_Detail
                globalPaymentCalculationOrder.orderTieringDetailModels.id = CachedModel.orderTieringDetailModels.id;
                globalPaymentCalculationOrder.orderTieringDetailModels.Order_Id = globalPaymentCalculationOrder.orderPaymentModels.id;
                globalPaymentCalculationOrder.orderTieringDetailModels.Tiering_Id = CachedModel.orderTieringDetailModels.Tiering_Id;
                globalPaymentCalculationOrder.orderTieringDetailModels.Tiering_Code = CachedModel.orderTieringDetailModels.Tiering_Code;
                globalPaymentCalculationOrder.orderTieringDetailModels.Reward_Type = CachedModel.orderTieringDetailModels.Reward_Type;
                globalPaymentCalculationOrder.orderTieringDetailModels.Tiering_Type = CachedModel.orderTieringDetailModels.Tiering_Type;
                globalPaymentCalculationOrder.orderTieringDetailModels.Description = CachedModel.orderTieringDetailModels.Description;
                globalPaymentCalculationOrder.orderTieringDetailModels.Value = CachedModel.orderTieringDetailModels.Value;
                globalPaymentCalculationOrder.orderTieringDetailModels.Cost_Tiering = CachedModel.orderTieringDetailModels.Cost_Tiering;
                globalPaymentCalculationOrder.orderTieringDetailModels.Tiering_Employee = CachedModel.orderTieringDetailModels.Tiering_Employee;
                db.tbl_Order_Tiering_Detail.Add(globalPaymentCalculationOrder.orderTieringDetailModels);
                #endregion

                #region tbl_Order_Promotion_Detail
                if (CachedModel.orderPromotionDetailList.Count != 0)
                {
                    foreach (var item in CachedModel.orderPromotionDetailList)
                    {
                        globalPaymentCalculationOrder.orderPromotionDetailModels = new tbl_Order_Promotion_Detail();
                        globalPaymentCalculationOrder.orderPromotionDetailModels.id = item.id;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Order_Id = globalPaymentCalculationOrder.orderPaymentModels.id;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Promotion_Id = item.Promotion_Id;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Promotion_Code = item.Promotion_Code;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Name = item.Name;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Reward_Type = item.Reward_Type;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Description = item.Description;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Value = item.Value;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Cost_Promotion = item.Cost_Promotion;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Promotion_Type = item.Promotion_Type;
                        globalPaymentCalculationOrder.orderPromotionDetailList.Add(globalPaymentCalculationOrder.orderPromotionDetailModels);
                    }
                    db.tbl_Order_Promotion_Detail.AddRange(globalPaymentCalculationOrder.orderPromotionDetailList);
                }
                #endregion
                db.SaveChanges();

                globalAPIResultPayment.StatusPaymentMidtrans.response_payment_midtrans_result = response_payment_midtrans_result;
                UICache.DataCache_APIPaymentMidtrans_Set(globalAPIResultPayment, response_payment_midtrans_result.IdChace + "resultmidtransinfo");
                UICache.DataCache_RequestPayment_Set(globalPaymentCalculationOrder, response_payment_midtrans_result.IdChace + "savetblinfopayment");

                return (response_payment_midtrans_result.IdChace);
                #endregion
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "SaveOrderPaymentInsert.Details");
                return (response_payment_midtrans_result.IdChace);
            }
        }

        public PartialViewResult _ResultAutomaticPayment(string IdChace)
        {
            try
            {
                GlobalAPIOnlinePaymentMidtrans globalAPIResultPayment = new GlobalAPIOnlinePaymentMidtrans();
                globalAPIResultPayment.StatusPaymentMidtrans = new ResponseStatusPaymentMidtrans();
                globalAPIResultPayment.StatusPaymentMidtrans.response_payment_midtrans_result = new ResponsePaymentMidtransResult();

                var CachedModel = UICache.DataCache_RequestPayment_Get(IdChace);
                var CachedModelPayInfo = UICache.DataCache_RequestPayment_Get(IdChace + "savetblinfopayment");
                var CachedModelResultInfo = UICache.DataCache_APIPaymentMidtrans_Get(IdChace + "resultmidtransinfo");

                #region Email Notification AUTOMATIC PAYMENT
                globalAPIResultPayment.SendMailMidtrans = new SendEmailNotifMidtrans();
                List<SendEmailNotifMidtrans> listDataEmailNotifMidtrans = new List<SendEmailNotifMidtrans>();

                globalAPIResultPayment.SendMailMidtrans.order_id = CachedModelResultInfo.StatusPaymentMidtrans.response_payment_midtrans_result.order_id;
                var orderDetailsID = UICommonFunction.GetAutoGeneratePaymentOrderIDDetailsMidtrans(CachedModelPayInfo.orderPaymentModels.Package);
                globalAPIResultPayment.SendMailMidtrans.description_order_id = orderDetailsID;
                globalAPIResultPayment.SendMailMidtrans.url_user_subscribe = UICommonFunction.GetParameter("URL_USER_SUBSCRIBE_MIDTRANS").ToString();
                globalAPIResultPayment.SendMailMidtrans.id = "sub_" + UICommonFunction.Encrypt(CachedModelPayInfo.userOrderModels.id.ToString());
                globalAPIResultPayment.SendMailMidtrans.name = CachedModelPayInfo.userOrderModels.Full_Name;
                globalAPIResultPayment.SendMailMidtrans.username = CachedModelPayInfo.userOrderModels.Email;
                globalAPIResultPayment.SendMailMidtrans.password = UICommonFunction.Decrypt(CachedModelPayInfo.userOrderModels.Password);
                globalAPIResultPayment.SendMailMidtrans.company_name = CachedModelPayInfo.userOrderModels.Company_Name;
                globalAPIResultPayment.SendMailMidtrans.email = CachedModelPayInfo.userOrderModels.Email;
                globalAPIResultPayment.SendMailMidtrans.phone_no = CachedModelPayInfo.userOrderModels.Number_Phone_1;
                string totalPAymentFormat = UICommonFunction.FormatMoney(CachedModelPayInfo.orderPaymentModels.Total_Payment.ToString());
                globalAPIResultPayment.SendMailMidtrans.total_payment = totalPAymentFormat;
                globalAPIResultPayment.SendMailMidtrans.payment_type = CachedModelResultInfo.StatusPaymentMidtrans.response_payment_midtrans_result.payment_type.Replace('_', ' ').ToUpper();
                globalAPIResultPayment.SendMailMidtrans.status_messages = CachedModelResultInfo.StatusPaymentMidtrans.response_payment_midtrans_result.status_message;
                listDataEmailNotifMidtrans.Add(globalAPIResultPayment.SendMailMidtrans);

                #region Add Body Email
                var dbBODYTEMPLATE = db.tbl_Notification_Template.Where(P => P.Template_Code == TEMPLATE_AUTOMATIC_PAYMENT_MIDTRANS).FirstOrDefault();
                //using streamreader for reading my htmltemplate
                string Body = null;
                using (StreamReader reader = new StreamReader(Server.MapPath(dbBODYTEMPLATE.Body)))
                {
                    Body = reader.ReadToEnd();
                }
                #endregion

                #region Add Body Notif Email Manage
                var dbNOTIFBODYTEMPLATE = db.tbl_Notification_Template.Where(P => P.Template_Code == TEMPLATE_NOTIF_MANAGE_PAYMENT_MIDTRANS).FirstOrDefault();
                string NotifBody = null;
                using (StreamReader notifReader = new StreamReader(Server.MapPath(dbNOTIFBODYTEMPLATE.Body)))
                {
                    NotifBody = notifReader.ReadToEnd();
                }
                #endregion

                #region Sender Msg
                Notification.Sender(listDataEmailNotifMidtrans, TEMPLATE_AUTOMATIC_PAYMENT_MIDTRANS, globalAPIResultPayment.SendMailMidtrans.email, Body);

                string sendNotifUserTrial = Common.getSysParam("SALES_HARIGAJIAN");
                Notification.Sender(listDataEmailNotifMidtrans, TEMPLATE_NOTIF_MANAGE_PAYMENT_MIDTRANS, sendNotifUserTrial, NotifBody);

                #endregion

                #region Add Body Notif Email Subscribe
                if (CachedModelResultInfo.StatusPaymentMidtrans.response_payment_midtrans_result.status_code == "200")
                {
                    var dbNOTIFBODYTEMPLATESUBSCRIBE = db.tbl_Notification_Template.Where(P => P.Template_Code == TEMPLATE_NOTIF_USER_ACCOUNT_PAYMENT_MIDTRANS).FirstOrDefault();
                    string NotifBodySubscribe = null;
                    using (StreamReader notifReader = new StreamReader(Server.MapPath(dbNOTIFBODYTEMPLATESUBSCRIBE.Body)))
                    {
                        NotifBodySubscribe = notifReader.ReadToEnd();
                    }
                    Notification.Sender(listDataEmailNotifMidtrans, TEMPLATE_NOTIF_USER_ACCOUNT_PAYMENT_MIDTRANS, globalAPIResultPayment.SendMailMidtrans.email, NotifBodySubscribe);
                }
                #endregion

                #endregion

                globalAPIResultPayment.StatusPaymentMidtrans.response_payment_midtrans_result = CachedModelResultInfo.StatusPaymentMidtrans.response_payment_midtrans_result;
                globalAPIResultPayment.StatusPaymentMidtrans.response_payment_midtrans_result.name = CachedModelPayInfo.userOrderModels.Full_Name;
                DateTime dateFormatTransaction = DateTime.Parse(CachedModelResultInfo.StatusPaymentMidtrans.response_payment_midtrans_result.transaction_time);
                globalAPIResultPayment.StatusPaymentMidtrans.response_payment_midtrans_result.transaction_time = dateFormatTransaction.ToString("dd MMM yyyy HH:mm:ss");
                globalAPIResultPayment.StatusPaymentMidtrans.response_payment_midtrans_result.package_description = orderDetailsID;
                globalAPIResultPayment.StatusPaymentMidtrans.response_payment_midtrans_result.gross_amount = totalPAymentFormat;

                return PartialView(globalAPIResultPayment);
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "_ResultAutomaticPayment.Details");
                return PartialView();
            }
        }
        #endregion

        #region ResultManualPaymentMidtrans
        public PartialViewResult _ResultManualPayment(Global_Payment_Order globalPaymentRequestOrder)
        {
            try
            {
                Global_Payment_Order globalPaymentCalculationOrder = new Global_Payment_Order();
                globalPaymentCalculationOrder.userOrderModels = new tbl_User_Order();
                globalPaymentCalculationOrder.orderPaymentModels = new tbl_Order();
                globalPaymentCalculationOrder.orderTieringDetailModels = new tbl_Order_Tiering_Detail();
                globalPaymentCalculationOrder.orderPromotionDetailModels = new tbl_Order_Promotion_Detail();
                globalPaymentCalculationOrder.orderPromotionDetailList = new List<tbl_Order_Promotion_Detail>();

                UICache.DataCache_RequestPayment_Set(globalPaymentRequestOrder, globalPaymentRequestOrder.IdChace + "payinfo");

                var CachedModel = UICache.DataCache_RequestPayment_Get(globalPaymentRequestOrder.IdChace);
                var CachedModelPayInfo = UICache.DataCache_RequestPayment_Get(globalPaymentRequestOrder.IdChace + "payinfo");

                #region tbl_User_Order
                globalPaymentCalculationOrder.userOrderModels.id = Guid.NewGuid();
                globalPaymentCalculationOrder.userOrderModels.Company_Name = CachedModelPayInfo.userOrderModels.Company_Name;
                globalPaymentCalculationOrder.userOrderModels.Full_Name = CachedModelPayInfo.userOrderModels.Full_Name;
                var userNameVal = globalPaymentCalculationOrder.userOrderModels.Full_Name;
                var replaceUserName = userNameVal.Replace(" ", ".");
                globalPaymentCalculationOrder.userOrderModels.Username = CachedModelPayInfo.userOrderModels.Email;
                globalPaymentCalculationOrder.userOrderModels.Position = CachedModelPayInfo.userOrderModels.Position;
                globalPaymentCalculationOrder.userOrderModels.Email = CachedModelPayInfo.userOrderModels.Email;
                var encryptPass = UICommonFunction.Encrypt(CachedModelPayInfo.userOrderModels.Password);
                globalPaymentCalculationOrder.userOrderModels.Password = encryptPass;
                globalPaymentCalculationOrder.userOrderModels.Number_Phone_1 = CachedModelPayInfo.userOrderModels.Number_Phone_1;
                globalPaymentCalculationOrder.userOrderModels.Number_Phone_2 = CachedModelPayInfo.userOrderModels.Number_Phone_2;
                globalPaymentCalculationOrder.userOrderModels.Created_DateTime = DateTime.Now;
                db.tbl_User_Order.Add(globalPaymentCalculationOrder.userOrderModels);
                #endregion

                #region tbl_Order
                globalPaymentCalculationOrder.orderPaymentModels.id = Guid.NewGuid();
                globalPaymentCalculationOrder.orderPaymentModels.User_Order_Id = globalPaymentCalculationOrder.userOrderModels.id;
                globalPaymentCalculationOrder.orderPaymentModels.Billing_To = CachedModelPayInfo.orderPaymentModels.Billing_To;
                globalPaymentCalculationOrder.orderPaymentModels.Billing_Address = CachedModelPayInfo.orderPaymentModels.Billing_Address;
                globalPaymentCalculationOrder.orderPaymentModels.Package = CachedModelPayInfo.orderPaymentModels.Package;
                globalPaymentCalculationOrder.orderPaymentModels.Number_Employee = CachedModelPayInfo.orderPaymentModels.Number_Employee;
                globalPaymentCalculationOrder.orderPaymentModels.Month = CachedModelPayInfo.orderPaymentModels.Month;
                globalPaymentCalculationOrder.orderPaymentModels.Month_Period_Start = UICommonFunction.ConvertToDateTime(CachedModelPayInfo.strStartPeriod);
                var endperiodDate = globalPaymentCalculationOrder.orderPaymentModels.Month_Period_Start.Value.AddMonths(Convert.ToInt32(globalPaymentCalculationOrder.orderPaymentModels.Month));
                globalPaymentCalculationOrder.orderPaymentModels.Month_Period_End = endperiodDate;
                globalPaymentCalculationOrder.orderPaymentModels.Total_Package_Price = CachedModelPayInfo.orderPaymentModels.Total_Package_Price;
                globalPaymentCalculationOrder.orderPaymentModels.Cost_Implementation = CachedModelPayInfo.orderPaymentModels.Cost_Implementation;
                globalPaymentCalculationOrder.orderPaymentModels.Subtotal = CachedModelPayInfo.orderPaymentModels.Subtotal;
                globalPaymentCalculationOrder.orderPaymentModels.Total_Discount = CachedModel.fltDiscount;
                globalPaymentCalculationOrder.orderPaymentModels.Tax = CachedModelPayInfo.orderPaymentModels.Tax;
                globalPaymentCalculationOrder.orderPaymentModels.Payment_Date = DateTime.Now;
                globalPaymentCalculationOrder.orderPaymentModels.Valid_Start_Payment = DateTime.Now;
                globalPaymentCalculationOrder.orderPaymentModels.Valid_End_Payment = DateTime.Now;
                globalPaymentCalculationOrder.orderPaymentModels.Payment_Status = Convert.ToInt32("406");
                globalPaymentCalculationOrder.orderPaymentModels.Created_DateTime = DateTime.Now;
                var orderID = UICommonFunction.GetAutoGeneratePaymentOrderIDMidtrans("MIDTRANS_GENERATE_CODE_ID");
                globalPaymentCalculationOrder.orderPaymentModels.Order_No = orderID;
                globalPaymentCalculationOrder.orderPaymentModels.Total_Payment = CachedModelPayInfo.orderPaymentModels.Total_Payment;
                globalPaymentCalculationOrder.orderPaymentModels.Payment_Status_Description = "Transaction Pending";
                globalPaymentCalculationOrder.orderPaymentModels.Payment_Type = "Bank Transfer";
                db.tbl_Order.Add(globalPaymentCalculationOrder.orderPaymentModels);

                #endregion

                #region tbl_Order_Tiering_Detail
                globalPaymentCalculationOrder.orderTieringDetailModels.id = CachedModel.orderTieringDetailModels.id;
                globalPaymentCalculationOrder.orderTieringDetailModels.Order_Id = globalPaymentCalculationOrder.orderPaymentModels.id;
                globalPaymentCalculationOrder.orderTieringDetailModels.Tiering_Id = CachedModel.orderTieringDetailModels.Tiering_Id;
                globalPaymentCalculationOrder.orderTieringDetailModels.Tiering_Code = CachedModel.orderTieringDetailModels.Tiering_Code;
                globalPaymentCalculationOrder.orderTieringDetailModels.Reward_Type = CachedModel.orderTieringDetailModels.Reward_Type;
                globalPaymentCalculationOrder.orderTieringDetailModels.Tiering_Type = CachedModel.orderTieringDetailModels.Tiering_Type;
                globalPaymentCalculationOrder.orderTieringDetailModels.Description = CachedModel.orderTieringDetailModels.Description;
                globalPaymentCalculationOrder.orderTieringDetailModels.Value = CachedModel.orderTieringDetailModels.Value;
                globalPaymentCalculationOrder.orderTieringDetailModels.Cost_Tiering = CachedModel.orderTieringDetailModels.Cost_Tiering;
                globalPaymentCalculationOrder.orderTieringDetailModels.Tiering_Employee = CachedModel.orderTieringDetailModels.Tiering_Employee;
                db.tbl_Order_Tiering_Detail.Add(globalPaymentCalculationOrder.orderTieringDetailModels);
                #endregion

                #region tbl_Order_Promotion_Detail
                if (CachedModel.orderPromotionDetailList.Count != 0)
                {
                    foreach (var item in CachedModel.orderPromotionDetailList)
                    {
                        globalPaymentCalculationOrder.orderPromotionDetailModels = new tbl_Order_Promotion_Detail();
                        globalPaymentCalculationOrder.orderPromotionDetailModels.id = item.id;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Order_Id = globalPaymentCalculationOrder.orderPaymentModels.id;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Promotion_Id = item.Promotion_Id;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Promotion_Code = item.Promotion_Code;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Name = item.Name;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Reward_Type = item.Reward_Type;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Description = item.Description;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Value = item.Value;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Cost_Promotion = item.Cost_Promotion;
                        globalPaymentCalculationOrder.orderPromotionDetailModels.Promotion_Type = item.Promotion_Type;
                        globalPaymentCalculationOrder.orderPromotionDetailList.Add(globalPaymentCalculationOrder.orderPromotionDetailModels);
                    }
                    db.tbl_Order_Promotion_Detail.AddRange(globalPaymentCalculationOrder.orderPromotionDetailList);
                }
                #endregion

                db.SaveChanges();

                UICache.DataCache_RequestPayment_Set(globalPaymentCalculationOrder, globalPaymentRequestOrder.IdChace + "savetblinfopayment");

                #region Email Notification Manual Payment
                List<SendEmailNotifMidtrans> listDataEmailNotifMidtrans = new List<SendEmailNotifMidtrans>();
                var dataEmailNotifMidtransModels = new SendEmailNotifMidtrans();

                dataEmailNotifMidtransModels.order_id = globalPaymentCalculationOrder.orderPaymentModels.Order_No;
                var orderDetailsID = UICommonFunction.GetAutoGeneratePaymentOrderIDDetailsMidtrans(globalPaymentCalculationOrder.orderPaymentModels.Package);
                dataEmailNotifMidtransModels.description_order_id = orderDetailsID;
                dataEmailNotifMidtransModels.name = globalPaymentCalculationOrder.userOrderModels.Full_Name;
                dataEmailNotifMidtransModels.company_name = globalPaymentCalculationOrder.userOrderModels.Company_Name;
                dataEmailNotifMidtransModels.email = globalPaymentCalculationOrder.userOrderModels.Email;
                dataEmailNotifMidtransModels.phone_no = globalPaymentCalculationOrder.userOrderModels.Number_Phone_1;
                string totalPaymentFormat = UICommonFunction.FormatMoney(globalPaymentCalculationOrder.orderPaymentModels.Total_Payment.ToString());
                dataEmailNotifMidtransModels.total_payment = totalPaymentFormat;
                string getDataSysParam = Common.getSysParam("MIDTRANS_TRANSFER_ACCOUNT");
                string[] splitAccountTransfer = getDataSysParam.Split('|');
                dataEmailNotifMidtransModels.account_payment_transfer = splitAccountTransfer[0];
                dataEmailNotifMidtransModels.account_no_payment_transfer = splitAccountTransfer[1];
                dataEmailNotifMidtransModels.payment_type = globalPaymentCalculationOrder.orderPaymentModels.Payment_Type;
                listDataEmailNotifMidtrans.Add(dataEmailNotifMidtransModels);

                #region Add Body Email
                var dbBODYTEMPLATE = db.tbl_Notification_Template.Where(P => P.Template_Code == TEMPLATE_MANUAL_PAYMENT_MIDTRANS).FirstOrDefault();
                //using streamreader for reading my htmltemplate
                string Body = null;
                using (StreamReader reader = new StreamReader(Server.MapPath(dbBODYTEMPLATE.Body)))
                {
                    Body = reader.ReadToEnd();
                }
                #endregion

                #region Add Body Notif Email Manual
                var dbNOTIFBODYTEMPLATE = db.tbl_Notification_Template.Where(P => P.Template_Code == TEMPLATE_NOTIF_MANAGE_PAYMENT_MIDTRANS).FirstOrDefault();
                string NotifBody = null;
                using (StreamReader notifReader = new StreamReader(Server.MapPath(dbNOTIFBODYTEMPLATE.Body)))
                {
                    NotifBody = notifReader.ReadToEnd();
                }
                #endregion

                #region Sender Msg
                Notification.Sender(listDataEmailNotifMidtrans, TEMPLATE_MANUAL_PAYMENT_MIDTRANS, dataEmailNotifMidtransModels.email, Body);

                string sendNotifUserTrial = Common.getSysParam("SALES_HARIGAJIAN");
                Notification.Sender(listDataEmailNotifMidtrans, TEMPLATE_NOTIF_MANAGE_PAYMENT_MIDTRANS, sendNotifUserTrial, NotifBody);
                #endregion
                #endregion

                globalPaymentCalculationOrder.packagedescription = orderDetailsID;
                globalPaymentCalculationOrder.strTotalPayment = totalPaymentFormat;
                string formatPaymentDate = DateTime.Now.ToString("dd MMM yyyy HH:mm:ss");
                globalPaymentCalculationOrder.formatDatePayment = formatPaymentDate;

                return PartialView(globalPaymentCalculationOrder);
            }
            catch (Exception ex)
            {
                UIException.LogException(ex, "_ResultManualPayment.Details");
                return PartialView();
            }

        }
        #endregion

        #endregion

        public PartialViewResult _PackageBasic()
        {
            return PartialView();
        }
        public PartialViewResult _PackageAdvance()
        {
            return PartialView();
        }
        public PartialViewResult _PackageAdvancePlus()
        {
            return PartialView();
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