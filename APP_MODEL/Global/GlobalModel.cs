using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP_MODEL.ModelData;

namespace APP_MODEL.Global
{
    /// <summary>
    /// Created By : Ali Mubarokah
    /// Created Date : 20 Feb 2017
    /// Purpose : Global Class Model 
    /// 

    /// Purpose : Pagging Data
    public class PageQuery
    { 
        public Guid? idcache { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string filter1 { get; set; }
        public string filter2 { get; set; }
        public string filter3 { get; set; }
        public string filter4 { get; set; }
        public string filter5 { get; set; }
        public string filter6 { get; set; }
        public string filter7 { get; set; }
        public string filterpool1 { get; set; }
        public string filterpool2 { get; set; }
        public string filterpool3 { get; set; }
        public string filterpool4 { get; set; }
        public string filterpool5 { get; set; }
        public string misc { get; set; }
        public string CurrentSort { get; set; }
        public string SortDirection { get; set; }
        public int CurrentPageSize { get; set; }
        public int Page { get; set; }
        public int SkipPage { get; set; }
        public int PageCount { get; set; }

    }
     
    public class RuleOfButton
    {
        public int? Status { get; set; }
        public string AddRow { get; set; }
        public string DeleteRow { get; set; }
        public string Upload { get; set; }
        public string Save { get; set; }
        public string Submit { get; set; }
        public string CheckBox { get; set; }
        public bool isRegulator { get; set; }
    }

 
}

