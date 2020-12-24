using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace APP_MODEL.ModelData
{
    public class Global_API_Online_Payment
    {
        public string ErrorTransferred { get; set; }
        public ParameterPost ParameterPost { get; set; }
        public RequestDanamonToDanamon RequestDanamonToDanamon { get; set; }
        public ResponseDanamonToDanamon ResponseDanamonToDanamon { get; set; }
        public RequestDanamonToOthers RequestDanamonToOthers { get; set; }
        public ResponseDanamonToOthers ResponseDanamonToOthers { get; set; }
        public OAuthorizationTokenRequest OAuthorizationTokenRequest { get; set; }
        public OAuthorizationTokenResponse OAuthorizationTokenResponse { get; set; }
        public RequestDataOutputInquiryBalance RequestDataOutputInquiryBalance { get; set; }
        public ResponseDataOutputInquiryBalance ResponseDataOutputInquiryBalance { get; set; }
        public RequestDataOutputInquiryName RequestDataOutputInquiryName { get; set; }
        public ResponseDataOutputInquiryName ResponseDataOutputInquiryName { get; set; }
        public ResponseFailedDanamon ResponseFailedDanamon { get; set; }
        public DataTransfer DataTransfer { get; set; }
    }
    public class ResponseFailedDanamon
    {
        public string ErrorCode { get; set; }
        //public List<ResponseErrorMessageDanamon> ErrorMessage { get; set; }
        public ResponseErrorMessageDanamon ErrorMessage { get; set; }
        public string ErrorDescription { get; set; }
    }
    public class ResponseErrorMessageDanamon
    {
        public string Indonesian { get; set; }
        public string English { get; set; }
    }
    public class ParameterPost
    {
        public string Batch_Number { get; set; }
        public string Action_Type { get; set; }
    }
    public class RequestDanamonToDanamon
    {
        public string UserReferenceNumber { get; set; }
        public string RequestTime { get; set; }
        public string SourceAccountNumber { get; set; }
        public string SourceCardNumber { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public string BeneficiaryName { get; set; }
        public string Amount { get; set; }
        public string Description { get; set; }
        public string TransactionDate { get; set; }
        //public string channel_id { get; set; }
        //public string bin_no { get; set; }
        //public string user_ref_no { get; set; }
        //public DateTime request_time { get; set; }
        //public string cod_acct_no_dr { get; set; }
        //public string cod_card_no_dr { get; set; }
        //public string cod_acct_no_cr { get; set; }
        //public string benef_name { get; set; }
        //public string amount { get; set; }
        //public string desc { get; set; }
        //public DateTime date_txn { get; set; }
        //public string service_code { get; set; }
        //public string auth_token { get; set; }
    }
    public class ResponseDanamonToDanamon
    {
        public string UserReferenceNumber { get; set; }
        public string ResponseTime { get; set; }
        public string CodeStatus { get; set; }
        public string DescriptionStatus { get; set; }
        //public string channel_id { get; set; }
        //public string bin_no { get; set; }
        //public string code_status { get; set; }
        //public string desc_status { get; set; }
        //public string user_ref_no { get; set; }
        //public string service_code { get; set; }
        //public DateTime response_time { get; set; }
        //public string auth_token { get; set; }
    }
    public class RequestDanamonToOthers
    {
        public string UserReferenceNumber { get; set; }
        public string RequestTime { get; set; }
        public string SourceAccountNumber { get; set; }
        public string SourceCardNumber { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryAddress { get; set; }
        public string BeneficiaryType { get; set; }
        public string BeneficiaryStatus { get; set; }
        public string BeneficiaryBICode { get; set; }
        public string BeneficiaryBranchCode { get; set; }
        public string BeneficiaryBankName { get; set; }
        public string Amount { get; set; }
        public string Description { get; set; }
        public string TransactionDate { get; set; }
        //public string channel_id { get; set; }
        //public string bin_no { get; set; }
        //public string user_ref_no { get; set; }
        //public DateTime request_time { get; set; }
        //public string cod_acct_no_dr { get; set; }
        //public string cod_card_no_dr { get; set; }
        //public string transfer_type { get; set; }
        //public string cod_acct_no_cr { get; set; }
        //public string benef_name { get; set; }
        //public string benef_address { get; set; }
        //public string benef_type { get; set; }
        //public string benef_status { get; set; }
        //public string benef_bi_code { get; set; }
        //public string benef_bank_code { get; set; }
        //public string benef_brn_code { get; set; }
        //public string benef_bank_name { get; set; }
        //public string amount { get; set; }
        //public string desc { get; set; }
        //public DateTime date_txn { get; set; }
        //public string service_code { get; set; }
        //public string auth_token { get; set; }
    }
    public class ResponseDanamonToOthers
    {
        public string UserReferenceNumber { get; set; }
        public string ResponseTime { get; set; }
        public string CodeStatus { get; set; }
        public string DescriptionStatus { get; set; }
        //public string channel_id { get; set; }
        //public string bin_no { get; set; }
        //public string code_status { get; set; }
        //public string desc_status { get; set; }
        //public string user_ref_no { get; set; }
        //public string service_code { get; set; }
        //public DateTime response_time { get; set; }
        //public string auth_token { get; set; }
    }
    public class OAuthorizationTokenRequest
    {
        public string grant_type { get; set; }
    }
    public class OAuthorizationTokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string scope { get; set; }
    }
    public class RequestDataOutputInquiryBalance
    {
        public string UserReferenceNumber { get; set; }
        public string RequestTime { get; set; }
        public string AccountNumber { get; set; }

        //public string channel_id { get; set; }
        //public string bin_no { get; set; }
        //public string user_ref_no { get; set; }
        //public DateTime request_time { get; set; }
        //public string account_no { get; set; }
        //public string service_code { get; set; }
        //public string auth_token { get; set; }
    }
    public class ResponseDataOutputInquiryBalance
    {
        public string CodeStatus { get; set; }
        public string DescriptionStatus { get; set; }
        public string UserReferenceNumber { get; set; }
        public string ResponseTime { get; set; }
        public string AccountCurrency { get; set; }
        public string CurrentBalance { get; set; }
        public string AvailableBalance { get; set; }
        public string OverDraftLimit { get; set; }
        public string HoldAmount { get; set; }
        public string MinimumBalance { get; set; }
        //public string channel_id { get; set; }
        //public string bin_no { get; set; }
        //public string code_status { get; set; }
        //public string desc_status { get; set; }
        //public string user_ref_no { get; set; }
        //public string service_code { get; set; }
        //public DateTime response_time { get; set; }
        //public string auth_token { get; set; }
        //public string account_currency { get; set; }
        //public string current_balance { get; set; }
        //public string available_balance { get; set; }
        //public string od_limit { get; set; }
        //public DateTime hold_amount { get; set; }
        //public string minimum_balance { get; set; }
    }
    public class RequestDataOutputInquiryName
    {
        public string UserReferenceNumber { get; set; }
        public string RequestTime { get; set; }
        public string AccountNumber { get; set; }
        //public string channel_id { get; set; }
        //public string bin_no { get; set; }
        //public string user_ref_no { get; set; }
        //public DateTime request_time { get; set; }
        //public string account_no { get; set; }
        //public string bank_code { get; set; }
        //public string service_code { get; set; }
        //public string auth_token { get; set; }
    }
    public class ResponseDataOutputInquiryName
    {
        public string UserReferenceNumber { get; set; }
        public string ResponseTime { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string CodeStatus { get; set; }
        public string DescriptionStatus { get; set; }
        //public string channel_id { get; set; }
        //public string bin_no { get; set; }
        //public string code_status { get; set; }
        //public string desc_status { get; set; }
        //public string user_ref_no { get; set; }
        //public string bank_code { get; set; }
        //public string service_code { get; set; }
        //public string account_no { get; set; }
        //public string account_name { get; set; }
        //public DateTime response_time { get; set; }
        //public string auth_token { get; set; }
    }

    public class WebServiceLogs
    {
        public string[] StringBuilder { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public RequestDataOutputInquiryBalance ReqInqBalance { get; set; }
        public ResponseDataOutputInquiryBalance ResInqBalance { get; set; }
    }

    public class DataTransfer
    {
        public List<tbl_Trx_Bank_Transfer> ListTrxBankTransfer { get; set; }
        public List<tbl_Trx_Bank_Transfer_Detail> ListTrxBankTransferDetail { get; set; }
        public List<tbl_Trx_Bank_Transfer> ListTrxBankTransferDanamonToDanamon { get; set; }
        public List<tbl_Trx_Bank_Transfer_Detail> ListTrxBankTransferDetailDanamonToDanamon { get; set; }
        public List<tbl_Trx_Bank_Transfer> ListTrxBankTransferDanamonToOtherBank { get; set; }
        public List<tbl_Trx_Bank_Transfer_Detail> ListTrxBankTransferDetailToOtherBank { get; set; }
    }
}