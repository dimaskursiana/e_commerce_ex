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
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using APP_CORE.GetData;
using System.Net.Mail;
using System.Text.RegularExpressions;
using APP_CORE;
using System.IO;

namespace APP_COMMON
{
    public class ModelValidate
    {
        public static bool status = false;
        public static string ErrorMessage = "";
        InputValidation inputValidation = new InputValidation();
        #region validation organization maintenence

        #region validation Organization
        public static bool ValidationOrganization(Global_Organization inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                inputModel.organizationList = db.tbl_Organization.Where(s => s.Organization_Code == inputModel.organizationModels.Organization_Code && !(s.Authorize_Status == CoreVariable.CONST_AUTHORIZED && s.Status_Code == CoreVariable.CONST_STATUS_DELETED)).ToList();

                if (type == GlobalVariable.CONST_EDIT)
                {

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationModels.Organization_Code))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "ORGANIZATION CODE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_CODE_IS_EMPTY, language)
                        });
                    }

                }

                if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationModels.Organization_Name))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_ORGANIZATION_NAME_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_NAME_IS_EMPTY, language)
                    });
                }

                if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationModels.Organization_Service))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_ORGANIZATION_SERVICE_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_SERVICE_IS_EMPTY, language)
                    });
                }

                if (UICommonFunction.IsNotAlphaNumeric(inputModel.organizationModels.Organization_Name))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "Organization_Name",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORG_NAME_MUST_ALPHA_NUMERIC, language)
                    });
                }

            }
            catch (Exception ex)
            {
                status = false;
                ErrorMessage = ex.ToString();

                if (ex.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + ex.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, ex.StackTrace);
            }
            return status;
        }
        #endregion
        public static bool ValidationMapLocation(Global_Base_Location inputModel, string language, string type, User_Data USERDATA, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                if (type == GlobalVariable.CONST_CREATE)
                {
                    var baseLocation = db.tbl_Base_Location_Header.Where(s => s.Organization_Id == USERDATA.OrganizationSelected_Id && s.Reference_Id == inputModel.BaseLocationHeaderModel.Reference_Id && !(s.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && s.Status_Code == GlobalVariable.CONST_STATUS_DELETED)).ToList();
                    if (baseLocation.Count() > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_MAP_LOCATION_DUPLICATE_REF_ID,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_MAP_LOCATION_DUPLICATE_REF_ID, language)
                        });
                    }
                    if (inputModel.BaseLocationHeaderModel.Is_Anywhere_Time_In==false && inputModel.BaseLocationHeaderModel.Is_Anywhere_Time_Out == false)
                    {
                        foreach (var item in inputModel.strLatitude)
                        {
                            if (string.IsNullOrEmpty(item))
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_MAP_LOCATION_NAME,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_MAP_LOCATION_LATITUDE, language)
                                });
                            }
                        }

                        foreach (var item in inputModel.strLongitude)
                        {
                            if (string.IsNullOrEmpty(item))
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_MAP_LOCATION_NAME,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_MAP_LOCATION_LONGITUDE, language)
                                });
                            }
                        }

                        foreach (var item in inputModel.strLocationName)
                        {
                            if (string.IsNullOrEmpty(item))
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_MAP_LOCATION_NAME,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_MAP_LOCATION_NAME, language)
                                });
                            }
                        }

                        foreach (var item in inputModel.strRange)
                        {
                            if (string.IsNullOrEmpty(item))
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_MAP_LOCATION_NAME,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_MAP_LOCATION_RANGE, language)
                                });
                            }
                        }

                        foreach (var item in inputModel.strStartDt)
                        {
                            if (string.IsNullOrEmpty(item))
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_MAP_LOCATION_NAME,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_MAP_LOCATION_START_DATE, language)
                                });
                            }
                        }

                        foreach (var item in inputModel.strEndDt)
                        {
                            if (string.IsNullOrEmpty(item))
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_MAP_LOCATION_NAME,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_MAP_LOCATION_END_DATE, language)
                                });
                            }
                        }
                    }
                }
                if (type == GlobalVariable.CONST_EDIT)
                {
                    if (inputModel.BaseLocationHeaderModel.Is_Anywhere_Time_In == false && inputModel.BaseLocationHeaderModel.Is_Anywhere_Time_Out == false)
                    {
                        foreach (var item in inputModel.strLatitude)
                        {
                            if (string.IsNullOrEmpty(item))
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_MAP_LOCATION_NAME,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_MAP_LOCATION_LATITUDE, language)
                                });
                            }
                        }

                        foreach (var item in inputModel.strLongitude)
                        {
                            if (string.IsNullOrEmpty(item))
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_MAP_LOCATION_NAME,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_MAP_LOCATION_LONGITUDE, language)
                                });
                            }
                        }

                        foreach (var item in inputModel.strLocationName)
                        {
                            if (string.IsNullOrEmpty(item))
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_MAP_LOCATION_NAME,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_MAP_LOCATION_NAME, language)
                                });
                            }
                        }

                        foreach (var item in inputModel.strRange)
                        {
                            if (string.IsNullOrEmpty(item))
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_MAP_LOCATION_NAME,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_MAP_LOCATION_RANGE, language)
                                });
                            }
                        }

                        foreach (var item in inputModel.strStartDt)
                        {
                            if (string.IsNullOrEmpty(item))
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_MAP_LOCATION_NAME,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_MAP_LOCATION_START_DATE, language)
                                });
                            }
                        }

                        foreach (var item in inputModel.strEndDt)
                        {
                            if (string.IsNullOrEmpty(item))
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_MAP_LOCATION_NAME,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_MAP_LOCATION_END_DATE, language)
                                });
                            }
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                status = false;
                ErrorMessage = ex.ToString();

                if (ex.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + ex.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, ex.StackTrace);
            }
            return status;
        }
    
        public static bool ValidationExchangeFile(Global_Exchange_File inputModel,HttpPostedFileBase file, string language, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                if (file == null || file.ContentLength == 0)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_EXCHAGEFILE_FILE_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EXCHAGEFILE_FILE_EMPTY, language)
                    });
                }

                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        var ListFormatFile = db.tbl_SysParam.Where(s => s.Param_Code == "Format_File").FirstOrDefault().Value.Split('|');
                        int MaxSizeFile = int.Parse(db.tbl_SysParam.Where(s => s.Param_Code == "Max_File_Size").FirstOrDefault().Value);
                        var fileExt = Path.GetExtension(file.FileName);
                        int sizefile = file.ContentLength;

                        if (!ListFormatFile.Contains(fileExt.ToUpper()))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_EXCHAGEFILE_FILE_FORMAT,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EXCHAGEFILE_FILE_FORMAT, language)
                            });
                        }

                        if (sizefile > MaxSizeFile)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_EXCHAGEFILE_FILE_SIZE,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EXCHAGEFILE_FILE_SIZE, language)
                            });
                        }
                    }
                    
                }
               

            }
            catch (Exception ex)
            {
                status = false;
                ErrorMessage = ex.ToString();

                if (ex.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + ex.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, ex.StackTrace);
            }
            return status;
        }


        public static bool ValidationEmployeeDocument(Global_Employee_Document inputModel, HttpPostedFileBase file, string language, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                if (file == null || file.ContentLength == 0)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_EMP_DOC_FILE_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMP_DOC_FILE_EMPTY, language)
                    });
                }

                if (file != null)
                {


                    if (file.ContentLength > 0)
                    {
                        var ListFormatFile = db.tbl_SysParam.Where(s => s.Param_Code == "Format_File").FirstOrDefault().Value.Split('|');
                        int MaxSizeFile = int.Parse(db.tbl_SysParam.Where(s => s.Param_Code == "Max_File_Size").FirstOrDefault().Value);
                        var fileExt = Path.GetExtension(file.FileName).ToUpper();
                        int sizefile = file.ContentLength;

                        if(GeneralCore.EmployeeDocumentQuery().Where(p => p.Type == inputModel.employeeDocumentModels.Type).Count() > 0)//exist file
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_EMP_DOC_FILE_FILE_EXIST,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMP_DOC_FILE_FILE_EXIST, language).Replace("File Type", inputModel.employeeDocumentModels.Type)
                            });
                        }
                        
                        if (!ListFormatFile.Contains(fileExt))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_EMP_DOC_FILE_FILE_FORMAT,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMP_DOC_FILE_FILE_FORMAT, language)
                            });
                        }

                        if (sizefile > MaxSizeFile)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_EMP_DOC_FILE_FILE_SIZE,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMP_DOC_FILE_FILE_SIZE, language)
                            });
                        }


                    }

                }


            }
            catch (Exception ex)
            {
                status = false;
                ErrorMessage = ex.ToString();

                if (ex.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + ex.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, ex.StackTrace);
            }
            return status;
        }

        #region validation Organization Structure
        public static bool ValidationOrganizationStructure(Global_Organization_Structure inputModel, Guid OrganizationSelected, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                inputModel.organizationStructureList = GeneralCore.OrganizationStructurQuery().Where(s => s.Organization_ID == OrganizationSelected && s.Struktur == inputModel.organizationStructureModels.Struktur && s.Code == inputModel.organizationStructureModels.Code).ToList();

                if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationStructureModels.Struktur))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_ORGANIZATION_STRUCTURE_TYPE_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_STRUCTURE_TYPE_IS_EMPTY, language)
                    });
                }
                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationStructureModels.Code))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "CODE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_STRUCTURE_CODE_IS_EMPTY, language)
                        });
                    }

                    else if (inputModel.organizationStructureList.Count > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "CODE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_STRUCTURE_CODE_ALREADY_EXISTS, language)
                        });
                    }

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationStructureModels.Position_WLKP) && inputModel.organizationStructureModels.Struktur == "Positions")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Position_WLKP",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                        });
                    }
                }
                if (type == GlobalVariable.CONST_EDIT)
                {
                    User_Data USERDATA = new User_Data();
                    APP_CORE.CoreVariable Core = new APP_CORE.CoreVariable();
                    USERDATA = Core.CoreUserLogin();

                    var Data = GeneralCore.OrganizationStructurQuery().Where(p => p.id == inputModel.organizationStructureModels.id).FirstOrDefault();

                    var ExistCode = GeneralCore.OrganizationStructurQuery().Where(p => p.Struktur == inputModel.organizationStructureModels.Struktur &&
                                                                             p.Code == inputModel.organizationStructureModels.Code &&
                                                                             p.Organization_ID == inputModel.organizationStructureModels.Organization_ID).ToList();

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationStructureModels.Code))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "CODE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_STRUCTURE_CODE_IS_EMPTY, language)
                        });
                    }

                    if (Data.Code != inputModel.organizationStructureModels.Code && Data.Struktur == inputModel.organizationStructureModels.Struktur)
                    {
                        if (ExistCode.Count > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "CODE",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_STRUCTURE_CODE_ALREADY_EXISTS, language)
                            });
                        }
                    }

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationStructureModels.Position_WLKP) && inputModel.organizationStructureModels.Struktur == "Positions")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Position_WLKP",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                        });
                    }

                }
                //if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationStructureModels.Description))
                //{
                //    status = false;
                //    errMessage.Add(new Global_Error_Code()
                //    {
                //        Error_Code = GlobalVariable.CONST_ERR_ORGANIZATION_STRUCTURE_DESCRIPTION_IS_EMPTY,
                //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_STRUCTURE_DESCRIPTION_IS_EMPTY, language)
                //    });
                //}

            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region validation Organization Payroll Component
        public static bool ValidationOrganizationPayrollComponent(Global_Organization_Payroll_Component inputModel, string language, string type, User_Data UserData, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            InputValidation tes = new InputValidation();
            List<checkExistingObject_Result> ExistingObject = new List<checkExistingObject_Result>();
            string strParameterObject = "";
            try
            {
                inputModel.organizationPayrollComponentList = db.tbl_Organization_Payroll_Component.Where(s => s.Organization_Payroll_Component_Code == inputModel.organizationPayrollComponent.Organization_Payroll_Component_Code).ToList();
                var modelValidateFormula = db.tbl_Organization_Payroll_Component.Where(s => s.Organization_id == UserData.OrganizationSelected_Id && s.Authorize_Status == 1 && s.Status_Code == 1).ToList();
                if (modelValidateFormula.Count() > 0)
                {
                    if (inputModel.organizationPayrollComponent.Formula != null)
                    {
                        ICollection<string> matches =
                            Regex.Matches(inputModel.organizationPayrollComponent.Formula.Replace(Environment.NewLine, ""), @"\[([^]]*)\]")
                                .Cast<Match>()
                                .Select(x => x.Groups[1].Value)
                                .ToList();
                        ICollection<string> matchesExistFormula =
                                Regex.Matches(inputModel.organizationPayrollComponent.Formula.Replace(Environment.NewLine, ""), @"\@(\w+)\=")
                                    .Cast<Match>()
                                    .Select(x => x.Groups[1].Value)
                                    .ToList();
                        if (matchesExistFormula.Count() > 0)
                        {
                            foreach (var itemExist in matchesExistFormula)
                            {
                                strParameterObject = strParameterObject + "@" + itemExist + "|";
                            }
                        }
                        var SplitTag = inputModel.organizationPayrollComponent.Formula.Split('[');
                        if (SplitTag.Count() > 1)
                        {
                            for (int x = 0; x < SplitTag.Count(); x++)
                            {
                                if (x > 0)
                                {
                                    if (SplitTag[x].Contains(']'))
                                    {
                                    }
                                    else
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.ERR_FORMULA,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FORMULA, language)
                                        });
                                    }
                                }
                            }
                        }
                        var SplitTag2 = inputModel.organizationPayrollComponent.Formula.Split(']');
                        if (SplitTag2.Count() > 1)
                        {
                            for (int x = 0; x < SplitTag2.Count() - 1; x++)
                            {
                                if (SplitTag2[x].Contains('['))
                                {
                                }
                                else
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = GlobalVariable.ERR_FORMULA,
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FORMULA, language)
                                    });
                                }
                            }
                        }

                        int IndexStart = 0;
                        if (inputModel.organizationPayrollComponent.Formula.ToUpper().Contains("[DBO]."))
                        {
                            try
                            {
                                var strFormula = matches.ToList();
                                strParameterObject = strParameterObject.Remove(strParameterObject.Length - 1);
                                ExistingObject = db.checkExistingObject(strFormula[1].ToString(), strParameterObject).ToList();
                                if (ExistingObject.Count() > 0)
                                {
                                    if (ExistingObject.FirstOrDefault().isExists == false)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.ERR_FORMULA,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FORMULA, language)
                                        });
                                    }
                                }
                            }
                            catch
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_FORMULA,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FORMULA, language)
                                });
                            }
                            IndexStart = 2;
                        }
                        int i = 0;
                        foreach (var itemBracket in matches)
                        {
                            if (i >= IndexStart)
                            {
                                var componentCode = itemBracket.ToUpper();
                                componentCode = componentCode.Replace("$", "");
                                if (modelValidateFormula.Any(s => s.Organization_Payroll_Component_Code == componentCode))
                                {
                                }
                                else if (modelValidateFormula.Any(s => s.Component_Group == componentCode))
                                {
                                }
                                else
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = GlobalVariable.ERR_FORMULA,
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FORMULA, language)
                                    });
                                }
                            }
                            i++;
                        }
                    }


                }


                if (type == GlobalVariable.CONST_CREATE)
                {
                    
                    if( inputModel.organizationPayrollComponent.Currency==null)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_CURRENCY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_CURRENCY, language)
                        });
                    }
                    if (inputModel.organizationPayrollComponent.Tax_Deduction == null && inputModel.organizationPayrollComponent.Taxable_Type == "N")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_TAX_DEDUCTION__IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_TAX_DEDUCTION__IS_EMPTY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Component_Group))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_COMPONENT_GROUP_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_COMPONENT_GROUP_IS_EMPTY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Taxable_Type))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_TAXABLE_TYPE_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_TAXABLE_TYPE_IS_EMPTY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Amount_Type))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_AMOUNT_TYPE_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_AMOUNT_TYPE_IS_EMPTY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.SPT_Code))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "SPT_CODE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_SPT_CODE_IS_EMPTY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Calculation_Basic))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_CALCULATION_BASIC_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_CALCULATION_BASIC_IS_EMPTY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Formula))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_VALUE_FORMULA_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_VALUE_FORMULA_IS_EMPTY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Sign))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_SIGN_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_SIGN_IS_EMPTY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Tax_Policy) && inputModel.organizationPayrollComponent.Taxable_Type == "T")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_TAX_POLICY_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_TAX_POLICY_IS_EMPTY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Frequency) && inputModel.organizationPayrollComponent.Taxable_Type == "T")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_FREQUENCY_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FREQUENCY_IS_EMPTY, language)
                        });
                    }
                    if (inputModel.organizationPayrollComponent.Amount_Type == "FX" && inputModel.organizationPayrollComponent.Calculation_Basic != "FL")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_AMOUNT_TYPE_CALCULATION,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_AMOUNT_TYPE_CALCULATION, language)
                        });
                    }

                    //progress
                    //else if (inputModel.organizationPayrollComponent.Component_Group == "LN"&&inputModel.organizationPayrollComponent.Status_Code==1)
                    //{
                    //    status = false;
                    //    errMessage.Add(new Global_Error_Code()
                    //    {
                    //        Error_Code = "CODE",
                    //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ACTIVE_STATUS_COMPONENT_GROUP, language)
                    //    });
                    //}
                    if (inputModel.organizationPayrollComponent.Component_Group == "CB" && inputModel.organizationPayrollComponent.Amount_Type != "FX" || inputModel.organizationPayrollComponent.Component_Group == "CB" && inputModel.organizationPayrollComponent.Calculation_Basic != "FL")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_CALCULATION_BASIC_COMPENSATION,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_CALCULATION_BASIC_COMPENSATION, language)
                        });
                    }
                    if (inputModel.organizationPayrollComponent.Component_Group == "LN" && inputModel.organizationPayrollComponent.Amount_Type != "FX" && inputModel.organizationPayrollComponent.Calculation_Basic != "FL")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_CALCULATION_BASIC_LOAN,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_CALCULATION_BASIC_LOAN, language)
                        });
                    }
                    if (inputModel.organizationPayrollComponent.Organization_Payroll_Component_Code != null)
                    {
                        var CheckCode = db.tbl_Organization_Payroll_Component.Where(s => s.Organization_Payroll_Component_Code == inputModel.organizationPayrollComponent.Organization_Payroll_Component_Code).Select(s => s.Organization_Payroll_Component_Code).FirstOrDefault();
                        if (CheckCode != null)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.ERR_DUPLICATE_GENERATE_CODE,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_DUPLICATE_GENERATE_CODE, language)
                            });
                        }
                    }
                    if (inputModel.organizationPayrollComponent.Is_Prorate == true && UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Prorate_Base) || inputModel.organizationPayrollComponent.Is_New_Join == true && UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Prorate_Base))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_ORGANIZATION_COMPONENT_PRORATE_BASE,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ORGANIZATION_COMPONENT_PRORATE_BASE, GlobalVariable.CONST_LANG_EN)
                        });
                    }
                    //if (inputModel.organizationPayrollComponent.Formula != null)
                    //{

                    //    if (inputModel.organizationPayrollComponent.Amount_Type == "FR" && UICommonFunction.IsNumeric(inputModel.organizationPayrollComponent.Formula))
                    //    {
                    //        status = false;
                    //        errMessage.Add(new Global_Error_Code()
                    //        {
                    //            Error_Code = GlobalVariable.ERR_FORMULA,
                    //            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FORMULA, language)
                    //        });
                    //    }
                    //    if (inputModel.organizationPayrollComponent.Amount_Type == "FX" && !UICommonFunction.IsNumeric(inputModel.organizationPayrollComponent.Formula))
                    //    {
                    //        status = false;
                    //        errMessage.Add(new Global_Error_Code()
                    //        {
                    //            Error_Code = GlobalVariable.ERR_FORMULA,
                    //            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FORMULA, language)
                    //        });
                    //    }

                    //}

                }
                if (type == GlobalVariable.CONST_EDIT)
                {
                    if (inputModel.organizationPayrollComponent.Currency == null)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_CURRENCY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_CURRENCY, language)
                        });
                    }
                    if (inputModel.organizationPayrollComponent.Tax_Deduction == null && inputModel.organizationPayrollComponent.Taxable_Type == "N")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_TAX_DEDUCTION__IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_TAX_DEDUCTION__IS_EMPTY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Component_Group))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_COMPONENT_GROUP_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_COMPONENT_GROUP_IS_EMPTY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Taxable_Type))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_TAXABLE_TYPE_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_TAXABLE_TYPE_IS_EMPTY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Amount_Type))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_AMOUNT_TYPE_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_AMOUNT_TYPE_IS_EMPTY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Calculation_Basic))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_CALCULATION_BASIC_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_CALCULATION_BASIC_IS_EMPTY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Formula))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_VALUE_FORMULA_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_VALUE_FORMULA_IS_EMPTY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Sign))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_SIGN_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_SIGN_IS_EMPTY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Tax_Policy) && inputModel.organizationPayrollComponent.Taxable_Type == "T")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_TAX_POLICY_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_TAX_POLICY_IS_EMPTY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Frequency) && inputModel.organizationPayrollComponent.Taxable_Type == "T")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_FREQUENCY_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FREQUENCY_IS_EMPTY, language)
                        });
                    }
                    if (inputModel.organizationPayrollComponent.Amount_Type == "FX" && inputModel.organizationPayrollComponent.Calculation_Basic != "FL")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_AMOUNT_TYPE_CALCULATION,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_AMOUNT_TYPE_CALCULATION, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Component_Group))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_COMPONENT_GROUP_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_COMPONENT_GROUP_IS_EMPTY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Taxable_Type))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_TAXABLE_TYPE_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_TAXABLE_TYPE_IS_EMPTY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Amount_Type))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_AMOUNT_TYPE_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_AMOUNT_TYPE_IS_EMPTY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.SPT_Code))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "SPT_CODE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_SPT_CODE_IS_EMPTY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Calculation_Basic))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_CALCULATION_BASIC_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_CALCULATION_BASIC_IS_EMPTY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Formula))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_VALUE_FORMULA_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_VALUE_FORMULA_IS_EMPTY, language)
                        });
                    }
                    if (inputModel.organizationPayrollComponent.Is_Prorate == true &&  UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Prorate_Base) || inputModel.organizationPayrollComponent.Is_New_Join == true  &&  UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Prorate_Base))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_ORGANIZATION_COMPONENT_PRORATE_BASE,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ORGANIZATION_COMPONENT_PRORATE_BASE, GlobalVariable.CONST_LANG_EN)
                        });
                    }
                    
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Sign))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_SIGN_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_SIGN_IS_EMPTY, language)
                        });
                    }
                    //progress
                    //else if (inputModel.organizationPayrollComponent.Component_Group == "LN"&&inputModel.organizationPayrollComponent.Status_Code==1)
                    //{
                    //    status = false;
                    //    errMessage.Add(new Global_Error_Code()
                    //    {
                    //        Error_Code = "CODE",
                    //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ACTIVE_STATUS_COMPONENT_GROUP, language)
                    //    });
                    //}
                    if (inputModel.organizationPayrollComponent.Component_Group == "CB" && inputModel.organizationPayrollComponent.Amount_Type != "FX" || inputModel.organizationPayrollComponent.Component_Group == "CB" && inputModel.organizationPayrollComponent.Calculation_Basic != "FL")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_CALCULATION_BASIC_COMPENSATION,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_CALCULATION_BASIC_COMPENSATION, language)
                        });
                    }
                    if (inputModel.organizationPayrollComponent.Component_Group == "LN" && inputModel.organizationPayrollComponent.Amount_Type != "FX" && inputModel.organizationPayrollComponent.Calculation_Basic != "FL")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_CALCULATION_BASIC_LOAN,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_CALCULATION_BASIC_LOAN, language)
                        });
                    }
                    //else if (inputModel.organizationPayrollComponent.Formula != null)
                    //{

                    //    if (inputModel.organizationPayrollComponent.Amount_Type == "FR" && UICommonFunction.IsNumeric(inputModel.organizationPayrollComponent.Formula))
                    //    {
                    //        status = false;
                    //        errMessage.Add(new Global_Error_Code()
                    //        {
                    //            Error_Code = GlobalVariable.ERR_FORMULA,
                    //            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FORMULA, language)
                    //        });
                    //    }
                    //    else if (inputModel.organizationPayrollComponent.Amount_Type == "FX" && !UICommonFunction.IsNumeric(inputModel.organizationPayrollComponent.Formula))
                    //    {
                    //        status = false;
                    //        errMessage.Add(new Global_Error_Code()
                    //        {
                    //            Error_Code = GlobalVariable.ERR_FORMULA,
                    //            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FORMULA, language)
                    //        });
                    //    }

                    //}

                    var CheckUsedemployee = db.tbl_Employee_Payroll_Component.Where(p => p.Organization_Payroll_Component_Code == inputModel.organizationPayrollComponent.Organization_Payroll_Component_Code && p.Status_Code == CoreVariable.CONST_STATUS_ACTIVE).FirstOrDefault();
                    var CheckUsed = db.tbl_Payroll_Transaction.Where(p => p.Payroll_Component_Code == inputModel.organizationPayrollComponent.Organization_Payroll_Component_Code).ToList();
                    if (CheckUsed.Count() > 0 && inputModel.organizationPayrollComponent.Status_Code==0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_ORGANIZATION_ALREADY_USED,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_COMPONENT_CODE_ALREADY_CALCULATED, GlobalVariable.CONST_LANG_EN)
                        });
                    }
                    var tempIsAmount = false;
                    if(CheckUsedemployee!=null)
                    {
                        foreach (var item in CheckUsedemployee.tbl_Employee_Payroll_Component_Effective)
                        {
                            var strFormula = UICommonFunction.IsDecimal(item.Formula);
                            if (strFormula)
                            {
                                tempIsAmount = true;
                            }
                            else
                            {
                                tempIsAmount = false;
                                break;
                            }

                        }
                    }
                    
                    if (inputModel.organizationPayrollComponentList.Count() > 0)
                    {
                        var AmtValueDb = inputModel.organizationPayrollComponentList.FirstOrDefault().Amount_Type;
                        
                        if (AmtValueDb == "FR" && inputModel.organizationPayrollComponent.Amount_Type == "FX" && CheckUsedemployee !=null && tempIsAmount==false)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.ERR_ORGANIZATION_ALREADY_USED,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ORGANIZATION_UPDATE_EMPLOYEE_COMPONENT, GlobalVariable.CONST_LANG_EN)
                            });
                        }
                    }
                    //var CheckUsedbyReportGeneration = (from s in db.tbl_Report_Setting
                    //                                   join q in db.tbl_Report_Setting_Detail
                    //                                   on s.Id equals q.Payroll_Report_Id
                    //                                   select new { ID = q.Id, Organization_Id = s.Organization_id, Status_Code = s.Status_Code, Authorize_Status = s.Authorize_Status, Col_Name = q.Col_Name, Col_Description = q.Col_Description })
                    //                          .Where(t => t.Organization_Id == inputModel.organizationPayrollComponent.Organization_id && (t.Col_Name == "Calculated_" + inputModel.organizationPayrollComponent.Organization_Payroll_Component_Code || t.Col_Name == "Original_" + inputModel.organizationPayrollComponent.Organization_Payroll_Component_Code) &&t.Status_Code == CoreVariable.CONST_STATUS_ACTIVE).ToList();
                    //if (CheckUsedbyReportGeneration.Count() > 0)
                    //{
                    //    status = false;
                    //    errMessage.Add(new Global_Error_Code()
                    //    {
                    //        Error_Code = GlobalVariable.ERR_ORGANIZATION_ALREADY_USED,
                    //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ORGANIZATION_ALREADY_USED_REPORT, GlobalVariable.CONST_LANG_EN)
                    //    });
                    //}

                }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region PayrollVariable
        public static bool ValidationPayrollVariable(Global_Payroll_Variable inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            InputValidation tes = new InputValidation();

            try
            {
                //var modelVariable = db.tbl_Payroll_Variable.Where(s => s.Employee_ID == inputModel.payrollVariable.Employee_ID && s.Payroll_Period_ID == inputModel.payrollPeriod.id && !(s.Authorize_Status == CoreVariable.CONST_AUTHORIZED && s.Status_Code == CoreVariable.CONST_STATUS_DELETED)).ToList();
                
                var modelVariable =GeneralCore.PayrollVariableTblQuery().Where(s => s.Employee_ID == inputModel.payrollVariable.Employee_ID&&s.Payroll_Period_ID==null).ToList();

                List<decimal?> listCount = new List<decimal?>();
                listCount.Add(inputModel.payrollVariable.Weekday_Tier1);
                listCount.Add(inputModel.payrollVariable.Weekday_Tier2);
                listCount.Add(inputModel.payrollVariable.Weekday_Tier3);
                listCount.Add(inputModel.payrollVariable.Holiday_Tier1);
                listCount.Add(inputModel.payrollVariable.Holiday_Tier2);
                listCount.Add(inputModel.payrollVariable.Holiday_Tier3);
                listCount.Add(inputModel.payrollVariable.HIS_Tier1);
                listCount.Add(inputModel.payrollVariable.HIS_Tier2);
                listCount.Add(inputModel.payrollVariable.HIS_Tier3);
                if (inputModel.payrollVariable.Total_Overtime != null)
                {
                    if (inputModel.payrollVariable.Weekday_Tier3 != null || inputModel.payrollVariable.Weekday_Tier2 != null || inputModel.payrollVariable.Weekday_Tier1 != null)
                    {
                        if (inputModel.CalculationTotal != inputModel.payrollVariable.Total_Overtime)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_INVALID,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_INVALID, language)
                            });
                        }
                    }
                }
                if (inputModel.payrollVariable.Payroll_Period_ID!=null)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_INVALID,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CALCULATION_SETUP_PAYROLL_PERIOD_CLOSED, language)
                    });
                }
                if (inputModel.CalculationErr)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_INVALID,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_INVALID, language)
                    });
                }

                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (inputModel.payrollVariable.Total_Overtime == null && inputModel.payrollVariable.Working_Day == null && inputModel.payrollVariable.Overtime_Day == null && inputModel.payrollVariable.Absent_Day == null)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_MUST_BE_FILL,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_MUST_BE_FILL, language)
                        });
                    }
                    if (inputModel.Working_Days_Count == 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_CREATE_WORKING_TIME,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_CREATE_WORKING_TIME, language)
                        });
                    }
                    if (modelVariable.Count() > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_EXIST,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_EXIST, language)
                        });
                    }
                    HashSet<string> Hs = new HashSet<string>();
                    foreach (var item in inputModel.Variable)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            if (!Hs.Add(item))
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_DUPLICATE,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_DUPLICATE, language)
                                });
                            }
                        }
                    }

                    for (int i = 0; i < inputModel.Variable.Count(); i++)
                    {
                        if (inputModel.Variable[i] != string.Empty && inputModel.Variable_Value[i] == null)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_VALUE_MUST_BE_FILL,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_VALUE_MUST_BE_FILL, language)
                            });
                        }
                        if(inputModel.Variable[i]==string.Empty&& inputModel.Variable_Value[i] != null)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_VALUE_MUST_BE_FILL,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_SELECTED_EMPTY, language)
                            });
                        }
                    }

                    if (inputModel.payrollVariable.Employee_ID == null)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_EMPLOYEE_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_EMPLOYEE_EMPTY, language)
                        });
                    }
                    //                    if(listCount.Sum()!=0 )
                    //                    {
                    //if (listCount.Sum() != inputModel.payrollVariable.Total_Overtime)
                    //                    {
                    //                        status = false;
                    //                        errMessage.Add(new Global_Error_Code()
                    //                        {
                    //                            Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_INVALID,
                    //                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_INVALID, language)
                    //                        });
                    //                    }
                    //                    }

                    if ((inputModel.payrollVariable.Weekday_Tier3 != null && inputModel.payrollVariable.Weekday_Tier2 == null) || (inputModel.payrollVariable.Weekday_Tier2 != null && inputModel.payrollVariable.Weekday_Tier1 == null))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_INVALID,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_INVALID, language)
                        });
                    }
                    else if ((inputModel.payrollVariable.Holiday_Tier3 != null && inputModel.payrollVariable.Holiday_Tier2 == null) || (inputModel.payrollVariable.Holiday_Tier2 != null && inputModel.payrollVariable.Holiday_Tier1 == null))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_INVALID,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_INVALID, language)
                        });
                    }
                    else if ((inputModel.payrollVariable.HIS_Tier3 != null && inputModel.payrollVariable.HIS_Tier2 == null) || (inputModel.payrollVariable.HIS_Tier2 != null && inputModel.payrollVariable.HIS_Tier1 == null))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_INVALID,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_INVALID, language)
                        });
                    }

                }
                if (type == GlobalVariable.CONST_EDIT)
                {
                    HashSet<string> Hs = new HashSet<string>();
                    foreach (var item in inputModel.Variable)
                    {
                        if (!Hs.Add(item))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_DUPLICATE,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_DUPLICATE, language)
                            });
                        }
                    }
                    for (int i = 0; i < inputModel.Variable.Count(); i++)
                    {
                        if (inputModel.Variable[i] != string.Empty && inputModel.Variable_Value[i] == null)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_VALUE_MUST_BE_FILL,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_VALUE_MUST_BE_FILL, language)
                            });
                        }
                    }
                    if (inputModel.payrollVariable.Employee_ID == null)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_EMPLOYEE_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_EMPLOYEE_EMPTY, language)
                        });
                    }
                    if (inputModel.payrollVariable.Total_Overtime == null && inputModel.payrollVariable.Working_Day == null && inputModel.payrollVariable.Overtime_Day == null && inputModel.payrollVariable.Absent_Day == null)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_MUST_BE_FILL,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_MUST_BE_FILL, language)
                        });
                    }

                    //else if (listCount.Sum() != inputModel.payrollVariable.Total_Overtime)
                    //{
                    //    status = false;
                    //    errMessage.Add(new Global_Error_Code()
                    //    {
                    //        Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_INVALID,
                    //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_INVALID, language)
                    //    });
                    //}
                    else if ((inputModel.payrollVariable.Weekday_Tier3 != null && inputModel.payrollVariable.Weekday_Tier2 == null) || (inputModel.payrollVariable.Weekday_Tier2 != null && inputModel.payrollVariable.Weekday_Tier1 == null))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_INVALID,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_INVALID, language)
                        });
                    }
                    else if ((inputModel.payrollVariable.Holiday_Tier3 != null && inputModel.payrollVariable.Holiday_Tier2 == null) || (inputModel.payrollVariable.Holiday_Tier2 != null && inputModel.payrollVariable.Holiday_Tier1 == null))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_INVALID,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_INVALID, language)
                        });
                    }
                    else if ((inputModel.payrollVariable.HIS_Tier3 != null && inputModel.payrollVariable.HIS_Tier2 == null) || (inputModel.payrollVariable.HIS_Tier2 != null && inputModel.payrollVariable.HIS_Tier1 == null))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_INVALID,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_DATA_INVALID, language)
                        });
                    }
                }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region AdditionalPayroll
        public static bool ValidationAdditionalPayroll(Global_Additional_Payroll inputModel, string language, string type, User_Data inputData, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            InputValidation tes = new InputValidation();
            var strStatus = "";
            var strModel = new tbl_Appointment_Status_Information();
            try
            {
               
                if (inputModel.AdditionalPayroll.Employee_ID != null)
                {
                    if (inputData.Payroll_Period_Id == null)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_PERIOD_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_PERIOD_EMPTY, GlobalVariable.CONST_LANG_EN)
                        });
                        return status;
                    }
                    else
                    {
                        inputModel.vwadditionalPayrollList = db.vw_Additional_Payroll.Where(s => s.Employee_ID == inputModel.AdditionalPayroll.Employee_ID && s.Payroll_Period_ID == null && s.Category == "Permanent" && !(s.Record_Status == CoreVariable.CONST_STR_STATUS_DELETED && s.Authorize_Status == CoreVariable.CONST_STR_AUTHORIZED)).ToList();
                    }

                    var tempVwEmployeeApointmentStatus = db.tbl_Appointment_Status_Information.Where(s => s.tbl_Employee_Appointment.Employee_ID == inputModel.AdditionalPayroll.Employee_ID && !(s.tbl_Employee_Appointment.Status_Code == CoreVariable.CONST_STATUS_DELETED && s.tbl_Employee_Appointment.Authorize_Status == CoreVariable.CONST_AUTHORIZED)).ToList();
                    if (inputModel.Category == CoreVariable.CONST_EMPLOYEMENT_STATUS_NONPERMANENT)
                    {
                        List<string> ListTempStatus = new List<string>();
                        foreach (var item in inputModel.TransDate)
                        {
                            if (string.IsNullOrEmpty(item))
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_TRANS_DATE, language)
                                });
                            }
                            else
                            {
                                DateTime dateTime = DateTime.ParseExact(item, "dd/MM/yyyy",
                                  CultureInfo.InvariantCulture);
                                var tempstrModel = tempVwEmployeeApointmentStatus.Where(s => s.Effective_Date <= dateTime).OrderByDescending(s => s.Effective_Date).FirstOrDefault();
                                if (tempstrModel != null)
                                {
                                    var splitEmployeeStatus = tempstrModel.Employment_Status.Split('-');
                                    ListTempStatus.Add(splitEmployeeStatus[0].Trim());
                                }
                                strModel = tempstrModel;
                            }
                        }
                        if (ListTempStatus.Count() > 0)
                        {
                            foreach (var item in ListTempStatus)
                            {
                                if (item != inputModel.Category)
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL_HEADER,
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_PAYROLL_HEADER, language)
                                    });
                                }
                            }
                            if (status)
                            {
                                strStatus = ListTempStatus.FirstOrDefault();

                            }
                        }
                    }
                    else
                    {
                        strModel = tempVwEmployeeApointmentStatus.Where(s => s.Effective_Date <= inputData.PayrollEndDate).OrderByDescending(s => s.Effective_Date).FirstOrDefault();
                        if (strModel != null)
                        {
                            var splitEmployeeStatus = strModel.Employment_Status.Split('-');
                            strStatus = splitEmployeeStatus[0].Trim();
                            if (strStatus != inputModel.Category)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL_HEADER,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_PAYROLL_HEADER, language)
                                });
                            }
                        }
                        else
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_EFFECTIVE_APPOINTMENT, language)
                            });
                        }
                    }
                }
                else
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL_EMPLOYEE,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_PAYROLL_EMPLOYEE, language)
                    });
                }
                
                if (type == GlobalVariable.CONST_CREATE)
                {
                   if (status)
                    {
                        if (strModel != null)
                        {
                            var splitEmployeeStatus = strModel.Employment_Status.Split('-');
                            strStatus = splitEmployeeStatus[0].Trim();
                            if (strModel.Employment_Status == null)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_ADDITIONAL_EMPTY_APPOINTMENT,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_EMPTY_APPOINTMENT, language)
                                });
                            }
                        }
                        if (strStatus.Contains(GlobalVariable.CONST_EMPLOYEMENT_STATUS_NONPERMANENT))
                        {
                            #region nonpermanent
                            foreach (var item in inputModel.Amount)
                            {
                                if (item == null)
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL,
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_NON_PERMANENT_AMOUNT, language)
                                    });
                                }

                            }

                            HashSet<string> HSTransDate = new HashSet<string>();
                            var newArray = inputModel.TransDate;
                            var tempdetailvw = (from s in db.tbl_Additional_Payroll
                                                join q in db.tbl_Additional_Payroll_Detail
                                                on s.id equals q.Additional_Payroll_ID
                                                where !(s.Status_Code == CoreVariable.CONST_STATUS_DELETED && s.Authorize_Status == CoreVariable.CONST_AUTHORIZED)
                                                select new { Organization_ID = s.Organization_ID, Employee_ID = s.Employee_ID, Payroll_Period_ID = s.Payroll_Period_ID, Additional_Transaction_Date = q.Additional_Transaction_Date }).Where(s => s.Organization_ID == inputData.OrganizationSelected_Id ).ToList();
                            var FirstPeriod = db.vw_Payroll_Period.Where(s => s.Organization_ID == inputData.OrganizationSelected_Id).OrderBy(s => s.Tax_Period).Take(1).Select(s => s.Tax_Period).FirstOrDefault();
                            foreach (var item in inputModel.TransDate)
                            {
                                if (item.Trim() == "")
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL,
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_TRANS_DATE, language)
                                    });
                                }
                                else
                                {
                                    DateTime dateTime = DateTime.ParseExact(item, "dd/MM/yyyy",
                                     CultureInfo.InvariantCulture);
                                   var detailvw=( from s in tempdetailvw  select new {  Employee_ID = s.Employee_ID, Payroll_Period_ID = s.Payroll_Period_ID, Additional_Transaction_Date = s.Additional_Transaction_Date }).Where(e => e.Employee_ID == inputModel.AdditionalPayroll.Employee_ID && e.Payroll_Period_ID == null && e.Additional_Transaction_Date.Value.Month == dateTime.Month && e.Additional_Transaction_Date.Value.Year == dateTime.Year).ToList();
                                    if (detailvw.Count() > 0)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_PAYROLL_EXIST_GO_EDIT, language)
                                        });
                                    }
                                    if (HSTransDate.Count() != 0)
                                    {
                                        if (!HSTransDate.Contains(dateTime.Month.ToString()))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_PAYROLL_MONTH_DIFF, language)
                                            });
                                        }
                                    }
                                    HSTransDate.Add(dateTime.Month.ToString());
                                   
                                    if (dateTime.Month > inputData.PayrollEndDate_NP.Value.Month && dateTime.Year >= inputData.PayrollEndDate_NP.Value.Year)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_FUTURE_DATE, GlobalVariable.CONST_LANG_EN)
                                        });
                                    }
                                    if (FirstPeriod != null)
                                    {
                                        if (dateTime < FirstPeriod)
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_PERIOD_EMPTY,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_PERIOD_EMPTY, GlobalVariable.CONST_LANG_EN)
                                            });
                                            return status;
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            HashSet<string> Hs = new HashSet<string>();
                            if (inputModel.ComponentDetail != null)
                            {
                                foreach (var item in inputModel.ComponentDetail)
                                {
                                    if (!Hs.Add(item))
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_PAYROLL_DUPLICATE, language)
                                        });
                                    }
                                }
                            }
                            else
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_EMPTY_COMPONENT, language)
                                });
                            }
                            if (inputModel.vwadditionalPayrollList.Count() > 0)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_PAYROLL_EXIST, language)
                                });
                            }
                        }
                    }
                }
                if (type == GlobalVariable.CONST_EDIT)
                {
                    if(inputModel.Is_Calculation!=null)
                    {
                        if (inputModel.Is_Calculation==true)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_USED_CALCULATE, language)
                            });
                        }
                       
                    }
                    if (status)
                    {
                        if (strModel != null)
                        {
                            var splitEmployeeStatus = strModel.Employment_Status.Split('-');
                            strStatus = splitEmployeeStatus[0].Trim();
                            if (strStatus != inputModel.Category)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL_HEADER,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_PAYROLL_HEADER, language)
                                });
                            }
                            if (strModel.Employment_Status == null)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_ADDITIONAL_EMPTY_APPOINTMENT,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_EMPTY_APPOINTMENT, language)
                                });
                            }
                        }
                        if (strStatus.Contains(GlobalVariable.CONST_EMPLOYEMENT_STATUS_NONPERMANENT))
                        {
                            #region nonpermanent
                            if (inputModel.Amount != null)
                            {
                                foreach (var item in inputModel.Amount)
                                {
                                    if (item == null)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_NON_PERMANENT_AMOUNT, language)
                                        });
                                    }
                                }
                            }
                            HashSet<string> HSTransDate = new HashSet<string>();
                            if (inputModel.TransDate != null)
                            {
                                var newArray = inputModel.TransDate;
                                var tempData = db.tbl_Payroll_Period_Detail.Where(s => s.tbl_Payroll_Period.Organization_ID == inputData.OrganizationSelected_Id).ToList();
                                var FirstPeriod = db.vw_Payroll_Period.Where(s => s.Organization_ID == inputData.OrganizationSelected_Id).OrderBy(s => s.Tax_Period).Take(1).Select(s => s.Tax_Period).FirstOrDefault();
                                foreach (var item in inputModel.TransDate)
                                {
                                    if (item.Trim() == "")
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_TRANS_DATE, language)
                                        });
                                    }
                                    else
                                    {
                                        DateTime dateTime = DateTime.ParseExact(item, "dd/MM/yyyy",
                                         CultureInfo.InvariantCulture);
                                        if (HSTransDate.Count() != 0)
                                        {
                                            if (!HSTransDate.Contains(dateTime.Month.ToString()))
                                            {
                                                status = false;
                                                errMessage.Add(new Global_Error_Code()
                                                {
                                                    Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL,
                                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_PAYROLL_MONTH_DIFF, language)
                                                });
                                            }
                                        }
                                        HSTransDate.Add(dateTime.Month.ToString());
                                        var Data = tempData.Where(s=>s.Tax_Period.Month == dateTime.Month).FirstOrDefault();
                                        if (Data == null)
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_PERIOD_EMPTY,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_PERIOD_EMPTY, GlobalVariable.CONST_LANG_EN)
                                            });
                                        }
                                        if (dateTime.Month > inputData.Tax_Period_Date_NP.Value.Month && dateTime.Year >= inputData.PayrollEndDate_NP.Value.Year)
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_FUTURE_DATE, GlobalVariable.CONST_LANG_EN)
                                            });
                                        }
                                        if (FirstPeriod != null)
                                        {
                                            if (dateTime < FirstPeriod)
                                            {
                                                status = false;
                                                errMessage.Add(new Global_Error_Code()
                                                {
                                                    Error_Code = GlobalVariable.ERR_PAYROLL_VARIABLE_PERIOD_EMPTY,
                                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_VARIABLE_PERIOD_EMPTY, GlobalVariable.CONST_LANG_EN)
                                                });
                                                return status;
                                            }
                                        }
                                        //if (dateTime.Month != inputData.Tax_Period_Date.Value.Month)
                                        //{
                                        //    status = false;
                                        //    errMessage.Add(new Global_Error_Code()
                                        //    {
                                        //        Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL_MONTH_DIFF,
                                        //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_PAYROLL_MONTH_DIFF, language)
                                        //    });
                                        //}
                                        //if(dateTime.Month>inputData.Tax_Period_Date.Value.Month)
                                        //{
                                        //     status = false;
                                        //    errMessage.Add(new Global_Error_Code()
                                        //    {
                                        //        Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL_MONTH_DIFF,
                                        //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_PAYROLL_MONTH_DIFF, language)
                                        //    });
                                        //}


                                        //var strsplit = item.Split('/');
                                        //if(HSTransDate.Count()==0)
                                        //{
                                        //    HSTransDate.Add(strsplit[1]);
                                        //}
                                        //else
                                        //{
                                        //    if (!HSTransDate.Contains(strsplit[1]))
                                        //    {
                                        //        status = false;
                                        //        errMessage.Add(new Global_Error_Code()
                                        //        {
                                        //            Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL_MONTH_DIFF,
                                        //            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_PAYROLL_MONTH_DIFF, language)
                                        //        });
                                        //    }
                                        //}
                                    }
                                }
                            }
                            
                            #endregion
                        }
                        else
                        {
                            HashSet<string> Hs = new HashSet<string>();
                            if (inputModel.ComponentDetail != null)
                            {
                                foreach (var item in inputModel.ComponentDetail)
                                {
                                    if (!Hs.Add(item))
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_PAYROLL_DUPLICATE, language)
                                        });
                                    }
                                }
                            }
                            else
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_ADDITIONAL_PAYROLL,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ADDITIONAL_EMPTY_COMPONENT, language)
                                });
                            }
                        }
                    }
                }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region ValidationEmployeePayrollComponent
        public static bool ValidationEmployeePayrollComponent(Global_Employee_Payroll_Component inputModel, string language, string type, User_Data UserData, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            InputValidation tes = new InputValidation();
            List<checkExistingObject_Result> ExistingObject = new List<checkExistingObject_Result>();
           
            try
            {
                if (inputModel.EmployeePayrollComponent != null)
                    inputModel.EmployeePayrollComponentList = db.tbl_Employee_Payroll_Component.Where(s => s.Organization_Payroll_Component_Code == inputModel.organizationPayrollComponent.Organization_Payroll_Component_Code).ToList();
                var modelValidateFormula = db.tbl_Organization_Payroll_Component.Where(s => s.Organization_id == UserData.OrganizationSelected_Id && s.Authorize_Status == 1 && s.Status_Code == 1).ToList();
                if (modelValidateFormula.Count() > 0)
                {
                    foreach (var itemValue in inputModel.Remark)
                    {
                        string strParameterObject = "";
                        ICollection<string> matches =
                            Regex.Matches(itemValue.Replace(Environment.NewLine, ""), @"\[([^]]*)\]")
                                .Cast<Match>()
                                .Select(x => x.Groups[1].Value)
                                .ToList();
                        ICollection<string> matchesExistFormula =
                           Regex.Matches(itemValue.Replace(Environment.NewLine, ""), @"\@(\w+)\=")
                               .Cast<Match>()
                               .Select(x => x.Groups[1].Value)
                               .ToList();
                        if (matchesExistFormula.Count() > 0)
                        {
                            foreach (var itemExist in matchesExistFormula)
                            {
                                strParameterObject = strParameterObject + "@" + itemExist + "|";
                            }
                        }
                        var SplitTag = itemValue.Split('[');
                        if (SplitTag.Count() > 1)
                        {
                            for (int x = 0; x < SplitTag.Count(); x++)
                            {
                                if (x > 0)
                                {
                                    if (SplitTag[x].Contains(']'))
                                    {
                                    }
                                    else
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.ERR_FORMULA,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FORMULA, language)
                                        });
                                    }
                                }
                            }
                        }
                        var SplitTag2 = itemValue.Split(']');
                        if (SplitTag2.Count() > 1)
                        {
                            for (int x = 0; x < SplitTag2.Count() - 1; x++)
                            {
                                if (SplitTag2[x].Contains('['))
                                {
                                }
                                else
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = GlobalVariable.ERR_FORMULA,
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FORMULA, language)
                                    });
                                }
                            }
                        }
                        int IndexStart = 0;
                        if (itemValue.ToUpper().Contains("[DBO]."))
                        {
                            try
                            {
                                var strFormula = matches.ToList();
                                strParameterObject = strParameterObject.Remove(strParameterObject.Length - 1);
                                ExistingObject = db.checkExistingObject(strFormula[1].ToString(), strParameterObject).ToList();
                                if (ExistingObject.Count() > 0)
                                {
                                    if (ExistingObject.FirstOrDefault().isExists == false)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.ERR_FORMULA,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FORMULA, language)
                                        });
                                    }
                                }
                            }
                            catch
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_FORMULA,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FORMULA, language)
                                });
                            }
                            IndexStart = 2;
                        }
                        int i = 0;
                        foreach (var itemBracket in matches)
                        {
                            if (i >= IndexStart)
                            {
                                var componentCode = itemBracket.ToUpper();
                                componentCode = componentCode.Replace("$", "");
                                if (modelValidateFormula.Any(s => s.Organization_Payroll_Component_Code == componentCode))
                                {
                                }
                                else if (modelValidateFormula.Any(s => s.Component_Group == componentCode))
                                {
                                }
                                else
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = GlobalVariable.ERR_FORMULA,
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FORMULA, language)
                                    });
                                }
                            }
                            i++;
                        }
                    }
                }

                var modelvalidateFormulaEmployee = db.tbl_Employee_Payroll_Component.Where(s => s.Employee_id == UserData.EmployeeSelected.id && s.Authorize_Status == 1 && s.Status_Code == 1).ToList();
                if(modelvalidateFormulaEmployee.Count()>0)
                {
                    if (inputModel.organizationPayrollComponent.Amount_Type == "FR")
                    {
                        foreach (var itemValue in inputModel.Remark)
                        {

                            ICollection<string> matches =
                                Regex.Matches(itemValue.Replace(Environment.NewLine, ""), @"\[([^]]*)\]")
                                    .Cast<Match>()
                                    .Select(x => x.Groups[1].Value)
                                    .ToList();
                           
                            int IndexStart = 0;
                            int i = 0;
                            foreach (var itemBracket in matches)
                            {
                                if (i >= IndexStart)
                                {
                                    if (itemBracket.Contains("OPCC"))
                                    {
                                        var componentCode = itemBracket.ToUpper();
                                        componentCode = componentCode.Replace("$", "");
                                        if (modelvalidateFormulaEmployee.Any(s => s.Organization_Payroll_Component_Code == componentCode))
                                        {
                                        }
                                        //else if (modelvalidateFormulaEmployee.Any(s => s.Component_Group == componentCode))
                                        //{
                                        //}
                                        else
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.ERR_FORMULA,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FORMULA, language)
                                            });
                                        }
                                    }
                                }
                                i++;
                            }
                        }
                    }
                }
                List<DateTime> hsStartDate = new List<DateTime>();
                foreach (var item in inputModel.StartDate)
                {
                    if (string.IsNullOrEmpty(item))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_EMPLOYEE_COMPONENT_START_DATE,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMPLOYEE_COMPONENT_START_DATE, GlobalVariable.CONST_LANG_EN)
                        });
                        return status;
                    }
                    else
                    {
                        DateTime dateTimeStart = DateTime.ParseExact(item.Trim(), "dd/MM/yyyy",
                                                    CultureInfo.InvariantCulture);
                        hsStartDate.Add(dateTimeStart);
                    }
                }
                List<DateTime> hsEndDate = new List<DateTime>();
                foreach (var item in inputModel.EndDate)
                {
                    DateTime dateTimeEnd = DateTime.ParseExact(item.Trim(), "dd/MM/yyyy",
                           CultureInfo.InvariantCulture);
                    hsEndDate.Add(dateTimeEnd);
                }

                DateTime dtEndDate = new DateTime();
                DateTime dtStartDate = new DateTime();
                for (int i = 0; i < inputModel.EndDate.Count(); i++)
                {
                    if (!string.IsNullOrEmpty(inputModel.StartDate[i]))
                    {
                        DateTime dateTime = DateTime.ParseExact(inputModel.StartDate[i].Trim(), "dd/MM/yyyy",
                               CultureInfo.InvariantCulture);

                        if (dateTime < inputModel.CurrentPeriodStartDate)
                        {
                            if (type == GlobalVariable.CONST_CREATE)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.ERR_EMPLOYEE_NEWER_DATE,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMPLOYEE_NEWER_DATE, GlobalVariable.CONST_LANG_EN)
                                });
                            }
                        }
                    }
                    else
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_EMPLOYEE_COMPONENT_START_DATE,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMPLOYEE_COMPONENT_START_DATE, GlobalVariable.CONST_LANG_EN)
                        });
                    }
                    if (string.IsNullOrEmpty(inputModel.StartDate[i]))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_EMPLOYEE_COMPONENT_END_DATE,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMPLOYEE_COMPONENT_END_DATE, GlobalVariable.CONST_LANG_EN)
                        });
                    }
                    DateTime dateTimeStart = DateTime.ParseExact(inputModel.StartDate[i].Trim(), "dd/MM/yyyy",
                             CultureInfo.InvariantCulture);
                    DateTime dateTimeEnd = DateTime.ParseExact(inputModel.EndDate[i].Trim(), "dd/MM/yyyy",
                          CultureInfo.InvariantCulture);

                    if (dateTimeEnd < dateTimeStart)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_EMPLOYEE_NEWER_END_DATE,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMPLOYEE_NEWER_END_DATE, GlobalVariable.CONST_LANG_EN)
                        });
                    }
                    if (dtStartDate == DateTime.MinValue)
                        dtStartDate = hsStartDate[i];
                    if (dtEndDate == DateTime.MinValue)
                        dtEndDate = hsEndDate[i];
                    if (i > 0)
                    {
                        if (hsStartDate[i] <= dtEndDate && dtEndDate <= hsEndDate[i])
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.ERR_EMPLOYEE_END_DATE_OVERLAP,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMPLOYEE_END_DATE_OVERLAP, GlobalVariable.CONST_LANG_EN)
                            });
                        }
                    }
                }
                //foreach (var item in inputModel.StartDate)
                //{
                //    if (!string.IsNullOrEmpty(item))
                //    {
                //        DateTime dateTime = DateTime.ParseExact(item, "dd/MM/yyyy",
                //              CultureInfo.InvariantCulture);
                //        if (dateTime < inputModel.CurrentPeriodStartDate)
                //        {
                //            status = false;
                //            errMessage.Add(new Global_Error_Code()
                //            {
                //                Error_Code = GlobalVariable.ERR_EMPLOYEE_NEWER_DATE,
                //                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMPLOYEE_NEWER_DATE, GlobalVariable.CONST_LANG_EN)
                //            });
                //        }
                //    }
                //    else
                //    {
                //        status = false;
                //        errMessage.Add(new Global_Error_Code()
                //        {
                //            Error_Code = GlobalVariable.ERR_EMPLOYEE_COMPONENT_START_DATE,
                //            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMPLOYEE_COMPONENT_START_DATE, GlobalVariable.CONST_LANG_EN)
                //        });
                //    }
                //}
                //DateTime HsEndDate =new DateTime();
                //DateTime HsStartDate = new DateTime();
                //foreach(var item in inputModel.EndDate)
                //{
                //    if (!string.IsNullOrEmpty(item))
                //    {
                //        DateTime dateTime = DateTime.ParseExact(item, "dd/MM/yyyy",
                //              CultureInfo.InvariantCulture);
                //        if (HsEndDate == DateTime.MinValue)
                //            HsEndDate = dateTime;

                //        if (HsEndDate < dateTime)
                //        {
                //            status = false;
                //            errMessage.Add(new Global_Error_Code()
                //            {
                //                Error_Code = GlobalVariable.ERR_EMPLOYEE_END_DATE_OVERLAP,
                //                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMPLOYEE_END_DATE_OVERLAP, GlobalVariable.CONST_LANG_EN)
                //            });
                //        }
                //        if (HsStartDate == DateTime.MinValue)
                //            HsStartDate = dateTime;
                //        if (HsStartDate > dateTime)
                //        {
                //            status = false;
                //            errMessage.Add(new Global_Error_Code()
                //            {
                //                Error_Code = GlobalVariable.ERR_EMPLOYEE_END_DATE_OVERLAP,
                //                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMPLOYEE_END_DATE_OVERLAP, GlobalVariable.CONST_LANG_EN)
                //            });
                //        }
                //    }
                //    else
                //    {
                //        status = false;
                //        errMessage.Add(new Global_Error_Code()
                //        {
                //            Error_Code = GlobalVariable.ERR_EMPLOYEE_COMPONENT_END_DATE,
                //            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMPLOYEE_COMPONENT_END_DATE, GlobalVariable.CONST_LANG_EN)
                //        });
                //    }
                //}

                if (UserData.PayrollStartDate == null)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.ERR_COMPONENT_EMPTY_PAYROLL_PERIOD,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_COMPONENT_EMPTY_PAYROLL_PERIOD, GlobalVariable.CONST_LANG_EN)
                    });
                }
                var strStatus = "";
                if (UserData.EmployeeStatus != null)
                    strStatus = UserData.EmployeeStatus.Employment_Status;
                else
                {
                    var strModel = db.vw_Employee_Appointment_Status.Where(s => s.id == UserData.OrganizationSelected_Id && s.Effective_Date <= UserData.PayrollEndDate).OrderByDescending(s => s.Effective_Date).FirstOrDefault();
                    if (strModel != null)
                    {
                        strStatus = strModel.Employment_Status;
                        if (strModel.Employment_Status == null)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.ERR_CREATE_WORKING_TIME,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_CREATE_WORKING_TIME, language)
                            });
                        }
                    }
                }
                var modelTax = db.tbl_Tax.Where(s => s.Employee_ID == UserData.EmployeeSelected.id && s.Authorize_Status == 1 && s.Status_Code == 1).ToList();
                if (modelTax.Count() == 0)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.ERR_EMPLOYEE_TAX_INFO,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMPLOYEE_TAX_INFO, GlobalVariable.CONST_LANG_EN)
                    });
                }


                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (inputModel.organizationPayrollComponent.Currency == null)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_CURRENCY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_CURRENCY, language)
                        });
                    }
                    var baseworkingday = db.vw_Employee_Appointment_WorkingTime.Where(s => s.Employee_Id == UserData.EmployeeSelected.id).OrderBy(s => s.Start_Date).ToList();
                    var sysparam = db.tbl_SysParam.Where(s => s.Param_Code == "BaseWorkingActualProrate").FirstOrDefault().Value.Split('|');
                    if (baseworkingday.Count() > 0)
                    {
                        var strbaseWorking = baseworkingday.LastOrDefault().Base_Working_Day;
                        if (strbaseWorking.ToLower() == "actual")
                        {
                            if (inputModel.organizationPayrollComponent.Is_New_Join == true || inputModel.organizationPayrollComponent.Is_Prorate == true)
                            {
                                if (sysparam.Contains(inputModel.organizationPayrollComponent.Prorate_Base))
                                {
                                }
                                else
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = GlobalVariable.ERR_EMPLOYEE_COMPONENT_PRORATE_BASE,
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMPLOYEE_COMPONENT_PRORATE_BASE, GlobalVariable.CONST_LANG_EN)
                                    });
                                }
                            }
                        }
                    }
                    var duplicateEmployeComponent = GeneralCore.EmployeePayrollComponentQuery().Where(s => s.Organization_Payroll_Component_Code == inputModel.organizationPayrollComponent.Organization_Payroll_Component_Code).ToList();
                    if (duplicateEmployeComponent.Count() > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_EMPLOYEE_COMPONENT_DUPLICATE,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMPLOYEE_COMPONENT_DUPLICATE, language)
                        });
                    }
                    if (inputModel.organizationPayrollComponent.Tax_Deduction == null && inputModel.organizationPayrollComponent.Taxable_Type == "N")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_TAX_DEDUCTION__IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_TAX_DEDUCTION__IS_EMPTY, language)
                        });
                    }
                    else if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Tax_Policy) && inputModel.organizationPayrollComponent.Taxable_Type == "T")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_TAX_POLICY_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_TAX_POLICY_IS_EMPTY, language)
                        });
                    }
                    else if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Frequency) && inputModel.organizationPayrollComponent.Taxable_Type == "T")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_FREQUENCY_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FREQUENCY_IS_EMPTY, language)
                        });
                    }
                    else if (inputModel.organizationPayrollComponent.Amount_Type == "FX" && inputModel.organizationPayrollComponent.Calculation_Basic != "FL")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_AMOUNT_TYPE_CALCULATION,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_AMOUNT_TYPE_CALCULATION, language)
                        });
                    }

                    //progress
                    //else if (inputModel.organizationPayrollComponent.Component_Group == "LN"&&inputModel.organizationPayrollComponent.Status_Code==1)
                    //{
                    //    status = false;
                    //    errMessage.Add(new Global_Error_Code()
                    //    {
                    //        Error_Code = "CODE",
                    //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_ACTIVE_STATUS_COMPONENT_GROUP, language)
                    //    });
                    //}
                    else if (inputModel.organizationPayrollComponent.Component_Group == "CB" && inputModel.organizationPayrollComponent.Amount_Type != "FX" || inputModel.organizationPayrollComponent.Component_Group == "CB" && inputModel.organizationPayrollComponent.Calculation_Basic != "FL")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_CALCULATION_BASIC_COMPENSATION,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_CALCULATION_BASIC_COMPENSATION, language)
                        });
                    }
                    else if (inputModel.organizationPayrollComponent.Component_Group == "LN" && inputModel.organizationPayrollComponent.Amount_Type != "FX" && inputModel.organizationPayrollComponent.Calculation_Basic != "FL")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_CALCULATION_BASIC_LOAN,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_CALCULATION_BASIC_LOAN, language)
                        });
                    }
                    else if (inputModel.organizationPayrollComponent.Formula != null)
                    {

                        if (inputModel.organizationPayrollComponent.Amount_Type == "FR" && UICommonFunction.IsNumeric(inputModel.organizationPayrollComponent.Formula))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.ERR_FORMULA,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FORMULA, language)
                            });
                        }
                        else if (inputModel.organizationPayrollComponent.Amount_Type == "FX" && !UICommonFunction.IsNumeric(inputModel.organizationPayrollComponent.Formula))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.ERR_FORMULA,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FORMULA, language)
                            });
                        }

                    }
                }
                if (type == GlobalVariable.CONST_EDIT)
                {
                    var baseworkingday = db.vw_Employee_Appointment_WorkingTime.Where(s => s.Employee_Id == UserData.EmployeeSelected.id).OrderBy(s => s.Start_Date).ToList();
                    var sysparam = db.tbl_SysParam.Where(s => s.Param_Code == "BaseWorkingActualProrate").FirstOrDefault().Value.Split('|');
                    if (baseworkingday.Count() > 0)
                    {
                        var strbaseWorking = baseworkingday.LastOrDefault().Base_Working_Day;
                        if (strbaseWorking.ToLower() == "actual")
                        {
                            if (inputModel.organizationPayrollComponent.Is_New_Join == true || inputModel.organizationPayrollComponent.Is_Prorate == true)
                            {
                                if (sysparam.Contains(inputModel.organizationPayrollComponent.Prorate_Base))
                                {
                                }
                                else
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = GlobalVariable.ERR_EMPLOYEE_COMPONENT_PRORATE_BASE,
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMPLOYEE_COMPONENT_PRORATE_BASE, GlobalVariable.CONST_LANG_EN)
                                    });
                                }
                            }
                        }

                    }
                    var CheckUsed = (from s in db.tbl_Additional_Payroll
                                     join q in db.tbl_Additional_Payroll_Detail
                                     on s.id equals q.Additional_Payroll_ID
                                     select new { ID = q.id, Employee_ID = s.Employee_ID, ComponentCode = q.Additional_Component, Status_Code = s.Status_Code, Authorize_Status = s.Authorize_Status }).Where(t => t.Employee_ID == UserData.EmployeeSelected.id && t.ComponentCode == inputModel.organizationPayrollComponent.Organization_Payroll_Component_Code && t.Status_Code == CoreVariable.CONST_STATUS_ACTIVE).ToList();


                    if (CheckUsed.Count() > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_COMPONENT_CODE_ALREADY_USED,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_COMPONENT_CODE_ALREADY_USED, GlobalVariable.CONST_LANG_EN)
                        });
                    }
                    if (inputModel.viewEmployeePayrollComponent.Tax_Deduction == null && inputModel.organizationPayrollComponent.Taxable_Type == "N")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_TAX_DEDUCTION__IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_TAX_DEDUCTION__IS_EMPTY, language)
                        });
                    }
                    else if (UICommonFunction.StringIsNullOrEmpty(inputModel.viewEmployeePayrollComponent.Tax_Policy) && inputModel.organizationPayrollComponent.Taxable_Type == "T")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_TAX_POLICY_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_TAX_POLICY_IS_EMPTY, language)
                        });
                    }
                    else if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationPayrollComponent.Frequency) && inputModel.organizationPayrollComponent.Taxable_Type == "T")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_FREQUENCY_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FREQUENCY_IS_EMPTY, language)
                        });
                    }
                    else if (inputModel.organizationPayrollComponent.Amount_Type == "FX" && inputModel.organizationPayrollComponent.Calculation_Basic != "FL")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_AMOUNT_TYPE_CALCULATION,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_AMOUNT_TYPE_CALCULATION, language)
                        });
                    }

                    else if (inputModel.viewEmployeePayrollComponent.Component_Group == "CB" && inputModel.organizationPayrollComponent.Amount_Type != "FX" || inputModel.viewEmployeePayrollComponent.Component_Group == "CB" && inputModel.organizationPayrollComponent.Calculation_Basic != "FL")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_CALCULATION_BASIC_COMPENSATION,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_CALCULATION_BASIC_COMPENSATION, language)
                        });
                    }
                    else if (inputModel.viewEmployeePayrollComponent.Component_Group == "LN" && inputModel.organizationPayrollComponent.Amount_Type != "FX" && inputModel.organizationPayrollComponent.Calculation_Basic != "FL")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_CALCULATION_BASIC_LOAN,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_CALCULATION_BASIC_LOAN, language)
                        });
                    }
                    //else if (inputModel.viewEmployeePayrollComponent.f != null)
                    //{

                    //    if (inputModel.viewEmployeePayrollComponent.Amount_Type == "FR" && UICommonFunction.IsNumeric(inputModel.organizationPayrollComponent.Formula))
                    //    {
                    //        status = false;
                    //        errMessage.Add(new Global_Error_Code()
                    //        {
                    //            Error_Code = GlobalVariable.ERR_FORMULA,
                    //            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FORMULA, language)
                    //        });
                    //    }
                    //    else if (inputModel.viewEmployeePayrollComponent.Amount_Type == "FX" && !UICommonFunction.IsNumeric(inputModel.organizationPayrollComponent.Formula))
                    //    {
                    //        status = false;
                    //        errMessage.Add(new Global_Error_Code()
                    //        {
                    //            Error_Code = GlobalVariable.ERR_FORMULA,
                    //            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_FORMULA, language)
                    //        });
                    //    }

                    //}
                }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;

        }
        #endregion

        #region validation Contact Person
        public static bool ValidationContactPerson(Global_Contact_Person inputModel, string language, string Type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            InputValidation inputValidation = new InputValidation();
            try
            {

                if (UICommonFunction.StringIsNullOrEmpty(inputModel.contactPersonModels.Name))
                {
                    string Error_Code = GlobalVariable.CONST_ERR_NAME_IS_EMPTY;
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = Error_Code,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_NAME_IS_EMPTY, language)
                    });
                }

                if (UICommonFunction.StringIsNullOrEmpty(inputModel.contactPersonModels.Office_Phone))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "OFFICE PHONE NUMBER",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_OFFICE_PHONE_NUMBER_IS_EMPTY, language)
                    });
                }

                else if (!inputValidation.IsNumeric(inputModel.contactPersonModels.Office_Phone))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "OFFICE PHONE NUMBER",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_VALID_PHONE_NUMBER, language)
                    });
                }

                if (inputModel.contactPersonModels.Personal_Phone != null)
                {
                    if (!inputValidation.IsNumeric(inputModel.contactPersonModels.Personal_Phone))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "PERSONAL NUMBER",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_VALID_PHONE_NUMBER, language)
                        });
                    }
                }

                if (inputModel.contactPersonModels.Email != null)
                {
                    if (!inputModel.contactPersonModels.Email.Contains("@") || !inputModel.contactPersonModels.Email.Contains("."))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "EMAIL",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_USER_INVALID_EMAIL, language)
                        });
                    }
                }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region validation Head Office Branch

        public static bool ValidationHeadOfficeBranch(Global_HOBRANCH inputModel, string language, string type, Guid Org_ID, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            InputValidation inputValidation = new InputValidation();
            User_Data USERDATA = new User_Data();
            APP_CORE.CoreVariable Core = new APP_CORE.CoreVariable();
            USERDATA = Core.CoreUserLogin();

            var CheckCode = db.tbl_HeadOffice_Branch.Where(p => p.Code == inputModel.headOfficeBranch.Code && p.Organization_ID == Org_ID).ToList();
            var CheckTaxID = db.tbl_HeadOffice_Branch.Where(p => p.HO_Branch_TAX_ID != inputModel.headOfficeBranch.HO_Branch_TAX_ID).ToList();
            try
            {
                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.headOfficeBranch.Code))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "CODE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HO_BRANCH_CODE_IS_EMPTY, language)
                        });
                    }
                    else if (CheckCode.Count != 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "CODE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HO_BRANCH_CODE_ALREADY_EXISTS, language)
                        });
                    }
                }

                if (type == GlobalVariable.CONST_EDIT)
                {
                    var Data = db.tbl_HeadOffice_Branch.Where(p => p.id == inputModel.headOfficeBranch.id).FirstOrDefault().Code;
                    var ExistCode = db.tbl_HeadOffice_Branch.Where(p => p.Code == inputModel.headOfficeBranch.Code && p.Organization_ID == USERDATA.OrganizationSelected_Id).ToList();

                    var DataMapLocation = db.tbl_Base_Location_Header.Where(s => s.Organization_Id == USERDATA.OrganizationSelected_Id && s.Status_Code==GlobalVariable.CONST_STATUS_ACTIVE &s.Authorize_Status==GlobalVariable.CONST_AUTHORIZED).ToList();
                    var tempBranchLocation = db.tbl_HeadOffice_Branch_Location.Where(p => p.HeadOffice_Branch_ID == inputModel.headOfficeBranch.id).ToList();
                    if(tempBranchLocation.Count()>0)
                    {
                        if(DataMapLocation.Count()>0)
                        {
                            List<Global_Error_Code> tempError = new List<Global_Error_Code>();
                            foreach (var item in tempBranchLocation)
                            {
                                var checkMapLocation = DataMapLocation.Where(p => p.Reference_Id.Value == item.id ).ToList();
                                if(checkMapLocation.Count()>0)
                                {
                                    if (inputModel.headOfficeBranch.Status_Code != GlobalVariable.CONST_STATUS_ACTIVE)
                                    {
                                        var ErrorCount = tempError.Where(s => s.Error_Code == GlobalVariable.CONST_ERR_HO_BRANCH_MAP_LOCATION).ToList();
                                        if (ErrorCount.Count() < 1)
                                        {
                                            status = false;
                                            tempError.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_HO_BRANCH_MAP_LOCATION,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HO_BRANCH_MAP_LOCATION, language)
                                            });
                                        }
                                    }
                                }
                            }
                            errMessage.AddRange(tempError);
                        }
                    }

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.headOfficeBranch.Code))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "CODE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HO_BRANCH_CODE_IS_EMPTY, language)
                        });
                    }

                    if (Data != inputModel.headOfficeBranch.Code)
                    {
                        if (ExistCode.Count > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "CODE",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HO_BRANCH_CODE_ALREADY_EXISTS, language)
                            });
                        }
                    }
                    }

                #region Tax
                if (UICommonFunction.StringIsNullOrEmpty(inputModel.headOfficeBranch.Tax_Address))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "Tax_Address",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HO_BRANCH_ADDRESS_IS_EMPTY, language)
                    });
                }
                if (UICommonFunction.StringIsNullOrEmpty(inputModel.headOfficeBranch.Tax_District))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "Tax_District",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HO_BRANCH_TAX_DISTRICT_IS_EMPTY, language)
                    });
                }
                if (UICommonFunction.StringIsNullOrEmpty(inputModel.headOfficeBranch.Tax_Village))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "Tax_Village",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HO_BRANCH_TAX_VILLAGE_IS_EMPTY, language)
                    });
                }

                if (UICommonFunction.StringIsNullOrEmpty(inputModel.headOfficeBranch.Tax_Regency_City))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "Tax_Regency_City",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HO_BRANCH_TAX_CITY_REGENCY_IS_EMPTY, language)
                    });
                }
                if (UICommonFunction.StringIsNullOrEmpty(inputModel.headOfficeBranch.Tax_Province))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "Tax_Province",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CHOOSE_ONE, language)
                    });
                }
                if (UICommonFunction.StringIsNullOrEmpty(inputModel.headOfficeBranch.Tax_Country))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "Tax_Country",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CHOOSE_ONE, language)
                    });
                }
                if (UICommonFunction.StringIsNullOrEmpty(inputModel.headOfficeBranch.Tax_Post_Code))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "Tax_Post_Code",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HO_BRANCH_TAX_POST_IS_EMPTY, language)
                    });
                }
                #endregion

                List<string> ListLocation = new List<string>();
                foreach (var item in inputModel.locationarray)
                {
                    if (item != "")
                    {
                        ListLocation.Add(item);
                    }
                }
                if (ListLocation.Count <= 0)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_HO_BRANCH_LOCATION_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HO_BRANCH_LOCATION_IS_EMPTY, language)
                    });
                }

                /////============////

                if (inputModel.headOfficeBranch.HO_Branch_Phone_Number != null)
                {
                    if (!inputValidation.IsNumeric(inputModel.headOfficeBranch.HO_Branch_Phone_Number))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "HO_Branch_Phone_Number",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HO_BRANCH_VALID_NUMBER, language)
                        });
                    }
                }
                if (inputModel.headOfficeBranch.HO_Branch_Fax_Number != null)
                {
                    if (!inputValidation.IsNumeric(inputModel.headOfficeBranch.HO_Branch_Fax_Number))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "HO_Branch_Fax_Number",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HO_BRANCH_VALID_NUMBER, language)
                        });
                    }
                }
                if (UICommonFunction.StringIsNullOrEmpty(inputModel.headOfficeBranch.HO_Branch_TAX_ID))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_HO_BRANCH_TAXID_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HO_BRANCH_TAXID_IS_EMPTY, language)
                    });
                }
                if (inputModel.headOfficeBranch.AR_Phone_Number != null)
                {
                    if (!inputValidation.IsNumeric(inputModel.headOfficeBranch.AR_Phone_Number))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "AR_Phone_Number",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HO_BRANCH_VALID_NUMBER, language)
                        });
                    }
                }
                if (inputModel.headOfficeBranch.AR_Email != null)
                {
                    if (!inputModel.headOfficeBranch.AR_Email.Contains("@") || !inputModel.headOfficeBranch.AR_Email.Contains("."))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "EMAIL",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_USER_INVALID_EMAIL, language)
                        });
                    }
                }
               errMessage.Distinct();

            }

            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Holiday Clalendar Organization
        public static bool ValidationHolidayCalendarOrganization(Global_Holiday_Calender_Organization inputModel, string language, Guid Organization_Selected_ID, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();

            try
            {

                inputModel.holdayOrganizationModels.Holiday_Date = UICommonFunction.ConvertToDateTime(inputModel.strHoliday);
                DateTime dDate;
                DateTime.TryParseExact(inputModel.strHoliday, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dDate);
                //if (inputModel.Location_Holiday != null)
                //{
                List<tbl_Holiday_Calendar_Organization> ListAlreadyExist = new List<tbl_Holiday_Calendar_Organization>();
                //foreach (var item in inputModel.Location_Holiday)
                //{
                //tbl_Holiday_Calendar_Organization CheckAlreadyExist = new tbl_Holiday_Calendar_Organization();
                //CheckAlreadyExist = GeneralCore.HolidayCalendarOrganizationQuery().Where(p => (p.Holiday_Date.Year == dDate.Year && p.Holiday_Date.Month == dDate.Month && p.Holiday_Date.Day == dDate.Day)&& p.Location == item).FirstOrDefault();
                List<tbl_Holiday_Calendar_Organization> CheckAlreadyExist = GeneralCore.HolidayCalendarOrganizationQuery().Where(p => p.Holiday_Date.Year == dDate.Year && p.Holiday_Date.Month == dDate.Month && p.Holiday_Date.Day == dDate.Day).ToList();

                //  ListAlreadyExist.Add(CheckAlreadyExist);
                //}

                //if (type == GlobalVariable.CONST_CREATE && Boolean.Parse(inputModel.National) == false)
                if (type == GlobalVariable.CONST_CREATE && Boolean.Parse(inputModel.National) == true)
                {
                    if (CheckAlreadyExist.Count > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_HCO_ALREADY_EXISTS,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HCO_ALREADY_EXISTS, language)
                        });
                    }
                }

                //}

                if (inputModel.holdayOrganizationModels.Holiday_Date == DateTime.Parse("1/1/0001 12:00:00 AM"))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_HCO_DATE_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HCO_DATE_IS_EMPTY, language)
                    });
                }

                //if (type == GlobalVariable.CONST_CREATE && Boolean.Parse(inputModel.National) == false)

                //{
                //        if (inputModel.Location_Holiday == null)
                //        {
                //            status = false;
                //            errMessage.Add(new Global_Error_Code()
                //            {
                //                Error_Code = GlobalVariable.CONST_ERR_HCO_LOCATION_IS_EMPTY,
                //                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HCO_LOCATION_IS_EMPTY, language)
                //            });
                //        }
                //}

                //if (type == GlobalVariable.CONST_EDIT && Boolean.Parse(inputModel.National) == false)
                if (type == GlobalVariable.CONST_EDIT && Boolean.Parse(inputModel.National) == true)
                {
                    if (inputModel.Location_Holiday == null)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_HCO_LOCATION_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HCO_LOCATION_IS_EMPTY, language)
                        });
                    }
                }
            }

            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }

        #endregion

        #region Bank Information
        public static bool ValidationBankInformation(Global_Bank_Information inputModel, string language, string type,Guid OrgID, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            InputValidation inputValidation = new InputValidation();
            try
            {
                var ListBank = db.tbl_Bank_Information.Where(p => p.Organization_ID == OrgID && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED));
                inputModel.bankInformationList = ListBank.Where(s => s.Bank == inputModel.bankInformationModels.Bank &&
                                                                                s.Account_Number == inputModel.bankInformationModels.Account_Number).ToList();

                if (UICommonFunction.StringIsNullOrEmpty(inputModel.bankInformationModels.Bank))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_BANK_INFORMATION_BANK_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BANK_INFORMATION_BANK_IS_EMPTY, language)
                    });
                }
                else if // kolo dd bank code length > 15 caracter, ex (BCA) != (BCA PT.Bank Central Asia)
                    (inputModel.bankInformationModels.Bank.Length > 15)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_BANK_INFORMATION_BANK_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BANK_INFORMATION_BANK_MAX_LEN, language)
                    });
                }

                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.bankInformationModels.Account_Number))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "ACCOUNT",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BANK_INFORMATION_ACCOUNT_NUMBER_IS_EMPTY, language)
                        });
                    }
                    else if (inputModel.bankInformationList.Count > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "ACCOUNT",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BANK_INFORMATION_ACCOUNT_NUMBER_ALREADY_EXIST, language)
                        });
                    }
                    else if (inputValidation.IsNumeric(inputModel.bankInformationModels.Account_Number) == false)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "ACCOUNT",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BANK_INFORMATION_ACCOUNT_VALID_NUMBER, language)
                        });
                    }
                }

                if (type == GlobalVariable.CONST_EDIT)
                {
                    var EditExistBankAccount = db.tbl_Bank_Information.Where(p => p.id == inputModel.bankInformationModels.id).FirstOrDefault();

                    var ExistBankAccount = db.tbl_Bank_Information.Where(p => p.Bank == inputModel.bankInformationModels.Bank &&
                                                                         p.Account_Number == inputModel.bankInformationModels.Account_Number &&
                                                                         p.Account_Name == inputModel.bankInformationModels.Account_Name).ToList();

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.bankInformationModels.Account_Number))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "ACCOUNT",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BANK_INFORMATION_ACCOUNT_NUMBER_IS_EMPTY, language)
                        });
                    }
                    if (EditExistBankAccount.Account_Number != inputModel.bankInformationModels.Account_Number || EditExistBankAccount.Bank != inputModel.bankInformationModels.Bank)
                    {
                        if (ExistBankAccount.Count > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "ACCOUNT",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BANK_INFORMATION_ACCOUNT_NUMBER_ALREADY_EXIST, language)
                            });
                        }
                    }

                }

                if (UICommonFunction.StringIsNullOrEmpty(inputModel.bankInformationModels.Account_Name))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_BANK_INFORMATION_ACCOUNT_NAME_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BANK_INFORMATION_ACCOUNT_NAME_IS_EMPTY, language)
                    });
                }

                if (UICommonFunction.StringIsNullOrEmpty(inputModel.bankInformationModels.Currency_Code))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_BANK_INFORMATION_CURRENCY_CODE_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BANK_INFORMATION_CURRENCY_CODE_IS_EMPTY, language)
                    });
                }


                var DataListOfBank = db.tbl_List_Of_Bank.Where(p => p.Bank_Code == inputModel.bankInformationModels.Bank).FirstOrDefault();

                //cek Hsbc
                if (DataListOfBank.Is_Mandatory == true && DataListOfBank.Is_Length == "20")
                {
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.bankInformationModels.Debet_Account_ID))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_BANK_INFORMATION_ACCOUNT_DEBET_ACCOUNT_ID_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BANK_INFORMATION_ACCOUNT_DEBET_ACCOUNT_ID_IS_EMPTY, language)
                        });
                    }
                }
                //cek Hsbc & BCA Mandatory
                if (DataListOfBank.Is_Mandatory == true)
                {
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.bankInformationModels.Reference))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "REFERENCE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BANK_INFORMATION_REFERENCE_IS_EMPTY, language)
                        });
                    }

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.bankInformationModels.Payment_Set_Code))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_BANK_INFORMATION_PAYMENT_SET_CODE_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BANK_INFORMATION_PAYMENT_SET_CODE_IS_EMPTY, language)
                        });
                    }
                }
                //15 caracter For BCA
                if (DataListOfBank.Is_Mandatory == true && DataListOfBank.Is_Length == "15")
                {
                    if (inputModel.bankInformationModels.Reference.Length > 15)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "REFERENCE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_REFF_BANK_INFORMATION_MAX_LEN, language)
                        });
                    }
                }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }

        #endregion

        #region validation BPJS Manpower
        public static bool ValidationBPJSManpower(Global_BPJS_Manpower inputModel, Guid Organization_ID, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            InputValidation inputValidation = new InputValidation();
            errMessage = new List<Global_Error_Code>();
            User_Data USERDATA = new User_Data();
            APP_CORE.CoreVariable Core = new APP_CORE.CoreVariable();
            USERDATA = Core.CoreUserLogin();

            try
            {
                inputModel.BPJSManpowerList = db.tbl_BPJS_Manpower.Where(s => s.Npp_Number == inputModel.BPJSManpowerModels.Npp_Number).ToList();
                if (type == GlobalVariable.CONST_CREATE)
                {

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.BPJSManpowerModels.Npp_Number))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "NPP",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_NPP_NUMBER_IS_EMPTY, language)
                        });
                    }

                    else if (inputModel.BPJSManpowerList.Count > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "NPP",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_NPP_NUMBER_ALREADY_EXISTS, language)
                        });
                    }
                }


                if (type == GlobalVariable.CONST_EDIT)
                {
                    var NppNumber = "";
                    var EDITNPP = db.tbl_BPJS_Manpower.Where(p => p.id == inputModel.BPJSManpowerModels.id).FirstOrDefault();
                    if (EDITNPP != null) { NppNumber = EDITNPP.Npp_Number; }
                    var EXISTNPP = db.tbl_BPJS_Manpower.Where(p => p.Npp_Number == inputModel.BPJSManpowerModels.Npp_Number).ToList();

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.BPJSManpowerModels.Npp_Number))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "NPP",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_NPP_NUMBER_IS_EMPTY, language)
                        });
                    }
                    if (NppNumber != inputModel.BPJSManpowerModels.Npp_Number)
                    {
                        if (EXISTNPP.Count > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "NPP",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_NPP_NUMBER_ALREADY_EXISTS, language)
                            });
                        }
                    }
                }

                if (inputModel.BPJSManpowerModels.Office_Phone_Number != null)
                {
                    if (!inputValidation.IsNumeric(inputModel.BPJSManpowerModels.Office_Phone_Number))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "OFFICE PHONE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_OFFICE_PHONE_NUMBER_NOT_VALID, language)
                        });
                    }
                }

                if (inputModel.BPJSManpowerModels.Office_Fax_Number != null)
                {
                    if (!inputValidation.IsNumeric(inputModel.BPJSManpowerModels.Office_Fax_Number))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "FAX",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_OFFICE_FAX_NUMBER_NOT_VALID, language)
                        });
                    }
                }
                if (inputModel.BPJSManpowerModels.Phone_Mobile_Number != null)
                {
                    if (!inputValidation.IsNumeric(inputModel.BPJSManpowerModels.Phone_Mobile_Number))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "PHONE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PHONE_MOBILE_NUMBER_NOT_VALID, language)
                        });
                    }
                }

                if (inputModel.BPJSManpowerModels.Email != null)
                {
                    if (!inputModel.BPJSManpowerModels.Email.Contains("@") || !inputModel.BPJSManpowerModels.Email.Contains("."))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "EMAIL",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_USER_INVALID_EMAIL, language)
                        });
                    }
                }

            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region validation BPJS Healthcare
        public static bool ValidationBPJSHealthcare(Global_BPJS_Healthcare inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            InputValidation inputValidation = new InputValidation();
            errMessage = new List<Global_Error_Code>();
            User_Data USERDATA = new User_Data();
            APP_CORE.CoreVariable Core = new APP_CORE.CoreVariable();
            USERDATA = Core.CoreUserLogin();

            try
            {
                inputModel.BPJSHealthcareList = db.tbl_BPJS_Healthcare.Where(s => s.Npp_Number == inputModel.BPJSHealthcareModels.Npp_Number).ToList();

                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.BPJSHealthcareModels.Npp_Number))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "NPP",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_NPP_NUMBER_IS_EMPTY, language)
                        });
                    }

                    else if (inputModel.BPJSHealthcareList.Count > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "NPP",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_NPP_NUMBER_ALREADY_EXISTS, language)
                        });
                    }
                }
                if (type == GlobalVariable.CONST_EDIT)
                {
                    var NppNumber = "";
                    var EDITNPP = db.tbl_BPJS_Healthcare.Where(p => p.id == inputModel.BPJSHealthcareModels.id).FirstOrDefault().Npp_Number;
                    if (EDITNPP != null) { NppNumber = EDITNPP; }

                    var EXISTNPP = db.tbl_BPJS_Healthcare.Where(p => p.Npp_Number == inputModel.BPJSHealthcareModels.Npp_Number).ToList();

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.BPJSHealthcareModels.Npp_Number))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "NPP",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_NPP_NUMBER_IS_EMPTY, language)
                        });
                    }

                    if (EDITNPP != inputModel.BPJSHealthcareModels.Npp_Number)
                    {
                        if (EXISTNPP.Count > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "NPP",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_NPP_NUMBER_ALREADY_EXISTS, language)
                            });
                        }
                    }
                }

                if (inputModel.BPJSHealthcareModels.Office_Phone_Number != null)
                {
                    if (!inputValidation.IsNumeric(inputModel.BPJSHealthcareModels.Office_Phone_Number))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "OFFICE PHONE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_OFFICE_PHONE_NUMBER_NOT_VALID, language)
                        });
                    }
                }
                if (inputModel.BPJSHealthcareModels.Office_Fax_Number != null)
                {
                    if (!inputValidation.IsNumeric(inputModel.BPJSHealthcareModels.Office_Fax_Number))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "FAX",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_OFFICE_FAX_NUMBER_NOT_VALID, language)
                        });
                    }
                }

                if (inputModel.BPJSHealthcareModels.Phone_Mobile_Number != null)
                {
                    if (!inputValidation.IsNumeric(inputModel.BPJSHealthcareModels.Phone_Mobile_Number))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "PHONE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PHONE_MOBILE_NUMBER_NOT_VALID, language)
                        });
                    }
                }

                if (inputModel.BPJSHealthcareModels.Email != null)
                {
                    if (!inputModel.BPJSHealthcareModels.Email.Contains("@") || !inputModel.BPJSHealthcareModels.Email.Contains("."))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_EMAIL_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_USER_INVALID_EMAIL, language)
                        });
                    }
                }
            }

            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region validation Cost Center
        public static bool ValidationCostCenter(Global_Cost_Center inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                inputModel.costCenterList = db.tbl_Cost_Center.Where(s => s.Code == inputModel.costCenterModels.Code).ToList();
                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.costCenterModels.Code))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "CODE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_COST_CENTER_CODE_IS_EMPTY, language)
                        });
                    }

                    else if (inputModel.costCenterList.Count > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "CODE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_COST_CENTER_CODE_ALREADY_EXISTS, language)
                        });
                    }
                }
                if (type == GlobalVariable.CONST_EDIT)
                {
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.costCenterModels.Code))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "CODE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_COST_CENTER_CODE_IS_EMPTY, language)
                        });
                    }
                }

            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region validation Chart Of Account
        public static bool ValidationChartAccount(Global_Chart_Of_Account inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                inputModel.chartOfAccountList = db.tbl_Chart_Account.Where(s => s.Code == inputModel.chartOfAccountModels.Code).ToList();

                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.chartOfAccountModels.Code))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "CODE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_COST_CENTER_CODE_IS_EMPTY, language)
                        });
                    }

                    else if (inputModel.chartOfAccountList.Count > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "CODE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_COST_CENTER_CODE_ALREADY_EXISTS, language)
                        });
                    }
                }
                if (type == GlobalVariable.CONST_EDIT)
                {
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.chartOfAccountModels.Code))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "CODE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_COST_CENTER_CODE_IS_EMPTY, language)
                        });
                    }
                }
                if (UICommonFunction.StringIsNullOrEmpty(inputModel.chartOfAccountModels.Description))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_CHART_ACCOUNT_DESCRIPTION_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CHART_ACCOUNT_DESCRIPTION_IS_EMPTY, language)
                    });
                }
                if (UICommonFunction.StringIsNullOrEmpty(inputModel.chartOfAccountModels.Db_Cr))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_CHART_ACCOUNT_DB_CR_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CHART_ACCOUNT_DB_CR_IS_EMPTY, language)
                    });
                }


            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Payslip Information
        public static bool ValidationPayslipInformation(Global_Employee_Payslip_Info inputModel, Guid employeeSelectedID, string language, string type, out List<Global_Error_Code> errMessage, Guid EmployeeID)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                inputModel.employeePayslipInfoList = GeneralCore.PayslipQuery().Where(p => p.Payslip_Distribution == inputModel.employeePayslipInfoModel.Payslip_Distribution).ToList();

                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    var RecordData = GeneralCore.PayslipQuery().Where(p => p.Employee_ID == EmployeeID).FirstOrDefault();
                    if (RecordData != null)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_1_EMPLOYEE_ONLY_HAVE_1_RECORD_DATA,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_1_EMPLOYEE_ONLY_HAVE_1_RECORD_DATA, language)
                        });
                    }

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.employeePayslipInfoModel.Payslip_Distribution))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_PAYSLIP_INFORMATION_PAYSLIP_DISTRIBUTION_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PAYSLIP_INFORMATION_PAYSLIP_DISTRIBUTION_IS_EMPTY, language)
                        });
                    }
                    else if (inputModel.employeePayslipInfoList.Count > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Message",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PAYSLIP_INFORMATION_PAYSLIP_DISTRIBUTION, language)
                        });
                    }
                }
                #endregion

                #region Edit
                if (type == GlobalVariable.CONST_EDIT)
                {
                    inputModel.employeePayslipInfoList = GeneralCore.PayslipQuery().Where(s => s.id == inputModel.employeePayslipInfoModel.id).ToList();

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.employeePayslipInfoModel.Payslip_Distribution))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Payslip_Distribution",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PAYSLIP_INFORMATION_PAYSLIP_DISTRIBUTION_IS_EMPTY, language)
                        });
                    }

                    if (inputModel.employeePayslipInfoList.FirstOrDefault().Payslip_Distribution != inputModel.employeePayslipInfoModel.Payslip_Distribution)
                    {
                        inputModel.employeePayslipInfoList = GeneralCore.PayslipQuery().Where(s => s.Payslip_Distribution == inputModel.employeePayslipInfoModel.Payslip_Distribution).Take(1).ToList();
                        if (inputModel.employeePayslipInfoList.Count() > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Message",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PAYSLIP_INFORMATION_PAYSLIP_DISTRIBUTION, language)
                            });
                        }
                    }
                }
                #endregion
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }

        #endregion

        #endregion

        #region Payroll Report Generation
        public static bool ValidationPayrollReportGeneration(Global_Payroll_Report_Generation inputModel, string language, User_Data inputData, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                if (inputModel.Payroll_Period == null)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_REPORT_GENERATION_PAYROLL_PERIOD,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_REPORT_GENERATION_PAYROLL_PERIOD, language) //ubah
                    });
                }
                if (inputModel.Run == null)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_REPORT_GENERATION_RUN,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_REPORT_GENERATION_RUN, language) //ubah
                    });
                }
                //if (inputModel.HeadOfficeBranch == null)
                //{
                //    status = false;
                //    errMessage.Add(new Global_Error_Code()
                //    {
                //        Error_Code = GlobalVariable.CONST_ERR_REPORT_GENERATION_HEAD_OFFICE_BRANCH,
                //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_REPORT_GENERATION_HEAD_OFFICE_BRANCH, language) //ubah
                //    });
                //}
                //if (inputModel.Location == null)
                //{
                //    status = false;
                //    errMessage.Add(new Global_Error_Code()
                //    {
                //        Error_Code = GlobalVariable.CONST_ERR_REPORT_GENERATION_LOCATION,
                //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_REPORT_GENERATION_LOCATION, language) //ubah
                //    });
                //}
                //if (inputModel.Employement_Status == null)
                //{
                //    status = false;
                //    errMessage.Add(new Global_Error_Code()
                //    {
                //        Error_Code = GlobalVariable.CONST_ERR_REPORT_GENERATION_EMPLOYEMENT_STATUS,
                //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_REPORT_GENERATION_EMPLOYEMENT_STATUS, language) //ubah
                //    });
                //}
                //if (inputModel.Employee_Status == null)
                //{
                //    status = false;
                //    errMessage.Add(new Global_Error_Code()
                //    {
                //        Error_Code = GlobalVariable.CONST_ERR_REPORT_GENERATION_EMPLOYEE_STATUS,
                //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_REPORT_GENERATION_EMPLOYEE_STATUS, language) //ubah
                //    });
                //}
                //if (inputModel.Deputation == null)
                //{
                //    status = false;
                //    errMessage.Add(new Global_Error_Code()
                //    {
                //        Error_Code = GlobalVariable.CONST_ERR_REPORT_GENERATION_DEPUTATION,
                //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_REPORT_GENERATION_DEPUTATION, language) //ubah
                //    });
                //}
                //if (inputModel.Division == null)
                //{
                //    status = false;
                //    errMessage.Add(new Global_Error_Code()
                //    {
                //        Error_Code = GlobalVariable.CONST_ERR_REPORT_GENERATION_DIVISION,
                //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_REPORT_GENERATION_DIVISION, language) //ubah
                //    });
                //}
                //if (inputModel.Department == null)
                //{
                //    status = false;
                //    errMessage.Add(new Global_Error_Code()
                //    {
                //        Error_Code = GlobalVariable.CONST_ERR_REPORT_GENERATION_DEPARTMENT,
                //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_REPORT_GENERATION_DEPARTMENT, language) //ubah
                //    });
                //}
                //if (inputModel.Grade == null)
                //{
                //    status = false;
                //    errMessage.Add(new Global_Error_Code()
                //    {
                //        Error_Code = GlobalVariable.CONST_ERR_REPORT_GENERATION_GRADE,
                //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_REPORT_GENERATION_GRADE, language) //ubah
                //    });
                //}
                //if (inputModel.Position == null)
                //{
                //    status = false;
                //    errMessage.Add(new Global_Error_Code()
                //    {
                //        Error_Code = GlobalVariable.CONST_ERR_REPORT_GENERATION_POSITION,
                //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_REPORT_GENERATION_POSITION, language) //ubah
                //    });
                //}
                if (inputModel.Payroll_Report_Code == null)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_REPORT_GENERATION_PAYROLL_REPORT_CODE,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_REPORT_GENERATION_PAYROLL_REPORT_CODE, language) //ubah
                    });
                }
                if (inputModel.Report_Group == null)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_REPORT_GENERATION_REPORT_GROUP,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_REPORT_GENERATION_REPORT_GROUP, language) //ubah
                    });
                }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region 
        public static bool ValidationPayrollReportSetting(Global_Report_Setting inputModel, string language, string type, User_Data inputData, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            InputValidation tes = new InputValidation();

            try
            {
                var checkduplicateFormat = GeneralCore.PayrollReportSettingQuery().Where(s => s.Payroll_Report_Code == inputModel.PayrollReportSetting.Payroll_Report_Code).ToList();
                if (type == GlobalVariable.CONST_CREATE)
                {
                    if(checkduplicateFormat.Count()>0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "ReportSetting",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_PAYROLL_REPORT_SETTING_DUPLICATE, language)
                        });
                    }
                }
                if (inputModel.ListComponentSelected == null)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_PAYROLL_REPORT_SETTING_COMPONENT_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PAYROLL_REPORT_SETTING_COMPONENT_IS_EMPTY, language)
                    });
                }
                if (String.IsNullOrEmpty(inputModel.PayrollReportSetting.Description))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_PAYROLL_REPORT_SETTING_DESCRIPTION_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PAYROLL_REPORT_SETTING_DESCRIPTION_IS_EMPTY, language)
                    });
                }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion
        #region validation OrganizationWorkingTime
        public static bool ValidationOrganizationWorkingTime(Global_Organization_Working_Time inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            InputValidation inputValidation = new InputValidation();
            User_Data USERDATA = new User_Data();
            APP_CORE.CoreVariable Core = new APP_CORE.CoreVariable();
            USERDATA = Core.CoreUserLogin();
            try
            {
                inputModel.organzationWorkingTeamList = GeneralCore.OrganizationWorkingTimeQuery().Where(s => s.Working_Time_Code == inputModel.organzationWorkingTeamModels.Working_Time_Code && s.Organization_ID == USERDATA.OrganizationSelected_Id).ToList();

                #region Cerate
                if (type == GlobalVariable.CONST_CREATE)
                {

                    if(!String.IsNullOrEmpty(inputModel.strScheduleTimeIn) && !String.IsNullOrEmpty(inputModel.strScheduleTimeIn))
                    {
                        if(TimeSpan.Parse(inputModel.strScheduleTimeOut) <= TimeSpan.Parse(inputModel.strScheduleTimeIn))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_ORGANIZATION_WORKINGTIME_OVERLAP_SCHEDULE,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_WORKINGTIME_OVERLAP_SCHEDULE, language) //ubah
                            });
                        }
                    }

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organzationWorkingTeamModels.Working_Time_Code))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "CODE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_WORKINGTIME_CODE_IS_EMPTY, language) //ubah
                        });
                    }

                    else if (inputModel.organzationWorkingTeamList.Count > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "CODE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_WORKINGTIME_CODE_ALREADY_EXISTS, language)// ubah
                        });
                    }

                }
                #endregion

                #region Edit
                if (type == GlobalVariable.CONST_EDIT)
                {

                    var EditExistCode = db.tbl_Organization_Working_Time.Where(p => p.id == inputModel.organzationWorkingTeamModels.id).FirstOrDefault().Working_Time_Code;
                    var ExistCode = db.tbl_Organization_Working_Time.Where(p => p.Working_Time_Code == inputModel.organzationWorkingTeamModels.Working_Time_Code && p.Organization_ID == USERDATA.OrganizationSelected_Id).ToList();

                    var List = GeneralCore.OrganizationWorkingTimeQuery().Where(p => p.Working_Time_Code == inputModel.organzationWorkingTeamModels.Working_Time_Code).ToList();

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organzationWorkingTeamModels.Working_Time_Code))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "CODE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_WORKINGTIME_CODE_IS_EMPTY, language) //ubah
                        });
                    }

                    if (EditExistCode != inputModel.organzationWorkingTeamModels.Working_Time_Code)
                    {
                        if (ExistCode.Count > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "CODE",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_WORKINGTIME_CODE_ALREADY_EXISTS, language)
                            });
                        }
                    }
                }
                #endregion

                #region take out Datacheck box
                List<string> hari = new List<string>();
                foreach (var item in inputModel.strWorkingDay)
                {
                    if (item != "false")
                    {
                        hari.Add(item);
                    }
                }
                #endregion

                if (hari.Count == 0)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_ORGANIZATION_WORKINGTIME_WORKING_DAY_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_WORKINGTIME_WORKING_DAY_IS_EMPTY, language)
                    });
                }

                if (UICommonFunction.StringIsNullOrEmpty(inputModel.organzationWorkingTeamModels.Base_Working_Day))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_ORGANIZATION_WORKINGTIME_BASE_WORKIG_DAY_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_WORKINGTIME_BASE_WORKIG_DAY_IS_EMPTY, language)
                    });
                }
                if (inputModel.organzationWorkingTeamModels.Base_Working_Day == "Fix")
                {
                    string inputString = null;
                    string inputCalString = null;
                    inputString = inputModel.organzationWorkingTeamModels.Base_Fix.ToString();
                    inputCalString = inputModel.organzationWorkingTeamModels.Base_Calendar_Day.ToString();

                    if (inputModel.organzationWorkingTeamModels.Base_Fix == null)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "BASE FIX",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_WORKINGTIME_BASE_FIX_EMPTY, language)
                        });
                    }
                    else if (!inputValidation.IsNumeric(inputString) || inputModel.organzationWorkingTeamModels.Base_Fix <= 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "BASE FIX",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_WORKINGTIME_VALID_NUMBER, language)
                        });
                    }

                    if (inputModel.organzationWorkingTeamModels.Base_Calendar_Day == null)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "BASE CAL",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_WORKINGTIME_BASE_CALENDAR_EMPTY, language)
                        });
                    }
                    else if (!inputValidation.IsNumeric(inputCalString) || inputModel.organzationWorkingTeamModels.Base_Calendar_Day <= 0 || int.Parse(inputCalString) > 31)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "BASE CAL",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_WORKINGTIME_VALID_NUMBER, language)
                        });
                    }
                }

            }

            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region validation Leave Types
        public static bool ValidationLeaveTypes(Global_Leave_Types inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();

            User_Data USERDATA = new User_Data();
                    APP_CORE.CoreVariable Core = new APP_CORE.CoreVariable();
                    USERDATA = Core.CoreUserLogin();

            try
            {

                if (db.tbl_Leave_Types.Where(p => p.Organization_Id == USERDATA.OrganizationSelected.id && p.Leave_Code == inputModel.leaveTypesModels.Leave_Code).ToList().Count() > 0 && type == GlobalVariable.CONST_CREATE)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "CODE",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_LEAVE_CODE_IS_ALREADY_EXISTS, language)
                    });
                }

                if (UICommonFunction.StringIsNullOrEmpty(inputModel.leaveTypesModels.Leave_Code))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "CODE",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_LEAVE_CODE_IS_EMPTY, language)
                    });
                }

                if (UICommonFunction.StringIsNullOrEmpty(inputModel.leaveTypesModels.Leave_Description))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_LEAVE_DESCRIPTION_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_LEAVE_DESCRIPTION_IS_EMPTY, language)
                    });
                }

            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region validation General Parameter
        public static bool ValidationGeneralParameter(Global_General_Parameter inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {

                if (UICommonFunction.StringIsNullOrEmpty(inputModel.generalParameterModels.Field_Value))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "VALUE",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_VALUE_IS_EMPTY, language)
                    });
                }

                if (UICommonFunction.StringIsNullOrEmpty(inputModel.generalParameterModels.Field_Name))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_FIELD_NAME_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_NAME_IS_EMPTY, language)
                    });
                }

                if (type == GlobalVariable.CONST_CREATE)
                {
                    var ExistValue = db.tbl_General_Parameter.Where(p => p.Table_Name.ToUpper() == inputModel.generalParameterModels.Table_Name.ToUpper()
                                                                      && p.Field_Value.ToUpper() == inputModel.generalParameterModels.Field_Value.ToUpper()
                                                                      && p.Field_Name.ToUpper() == inputModel.generalParameterModels.Field_Value.ToUpper()).ToList();
                    if (ExistValue.Count > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_FIELD_VALUE_AND_NAME_IS_ALREADY_EXISTS,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_VALUE_AND_NAME_IS_ALREADY_EXISTS, language)
                        });
                    }
                }

                if (type == GlobalVariable.CONST_EDIT)
                {
                    var Model = db.tbl_General_Parameter.Find(inputModel.generalParameterModels.id);
                    if (Model.Table_Name.ToUpper() != inputModel.generalParameterModels.Table_Name.ToUpper()
                        || Model.Field_Value.ToUpper() != inputModel.generalParameterModels.Field_Value.ToUpper()
                        || Model.Field_Name.ToUpper() != inputModel.generalParameterModels.Field_Name.ToUpper())
                    {
                        var ExistValue = db.tbl_General_Parameter.Where(p => p.Table_Name == inputModel.generalParameterModels.Table_Name && p.Field_Value == inputModel.generalParameterModels.Field_Value && p.Field_Name == inputModel.generalParameterModels.Field_Name).ToList();
                        if (ExistValue.Count > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_FIELD_VALUE_AND_NAME_IS_ALREADY_EXISTS,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_VALUE_AND_NAME_IS_ALREADY_EXISTS, language)
                            });
                        }
                    }
                }




            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region validation User
        public static bool ValidationUser(Global_User inputModel, string language, string type, Guid OrganizationSelected, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();

            try
            {
                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    var UserExist = GeneralCore.UserQueryValidasi().Where(p => p.Username == inputModel.userModels.Username).ToList();
                    var UserEmail = GeneralCore.UserQueryValidasi().Where(p => p.Email == inputModel.userModels.Email).ToList();
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.userModels.Username))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "USERNAME",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_USERNAME_IS_EMPTY, language)
                        });
                    }

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.userModels.Email))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "EMAIL",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMAIL_IS_EMPTY, language)
                        });
                    }

                    if (UserExist.Count > 0)
                    {
                        //var sumWebUser = UserExist.Where(p => p.Is_Mobile_Phone == false || p.Is_Mobile_Phone == null).Count();
                        //var sumMobileUser = UserExist.Where(p => p.Is_Mobile_Phone == true).Count();

                        if (UserExist.Where(p => p.Is_Mobile_Phone == false || p.Is_Mobile_Phone == null).Count() > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "USERNAME",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_USER_ALREADY_EXISTS, language)
                            });
                        }
                    }

                    if (UserEmail.Count > 0)
                    {
                        if (UserEmail.Where(p => p.Is_Mobile_Phone == false || p.Is_Mobile_Phone == null).Count() > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "EMAIL",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMAIL_ALREADY_EXISTS, language)
                            });
                        }
                    }
                }
                #endregion

                #region Edit
                if (type == GlobalVariable.CONST_EDIT)
                {
                    var UserOtherUsernameExist = GeneralCore.UserQueryValidasi().Where(p => p.id != inputModel.userModels.id && p.Username == inputModel.userModels.Username).Count();
                    var UserOtherEmailExist = GeneralCore.UserQueryValidasi().Where(p => p.id != inputModel.userModels.id && p.Username == inputModel.userModels.Username).Count();
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.userModels.Username))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "USERNAME",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_USERNAME_IS_EMPTY, language)
                        });
                    }

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.userModels.Email))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "EMAIL",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMAIL_IS_EMPTY, language)
                        });
                    }



                    if (inputModel.Exist_Username != inputModel.userModels.Username)
                    {
                        if (UserOtherUsernameExist > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "USERNAME",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_USER_ALREADY_EXISTS, language)
                            });
                        }
                    }

                    if (inputModel.Exist_Username != inputModel.userModels.Email)
                    {
                        if (UserOtherEmailExist > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "EMAIL",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMAIL_ALREADY_EXISTS, language)
                            });
                        }
                    }
                    
                }
                #endregion

                #region non ldap user
                if (inputModel.userModels.LDAP == "Non LDAP")
                {
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.userModels.Password))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "PASSWORD",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PASSWORD_IS_EMPTY, language)
                        });
                    }

                    else if (inputModel.userModels.Password.Length < 8)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "PASSWORD",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PASSWORD_LENGTH, language)
                        });
                    }
                }
                #endregion

                if (inputModel.userModels.Email != null)
                {
                    if (!inputModel.userModels.Email.Contains("@") && !inputModel.userModels.Email.Contains("."))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "EMAIL",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_USER_INVALID_EMAIL, language)
                        });
                    }
                }

                if (inputModel.organization_role_array == null)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_ROLECODE_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ROLECODE_IS_EMPTY, language)
                    });
                }

                if (inputModel.userOrganizationTeamModels.Organization_Team_ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_TEAM_CODE_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TEAM_CODE_IS_EMPTY, language)
                    });
                }

            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region validation Generate Bank File
        public static bool ValidationGenerateBankFile(Global_GenerateBankFile inputModel, string language, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();

            try
            {
                status = true;

                if (inputModel.generateBankFileModels.Total_Amount != Convert.ToDecimal(inputModel.vwGenerateBankFileDetailsSummaryList.Sum(s => s.Employee_Salary_Transferred)))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "TOTAL_AMOUNT_TOTAL_ACCOUNT",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TOTAL_AMOUNT_TOTAL_ACCOUNT, language)
                    });
                }

                //file Tipe mandatory
                if (UICommonFunction.StringIsNullOrEmpty(inputModel.generateBankFileModels.Output_File_Type))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "ORGANIZATION",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_GBF_FILE_TYPE, language)
                    });
                }
            
                  //file Tipe mandatory
                   if (UICommonFunction.StringIsNullOrEmpty(inputModel.generateBankFileModels.Output_File_Type))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "FILEDETAIL",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_GBF_FILE_TYPE, language)
                        });
                    }

                   //File name Mandatory
                   if (UICommonFunction.StringIsNullOrEmpty(inputModel.generateBankFileModels.Output_File_Name))
                   {
                       status = false;
                       errMessage.Add(new Global_Error_Code()
                       {
                           Error_Code = "FILEDETAIL",
                           Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_GBF_FILE_NAME, language)
                       });
                   }

                if (UICommonFunction.StringIsNullOrEmpty(inputModel.strTransferDate))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "strTransferDateVal",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                    });
                }


                //IF Source Account Bank HSBC
                if (inputModel.BankInformationModel.Bank == "HSBC")
                {
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.generateBankFileModels.File_Reference))//File_Reference mandatory
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_GBF_REFERENCE,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_GBF_REFERENCE, language)
                        });
                    }
                     if (UICommonFunction.StringIsNullOrEmpty(inputModel.generateBankFileModels.Transfer_Message))//Transfer_Message mandatory
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_GBF_TRANSFER_MESS,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_GBF_TRANSFER_MESS, language)
                        });
                    }
                }


            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region validation Organization Team
        public static bool ValidationOrganizationTeam(Global_Organization_Team inputModel, string language, string type, Guid orgID, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();

            errMessage = new List<Global_Error_Code>();
            try
            {
                inputModel.organizationTeamList = GeneralCore.OrganizationTeamQuery().Where(s => s.Team_Code == inputModel.organizationTeamModels.Team_Code).ToList();
                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationTeamModels.Team_Code))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "CODE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TEAM_CODE_IS_EMPTY, language)
                        });
                    }

                    else if (inputModel.organizationTeamList.Count > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "CODE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TEAM_CODE_ALREADY_EXISTS, language)
                        });
                    }
                }
                if (type == GlobalVariable.CONST_EDIT)
                {
                    User_Data USERDATA = new User_Data();
                    APP_CORE.CoreVariable Core = new APP_CORE.CoreVariable();
                    USERDATA = Core.CoreUserLogin();

                    var EditExistCodeOrg = GeneralCore.OrganizationTeamQuery().Where(p => p.id != inputModel.organizationTeamModels.id && p.Team_Code == inputModel.organizationTeamModels.Team_Code).Count();

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationTeamModels.Team_Code))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "CODE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TEAM_CODE_IS_EMPTY, language)
                        });
                    }

                    if (EditExistCodeOrg > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "CODE",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TEAM_CODE_ALREADY_EXISTS, language)
                        });
                        
                    }
                }

                if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationTeamModels.Team_Description))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_TEAM_DESCRIPTION_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TEAM_DESCRIPTION_IS_EMPTY, language)
                    });
                }

                if (inputModel.organization_id_array == null)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_CLIENT_ORGANIZATION_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CLIENT_ORGANIZATION_IS_EMPTY, language)
                    });
                }

            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation BankSetting
        public static bool ValidationBankSetting(Global_Bank_Setting inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    //var bankList = db.tbl_List_Of_Bank.Where(s => s.Bank_Code == inputModel.bankSettingModels.Bank_Code || s.BI_Bank_Code == inputModel.bankSettingModels.BI_Bank_Code).ToList();
                    var bankList = db.tbl_List_Of_Bank.Where(s => (s.Bank_Code == inputModel.bankSettingModels.Bank_Code || s.BI_Bank_Code == inputModel.bankSettingModels.BI_Bank_Code) && !(s.Authorize_Status == CoreVariable.CONST_AUTHORIZED && s.Status_Code == CoreVariable.CONST_STATUS_DELETED)).ToList();

                    if (bankList.Count > 0)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Message",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BANK_CODE_AND_BI_BANK_CODE_IS_ALREADY_EXIST, language)
                            });
                        }
                    }

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.bankSettingModels.Bank_Code))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "bankCode",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BANK_CODE_IS_EMPTY, language)
                        });
                    }

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.bankSettingModels.BI_Bank_Code))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "biBankCode",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BI_BANK_CODE_IS_EMPTY, language)
                        });
                    }

                    for (int i = 0; i < inputModel.branchCode.Length; i++)
                    {
                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.branchCode[i]))
                        {
                            if (!UICommonFunction.StringIsNullOrEmpty(inputModel.branchName[i]) || !UICommonFunction.StringIsNullOrEmpty(inputModel.remarks[i]))
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "branchCode",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BRANCH_CODE_IS_EMPTY, language)
                                });
                            }
                        }

                        //string inputBranchCode = inputModel.branchCode[i];
                        //var existData = db.tbl_Branch_List_Of_Bank.Where(p => p.Branch_Code == inputBranchCode).ToList();
                        //if (existData.Count > 0)
                        //{
                        //    status = false;
                        //    errMessage.Add(new Global_Error_Code()
                        //    {
                        //        Error_Code = "branchCodeAlreadyExist",
                        //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BRANCH_CODE_IS_ALREADY_EXIST, language)
                        //    });
                        //}
                    }

                    var validateBranchCodeList = db.tbl_Branch_List_Of_Bank.Where(s => inputModel.branchCode.Contains(s.Branch_Code) && s.List_Of_Bank_ID != inputModel.bankSettingModels.id && !(s.tbl_List_Of_Bank.Authorize_Status == CoreVariable.CONST_AUTHORIZED && s.tbl_List_Of_Bank.Status_Code == CoreVariable.CONST_STATUS_DELETED)).Select(p => p.Branch_Code).ToList();
                    //var validateBranchCodeList = db.tbl_Branch_List_Of_Bank.Where(s => inputModel.branchCode.Contains(s.Branch_Code) && s.List_Of_Bank_ID != inputModel.bankSettingModels.id && !(s.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED)).Select(p => p.Branch_Code).ToList();

                    if (validateBranchCodeList.Count() > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "branchCodeAlreadyExist",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BRANCH_CODE_IS_ALREADY_EXIST, language)
                        });

                    }
                }
                #endregion

                #region Edit
                if (type == GlobalVariable.CONST_EDIT)
                {
                    //var validateCodeBIBankList = db.tbl_List_Of_Bank.Where(s => s.id == inputModel.bankSettingModels.id).ToList();
                    var validateCodeBIBankList = db.tbl_List_Of_Bank.Where(s => s.id == inputModel.bankSettingModels.id && !(s.Authorize_Status == CoreVariable.CONST_AUTHORIZED && s.Status_Code == CoreVariable.CONST_STATUS_DELETED)).ToList();

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.bankSettingModels.Bank_Code))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "bankCode",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BANK_CODE_IS_EMPTY, language)
                        });
                    }

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.bankSettingModels.BI_Bank_Code))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "biBankCode",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BI_BANK_CODE_IS_EMPTY, language)
                        });
                    }

                    if (validateCodeBIBankList.FirstOrDefault().Bank_Code != inputModel.bankSettingModels.Bank_Code && validateCodeBIBankList.FirstOrDefault().BI_Bank_Code != inputModel.bankSettingModels.BI_Bank_Code)
                    {
                        //validateCodeBIBankList = db.tbl_List_Of_Bank.Where(s => s.Bank_Code == inputModel.bankSettingModels.Bank_Code && s.BI_Bank_Code == inputModel.bankSettingModels.BI_Bank_Code).Take(1).ToList();
                        validateCodeBIBankList = db.tbl_List_Of_Bank.Where(s => s.Bank_Code == inputModel.bankSettingModels.Bank_Code && s.BI_Bank_Code == inputModel.bankSettingModels.BI_Bank_Code && !(s.Authorize_Status == CoreVariable.CONST_AUTHORIZED && s.Status_Code == CoreVariable.CONST_STATUS_DELETED)).Take(1).ToList();

                        if (validateCodeBIBankList.Count() > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Message",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HOLIDAY_DATE_ALREADY_EXIST, language)
                            });
                        }
                    }

                    for (int i = 0; i < inputModel.branchCode.Length; i++)
                    {
                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.branchCode[i]))
                        {
                            if (!UICommonFunction.StringIsNullOrEmpty(inputModel.branchName[i]) || !UICommonFunction.StringIsNullOrEmpty(inputModel.remarks[i]))
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "branchCode",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BRANCH_CODE_IS_EMPTY, language)
                                });
                            }
                        }
                    }

                    var validateBranchCodeList = db.tbl_Branch_List_Of_Bank.Where(s => inputModel.branchCode.Contains(s.Branch_Code) && s.List_Of_Bank_ID != inputModel.bankSettingModels.id && !(s.tbl_List_Of_Bank.Authorize_Status == CoreVariable.CONST_AUTHORIZED && s.tbl_List_Of_Bank.Status_Code == CoreVariable.CONST_STATUS_DELETED)).Select(p => p.Branch_Code).ToList();
                    if (validateBranchCodeList.Count() > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Message",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BRANCH_CODE_IS_ALREADY_EXIST, language)
                        });

                    }
                }
                #endregion
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation Exchange Rate
        public static bool ValidationExchangeRate(Global_Exchange_Rate inputModel, Guid selectedOrgID, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                var LengthData = inputModel.Cfrom.Length;

                if (UICommonFunction.StringIsNullOrEmpty(inputModel.strEffectivedate))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_EC_EFFECTIVE_DATE_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EC_EFFECTIVE_DATE_IS_EMPTY, language)
                    });
                }

                #region take Data C_From
                List<string> C_From = new List<string>();
                foreach (var item in inputModel.Cfrom)
                {
                    if (!UICommonFunction.StringIsNullOrEmpty(item))
                    {
                        C_From.Add(item);
                    }
                }
                #endregion
                #region take Data C_To
                List<string> C_To = new List<string>();
                foreach (var item in inputModel.Cto)
                {
                    if (!UICommonFunction.StringIsNullOrEmpty(item))
                    {
                        C_To.Add(item);
                    }
                }
                #endregion
                #region take Data Rate
                List<string> Rate = new List<string>();
                foreach (var item in inputModel.Rate)
                {
                    if (!UICommonFunction.StringIsNullOrEmpty(item))
                    {
                        Rate.Add(item);
                    }
                }
                #endregion

                if (C_From.Count != LengthData)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "TABLE",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EC_C_FROM_IS_EMPTY, language)
                    });
                }

                if (C_To.Count != LengthData)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "TABLE",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EC_C_TO_IS_EMPTY, language)
                    });
                }

                if (Rate.Count != LengthData)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "TABLE",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EC_RATE_IS_EMPTY, language)
                    });
                }

                #region CREATE
                if (type == GlobalVariable.CONST_CREATE)
                {
                    DateTime valEffectivedate = UICommonFunction.ConvertToDateTime(inputModel.strEffectivedate);

                    Get_Payroll_Period_Result Func_period = new Get_Payroll_Period_Result();
                    Func_period = db.Get_Payroll_Period(selectedOrgID).FirstOrDefault();

                    if (valEffectivedate != DateTime.Parse("1/1/0001 12:00:00 AM"))
                    {
                        var existDataExchangeRate = GeneralCore.ExchangeRateQuery().Where(p => p.Effective_Date == valEffectivedate).FirstOrDefault();
                        if (existDataExchangeRate != null)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "valEffectiveDateAlrreadyExists",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EFFECTIVE_DATE_ALREADY_EXISTS, language) // ex idr ke idr ga boleh
                            });
                        }

                        //if (Func_period != null)
                        //{
                        //    if (valEffectivedate < Func_period.Payroll_Period_Start_Date)
                        //    {
                        //        status = false;
                        //        errMessage.Add(new Global_Error_Code()
                        //        {
                        //            Error_Code = "valEffectiveDate",
                        //            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EFFECTIVE_DATE_NOT_VALID, language) // ex idr ke idr ga boleh
                        //        });
                        //    }
                        //}

                        //DateTime dateTimeNow = DateTime.Now;
                        //if (valEffectivedate.DayOfYear < dateTimeNow.DayOfYear)
                        //{
                        //    status = false;
                        //    errMessage.Add(new Global_Error_Code()
                        //    {
                        //        Error_Code = "valEffectiveDateBackDate",
                        //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EFFECTIVE_DATE_NOT_VALID, language) // ex idr ke idr ga boleh
                        //    });
                        //}
                    }

                    for (int i = 0; i < LengthData; i++)
                    {
                        List<tbl_Exchange_Rate> ListExist = new List<tbl_Exchange_Rate>();
                        if (C_From[i] == C_To[i])
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "TABLE",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ECF_ECT_SAME, language) // ex idr ke idr ga boleh
                            });
                        }


                        //for (int j = 0; j < LengthData; j++)
                        //{
                        //    DateTime InputDate = DateTime.ParseExact(inputModel.strEffectivedate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //    List<tbl_Exchange_Rate> DataExist = GeneralCore.ExchangeRateQuery().Where(p => p.Effective_Date.Day == InputDate.Day && p.Effective_Date.Month == InputDate.Month && p.Effective_Date.Year == InputDate.Year).ToList();
                        //    //var DataExist = GeneralCore.ExchangeRateDetailQuery().Where(p => p.tbl_Exchange_Rate.Effective_Date.Day == InputDate.Day && p.tbl_Exchange_Rate.Effective_Date.Month == InputDate.Month && p.Effective_Date.Year == InputDate.Year).ToList();

                        //    var DataExistr = DataExist.Where(p => p.Currency_From == inputModel.Cfrom[j] && p.Currency_To == inputModel.Cto[j]).FirstOrDefault();

                        //    if (DataExistr != null)
                        //    {
                        //        if (DataExistr.Currency_From == inputModel.Cfrom[j] && DataExistr.Currency_To == inputModel.Cto[j])
                        //        {
                        //            status = false;
                        //            errMessage.Add(new Global_Error_Code()
                        //            {
                        //                Error_Code = "TABLE",
                        //                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EC_IS_ALREADY_EXIST, language) // data dari cfrom dan cto berdasarkan tanggal  yang sama ga boleh
                        //            });
                        //        }
                        //    }

                        //}
                    }
                }
                #endregion

            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }

        #endregion

        #region Validation Organization Group
        public static bool ValidationOrganizationGroup(Global_Organization_Group inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            User_Data USERDATA = new User_Data();
            APP_CORE.CoreVariable Core = new APP_CORE.CoreVariable();
            USERDATA = Core.CoreUserLogin();
            try
            {
                var LengthData = inputModel.M_Organization_Client_ID.Length;

                #region Add Data List
                List<tbl_Organization_Group_Detail> ListDetail = new List<tbl_Organization_Group_Detail>();
                for (int i = 0; i < LengthData; i++)
                {
                    tbl_Organization_Group_Detail ModelDetail = new tbl_Organization_Group_Detail();
                    ModelDetail.id = Guid.NewGuid();
                    if (inputModel.M_Organization_Client_ID[i] != "")
                    {
                        ModelDetail.Organization_Client_ID = Guid.Parse(inputModel.M_Organization_Client_ID[i]);
                    }
                    ModelDetail.Relationship_Type = inputModel.M_Relationship_Type[i];
                    ListDetail.Add(ModelDetail);
                }
                #endregion

                List<tbl_Organization_Group> ListExistDetail = new List<tbl_Organization_Group>();
                var Holding = db.tbl_SysParam.Where(p => p.Param_Code == "GRP_HOLDING").FirstOrDefault().Value;

                List<string> GroupDetail = db.tbl_Organization_Group.Where(p => p.Authorize_Status != GlobalVariable.CONST_AUTHORIZED && p.Status_Code !=GlobalVariable.CONST_STATUS_DELETED).Select(p=>p.id.ToString()).ToList();
                var detailexistholding = db.tbl_Organization_Group_Detail.Where(p => GroupDetail.Contains(p.Organization_Group_ID.ToString()) && p.Relationship_Type == Holding).ToList();

                if (LengthData <= 1)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_ORG_GROUP_MIN_ORGANIZQATION,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORG_GROUP_MIN_ORGANIZQATION, language)
                    });
                }

                    foreach (var item in ListDetail)
                    {
                        if (item.Relationship_Type == "") // relationship ga di pilih 
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_ORG_GROUP_RELATIONSHIP_EMPTY,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORG_GROUP_RELATIONSHIP_EMPTY, language)
                            });
                        }
                        if (item.Organization_Client_ID == null) // organization client ga di pilih 
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_ORG_GROUP_ORGANIZATION_EMPTY,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORG_GROUP_ORGANIZATION_EMPTY, language)
                            });
                        }

                        if (item.Organization_Client_ID != null && type == GlobalVariable.CONST_CREATE) // check already holding
                        {
                            var ChkHolding = detailexistholding.Where(p => p.Organization_Client_ID == item.Organization_Client_ID && p.Relationship_Type == item.Relationship_Type).FirstOrDefault();
                            if (ChkHolding != null)
                            {
                                var organizationName = db.tbl_Organization.Where(p => p.id == item.Organization_Client_ID).FirstOrDefault().Organization_Name;
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.CONST_ERR_ORG_GROUP_ALREADY_HOLDING,
                                    Error_Description = organizationName + " " + UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORG_GROUP_ALREADY_HOLDING, language)
                                });
                            }
                        }

                    }
                

                var HoldingCount = ListDetail.Where(a=>a.Relationship_Type == "HOLDING").ToList().Count();
                if (HoldingCount > 1) // Gaboleh ada 2 holding di list Detail
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_ORG_GROUP_HOLDING_MORE_ONE,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORG_GROUP_HOLDING_MORE_ONE, language)
                    });
                }

            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }

        #endregion

        #region Validation Personal Information
        public static bool ValidationPersonalInformation(Global_Employee_Personal_Info inputModel, string language, string type, out List<Global_Error_Code> errMessage, Guid OrganizationID)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.employeeModel.Employee_No))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_EMPLOYEE_ID_NUMBER_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMPLOYEE_ID_NUMBER_IS_EMPTY, language)
                        });
                    }

                    var dbListEmployee = db.tbl_Employee.ToList();
                    var EmployeeIDNum = inputModel.employeeModel.Employee_No;
                    var ExistData = dbListEmployee.Where(p => p.Employee_No == EmployeeIDNum && p.Organization_ID == OrganizationID && p.Status_Code==1 && p.Authorize_Status==1).ToList();

                    if (ExistData.Count > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_EMPLOYEE_ID_NUMBER_ALREADY_EXIST,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMPLOYEE_ID_NUMBER_ALREADY_EXIST, language)
                        });
                    }

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.employeeModel.Full_Name))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_FULL_NAME_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FULL_NAME_IS_EMPTY, language)
                        });
                    }

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.employeeModel.Gender) || inputModel.employeeModel.Gender.ToUpper() == "SELECT")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_GENDER_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_GENDER_IS_EMPTY, language)
                        });
                    }

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.employeeModel.Nationality) || inputModel.employeeModel.Nationality.ToUpper() == "SELECT")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_NATIONALITY_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_NATIONALITY_IS_EMPTY, language)
                        });
                    }

                    //int LastDate = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

                    if (inputModel.idNumber != null)
                    {
                        for (int i = 0; i < inputModel.idNumber.Length; i++)
                        {
                            //if (!UICommonFunction.StringIsNullOrEmpty(inputModel.idNumber[i]) && UICommonFunction.StringIsNullOrEmpty(inputModel.idType[i]))
                            //{
                            //    status = false;
                            //    errMessage.Add(new Global_Error_Code()
                            //    {
                            //        Error_Code = GlobalVariable.CONST_ERR_IDTYPE_IS_EMPTY,
                            //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_IDTYPE_IS_EMPTY, language)
                            //    });
                            //}

                            if (!UICommonFunction.StringIsNullOrEmpty(inputModel.idType[i]) && UICommonFunction.StringIsNullOrEmpty(inputModel.idNumber[i]))
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.CONST_ERR_IDNUMBER_IS_EMPTY,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_IDNUMBER_IS_EMPTY, language)
                                });
                            }
                        }
                    }

                    #region Validate Duplicate ID Type
                    //HashSet<string> hsidType = new HashSet<string>();
                    var FingerID = db.tbl_Employee_ID_Information.Where(p => p.ID_Type == "FINGERID" && p.Organization_ID == OrganizationID).ToList();//Select(i => new { i.ID_Type, i.ID_Number }).ToList();
                    for(int i = 0; i < inputModel.idType.Length; i++ )
                    {
                        if (Array.FindAll(inputModel.idType, p => p.Equals(inputModel.idType[i])).Count() > 1)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_DUPLICATE_IDTYPE,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DUPLICATE_IDTYPE, language)
                            });
                            break;
                        }
                        if(inputModel.idType[i] == "FINGERID" && FingerID.Where(p => p.ID_Type == inputModel.idType[i] && p.ID_Number == inputModel.idNumber[i]).Count() > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_DUPLICATE_IDTYPE,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DUPLICATE_IDTYPE, language)
                            });
                            break;
                        }
                        
                    }
                    //foreach (var item in inputModel.idType)
                    //{
                    //    if (Array.FindAll(inputModel.idType, p => p.Equals(item)).Count() > 1)
                    //    {
                    //        status = false;
                    //        errMessage.Add(new Global_Error_Code()
                    //        {
                    //            Error_Code = GlobalVariable.CONST_ERR_DUPLICATE_IDTYPE,
                    //            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DUPLICATE_IDTYPE, language)
                    //        });
                    //        break;
                    //    }
                    //}
                    #endregion

                    #region Validate Duplicate Relationship
                    //HashSet<string> hsidType = new HashSet<string>();
                    foreach (var item in inputModel.relationship)
                    {
                        if (Array.FindAll(inputModel.relationship, p => p.Equals(item)).Count() > 1)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_DUPLICATE_RELATIONSHIP,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DUPLICATE_RELATIONSHIP, language)
                            });
                            break;
                        }
                    }
                    #endregion

                    if (inputModel.employeeModel.Nationality.ToUpper().Contains(GlobalVariable.CONST_ID_TYPE_NATIONALITY_INDONESIA))
                    {
                        if (!inputModel.idType.Contains(GlobalVariable.CONST_ID_TYPE_KTP))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_IDTYPE_KTP_IS_MANDATORY,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_IDTYPE_KTP_IS_MANDATORY, language)
                            });
                        }
                    }
                    else if (!inputModel.employeeModel.Nationality.ToUpper().Contains(GlobalVariable.CONST_ID_TYPE_NATIONALITY_INDONESIA))
                    {
                        if (!inputModel.idType.Contains(GlobalVariable.CONST_ID_TYPE_PASSPORT) && !inputModel.idType.Contains(GlobalVariable.CONST_ID_TYPE_KITAS))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_IDTYPE_PASSPORT_OR_KITAS_IS_MANDATORY,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_IDTYPE_PASSPORT_OR_KITAS_IS_MANDATORY, language)
                            });
                        }
                    }

                    if (inputModel.idNumber != null)
                    {
                        for (int i = 0; i < inputModel.idNumber.Length; i++)
                        {
                            if (inputModel.employeeModel.Nationality.ToUpper().Contains(GlobalVariable.CONST_ID_TYPE_NATIONALITY_INDONESIA))
                            {
                                if (UICommonFunction.StringIsNullOrEmpty(inputModel.idType[i]))
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = GlobalVariable.CONST_ERR_IDTYPE_IS_EMPTY,
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_IDTYPE_IS_EMPTY, language)
                                    });
                                }
                            }
                            else if (!inputModel.employeeModel.Nationality.ToUpper().Contains(GlobalVariable.CONST_ID_TYPE_NATIONALITY_INDONESIA))
                            {
                                if (UICommonFunction.StringIsNullOrEmpty(inputModel.idType[i]))
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = GlobalVariable.CONST_ERR_IDTYPE_IS_EMPTY,
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_IDTYPE_IS_EMPTY, language)
                                    });
                                }

                            }
                        }
                    }
                }
                #endregion

                #region Edit
                if (type == GlobalVariable.CONST_EDIT)
                {
                    var dbListEmployee = db.tbl_Employee.ToList();
                    var oldEmployeeNo = inputModel.oldEmployeeNo;
                    var EmployeeIDNum = inputModel.employeeModel.Employee_No;
                    if (oldEmployeeNo != EmployeeIDNum)
                    {
                        var ExistData = dbListEmployee.Where(p => (p.Employee_No == EmployeeIDNum && p.Organization_ID == OrganizationID) && p.Status_Code == 1 && p.Authorize_Status == 1).ToList();

                        if (ExistData.Count > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_EMPLOYEE_ID_NUMBER_ALREADY_EXIST,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMPLOYEE_ID_NUMBER_ALREADY_EXIST, language)
                            });
                        }
                    }
                    
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.employeeModel.Employee_No))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_EMPLOYEE_ID_NUMBER_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMPLOYEE_ID_NUMBER_IS_EMPTY, language)
                        });
                    }

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.employeeModel.Full_Name))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_FULL_NAME_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FULL_NAME_IS_EMPTY, language)
                        });
                    }

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.employeeModel.Gender) || inputModel.employeeModel.Gender.ToUpper() == "SELECT")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_GENDER_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_GENDER_IS_EMPTY, language)
                        });
                    }

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.employeeModel.Nationality) || inputModel.employeeModel.Nationality.ToUpper() == "SELECT")
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_NATIONALITY_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_NATIONALITY_IS_EMPTY, language)
                        });
                    }

                    if (inputModel.idNumber != null)
                    {
                        for (int i = 0; i < inputModel.idNumber.Length; i++)
                        {
                            if (!UICommonFunction.StringIsNullOrEmpty(inputModel.idNumber[i]) && UICommonFunction.StringIsNullOrEmpty(inputModel.idType[i]))
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.CONST_ERR_IDTYPE_IS_EMPTY,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_IDTYPE_IS_EMPTY, language)
                                });
                            }

                            if (!UICommonFunction.StringIsNullOrEmpty(inputModel.idType[i]) && UICommonFunction.StringIsNullOrEmpty(inputModel.idNumber[i]))
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.CONST_ERR_IDNUMBER_IS_EMPTY,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_IDNUMBER_IS_EMPTY, language)
                                });
                            }
                        }
                    }

                    #region Validate Duplicate ID Type
                    //HashSet<string> hsidType = new HashSet<string>();
                    var FingerID = db.tbl_Employee_ID_Information.Where(p => p.ID_Type == "FINGERID" && p.Organization_ID == OrganizationID).ToList();
                    for (int i = 0; i < inputModel.idType.Length; i++)
                    {
                        if (Array.FindAll(inputModel.idType, p => p.Equals(inputModel.idType[i])).Count() > 1)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_DUPLICATE_IDTYPE,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DUPLICATE_IDTYPE, language)
                            });
                            break;
                        }

                        var ExistingData = FingerID.Where(p => p.Employee_ID != inputModel.employeeModel.id && p.ID_Number == inputModel.idNumber[i]).ToList();
                        if (inputModel.idType[i] == "FINGERID")
                        {
                            if(ExistingData.Count() > 0)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.CONST_ERR_DUPLICATE_FINGER_ID,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DUPLICATE_FINGER_ID, language)
                                });
                                break;
                            }
                        }
                    }
                    //foreach (var item in inputModel.idType)
                    //{
                    //    if (Array.FindAll(inputModel.idType, p => p.Equals(item)).Count() > 1)
                    //    {
                    //        status = false;
                    //        errMessage.Add(new Global_Error_Code()
                    //        {
                    //            Error_Code = GlobalVariable.CONST_ERR_DUPLICATE_IDTYPE,
                    //            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DUPLICATE_IDTYPE, language)
                    //        });
                    //        break;
                    //    }
                    //}
                    #endregion

                    #region Validate Duplicate Relationship
                    //HashSet<string> hsidType = new HashSet<string>();
                    foreach (var item in inputModel.relationship)
                    {
                        if (Array.FindAll(inputModel.relationship, p => p.Equals(item)).Count() > 1)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_DUPLICATE_RELATIONSHIP,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DUPLICATE_RELATIONSHIP, language)
                            });
                            break;
                        }
                    }
                    #endregion

                    if (inputModel.employeeModel.Nationality.ToUpper().Contains(GlobalVariable.CONST_ID_TYPE_NATIONALITY_INDONESIA))
                    {
                        if (!inputModel.idType.Contains(GlobalVariable.CONST_ID_TYPE_KTP))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_IDTYPE_KTP_IS_MANDATORY,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_IDTYPE_KTP_IS_MANDATORY, language)
                            });
                        }
                    }
                    else if (!inputModel.employeeModel.Nationality.ToUpper().Contains(GlobalVariable.CONST_ID_TYPE_NATIONALITY_INDONESIA))
                    {
                        if (!inputModel.idType.Contains(GlobalVariable.CONST_ID_TYPE_PASSPORT) && !inputModel.idType.Contains(GlobalVariable.CONST_ID_TYPE_KITAS))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_IDTYPE_PASSPORT_OR_KITAS_IS_MANDATORY,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_IDTYPE_PASSPORT_OR_KITAS_IS_MANDATORY, language)
                            });
                        }
                    }

                    if (inputModel.idNumber != null)
                    {
                        for (int i = 0; i < inputModel.idNumber.Length; i++)
                        {
                            if (inputModel.employeeModel.Nationality.ToUpper().Contains(GlobalVariable.CONST_ID_TYPE_NATIONALITY_INDONESIA))
                            {
                                if (UICommonFunction.StringIsNullOrEmpty(inputModel.idType[i]))
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = GlobalVariable.CONST_ERR_IDTYPE_IS_EMPTY,
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_IDTYPE_IS_EMPTY, language)
                                    });
                                }
                            }
                            else if (!inputModel.employeeModel.Nationality.ToUpper().Contains(GlobalVariable.CONST_ID_TYPE_NATIONALITY_INDONESIA))
                            {
                                if (UICommonFunction.StringIsNullOrEmpty(inputModel.idType[i]))
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = GlobalVariable.CONST_ERR_IDTYPE_IS_EMPTY,
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_IDTYPE_IS_EMPTY, language)
                                    });
                                }

                            }
                        }
                    }
                }
                #endregion
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }

        #endregion

        #region Validation Appointment Information
        public static bool ValidationAppointmentInformation(Global_Employee_Appointment inputModel, string language, string type, out List<Global_Error_Code> errMessage, Guid EmployeeID, Guid OrganizationID)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                if (inputModel.CheckedTaxPeriod == false)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_CHEECK_TAX_PERIOD,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_CHEECK_TAX_PERIOD, language)
                    });
                }
                else
                {
                    #region Create
                    if (type == GlobalVariable.CONST_CREATE)
                    {
                        var RecordData = GeneralCore.AppointmentInformationStatusQuery(EmployeeID).Where(p => p.tbl_Employee_Appointment.Employee_ID == EmployeeID).FirstOrDefault();
                        if (RecordData != null)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_1_EMPLOYEE_ONLY_HAVE_1_RECORD_DATA,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_1_EMPLOYEE_ONLY_HAVE_1_RECORD_DATA, language)
                            });
                        }

                        #region Validate Date_Of_Hire Empty
                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.strDate_Of_Hire))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_DATE_OF_HIRE_IS_EMPTY,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DATE_OF_HIRE_IS_EMPTY, language)
                            });
                        }
                        #endregion

                        #region Validate Join_Date Empty
                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.strJoin_Date))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_JOIN_DATE_IS_EMPTY,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_JOIN_DATE_IS_EMPTY, language)
                            });
                        }
                        #endregion

                        #region Validate ERR_JOIN_DATE_IS_NOT_VALID
                        if (UICommonFunction.ConvertToDateTime(inputModel.strJoin_Date) > UICommonFunction.ConvertToDateTime(inputModel.strDate_Of_Hire))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_JOIN_DATE_IS_NOT_VALID,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_JOIN_DATE_IS_NOT_VALID, language)
                            });
                        }
                        #endregion

                        #region Validate Status Information
                        if (inputModel.EmploymentStatus == null && inputModel.EmployeeStatus == null)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_EMPLOYMENT_STATUS_IS_EMPTY,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMPLOYMENT_STATUS_IS_EMPTY, language)
                            });
                        }
                        else
                        {
                            if (inputModel.WorkLocation.Count() == 1 && inputModel.EmploymentStatus[0] == "" && inputModel.EmployeeStatus[0] == "" && inputModel.effectiveDateStatusInfo[0] == "")
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_STATUS_INFORMATION_IS_EMPTY,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_STATUS_INFORMATION_IS_EMPTY, language)
                                });
                            }
                            else
                            {
                                HashSet<string> hseffectiveDateStatusInfo = new HashSet<string>();
                                bool ErrorEmployementStatusEmpty = false;
                                bool ErrorEmployementStatusNotHaveTaxID = false;
                                bool ErrorEmployeeStatusEmpty = false;
                                bool ErrorEffectiveDateEmpty = false;
                                bool ErrorEffectiveDateNotValid = false;
                                bool ErrorEffectiveDateCannotBeSame = false;
                                bool ErrorEndDateOfProbationNotValid = false;

                                for (int i = 0; i < inputModel.EmploymentStatus.Length; i++)
                                {
                                    if (inputModel.EmploymentStatus[i] == "" && inputModel.EmployeeStatus[i] == "" && inputModel.effectiveDateStatusInfo[i] == "")
                                    {
                                    }
                                    else
                                    {
                                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.EmploymentStatus[i]) && ErrorEmployementStatusEmpty == false)
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_EMPLOYMENT_STATUS_IS_EMPTY,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMPLOYMENT_STATUS_IS_EMPTY, language)
                                            });
                                            ErrorEmployementStatusEmpty = true;
                                        }
                                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.EmployeeStatus[i]) && ErrorEmployeeStatusEmpty == false)
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_EMPLOYEE_STATUS_IS_EMPTY,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMPLOYEE_STATUS_IS_EMPTY, language)
                                            });
                                            ErrorEmployeeStatusEmpty = true;
                                        }
                                        if (!inputModel.EmploymentStatus[i].Contains(GlobalVariable.CONST_PERMANENT) || !inputModel.EmploymentStatus[i].Contains(GlobalVariable.CONST_CONTRACT) || !inputModel.EmploymentStatus[i].Contains(GlobalVariable.CONST_PROBATION) || inputModel.EmploymentStatus[i].Contains(GlobalVariable.CONST_PERMANENT_EXPATRIATE_PERMANENT) || UICommonFunction.StringIsNullOrEmpty(inputModel.EmploymentStatus[i]))
                                        {
                                            if (UICommonFunction.StringIsNullOrEmpty(inputModel.effectiveDateStatusInfo[i]) && ErrorEffectiveDateEmpty == false)
                                            {
                                                status = false;
                                                errMessage.Add(new Global_Error_Code()
                                                {
                                                    Error_Code = GlobalVariable.CONST_ERR_STATUS_INFO_EFF_DATE_IS_EMPTY,
                                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_STATUS_INFO_EFF_DATE_IS_EMPTY, language)
                                                });
                                                ErrorEffectiveDateEmpty = true;
                                            }
                                        }

                                        var TaxNo = "";
                                        var Tax = db.tbl_Tax.Where(p => p.Employee_ID == EmployeeID && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED)).FirstOrDefault();
                                        if (Tax != null)
                                            TaxNo = Tax.Tax_No;

                                        if (inputModel.EmploymentStatus[i].Contains(GlobalVariable.CONST_NON_PERMANENT_WAJIB_PAJAK_LUAR_NEGERI) && !UICommonFunction.StringIsNullOrEmpty(TaxNo) && inputModel.EmployeeStatus[i].Contains(GlobalVariable.CONST_EMPLOYEE_STATUS_ACTIVE) && ErrorEmployementStatusNotHaveTaxID == false)
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_EMPLOYMENT_STATUS_CANNOT_HAVE_TAXID,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMPLOYMENT_STATUS_CANNOT_HAVE_TAXID, language)
                                            });
                                            ErrorEmployementStatusNotHaveTaxID = true;
                                        }

                                        Guid TaxID = new Guid();
                                        if (Tax != null)
                                            TaxID = Tax.id;

                                        var TaxStatusYear = db.tbl_Tax_Status_Effective_Year.Where(p => p.Tax_ID == TaxID).FirstOrDefault();
                                        var TaxStatus = "";
                                        if (TaxStatusYear != null)
                                            TaxStatus = TaxStatusYear.Tax_Status;

                                        if (!String.IsNullOrEmpty(inputModel.effectiveDateStatusInfo[i]) && !String.IsNullOrEmpty(inputModel.strDate_Of_Hire))
                                        {
                                            if (ErrorEffectiveDateNotValid == false)
                                            {
                                                if (UICommonFunction.ConvertToDateTime(inputModel.effectiveDateStatusInfo[i]) < UICommonFunction.ConvertToDateTime(inputModel.strDate_Of_Hire))
                                                {
                                                    status = false;
                                                    errMessage.Add(new Global_Error_Code()
                                                    {
                                                        Error_Code = GlobalVariable.CONST_ERR_STAT_INFO_EFF_DATE_IS_NOT_VALID,
                                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_STAT_INFO_EFF_DATE_IS_NOT_VALID, language)
                                                    });
                                                    ErrorEffectiveDateNotValid = true;
                                                }
                                            }
                                        }

                                        if (ErrorEffectiveDateCannotBeSame == false)
                                        {
                                            if (!hseffectiveDateStatusInfo.Add(inputModel.effectiveDateStatusInfo[i].ToString()))
                                            {
                                                status = false;
                                                errMessage.Add(new Global_Error_Code()
                                                {
                                                    Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_STATUS_INFORMATION_EFF_DATE_CANNOT_BE_SAME,
                                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_STATUS_INFORMATION_EFF_DATE_CANNOT_BE_SAME, language)
                                                });
                                                ErrorEffectiveDateCannotBeSame = true;
                                            }
                                        }

                                        if (!String.IsNullOrEmpty(inputModel.strEnd_Date_of_Probation) && !String.IsNullOrEmpty(inputModel.strDate_Of_Hire))
                                        {
                                            if (ErrorEndDateOfProbationNotValid == false)
                                            {
                                                //if (UICommonFunction.ConvertToDateTime(inputModel.strEnd_Date_of_Probation) < UICommonFunction.ConvertToDateTime(inputModel.effectiveDateStatusInfo[i]) && (inputModel.EmploymentStatus[i] == GlobalVariable.CONST_PROBATION))
                                                if (UICommonFunction.ConvertToDateTime(inputModel.strEnd_Date_of_Probation) < UICommonFunction.ConvertToDateTime(inputModel.strDate_Of_Hire))
                                                {
                                                    status = false;
                                                    errMessage.Add(new Global_Error_Code()
                                                    {
                                                        Error_Code = GlobalVariable.CONST_ERR_END_DATE_OF_PROBATION_IS_NOT_VALID,
                                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_END_DATE_OF_PROBATION_IS_NOT_VALID, language)
                                                    });
                                                    ErrorEndDateOfProbationNotValid = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        #endregion

                        #region Validate Status Info Effective Date Overlap
                        //if (inputModel.effectiveDateStatusInfo != null)
                        //{
                        //    HashSet<string> hseffectiveDateStatusInfo = new HashSet<string>();
                        //    foreach (var item in inputModel.effectiveDateStatusInfo)
                        //    {
                        //        if (!hseffectiveDateStatusInfo.Add(item.ToString()))
                        //        {
                        //            status = false;
                        //            errMessage.Add(new Global_Error_Code()
                        //            {
                        //                Error_Code = GlobalVariable.CONST_ERR_STAT_INFO_EFF_DATE_IS_NOT_VALID,
                        //                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_STAT_INFO_EFF_DATE_IS_NOT_VALID, language)
                        //            });
                        //        }
                        //    }
                        //}
                        #endregion

                        #region Validate Work Location
                        if (inputModel.WorkLocation == null || inputModel.effectiveDateWorkLocation != null)
                        {
                            if (inputModel.WorkLocation.Count() == 1 && inputModel.WorkLocation[0] == "" && inputModel.effectiveDateWorkLocation[0] == "")
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY, language)
                                });
                            }
                            else
                            {
                                HashSet<string> hseffectiveDateWorkLocation = new HashSet<string>();
                                bool ErrorWorkLocationCode = false;
                                bool ErrorEffectiveWorkLocation = false;
                                bool ErrorEffectiveWorkLocation_Not_Valid = false;
                                bool ErrorEffectiveWorkLocation_Cannot_Be_Same = false;

                                for (int j = 0; j < inputModel.WorkLocation.Length; j++)
                                {
                                    if (inputModel.WorkLocation[j] == "" && inputModel.effectiveDateWorkLocation[j] == "")
                                    {
                                    }
                                    else
                                    {
                                        if (inputModel.WorkLocation[j] == "" && ErrorWorkLocationCode == false)
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY, language)
                                            });
                                            ErrorWorkLocationCode = true;
                                        }
                                        if (inputModel.effectiveDateWorkLocation[j] == "" && ErrorEffectiveWorkLocation == false)
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_WORK_LOCATION_EFF_DATE_IS_EMPTY,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORK_LOCATION_EFF_DATE_IS_EMPTY, language)
                                            });
                                            ErrorEffectiveWorkLocation = true;
                                        }
                                    }

                                    if (!String.IsNullOrEmpty(inputModel.WorkLocation[j]) && !String.IsNullOrEmpty(inputModel.effectiveDateWorkLocation[j]))
                                    {
                                        if (ErrorEffectiveWorkLocation_Not_Valid == false)
                                        {
                                            if (UICommonFunction.ConvertToDateTime(inputModel.effectiveDateWorkLocation[j]) < UICommonFunction.ConvertToDateTime(inputModel.strDate_Of_Hire))
                                            {
                                                status = false;
                                                errMessage.Add(new Global_Error_Code()
                                                {
                                                    Error_Code = GlobalVariable.CONST_ERR_WORK_LOCATION_EFF_DATE_IS_NOT_VALID,
                                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORK_LOCATION_EFF_DATE_IS_NOT_VALID, language)
                                                });
                                                ErrorEffectiveWorkLocation_Not_Valid = true;
                                            }
                                        }
                                        if (ErrorEffectiveWorkLocation_Cannot_Be_Same == false)
                                        {
                                            if (!hseffectiveDateWorkLocation.Add(inputModel.effectiveDateWorkLocation[j].ToString()))
                                            {
                                                status = false;
                                                errMessage.Add(new Global_Error_Code()
                                                {
                                                    Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_WORK_LOCATION_EFF_DATE_CANNOT_BE_SAME,
                                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_WORK_LOCATION_EFF_DATE_CANNOT_BE_SAME, language)
                                                });
                                                ErrorEffectiveWorkLocation_Cannot_Be_Same = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY, language)
                            });
                        }
                        //if (inputModel.WorkLocation == null)
                        //{
                        //    status = false;
                        //    errMessage.Add(new Global_Error_Code()
                        //    {
                        //        Error_Code = GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY,
                        //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY, language)
                        //    });
                        //}
                        //else
                        //{
                        //    if (inputModel.EmploymentStatus == null)
                        //    {
                        //        status = false;
                        //        errMessage.Add(new Global_Error_Code()
                        //        {
                        //            Error_Code = GlobalVariable.CONST_ERR_EMPLOYMENT_STATUS_IS_EMPTY,
                        //            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMPLOYMENT_STATUS_IS_EMPTY, language)
                        //        });
                        //    }
                        //    else
                        //    {
                        //        for (int i = 0; i < inputModel.EmploymentStatus.Length; i++)
                        //        {
                        //            if (!UICommonFunction.StringIsNullOrEmpty(inputModel.EmploymentStatus[i]))
                        //            {
                        //                for (int j = 0; j < inputModel.WorkLocation.Length; j++)
                        //                {
                        //                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.WorkLocation[j]))
                        //                    {
                        //                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.WorkLocation[j]) || UICommonFunction.StringIsNullOrEmpty(inputModel.effectiveDateWorkLocation[j]))
                        //                        {
                        //                            status = false;
                        //                            errMessage.Add(new Global_Error_Code()
                        //                            {
                        //                                Error_Code = GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY,
                        //                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY, language)
                        //                            });
                        //                        }
                        //                    }
                        //                    if (UICommonFunction.ConvertToDateTime(inputModel.effectiveDateWorkLocation[j]) < UICommonFunction.ConvertToDateTime(inputModel.strDate_Of_Hire))
                        //                    {
                        //                        status = false;
                        //                        errMessage.Add(new Global_Error_Code()
                        //                        {
                        //                            Error_Code = "ERR_WORK_LOCATION_EFF_DATE",
                        //                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORK_LOCATION_EFF_DATE_IS_NOT_VALID, language)
                        //                        });
                        //                    }
                        //                }
                        //            }
                        //            else
                        //            {
                        //                for (int j = 0; j < inputModel.WorkLocation.Length; j++)
                        //                {
                        //                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.WorkLocation[j]))
                        //                    {
                        //                        status = false;
                        //                        errMessage.Add(new Global_Error_Code()
                        //                        {
                        //                            Error_Code = GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY,
                        //                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY, language)
                        //                        });
                        //                    }
                        //                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.effectiveDateWorkLocation[j]))
                        //                    {
                        //                        status = false;
                        //                        errMessage.Add(new Global_Error_Code()
                        //                        {
                        //                            Error_Code = "ERR_WORK_LOCATION_EFF_DATE",
                        //                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORK_LOCATION_EFF_DATE_IS_EMPTY, language)
                        //                        });
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        #endregion

                        #region Validate Work Location Effective Date Overlap
                        //if (inputModel.effectiveDateWorkLocation != null)
                        //{
                        //    HashSet<string> hseffectiveDateWorkLocation = new HashSet<string>();
                        //    foreach (var item in inputModel.effectiveDateWorkLocation)
                        //    {
                        //        if (!hseffectiveDateWorkLocation.Add(item.ToString()))
                        //        {
                        //            status = false;
                        //            errMessage.Add(new Global_Error_Code()
                        //            {
                        //                Error_Code = "ERR_WORK_LOCATION_EFF_DATE",
                        //                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORK_LOCATION_EFF_DATE_IS_NOT_VALID, language)
                        //            });
                        //        }
                        //    }
                        //}
                        #endregion

                        #region Validate ERR_CONTRACT_END_DATE_IS_NOT_VALID
                        if (inputModel.ContractNo != null)
                        {
                            for (int i = 0; i < inputModel.ContractNo.Length; i++)
                            {
                                if (UICommonFunction.ConvertToDateTime(inputModel.ContractStartDate[i]) > UICommonFunction.ConvertToDateTime(inputModel.ContractEndDate[i]))
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = GlobalVariable.CONST_ERR_CONTRACT_END_DATE_IS_NOT_VALID,
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CONTRACT_END_DATE_IS_NOT_VALID, language)
                                    });
                                }
                            }
                        }
                        #endregion

                        #region Validate ERR_CONTRACT_DATE_IS_NOT_VALID
                        if (inputModel.ContractStartDate != null)
                        {
                            for (int i = 0; i < inputModel.ContractStartDate.Length; i++)
                            {
                                if (inputModel.effectiveDateStatusInfo != null)
                                {
                                    for (int j = 0; j < inputModel.effectiveDateStatusInfo.Length; j++)
                                    {
                                        if (UICommonFunction.ConvertToDateTime(inputModel.ContractStartDate[i]) < UICommonFunction.ConvertToDateTime(inputModel.effectiveDateStatusInfo[j]) && (inputModel.EmploymentStatus[j] == GlobalVariable.CONST_CONTRACT) && (inputModel.EmployeeStatus[j] == GlobalVariable.CONST_EMPLOYEE_STATUS_ACTIVE))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_CONTRACT_DATE_IS_NOT_VALID,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CONTRACT_DATE_IS_NOT_VALID, language)
                                            });
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Validate Working Time
                        if (inputModel.WorkingTimeCode != null || inputModel.StartDateWorkingTime != null || inputModel.EndDateWorkingTime != null)
                        {
                            if (inputModel.WorkingTimeCode.Count() == 1 && inputModel.WorkingTimeCode[0] == "" && inputModel.StartDateWorkingTime[0] == "" && inputModel.EndDateWorkingTime[0] == "")
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.CONST_ERR_WORKING_TIME_IS_EMPTY,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORKING_TIME_IS_EMPTY, language)
                                });
                            }
                            else
                            {
                                List<tbl_Appointment_Working_Time> WorkingTime = new List<tbl_Appointment_Working_Time>();
                                HashSet<string> hsStartDateWorkingTime = new HashSet<string>();
                                HashSet<string> hsEndDateWorkingTime = new HashSet<string>();
                                bool ErrorWorkingTimeCode = false;
                                bool ErrorStartDateEmpty = false;
                                bool ErrorEndDateEmpty = false;
                                bool ErrorStartDateNotValid = false;
                                bool ErrorEndDateNotValid = false;
                                bool ErrorStartDateCannotBeSame = false;
                                bool ErrorEndDateCannotBeSame = false;


                                for (int j = 0; j < inputModel.WorkingTimeCode.Length; j++)
                                {
                                    if (!UICommonFunction.StringIsNullOrEmpty(inputModel.WorkingTimeCode[j]) && !String.IsNullOrEmpty(inputModel.StartDateWorkingTime[j]) && !String.IsNullOrEmpty(inputModel.EndDateWorkingTime[j]))
                                    {
                                        tbl_Appointment_Working_Time tbl_Appointment_Working_Time = new APP_MODEL.ModelData.tbl_Appointment_Working_Time();
                                        tbl_Appointment_Working_Time.Working_Time_Code = inputModel.WorkingTimeCode[j];
                                        tbl_Appointment_Working_Time.Start_Date = UICommonFunction.ConvertToDateTime(inputModel.StartDateWorkingTime[j]);
                                        tbl_Appointment_Working_Time.End_Date = UICommonFunction.ConvertToDateTime(inputModel.EndDateWorkingTime[j]);
                                        WorkingTime.Add(tbl_Appointment_Working_Time);
                                    }

                                    if (inputModel.WorkingTimeCode[j] == "" && inputModel.StartDateWorkingTime[j] == "" && inputModel.EndDateWorkingTime[j] == "")
                                    {
                                    }
                                    else
                                    {
                                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.WorkingTimeCode[j]) && ErrorWorkingTimeCode == false)
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_WORKING_TIME_IS_EMPTY,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORKING_TIME_IS_EMPTY, language)
                                            });
                                            ErrorWorkingTimeCode = true;
                                        }
                                        if (inputModel.StartDateWorkingTime[j] == "" && ErrorStartDateEmpty == false)
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_STAT_INFO_START_DATE_IS_EMPTY,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_STAT_INFO_START_DATE_IS_EMPTY, language)
                                            });
                                            ErrorStartDateEmpty = true;
                                        }
                                        if (inputModel.EndDateWorkingTime[j] == "" && ErrorEndDateEmpty == false)
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_STAT_INFO_END_DATE_IS_EMPTY,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_STAT_INFO_END_DATE_IS_EMPTY, language)
                                            });
                                            ErrorEndDateEmpty = true;
                                        }
                                    }

                                    if (ErrorStartDateNotValid == false)
                                    {
                                        if (UICommonFunction.ConvertToDateTime(inputModel.StartDateWorkingTime[j]) < UICommonFunction.ConvertToDateTime(inputModel.strDate_Of_Hire))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_WORKING_TIME_START_DATE_IS_NOT_VALID,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORKING_TIME_START_DATE_IS_NOT_VALID, language)
                                            });
                                            ErrorStartDateNotValid = true;
                                        }
                                    }

                                    if (ErrorEndDateNotValid == false)
                                    {
                                        if (UICommonFunction.ConvertToDateTime(inputModel.StartDateWorkingTime[j]) > UICommonFunction.ConvertToDateTime(inputModel.EndDateWorkingTime[j]))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_WORKING_TIME_END_DATE_IS_NOT_VALID,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORKING_TIME_END_DATE_IS_NOT_VALID, language)
                                            });
                                            ErrorEndDateNotValid = true;
                                        }
                                    }

                                    if (ErrorStartDateCannotBeSame == false)
                                    {
                                        if (!hsStartDateWorkingTime.Add(inputModel.StartDateWorkingTime[j].ToString()))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_WORKING_TIME_START_DATE_CANNOT_BE_SAME,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_WORKING_TIME_START_DATE_CANNOT_BE_SAME, language)
                                            });
                                            ErrorStartDateCannotBeSame = true;
                                        }
                                    }

                                    if (ErrorEndDateCannotBeSame == false)
                                    {
                                        if (!hsEndDateWorkingTime.Add(inputModel.EndDateWorkingTime[j].ToString()))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_WORKING_TIME_END_DATE_CANNOT_BE_SAME,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_WORKING_TIME_END_DATE_CANNOT_BE_SAME, language)
                                            });
                                            ErrorEndDateCannotBeSame = true;
                                        }
                                    }
                                    #region Checking Overlap One More Row
                                    if (ErrorStartDateNotValid == false)
                                    {
                                        for (int wt = 0; wt < WorkingTime.Count(); wt++)
                                        {
                                            if (wt > 0)
                                            {
                                                if (WorkingTime[wt].Start_Date < WorkingTime[wt - 1].End_Date)
                                                {
                                                    status = false;
                                                    errMessage.Add(new Global_Error_Code()
                                                    {
                                                        Error_Code = GlobalVariable.CONST_ERR_WORKING_TIME_START_DATE_IS_NOT_VALID,
                                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORKING_TIME_START_DATE_IS_NOT_VALID, language)
                                                    });
                                                    ErrorStartDateNotValid = true;
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                        else
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_WORKING_TIME_IS_EMPTY,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORKING_TIME_IS_EMPTY, language)
                            });
                        }
                        #endregion

                        #region Validate Working Time StartDate Overlap
                        //if (inputModel.StartDateWorkingTime != null)
                        //{
                        //    HashSet<string> hsStartDateWorkingTime = new HashSet<string>();
                        //    foreach (var item in inputModel.StartDateWorkingTime)
                        //    {
                        //        if (!hsStartDateWorkingTime.Add(item.ToString()))
                        //        {
                        //            status = false;
                        //            errMessage.Add(new Global_Error_Code()
                        //            {
                        //                Error_Code = GlobalVariable.CONST_ERR_WORKING_TIME_START_DATE_IS_NOT_VALID,
                        //                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORKING_TIME_START_DATE_IS_NOT_VALID, language)
                        //            });
                        //        }
                        //    }
                        //}
                        #endregion

                        #region Validate ERR_POSITION_EFF_DATE_IS_NOT_VALID
                        if (inputModel.PositionCode != null || inputModel.effectiveDatePosition != null)
                        {
                            HashSet<string> hseffectiveDatePosition = new HashSet<string>();
                            bool ErrorPositionCode = false;
                            bool ErrorEffectivePosition = false;
                            bool ErrorEffectivePosition_Not_Valid = false;
                            bool ErrorEffectivePosition_Cannot_Be_Same = false;

                            for (int i = 0; i < inputModel.PositionCode.Length; i++)
                            {
                                if (inputModel.PositionCode[i] == "" && inputModel.effectiveDatePosition[i] == "")
                                {
                                }
                                else
                                {
                                    if (inputModel.PositionCode[i] == "" && ErrorPositionCode == false)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_POSITION_CODE_IS_EMPTY,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_POSITION_CODE_IS_EMPTY, language)
                                        });
                                        ErrorPositionCode = true;
                                    }
                                    if (inputModel.effectiveDatePosition[i] == "" && ErrorEffectivePosition == false)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_POSITION_EFFECTIVE_DATE_IS_EMPTY,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_POSITION_EFFECTIVE_DATE_IS_EMPTY, language)
                                        });
                                        ErrorEffectivePosition = true;
                                    }
                                }

                                if (!UICommonFunction.StringIsNullOrEmpty(inputModel.PositionCode[i]) && !String.IsNullOrEmpty(inputModel.effectiveDatePosition[i]))
                                {
                                    if (ErrorEffectivePosition_Not_Valid == false)
                                    {
                                        if (UICommonFunction.ConvertToDateTime(inputModel.effectiveDatePosition[i]) < UICommonFunction.ConvertToDateTime(inputModel.strDate_Of_Hire))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_POSITION_EFF_DATE_IS_NOT_VALID,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_POSITION_EFF_DATE_IS_NOT_VALID, language)
                                            });
                                            ErrorEffectivePosition_Not_Valid = true;
                                        }
                                    }
                                    if (ErrorEffectivePosition_Cannot_Be_Same == false)
                                    {
                                        if (!hseffectiveDatePosition.Add(inputModel.effectiveDatePosition[i].ToString()))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_POSITION_EFF_DATE_CANNOT_BE_SAME,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_POSITION_EFF_DATE_CANNOT_BE_SAME, language)
                                            });
                                            ErrorEffectivePosition_Cannot_Be_Same = true;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Validate Position Effective Date Overlap
                        //if (inputModel.effectiveDatePosition != null)
                        //{
                        //HashSet<string> hseffectiveDatePosition = new HashSet<string>();
                        //foreach (var item in inputModel.effectiveDatePosition)
                        //{
                        //    if (!hseffectiveDatePosition.Add(item.ToString()))
                        //    {
                        //        status = false;
                        //        errMessage.Add(new Global_Error_Code()
                        //        {
                        //            Error_Code = GlobalVariable.CONST_ERR_POSITION_EFF_DATE_IS_NOT_VALID,
                        //            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_POSITION_EFF_DATE_IS_NOT_VALID, language)
                        //        });
                        //    }
                        //}
                        //}
                        #endregion

                        #region Validate ERR_DIVISION_EFF_DATE_IS_NOT_VALID
                        if (inputModel.DivisionCode != null || inputModel.effectiveDateDivision != null)
                        {
                            HashSet<string> hseffectiveDateDivision = new HashSet<string>();
                            bool ErrorDivisionCode = false;
                            bool ErrorEffectiveDivision = false;
                            bool ErrorEffectiveDivision_Not_Valid = false;
                            bool ErrorEffectiveDivision_Cannot_Be_Same = false;

                            for (int i = 0; i < inputModel.DivisionCode.Length; i++)
                            {
                                if (inputModel.DivisionCode[i] == "" && inputModel.effectiveDateDivision[i] == "")
                                {
                                }
                                else
                                {
                                    if (inputModel.DivisionCode[i] == "" && ErrorDivisionCode == false)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_DIVISION_CODE_IS_EMPTY,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_DIVISION_CODE_IS_EMPTY, language)
                                        });
                                        ErrorDivisionCode = true;
                                    }
                                    if (inputModel.effectiveDateDivision[i] == "" && ErrorEffectiveDivision == false)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_DIVISION_EFFECTIVE_DATE_IS_EMPTY,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_DIVISION_EFFECTIVE_DATE_IS_EMPTY, language)
                                        });
                                        ErrorEffectiveDivision = true;
                                    }
                                }
                                if (!UICommonFunction.StringIsNullOrEmpty(inputModel.DivisionCode[i]) && !String.IsNullOrEmpty(inputModel.effectiveDateDivision[i]))
                                {
                                    if (ErrorEffectiveDivision_Not_Valid == false)
                                    {
                                        if (UICommonFunction.ConvertToDateTime(inputModel.effectiveDateDivision[i]) < UICommonFunction.ConvertToDateTime(inputModel.strDate_Of_Hire))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_DIVISION_EFF_DATE_IS_NOT_VALID,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DIVISION_EFF_DATE_IS_NOT_VALID, language)
                                            });
                                            ErrorEffectiveDivision_Not_Valid = true;
                                        }
                                    }
                                    if (ErrorEffectiveDivision_Cannot_Be_Same == false)
                                    {
                                        if (!hseffectiveDateDivision.Add(inputModel.effectiveDateDivision[i].ToString()))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_DIVISION_EFF_DATE_CANNOT_BE_SAME,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_DIVISION_EFF_DATE_CANNOT_BE_SAME, language)
                                            });
                                            ErrorEffectiveDivision_Cannot_Be_Same = true;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Validate Division Effective Date Overlap
                        //if (inputModel.effectiveDateDivision != null)
                        //{
                        //    HashSet<string> hseffectiveDateDivision = new HashSet<string>();
                        //    foreach (var item in inputModel.effectiveDateDivision)
                        //    {
                        //        if (!hseffectiveDateDivision.Add(item.ToString()))
                        //        {
                        //            status = false;
                        //            errMessage.Add(new Global_Error_Code()
                        //            {
                        //                Error_Code = GlobalVariable.CONST_ERR_DIVISION_EFF_DATE_IS_NOT_VALID,
                        //                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DIVISION_EFF_DATE_IS_NOT_VALID, language)
                        //            });
                        //        }
                        //    }
                        //}
                        #endregion

                        #region Validate ERR_DEPARTMENT_EFF_DATE_IS_NOT_VALID
                        if (inputModel.DepartmentCode != null || inputModel.effectiveDateDepartment != null)
                        {
                            HashSet<string> hseffectiveDateDepartment = new HashSet<string>();
                            bool ErrorDepartmentCode = false;
                            bool ErrorEffectiveDepartment = false;
                            bool ErrorEffectiveDepartment_Not_Valid = false;
                            bool ErrorEffectiveDepartment_Cannot_Be_Same = false;

                            for (int i = 0; i < inputModel.DepartmentCode.Length; i++)
                            {
                                if (inputModel.DepartmentCode[i] == "" && inputModel.effectiveDateDepartment[i] == "")
                                {
                                }
                                else
                                {
                                    if (inputModel.DepartmentCode[i] == "" && ErrorDepartmentCode == false)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_DEPARTMENT_CODE_IS_EMPTY,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_DEPARTMENT_CODE_IS_EMPTY, language)
                                        });
                                        ErrorDepartmentCode = true;
                                    }
                                    if (inputModel.effectiveDateDepartment[i] == "" && ErrorEffectiveDepartment == false)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_DEPARTMENT_EFFECTIVE_DATE_IS_EMPTY,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_DEPARTMENT_EFFECTIVE_DATE_IS_EMPTY, language)
                                        });
                                        ErrorEffectiveDepartment = true;
                                    }
                                }

                                if (!UICommonFunction.StringIsNullOrEmpty(inputModel.DepartmentCode[i]) && !String.IsNullOrEmpty(inputModel.effectiveDateDepartment[i]))
                                {
                                    if (ErrorEffectiveDepartment_Not_Valid == false)
                                    {
                                        if (UICommonFunction.ConvertToDateTime(inputModel.effectiveDateDepartment[i]) < UICommonFunction.ConvertToDateTime(inputModel.strDate_Of_Hire))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_DEPARTMENT_EFF_DATE_IS_NOT_VALID,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DEPARTMENT_EFF_DATE_IS_NOT_VALID, language)
                                            });
                                            ErrorEffectiveDepartment_Not_Valid = true;
                                        }
                                    }
                                    if (ErrorEffectiveDepartment_Cannot_Be_Same == false)
                                    {
                                        if (!hseffectiveDateDepartment.Add(inputModel.effectiveDateDepartment[i].ToString()))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_DEPARTMENT_EFF_DATE_CANNOT_BE_SAME,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_DEPARTMENT_EFF_DATE_CANNOT_BE_SAME, language)
                                            });
                                            ErrorEffectiveDepartment_Cannot_Be_Same = true;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Validate Department Effective Date Overlap
                        //if (inputModel.effectiveDateDepartment != null)
                        //{
                        //    HashSet<string> hseffectiveDateDepartment = new HashSet<string>();
                        //    foreach (var item in inputModel.effectiveDateDepartment)
                        //    {
                        //        if (!hseffectiveDateDepartment.Add(item.ToString()))
                        //        {
                        //            status = false;
                        //            errMessage.Add(new Global_Error_Code()
                        //            {
                        //                Error_Code = GlobalVariable.CONST_ERR_DEPARTMENT_EFF_DATE_IS_NOT_VALID,
                        //                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DEPARTMENT_EFF_DATE_IS_NOT_VALID, language)
                        //            });
                        //        }
                        //    }
                        //}
                        #endregion

                        #region Validate ERR_GRADE_EFF_DATE_IS_NOT_VALID
                        if (inputModel.GradeCode != null || inputModel.effectiveDateGrade != null)
                        {
                            HashSet<string> hseffectiveDateGrade = new HashSet<string>();
                            bool ErrorGradeCode = false;
                            bool ErrorEffectiveGrade = false;
                            bool ErrorEffetiveGrade_Not_Valid = false;
                            bool ErrorEffectiveGrade_Cannot_Be_Same = false;

                            for (int i = 0; i < inputModel.GradeCode.Length; i++)
                            {
                                if (inputModel.GradeCode[i] == "" && inputModel.effectiveDateGrade[i] == "")
                                {
                                }
                                else
                                {
                                    if (inputModel.GradeCode[i] == "" && ErrorGradeCode == false)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_GRADE_CODE_IS_EMPTY,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_GRADE_CODE_IS_EMPTY, language)
                                        });
                                        ErrorGradeCode = true;
                                    }

                                    if (inputModel.effectiveDateGrade[i] == "" && ErrorEffectiveGrade == false)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_GRADE_EFFECTIVE_DATE_IS_EMPTY,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_GRADE_EFFECTIVE_DATE_IS_EMPTY, language)
                                        });
                                        ErrorEffectiveGrade = true;
                                    }
                                }

                                if (!UICommonFunction.StringIsNullOrEmpty(inputModel.GradeCode[i]) && !String.IsNullOrEmpty(inputModel.effectiveDateGrade[i]))
                                {
                                    if (ErrorEffetiveGrade_Not_Valid == false)
                                    {
                                        if (UICommonFunction.ConvertToDateTime(inputModel.effectiveDateGrade[i]) < UICommonFunction.ConvertToDateTime(inputModel.strDate_Of_Hire))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_GRADE_EFF_DATE_IS_NOT_VALID,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_GRADE_EFF_DATE_IS_NOT_VALID, language)
                                            });
                                            ErrorEffetiveGrade_Not_Valid = true;
                                        }
                                    }
                                    if (ErrorEffectiveGrade_Cannot_Be_Same == false)
                                    {
                                        if (!hseffectiveDateGrade.Add(inputModel.effectiveDateGrade[i].ToString()))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_GRADE_EFF_DATE_CANNOT_BE_SAME,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_GRADE_EFF_DATE_CANNOT_BE_SAME, language)
                                            });
                                            ErrorEffectiveGrade_Cannot_Be_Same = true;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Validate Grade Effective Date Overlap
                        //if (inputModel.effectiveDateGrade != null)
                        //{
                        //    HashSet<string> hseffectiveDateGrade = new HashSet<string>();
                        //    foreach (var item in inputModel.effectiveDateGrade)
                        //    {
                        //        if (!hseffectiveDateGrade.Add(item.ToString()))
                        //        {
                        //            status = false;
                        //            errMessage.Add(new Global_Error_Code()
                        //            {
                        //                Error_Code = GlobalVariable.CONST_ERR_GRADE_EFF_DATE_IS_NOT_VALID,
                        //                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_GRADE_EFF_DATE_IS_NOT_VALID, language)
                        //            });
                        //        }
                        //    }
                        //}
                        #endregion
                    }
                    #endregion

                    #region Edit
                    if (type == GlobalVariable.CONST_EDIT)
                    {

                        #region Validate Date_Of_Hire Empty
                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.strDate_Of_Hire))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_DATE_OF_HIRE_IS_EMPTY,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DATE_OF_HIRE_IS_EMPTY, language)
                            });
                        }
                        #endregion

                        #region Validate Join_Date Empty
                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.strJoin_Date))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_JOIN_DATE_IS_EMPTY,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_JOIN_DATE_IS_EMPTY, language)
                            });
                        }
                        #endregion

                        #region Validate ERR_JOIN_DATE_IS_NOT_VALID
                        if (UICommonFunction.ConvertToDateTime(inputModel.strJoin_Date) > UICommonFunction.ConvertToDateTime(inputModel.strDate_Of_Hire))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_JOIN_DATE_IS_NOT_VALID,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_JOIN_DATE_IS_NOT_VALID, language)
                            });
                        }
                        #endregion

                        #region Validate Status Information
                        if (inputModel.EmploymentStatus == null && inputModel.EmployeeStatus == null)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_STATUS_INFORMATION_IS_EMPTY,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_STATUS_INFORMATION_IS_EMPTY, language)
                            });
                        }
                        else
                        {
                            if (inputModel.WorkLocation.Count() == 1 && inputModel.EmploymentStatus[0] == "" && inputModel.EmployeeStatus[0] == "" && inputModel.effectiveDateStatusInfo[0] == "")
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_STATUS_INFORMATION_IS_EMPTY,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_STATUS_INFORMATION_IS_EMPTY, language)
                                });
                            }
                            else
                            {
                                HashSet<string> hseffectiveDateStatusInfo = new HashSet<string>();
                                bool ErrorEmployementStatusEmpty = false;
                                bool ErrorEmployementStatusNotHaveTaxID = false;
                                bool ErrorEmployeeStatusEmpty = false;
                                bool ErrorEffectiveDateEmpty = false;
                                bool ErrorEffectiveDateNotValid = false;
                                bool ErrorEffectiveDateCannotBeSame = false;
                                bool ErrorEndDateOfProbationNotValid = false;

                                for (int i = 0; i < inputModel.EmploymentStatus.Length; i++)
                                {
                                    if (inputModel.EmploymentStatus[i] == "" && inputModel.EmployeeStatus[i] == "" && inputModel.effectiveDateStatusInfo[i] == "")
                                    {
                                    }
                                    else
                                    {
                                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.EmploymentStatus[i]) && ErrorEmployementStatusEmpty == false)
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_EMPLOYMENT_STATUS_IS_EMPTY,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMPLOYMENT_STATUS_IS_EMPTY, language)
                                            });
                                            ErrorEmployementStatusEmpty = true;
                                        }
                                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.EmployeeStatus[i]) && ErrorEmployeeStatusEmpty == false)
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_EMPLOYEE_STATUS_IS_EMPTY,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMPLOYEE_STATUS_IS_EMPTY, language)
                                            });
                                            ErrorEmployeeStatusEmpty = true;
                                        }
                                        if (!inputModel.EmploymentStatus[i].Contains(GlobalVariable.CONST_PERMANENT) || !inputModel.EmploymentStatus[i].Contains(GlobalVariable.CONST_CONTRACT) || !inputModel.EmploymentStatus[i].Contains(GlobalVariable.CONST_PROBATION) || inputModel.EmploymentStatus[i].Contains(GlobalVariable.CONST_PERMANENT_EXPATRIATE_PERMANENT) || UICommonFunction.StringIsNullOrEmpty(inputModel.EmploymentStatus[i]))
                                        {
                                            if (UICommonFunction.StringIsNullOrEmpty(inputModel.effectiveDateStatusInfo[i]) && ErrorEffectiveDateEmpty == false)
                                            {
                                                status = false;
                                                errMessage.Add(new Global_Error_Code()
                                                {
                                                    Error_Code = GlobalVariable.CONST_ERR_STATUS_INFO_EFF_DATE_IS_EMPTY,
                                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_STATUS_INFO_EFF_DATE_IS_EMPTY, language)
                                                });
                                                ErrorEffectiveDateEmpty = true;
                                            }
                                        }

                                        var TaxNo = "";
                                        var Tax = db.tbl_Tax.Where(p => p.Employee_ID == EmployeeID && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED)).FirstOrDefault();
                                        if (Tax != null)
                                            TaxNo = Tax.Tax_No;

                                        if (inputModel.EmploymentStatus[i].Contains(GlobalVariable.CONST_NON_PERMANENT_WAJIB_PAJAK_LUAR_NEGERI) && !UICommonFunction.StringIsNullOrEmpty(TaxNo) && inputModel.EmployeeStatus[i].Contains(GlobalVariable.CONST_EMPLOYEE_STATUS_ACTIVE) && ErrorEmployementStatusNotHaveTaxID == false)
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_EMPLOYMENT_STATUS_CANNOT_HAVE_TAXID,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMPLOYMENT_STATUS_CANNOT_HAVE_TAXID, language)
                                            });
                                            ErrorEmployementStatusNotHaveTaxID = true;
                                        }

                                        Guid TaxID = new Guid();
                                        if (Tax != null)
                                            TaxID = Tax.id;

                                        var TaxStatusYear = db.tbl_Tax_Status_Effective_Year.Where(p => p.Tax_ID == TaxID).FirstOrDefault();
                                        var TaxStatus = "";
                                        if (TaxStatusYear != null)
                                            TaxStatus = TaxStatusYear.Tax_Status;

                                        if (!String.IsNullOrEmpty(inputModel.effectiveDateStatusInfo[i]) && !String.IsNullOrEmpty(inputModel.strDate_Of_Hire))
                                        {
                                            if (ErrorEffectiveDateNotValid == false)
                                            {
                                                if (UICommonFunction.ConvertToDateTime(inputModel.effectiveDateStatusInfo[i]) < UICommonFunction.ConvertToDateTime(inputModel.strDate_Of_Hire))
                                                {
                                                    status = false;
                                                    errMessage.Add(new Global_Error_Code()
                                                    {
                                                        Error_Code = GlobalVariable.CONST_ERR_STAT_INFO_EFF_DATE_IS_NOT_VALID,
                                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_STAT_INFO_EFF_DATE_IS_NOT_VALID, language)
                                                    });
                                                    ErrorEffectiveDateNotValid = true;
                                                }
                                            }
                                        }

                                        if (ErrorEffectiveDateCannotBeSame == false)
                                        {
                                            if (!hseffectiveDateStatusInfo.Add(inputModel.effectiveDateStatusInfo[i].ToString()))
                                            {
                                                status = false;
                                                errMessage.Add(new Global_Error_Code()
                                                {
                                                    Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_STATUS_INFORMATION_EFF_DATE_CANNOT_BE_SAME,
                                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_STATUS_INFORMATION_EFF_DATE_CANNOT_BE_SAME, language)
                                                });
                                                ErrorEffectiveDateCannotBeSame = true;
                                            }
                                        }

                                        if (!String.IsNullOrEmpty(inputModel.strEnd_Date_of_Probation) && !String.IsNullOrEmpty(inputModel.strDate_Of_Hire))
                                        {
                                            if (ErrorEndDateOfProbationNotValid == false)
                                            {
                                                //if (UICommonFunction.ConvertToDateTime(inputModel.strEnd_Date_of_Probation) < UICommonFunction.ConvertToDateTime(inputModel.effectiveDateStatusInfo[i]) && (inputModel.EmploymentStatus[i] == GlobalVariable.CONST_PROBATION))
                                                if (UICommonFunction.ConvertToDateTime(inputModel.strEnd_Date_of_Probation) < UICommonFunction.ConvertToDateTime(inputModel.strDate_Of_Hire))
                                                {
                                                    status = false;
                                                    errMessage.Add(new Global_Error_Code()
                                                    {
                                                        Error_Code = GlobalVariable.CONST_ERR_END_DATE_OF_PROBATION_IS_NOT_VALID,
                                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_END_DATE_OF_PROBATION_IS_NOT_VALID, language)
                                                    });
                                                    ErrorEndDateOfProbationNotValid = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Validate Status Info Effective Date Overlap
                        //if (inputModel.effectiveDateStatusInfo != null)
                        //{
                        //    HashSet<string> hseffectiveDateStatusInfo = new HashSet<string>();
                        //    foreach (var item in inputModel.effectiveDateStatusInfo)
                        //    {
                        //        if (!hseffectiveDateStatusInfo.Add(item.ToString()))
                        //        {
                        //            status = false;
                        //            errMessage.Add(new Global_Error_Code()
                        //            {
                        //                Error_Code = GlobalVariable.CONST_ERR_STAT_INFO_EFF_DATE_IS_NOT_VALID,
                        //                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_STAT_INFO_EFF_DATE_IS_NOT_VALID, language)
                        //            });
                        //        }
                        //    }
                        //}
                        #endregion

                        #region Validate ERR_CONTRACT_END_DATE_IS_NOT_VALID
                        if (inputModel.ContractNo != null)
                        {
                            for (int i = 0; i < inputModel.ContractNo.Length; i++)
                            {
                                if (!string.IsNullOrEmpty(inputModel.ContractNo[i]))
                                {
                                    if (UICommonFunction.ConvertToDateTime(inputModel.ContractStartDate[i]) > UICommonFunction.ConvertToDateTime(inputModel.ContractEndDate[i]))
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.CONST_ERR_CONTRACT_END_DATE_IS_NOT_VALID,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CONTRACT_END_DATE_IS_NOT_VALID, language)
                                        });
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Validate ERR_CONTRACT_DATE_IS_NOT_VALID
                        if (inputModel.ContractStartDate != null)
                        {
                            for (int i = 0; i < inputModel.ContractStartDate.Length; i++)
                            {
                                if (!string.IsNullOrEmpty(inputModel.ContractStartDate[i]))
                                {
                                    if (inputModel.effectiveDateStatusInfo != null)
                                    {
                                        for (int j = 0; j < inputModel.effectiveDateStatusInfo.Length; j++)
                                        {
                                            if (!string.IsNullOrEmpty(inputModel.effectiveDateStatusInfo[j]))
                                            {
                                                if (UICommonFunction.ConvertToDateTime(inputModel.ContractStartDate[i]) < UICommonFunction.ConvertToDateTime(inputModel.effectiveDateStatusInfo[j]) && (inputModel.EmploymentStatus[j] == GlobalVariable.CONST_CONTRACT) && (inputModel.EmployeeStatus[j] == GlobalVariable.CONST_EMPLOYEE_STATUS_ACTIVE))
                                                {
                                                    status = false;
                                                    errMessage.Add(new Global_Error_Code()
                                                    {
                                                        Error_Code = GlobalVariable.CONST_ERR_CONTRACT_DATE_IS_NOT_VALID,
                                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CONTRACT_DATE_IS_NOT_VALID, language)
                                                    });
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Validate Work Location
                        if (inputModel.WorkLocation != null || inputModel.effectiveDateWorkLocation != null)
                        {
                            if (inputModel.WorkLocation.Count() == 1 && inputModel.WorkLocation[0] == "" && inputModel.effectiveDateWorkLocation[0] == "")
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY, language)
                                });
                            }
                            else
                            {
                                HashSet<string> hseffectiveDateWorkLocation = new HashSet<string>();
                                bool ErrorWorkLocationCode = false;
                                bool ErrorEffectiveWorkLocation = false;
                                bool ErrorEffectiveWorkLocation_Not_Valid = false;
                                bool ErrorEffectiveWorkLocation_Cannot_Be_Same = false;

                                for (int j = 0; j < inputModel.WorkLocation.Length; j++)
                                {
                                    if (inputModel.WorkLocation[j] == "" && inputModel.effectiveDateWorkLocation[j] == "")
                                    {
                                    }
                                    else
                                    {
                                        if (inputModel.WorkLocation[j] == "" && ErrorWorkLocationCode == false)
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY, language)
                                            });
                                            ErrorWorkLocationCode = true;
                                        }
                                        if (inputModel.effectiveDateWorkLocation[j] == "" && ErrorEffectiveWorkLocation == false)
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_WORK_LOCATION_EFF_DATE_IS_EMPTY,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORK_LOCATION_EFF_DATE_IS_EMPTY, language)
                                            });
                                            ErrorEffectiveWorkLocation = true;
                                        }
                                    }

                                    if (!String.IsNullOrEmpty(inputModel.WorkLocation[j]) && !String.IsNullOrEmpty(inputModel.effectiveDateWorkLocation[j]))
                                    {
                                        if (ErrorEffectiveWorkLocation_Not_Valid == false)
                                        {
                                            if (UICommonFunction.ConvertToDateTime(inputModel.effectiveDateWorkLocation[j]) < UICommonFunction.ConvertToDateTime(inputModel.strDate_Of_Hire))
                                            {
                                                status = false;
                                                errMessage.Add(new Global_Error_Code()
                                                {
                                                    Error_Code = GlobalVariable.CONST_ERR_WORK_LOCATION_EFF_DATE_IS_NOT_VALID,
                                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORK_LOCATION_EFF_DATE_IS_NOT_VALID, language)
                                                });
                                                ErrorEffectiveWorkLocation_Not_Valid = true;
                                            }
                                        }
                                        if (ErrorEffectiveWorkLocation_Cannot_Be_Same == false)
                                        {
                                            if (!hseffectiveDateWorkLocation.Add(inputModel.effectiveDateWorkLocation[j].ToString()))
                                            {
                                                status = false;
                                                errMessage.Add(new Global_Error_Code()
                                                {
                                                    Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_WORK_LOCATION_EFF_DATE_CANNOT_BE_SAME,
                                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_WORK_LOCATION_EFF_DATE_CANNOT_BE_SAME, language)
                                                });
                                                ErrorEffectiveWorkLocation_Cannot_Be_Same = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY, language)
                            });
                        }
                        //if (inputModel.WorkLocation == null)
                        //{
                        //    status = false;
                        //    errMessage.Add(new Global_Error_Code()
                        //    {
                        //        Error_Code = GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY,
                        //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY, language)
                        //    });
                        //}
                        //else
                        //{
                        //    if (inputModel.EmploymentStatus == null)
                        //    {
                        //        status = false;
                        //        errMessage.Add(new Global_Error_Code()
                        //        {
                        //            Error_Code = GlobalVariable.CONST_ERR_EMPLOYMENT_STATUS_IS_EMPTY,
                        //            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMPLOYMENT_STATUS_IS_EMPTY, language)
                        //        });
                        //    }
                        //    else
                        //    {
                        //        for (int i = 0; i < inputModel.EmploymentStatus.Length; i++)
                        //        {
                        //            if (!UICommonFunction.StringIsNullOrEmpty(inputModel.EmploymentStatus[i]))
                        //            {
                        //                for (int j = 0; j < inputModel.WorkLocation.Length; j++)
                        //                {
                        //                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.WorkLocation[j]))
                        //                    {
                        //                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.WorkLocation[j]) || UICommonFunction.StringIsNullOrEmpty(inputModel.effectiveDateWorkLocation[j]))
                        //                        {
                        //                            status = false;
                        //                            errMessage.Add(new Global_Error_Code()
                        //                            {
                        //                                Error_Code = GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY,
                        //                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY, language)
                        //                            });
                        //                        }
                        //                    }
                        //                    if (UICommonFunction.ConvertToDateTime(inputModel.effectiveDateWorkLocation[j]) < UICommonFunction.ConvertToDateTime(inputModel.strDate_Of_Hire))
                        //                    {
                        //                        status = false;
                        //                        errMessage.Add(new Global_Error_Code()
                        //                        {
                        //                            Error_Code = "ERR_WORK_LOCATION_EFF_DATE",
                        //                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORK_LOCATION_EFF_DATE_IS_NOT_VALID, language)
                        //                        });
                        //                    }
                        //                }
                        //            }
                        //            else
                        //            {
                        //                for (int j = 0; j < inputModel.WorkLocation.Length; j++)
                        //                {
                        //                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.WorkLocation[j]))
                        //                    {
                        //                        status = false;
                        //                        errMessage.Add(new Global_Error_Code()
                        //                        {
                        //                            Error_Code = GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY,
                        //                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORK_LOCATION_IS_EMPTY, language)
                        //                        });
                        //                    }
                        //                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.effectiveDateWorkLocation[j]))
                        //                    {
                        //                        status = false;
                        //                        errMessage.Add(new Global_Error_Code()
                        //                        {
                        //                            Error_Code = "ERR_WORK_LOCATION_EFF_DATE",
                        //                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORK_LOCATION_EFF_DATE_IS_EMPTY, language)
                        //                        });
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        #endregion

                        #region Validate Work Location Effective Date Overlap
                        //if (inputModel.effectiveDateWorkLocation != null)
                        //{
                        //    HashSet<string> hseffectiveDateWorkLocation = new HashSet<string>();
                        //    foreach (var item in inputModel.effectiveDateWorkLocation)
                        //    {
                        //        if (!hseffectiveDateWorkLocation.Add(item.ToString()))
                        //        {
                        //            status = false;
                        //            errMessage.Add(new Global_Error_Code()
                        //            {
                        //                Error_Code = "ERR_WORK_LOCATION_EFF_DATE",
                        //                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORK_LOCATION_EFF_DATE_IS_NOT_VALID, language)
                        //            });
                        //        }
                        //    }
                        //}
                        #endregion

                        #region Validate Working Time
                        if (inputModel.WorkingTimeCode != null || inputModel.StartDateWorkingTime != null || inputModel.EndDateWorkingTime != null)
                        {
                            if (inputModel.WorkingTimeCode.Count() == 1 && inputModel.WorkingTimeCode[0] == "" && inputModel.StartDateWorkingTime[0] == "" && inputModel.EndDateWorkingTime[0] == "")
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.CONST_ERR_WORKING_TIME_IS_EMPTY,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORKING_TIME_IS_EMPTY, language)
                                });
                            }
                            else
                            {
                                List<tbl_Appointment_Working_Time> WorkingTime = new List<tbl_Appointment_Working_Time>();
                                HashSet<string> hsStartDateWorkingTime = new HashSet<string>();
                                HashSet<string> hsEndDateWorkingTime = new HashSet<string>();
                                bool ErrorWorkingTimeCode = false;
                                bool ErrorStartDateEmpty = false;
                                bool ErrorEndDateEmpty = false;
                                bool ErrorStartDateNotValid = false;
                                bool ErrorEndDateNotValid = false;
                                bool ErrorStartDateCannotBeSame = false;
                                bool ErrorEndDateCannotBeSame = false;


                                for (int j = 0; j < inputModel.WorkingTimeCode.Length; j++)
                                {
                                    if (!UICommonFunction.StringIsNullOrEmpty(inputModel.WorkingTimeCode[j]) && !String.IsNullOrEmpty(inputModel.StartDateWorkingTime[j]) && !String.IsNullOrEmpty(inputModel.EndDateWorkingTime[j]))
                                    {
                                        tbl_Appointment_Working_Time tbl_Appointment_Working_Time = new APP_MODEL.ModelData.tbl_Appointment_Working_Time();
                                        tbl_Appointment_Working_Time.Working_Time_Code = inputModel.WorkingTimeCode[j];
                                        tbl_Appointment_Working_Time.Start_Date = UICommonFunction.ConvertToDateTime(inputModel.StartDateWorkingTime[j]);
                                        tbl_Appointment_Working_Time.End_Date = UICommonFunction.ConvertToDateTime(inputModel.EndDateWorkingTime[j]);
                                        WorkingTime.Add(tbl_Appointment_Working_Time);
                                    }

                                    if (inputModel.WorkingTimeCode[j] == "" && inputModel.StartDateWorkingTime[j] == "" && inputModel.EndDateWorkingTime[j] == "")
                                    {
                                    }
                                    else
                                    {
                                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.WorkingTimeCode[j]) && ErrorWorkingTimeCode == false)
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_WORKING_TIME_IS_EMPTY,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORKING_TIME_IS_EMPTY, language)
                                            });
                                            ErrorWorkingTimeCode = true;
                                        }
                                        if (inputModel.StartDateWorkingTime[j] == "" && ErrorStartDateEmpty == false)
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_STAT_INFO_START_DATE_IS_EMPTY,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_STAT_INFO_START_DATE_IS_EMPTY, language)
                                            });
                                            ErrorStartDateEmpty = true;
                                        }
                                        if (inputModel.EndDateWorkingTime[j] == "" && ErrorEndDateEmpty == false)
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_STAT_INFO_END_DATE_IS_EMPTY,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_STAT_INFO_END_DATE_IS_EMPTY, language)
                                            });
                                            ErrorEndDateEmpty = true;
                                        }
                                    }

                                    if (ErrorStartDateNotValid == false)
                                    {
                                        if (UICommonFunction.ConvertToDateTime(inputModel.StartDateWorkingTime[j]) < UICommonFunction.ConvertToDateTime(inputModel.strDate_Of_Hire))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_WORKING_TIME_START_DATE_IS_NOT_VALID,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORKING_TIME_START_DATE_IS_NOT_VALID, language)
                                            });
                                            ErrorStartDateNotValid = true;
                                        }
                                    }

                                    if (ErrorEndDateNotValid == false)
                                    {
                                        if (UICommonFunction.ConvertToDateTime(inputModel.StartDateWorkingTime[j]) > UICommonFunction.ConvertToDateTime(inputModel.EndDateWorkingTime[j]))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_WORKING_TIME_END_DATE_IS_NOT_VALID,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORKING_TIME_END_DATE_IS_NOT_VALID, language)
                                            });
                                            ErrorEndDateNotValid = true;
                                        }
                                    }

                                    if (ErrorStartDateCannotBeSame == false)
                                    {
                                        if (!hsStartDateWorkingTime.Add(inputModel.StartDateWorkingTime[j].ToString()))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_WORKING_TIME_START_DATE_CANNOT_BE_SAME,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_WORKING_TIME_START_DATE_CANNOT_BE_SAME, language)
                                            });
                                            ErrorStartDateCannotBeSame = true;
                                        }
                                    }

                                    if (ErrorEndDateCannotBeSame == false)
                                    {
                                        if (!hsEndDateWorkingTime.Add(inputModel.EndDateWorkingTime[j].ToString()))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_WORKING_TIME_END_DATE_CANNOT_BE_SAME,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_WORKING_TIME_END_DATE_CANNOT_BE_SAME, language)
                                            });
                                            ErrorEndDateCannotBeSame = true;
                                        }
                                    }

                                    #region Checking Overlap One More Row
                                    if (ErrorStartDateNotValid == false)
                                    {
                                        for (int wt = 0; wt < WorkingTime.Count(); wt++)
                                        {
                                            if (wt > 0)
                                            {
                                                if (WorkingTime[wt].Start_Date < WorkingTime[wt - 1].End_Date)
                                                {
                                                    status = false;
                                                    errMessage.Add(new Global_Error_Code()
                                                    {
                                                        Error_Code = GlobalVariable.CONST_ERR_WORKING_TIME_START_DATE_IS_NOT_VALID,
                                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORKING_TIME_START_DATE_IS_NOT_VALID, language)
                                                    });
                                                    ErrorStartDateNotValid = true;
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                        else
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_WORKING_TIME_IS_EMPTY,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORKING_TIME_IS_EMPTY, language)
                            });
                        }
                        #endregion

                        #region Validate Working Time StartDate Overlap
                        //if (inputModel.StartDateWorkingTime != null)
                        //{
                        //    HashSet<string> hsStartDateWorkingTime = new HashSet<string>();
                        //    foreach (var item in inputModel.StartDateWorkingTime)
                        //    {
                        //        if (!hsStartDateWorkingTime.Add(item.ToString()))
                        //        {
                        //            status = false;
                        //            errMessage.Add(new Global_Error_Code()
                        //            {
                        //                Error_Code = GlobalVariable.CONST_ERR_WORKING_TIME_START_DATE_IS_NOT_VALID,
                        //                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_WORKING_TIME_START_DATE_IS_NOT_VALID, language)
                        //            });
                        //        }
                        //    }
                        //}
                        #endregion

                        #region Validate ERR_POSITION_EFF_DATE_IS_NOT_VALID
                        if (inputModel.PositionCode != null || inputModel.effectiveDatePosition != null)
                        {
                            HashSet<string> hseffectiveDatePosition = new HashSet<string>();
                            bool ErrorPositionCode = false;
                            bool ErrorEffectivePosition = false;
                            bool ErrorEffectivePosition_Not_Valid = false;
                            bool ErrorEffectivePosition_Cannot_Be_Same = false;

                            for (int i = 0; i < inputModel.PositionCode.Length; i++)
                            {
                                if (inputModel.PositionCode[i] == "" && inputModel.effectiveDatePosition[i] == "")
                                {
                                }
                                else
                                {
                                    if (inputModel.PositionCode[i] == "" && ErrorPositionCode == false)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_POSITION_CODE_IS_EMPTY,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_POSITION_CODE_IS_EMPTY, language)
                                        });
                                        ErrorPositionCode = true;
                                    }
                                    if (inputModel.effectiveDatePosition[i] == "" && ErrorEffectivePosition == false)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_POSITION_EFFECTIVE_DATE_IS_EMPTY,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_POSITION_EFFECTIVE_DATE_IS_EMPTY, language)
                                        });
                                        ErrorEffectivePosition = true;
                                    }
                                }

                                if (!UICommonFunction.StringIsNullOrEmpty(inputModel.PositionCode[i]) && !String.IsNullOrEmpty(inputModel.effectiveDatePosition[i]))
                                {
                                    if (ErrorEffectivePosition_Not_Valid == false)
                                    {
                                        if (UICommonFunction.ConvertToDateTime(inputModel.effectiveDatePosition[i]) < UICommonFunction.ConvertToDateTime(inputModel.strDate_Of_Hire))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_POSITION_EFF_DATE_IS_NOT_VALID,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_POSITION_EFF_DATE_IS_NOT_VALID, language)
                                            });
                                            ErrorEffectivePosition_Not_Valid = true;
                                        }
                                    }
                                    if (ErrorEffectivePosition_Cannot_Be_Same == false)
                                    {
                                        if (!hseffectiveDatePosition.Add(inputModel.effectiveDatePosition[i].ToString()))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_POSITION_EFF_DATE_CANNOT_BE_SAME,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_POSITION_EFF_DATE_CANNOT_BE_SAME, language)
                                            });
                                            ErrorEffectivePosition_Cannot_Be_Same = true;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Validate Position Effective Date Overlap
                        //if (inputModel.effectiveDatePosition != null)
                        //{
                        //HashSet<string> hseffectiveDatePosition = new HashSet<string>();
                        //foreach (var item in inputModel.effectiveDatePosition)
                        //{
                        //    if (!hseffectiveDatePosition.Add(item.ToString()))
                        //    {
                        //        status = false;
                        //        errMessage.Add(new Global_Error_Code()
                        //        {
                        //            Error_Code = GlobalVariable.CONST_ERR_POSITION_EFF_DATE_IS_NOT_VALID,
                        //            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_POSITION_EFF_DATE_IS_NOT_VALID, language)
                        //        });
                        //    }
                        //}
                        //}
                        #endregion

                        #region Validate ERR_DIVISION_EFF_DATE_IS_NOT_VALID
                        if (inputModel.DivisionCode != null || inputModel.effectiveDateDivision != null)
                        {
                            HashSet<string> hseffectiveDateDivision = new HashSet<string>();
                            bool ErrorDivisionCode = false;
                            bool ErrorEffectiveDivision = false;
                            bool ErrorEffectiveDivision_Not_Valid = false;
                            bool ErrorEffectiveDivision_Cannot_Be_Same = false;

                            for (int i = 0; i < inputModel.DivisionCode.Length; i++)
                            {
                                if (inputModel.DivisionCode[i] == "" && inputModel.effectiveDateDivision[i] == "")
                                {
                                }
                                else
                                {
                                    if (inputModel.DivisionCode[i] == "" && ErrorDivisionCode == false)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_DIVISION_CODE_IS_EMPTY,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_DIVISION_CODE_IS_EMPTY, language)
                                        });
                                        ErrorDivisionCode = true;
                                    }
                                    if (inputModel.effectiveDateDivision[i] == "" && ErrorEffectiveDivision == false)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_DIVISION_EFFECTIVE_DATE_IS_EMPTY,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_DIVISION_EFFECTIVE_DATE_IS_EMPTY, language)
                                        });
                                        ErrorEffectiveDivision = true;
                                    }
                                }

                                if (!UICommonFunction.StringIsNullOrEmpty(inputModel.DivisionCode[i]) && !String.IsNullOrEmpty(inputModel.effectiveDateDivision[i]))
                                {
                                    if (ErrorEffectiveDivision_Not_Valid == false)
                                    {
                                        if (UICommonFunction.ConvertToDateTime(inputModel.effectiveDateDivision[i]) < UICommonFunction.ConvertToDateTime(inputModel.strDate_Of_Hire))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_DIVISION_EFF_DATE_IS_NOT_VALID,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DIVISION_EFF_DATE_IS_NOT_VALID, language)
                                            });
                                            ErrorEffectiveDivision_Not_Valid = true;
                                        }
                                    }
                                    if (ErrorEffectiveDivision_Cannot_Be_Same == false)
                                    {
                                        if (!hseffectiveDateDivision.Add(inputModel.effectiveDateDivision[i].ToString()))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_DIVISION_EFF_DATE_CANNOT_BE_SAME,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_DIVISION_EFF_DATE_CANNOT_BE_SAME, language)
                                            });
                                            ErrorEffectiveDivision_Cannot_Be_Same = true;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Validate Division Effective Date Overlap
                        //if (inputModel.effectiveDateDivision != null)
                        //{
                        //    HashSet<string> hseffectiveDateDivision = new HashSet<string>();
                        //    foreach (var item in inputModel.effectiveDateDivision)
                        //    {
                        //        if (!hseffectiveDateDivision.Add(item.ToString()))
                        //        {
                        //            status = false;
                        //            errMessage.Add(new Global_Error_Code()
                        //            {
                        //                Error_Code = GlobalVariable.CONST_ERR_DIVISION_EFF_DATE_IS_NOT_VALID,
                        //                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DIVISION_EFF_DATE_IS_NOT_VALID, language)
                        //            });
                        //        }
                        //    }
                        //}
                        #endregion

                        #region Validate ERR_DEPARTMENT_EFF_DATE_IS_NOT_VALID
                        if (inputModel.DepartmentCode != null || inputModel.effectiveDateDepartment != null)
                        {
                            HashSet<string> hseffectiveDateDepartment = new HashSet<string>();
                            bool ErrorDepartmentCode = false;
                            bool ErrorEffectiveDepartment = false;
                            bool ErrorEffectiveDepartment_Not_Valid = false;
                            bool ErrorEffectiveDepartment_Cannot_Be_Same = false;

                            for (int i = 0; i < inputModel.DepartmentCode.Length; i++)
                            {
                                if (inputModel.DepartmentCode[i] == "" && inputModel.effectiveDateDepartment[i] == "")
                                {
                                }
                                else
                                {
                                    if (inputModel.DepartmentCode[i] == "" && ErrorDepartmentCode == false)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_DEPARTMENT_CODE_IS_EMPTY,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_DEPARTMENT_CODE_IS_EMPTY, language)
                                        });
                                        ErrorDepartmentCode = true;
                                    }
                                    if (inputModel.effectiveDateDepartment[i] == "" && ErrorEffectiveDepartment == false)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_DEPARTMENT_EFFECTIVE_DATE_IS_EMPTY,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_DEPARTMENT_EFFECTIVE_DATE_IS_EMPTY, language)
                                        });
                                        ErrorEffectiveDepartment = true;
                                    }
                                }

                                if (!UICommonFunction.StringIsNullOrEmpty(inputModel.DepartmentCode[i]) && !String.IsNullOrEmpty(inputModel.effectiveDateDepartment[i]))
                                {
                                    if (ErrorEffectiveDepartment_Not_Valid == false)
                                    {
                                        if (UICommonFunction.ConvertToDateTime(inputModel.effectiveDateDepartment[i]) < UICommonFunction.ConvertToDateTime(inputModel.strDate_Of_Hire))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_DEPARTMENT_EFF_DATE_IS_NOT_VALID,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DEPARTMENT_EFF_DATE_IS_NOT_VALID, language)
                                            });
                                            ErrorEffectiveDepartment_Not_Valid = true;
                                        }
                                    }
                                    if (ErrorEffectiveDepartment_Cannot_Be_Same == false)
                                    {
                                        if (!hseffectiveDateDepartment.Add(inputModel.effectiveDateDepartment[i].ToString()))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_DEPARTMENT_EFF_DATE_CANNOT_BE_SAME,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_DEPARTMENT_EFF_DATE_CANNOT_BE_SAME, language)
                                            });
                                            ErrorEffectiveDepartment_Cannot_Be_Same = true;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Validate Department Effective Date Overlap
                        //if (inputModel.effectiveDateDepartment != null)
                        //{
                        //    HashSet<string> hseffectiveDateDepartment = new HashSet<string>();
                        //    foreach (var item in inputModel.effectiveDateDepartment)
                        //    {
                        //        if (!hseffectiveDateDepartment.Add(item.ToString()))
                        //        {
                        //            status = false;
                        //            errMessage.Add(new Global_Error_Code()
                        //            {
                        //                Error_Code = GlobalVariable.CONST_ERR_DEPARTMENT_EFF_DATE_IS_NOT_VALID,
                        //                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DEPARTMENT_EFF_DATE_IS_NOT_VALID, language)
                        //            });
                        //        }
                        //    }
                        //}
                        #endregion

                        #region Validate ERR_GRADE_EFF_DATE_IS_NOT_VALID
                        if (inputModel.GradeCode != null || inputModel.effectiveDateGrade != null)
                        {
                            HashSet<string> hseffectiveDateGrade = new HashSet<string>();
                            bool ErrorGradeCode = false;
                            bool ErrorEffectiveGrade = false;
                            bool ErrorEffetiveGrade_Not_Valid = false;
                            bool ErrorEffectiveGrade_Cannot_Be_Same = false;

                            for (int i = 0; i < inputModel.GradeCode.Length; i++)
                            {
                                if (inputModel.GradeCode[i] == "" && inputModel.effectiveDateGrade[i] == "")
                                {
                                }
                                else
                                {
                                    if (inputModel.GradeCode[i] == "" && ErrorGradeCode == false)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_GRADE_CODE_IS_EMPTY,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_GRADE_CODE_IS_EMPTY, language)
                                        });
                                        ErrorGradeCode = true;
                                    }

                                    if (inputModel.effectiveDateGrade[i] == "" && ErrorEffectiveGrade == false)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_GRADE_EFFECTIVE_DATE_IS_EMPTY,
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_GRADE_EFFECTIVE_DATE_IS_EMPTY, language)
                                        });
                                        ErrorEffectiveGrade = true;
                                    }
                                }

                                if (!UICommonFunction.StringIsNullOrEmpty(inputModel.GradeCode[i]) && !String.IsNullOrEmpty(inputModel.effectiveDateGrade[i]))
                                {
                                    if (ErrorEffetiveGrade_Not_Valid == false)
                                    {
                                        if (UICommonFunction.ConvertToDateTime(inputModel.effectiveDateGrade[i]) < UICommonFunction.ConvertToDateTime(inputModel.strDate_Of_Hire))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_GRADE_EFF_DATE_IS_NOT_VALID,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_GRADE_EFF_DATE_IS_NOT_VALID, language)
                                            });
                                            ErrorEffetiveGrade_Not_Valid = true;
                                        }
                                    }
                                    if (ErrorEffectiveGrade_Cannot_Be_Same == false)
                                    {
                                        if (!hseffectiveDateGrade.Add(inputModel.effectiveDateGrade[i].ToString()))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_APPOINTMENT_GRADE_EFF_DATE_CANNOT_BE_SAME,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_APPOINTMENT_GRADE_EFF_DATE_CANNOT_BE_SAME, language)
                                            });
                                            ErrorEffectiveGrade_Cannot_Be_Same = true;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Validate Grade Effective Date Overlap
                        //if (inputModel.effectiveDateGrade != null)
                        //{
                        //    HashSet<string> hseffectiveDateGrade = new HashSet<string>();
                        //    foreach (var item in inputModel.effectiveDateGrade)
                        //    {
                        //        if (!hseffectiveDateGrade.Add(item.ToString()))
                        //        {
                        //            status = false;
                        //            errMessage.Add(new Global_Error_Code()
                        //            {
                        //                Error_Code = GlobalVariable.CONST_ERR_GRADE_EFF_DATE_IS_NOT_VALID,
                        //                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_GRADE_EFF_DATE_IS_NOT_VALID, language)
                        //            });
                        //        }
                        //    }
                        //}
                        #endregion
                    }
                    #endregion
                }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation Tax Information

        public static bool ValidationTaxInformation(Global_Employee_Tax_Info inputModel, string language, string type, out List<Global_Error_Code> errMessage, Guid EmployeeID)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                if (inputModel.CheckedTaxPeriod == false)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_CHEECK_TAX_PERIOD,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_CHEECK_TAX_PERIOD, language)
                    });
                }
                else
                {
                    #region Create
                    if (type == GlobalVariable.CONST_CREATE)
                    {
                        var RecordData = GeneralCore.TaxQuery().Where(p => p.Employee_ID == EmployeeID).FirstOrDefault();
                        if (RecordData != null)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_1_EMPLOYEE_ONLY_HAVE_1_RECORD_DATA,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_1_EMPLOYEE_ONLY_HAVE_1_RECORD_DATA, language)
                            });
                        }

                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.taxInfoModel.Tax_No))
                        {
                            if (inputModel.taxInfoModel.Effective_Date != null)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.CONST_ERR_EFFECTIVE_DATE_MUST_BE_EMPTY,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EFFECTIVE_DATE_MUST_BE_EMPTY, language)
                                });
                            }
                        }

                        #region Employment Status Permanent
                        Guid IdEmployeeAppointment = Guid.NewGuid();
                        var EmployeeAppointment = db.tbl_Employee_Appointment.Where(p => p.Employee_ID == EmployeeID && (p.Status_Code == CoreVariable.CONST_STATUS_ACTIVE && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED)).FirstOrDefault();
                        if (EmployeeAppointment != null)
                            IdEmployeeAppointment = EmployeeAppointment.id;

                        var EmploymentAppointmentStatus = db.tbl_Appointment_Status_Information.Where(p => p.Appointment_Id == IdEmployeeAppointment).FirstOrDefault();
                        string EmploymentStatus = "";
                        string EmployeeStatus = "";
                        if (EmploymentAppointmentStatus != null)
                        {
                            EmploymentStatus = EmploymentAppointmentStatus.Employment_Status;
                            EmployeeStatus = EmploymentAppointmentStatus.Employee_Status;
                        }

                        if (string.IsNullOrEmpty(EmploymentStatus))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_MUST_BE_CREATE_APPOINTMENT,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_MUST_BE_CREATE_APPOINTMENT, language)
                            });
                        }

                        if (inputModel.taxStatus == null)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_TAX_STATUS_IS_EMPTY,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TAX_STATUS_IS_EMPTY, language)
                            });
                        }
                        else
                        {
                            if ((EmploymentStatus.Contains(GlobalVariable.CONST_PERMANENT) || EmploymentStatus.Contains(GlobalVariable.CONST_CONTRACT) || EmploymentStatus.Contains(GlobalVariable.CONST_PROBATION) || EmploymentStatus.Contains(GlobalVariable.CONST_PERMANENT_EXPATRIATE_PERMANENT)) && EmployeeStatus.Contains(GlobalVariable.CONST_EMPLOYEE_STATUS_ACTIVE))
                            {
                                if (inputModel.taxStatus != null)
                                {
                                    for (int i = 0; i < inputModel.taxStatus.Length; i++)
                                    {
                                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.taxStatus[i]))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_TAX_STATUS_IS_EMPTY,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TAX_STATUS_IS_EMPTY, language)
                                            });
                                        }
                                        else if (!UICommonFunction.StringIsNullOrEmpty(inputModel.taxStatus[i]))
                                        {
                                            if (UICommonFunction.StringIsNullOrEmpty(inputModel.statusEffectiveYear[i]))
                                            {
                                                status = false;
                                                errMessage.Add(new Global_Error_Code()
                                                {
                                                    Error_Code = GlobalVariable.CONST_ERR_YEAR_STATUS_IS_EMPTY,
                                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_YEAR_STATUS_IS_EMPTY, language)
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (inputModel.taxStatus != null)
                                {
                                    for (int i = 0; i < inputModel.taxStatus.Length; i++)
                                    {
                                        if (!UICommonFunction.StringIsNullOrEmpty(inputModel.taxStatus[i]))
                                        {
                                            if (UICommonFunction.StringIsNullOrEmpty(inputModel.statusEffectiveYear[i]))
                                            {
                                                status = false;
                                                errMessage.Add(new Global_Error_Code()
                                                {
                                                    Error_Code = GlobalVariable.CONST_ERR_YEAR_STATUS_IS_EMPTY,
                                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_YEAR_STATUS_IS_EMPTY, language)
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        var dateofhire = EmployeeAppointment.Date_Of_Hire;
                        if (!UICommonFunction.StringIsNullOrEmpty(inputModel.taxInfoModel.Tax_No))
                        {
                            if (inputModel.taxInfoModel.Effective_Date != null)
                            {
                                if (inputModel.taxInfoModel.Effective_Date < dateofhire)
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = GlobalVariable.CONST_ERR_EFFECTIVE_DATE_CANNOT_BE_LESS_THAN_DATE_OF_HIRE,
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EFFECTIVE_DATE_CANNOT_BE_LESS_THAN_DATE_OF_HIRE, language)
                                    });
                                }
                            }
                        }

                        #region Employment Status Non Permanent – Wajib Pajak Luar Negeri

                        if (EmploymentStatus.Contains(GlobalVariable.CONST_NON_PERMANENT_WAJIB_PAJAK_LUAR_NEGERI) && EmployeeStatus.Contains(GlobalVariable.CONST_EMPLOYEE_STATUS_ACTIVE))
                        {
                            if (!UICommonFunction.StringIsNullOrEmpty(inputModel.taxInfoModel.Tax_No))
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.CONST_ERR_EMPLOYMENT_STATUS_WAJIB_PAJAK_LUAR_NEGERI,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMPLOYMENT_STATUS_WAJIB_PAJAK_LUAR_NEGERI, language)
                                });
                            }
                        }

                        #endregion

                        //if (string.IsNullOrEmpty(EmploymentStatus) || inputModel.taxStatus == null)
                        //{
                        //    status = false;
                        //    errMessage.Add(new Global_Error_Code()
                        //    {
                        //        Error_Code = GlobalVariable.CONST_ERR_TAX_STATUS_IS_EMPTY,
                        //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TAX_STATUS_IS_EMPTY, language)
                        //    });
                        //}
                        //else
                        //{
                        //    if ((GlobalVariable.CONST_NON_PERMANENT_WAJIB_PAJAK_LUAR_NEGERI.Contains(EmploymentStatus) || GlobalVariable.CONST_NON_PERMANENT_KOMISARIS.Contains(EmploymentStatus) || GlobalVariable.CONST_NON_PERMANENT_TENAGA_AHLI.Contains(EmploymentStatus) || GlobalVariable.CONST_NON_PERMANENT_MANTAN_PEGAWAI.Contains(EmploymentStatus)) && GlobalVariable.CONST_EMPLOYEE_STATUS_ACTIVE.Contains(EmployeeStatus))
                        //    {
                        //        if (inputModel.taxStatus != null)
                        //        {
                        //            for (int i = 0; i < inputModel.taxStatus.Length; i++)
                        //            {
                        //                if (!UICommonFunction.StringIsNullOrEmpty(inputModel.taxStatus[i]))
                        //                {
                        //                    status = false;
                        //                    errMessage.Add(new Global_Error_Code()
                        //                    {
                        //                        Error_Code = GlobalVariable.CONST_ERR_TAX_STATUS_CANNOT_BE_ASSIGNED,
                        //                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TAX_STATUS_CANNOT_BE_ASSIGNED, language)
                        //                    });
                        //                }
                        //             }
                        //        }
                        //    }
                        //}
                        #region Validate Status Effective Year
                        if (inputModel.statusEffectiveYear != null)
                        {
                            HashSet<string> hsstatusEffectiveYear = new HashSet<string>();
                            foreach (var item in inputModel.statusEffectiveYear)
                            {
                                if (!hsstatusEffectiveYear.Add(item.ToString()))
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = GlobalVariable.CONST_ERR_EMPLOYEE_CANNOT_HAVE_SAME_YEAR,
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMPLOYEE_CANNOT_HAVE_SAME_YEAR, language)
                                    });
                                }
                            }
                        }
                        #endregion

                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.taxInfoModel.Salary_Tax_Policy))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_SALARY_TAX_POLICY_IS_EMPTY,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_SALARY_TAX_POLICY_IS_EMPTY, language)
                            });
                        }
                    }
                    #endregion

                    #region Edit
                    if (type == GlobalVariable.CONST_EDIT)
                    {
                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.taxInfoModel.Tax_No))
                        {
                            if (inputModel.taxInfoModel.Effective_Date != null)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.CONST_ERR_EFFECTIVE_DATE_MUST_BE_EMPTY,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EFFECTIVE_DATE_MUST_BE_EMPTY, language)
                                });
                            }
                        }

                        #region Employment Status Permanent

                        Guid IdEmployeeAppointment = Guid.NewGuid();
                        var EmployeeAppointment = db.tbl_Employee_Appointment.Where(p => p.Employee_ID == EmployeeID && !(p.Status_Code == CoreVariable.CONST_STATUS_DELETED && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED)).FirstOrDefault();
                        if (EmployeeAppointment != null)
                            IdEmployeeAppointment = EmployeeAppointment.id;

                        var EmploymentAppointmentStatus = db.tbl_Appointment_Status_Information.Where(p => p.Appointment_Id == IdEmployeeAppointment).FirstOrDefault();
                        string EmploymentStatus = "";
                        string EmployeeStatus = "";
                        if (EmploymentAppointmentStatus != null)
                        {
                            EmploymentStatus = EmploymentAppointmentStatus.Employment_Status;
                            EmployeeStatus = EmploymentAppointmentStatus.Employee_Status;
                        }

                        if (string.IsNullOrEmpty(EmploymentStatus))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_MUST_BE_CREATE_APPOINTMENT,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_MUST_BE_CREATE_APPOINTMENT, language)
                            });
                        }

                        if (inputModel.taxStatus == null)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_TAX_STATUS_IS_EMPTY,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TAX_STATUS_IS_EMPTY, language)
                            });
                        }
                        else
                        {
                            if ((EmploymentStatus.Contains(GlobalVariable.CONST_PERMANENT) || EmploymentStatus.Contains(GlobalVariable.CONST_CONTRACT) || EmploymentStatus.Contains(GlobalVariable.CONST_PROBATION) || EmploymentStatus.Contains(GlobalVariable.CONST_PERMANENT_EXPATRIATE_PERMANENT)) && EmployeeStatus.Contains(GlobalVariable.CONST_EMPLOYEE_STATUS_ACTIVE))
                            {
                                if (inputModel.taxStatus != null)
                                {
                                    for (int i = 0; i < inputModel.taxStatus.Length; i++)
                                    {
                                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.taxStatus[i]))
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = GlobalVariable.CONST_ERR_TAX_STATUS_IS_EMPTY,
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TAX_STATUS_IS_EMPTY, language)
                                            });
                                        }

                                        else if (!UICommonFunction.StringIsNullOrEmpty(inputModel.taxStatus[i]))
                                        {
                                            if (UICommonFunction.StringIsNullOrEmpty(inputModel.statusEffectiveYear[i]))
                                            {
                                                status = false;
                                                errMessage.Add(new Global_Error_Code()
                                                {
                                                    Error_Code = GlobalVariable.CONST_ERR_YEAR_STATUS_IS_EMPTY,
                                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_YEAR_STATUS_IS_EMPTY, language)
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (inputModel.taxStatus != null)
                                {
                                    for (int i = 0; i < inputModel.taxStatus.Length; i++)
                                    {
                                        if (!UICommonFunction.StringIsNullOrEmpty(inputModel.taxStatus[i]))
                                        {
                                            if (UICommonFunction.StringIsNullOrEmpty(inputModel.statusEffectiveYear[i]))
                                            {
                                                status = false;
                                                errMessage.Add(new Global_Error_Code()
                                                {
                                                    Error_Code = GlobalVariable.CONST_ERR_YEAR_STATUS_IS_EMPTY,
                                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_YEAR_STATUS_IS_EMPTY, language)
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        var dateofhire = EmployeeAppointment.Date_Of_Hire;
                        if (!UICommonFunction.StringIsNullOrEmpty(inputModel.taxInfoModel.Tax_No))
                        {
                            if (inputModel.taxInfoModel.Effective_Date != null)
                            {
                                if (inputModel.taxInfoModel.Effective_Date < dateofhire)
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = GlobalVariable.CONST_ERR_EFFECTIVE_DATE_CANNOT_BE_LESS_THAN_DATE_OF_HIRE,
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EFFECTIVE_DATE_CANNOT_BE_LESS_THAN_DATE_OF_HIRE, language)
                                    });
                                }
                            }
                        }

                        #region Employment Status Non Permanent – Wajib Pajak Luar Negeri

                        if (EmploymentStatus.Contains(GlobalVariable.CONST_NON_PERMANENT_WAJIB_PAJAK_LUAR_NEGERI) && EmployeeStatus.Contains(GlobalVariable.CONST_EMPLOYEE_STATUS_ACTIVE))
                        {
                            if (!UICommonFunction.StringIsNullOrEmpty(inputModel.taxInfoModel.Tax_No))
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.CONST_ERR_EMPLOYMENT_STATUS_WAJIB_PAJAK_LUAR_NEGERI,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMPLOYMENT_STATUS_WAJIB_PAJAK_LUAR_NEGERI, language)
                                });
                            }
                        }

                        #endregion

                        //if (string.IsNullOrEmpty(EmploymentStatus) || inputModel.taxStatus == null)
                        //{
                        //    status = false;
                        //    errMessage.Add(new Global_Error_Code()
                        //    {
                        //        Error_Code = GlobalVariable.CONST_ERR_TAX_STATUS_IS_EMPTY,
                        //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TAX_STATUS_IS_EMPTY, language)
                        //    });
                        //}
                        //else
                        //{
                        //    if (GlobalVariable.CONST_NON_PERMANENT_WAJIB_PAJAK_LUAR_NEGERI.Contains(EmploymentStatus) || GlobalVariable.CONST_NON_PERMANENT_KOMISARIS.Contains(EmploymentStatus) || GlobalVariable.CONST_NON_PERMANENT_TENAGA_AHLI.Contains(EmploymentStatus) || GlobalVariable.CONST_NON_PERMANENT_MANTAN_PEGAWAI.Contains(EmploymentStatus))
                        //    {
                        //        for (int i = 0; i < inputModel.taxStatus.Length; i++)
                        //        {
                        //            if (!UICommonFunction.StringIsNullOrEmpty(inputModel.taxStatus[i]))
                        //            {
                        //                status = false;
                        //                errMessage.Add(new Global_Error_Code()
                        //                {
                        //                    Error_Code = GlobalVariable.CONST_ERR_TAX_STATUS_CANNOT_BE_ASSIGNED,
                        //                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TAX_STATUS_CANNOT_BE_ASSIGNED, language)
                        //                });
                        //            }
                        //        }
                        //    }
                        //}
                        #region Validate Status Effective Year
                        if (inputModel.statusEffectiveYear != null)
                        {
                            HashSet<string> hsstatusEffectiveYear = new HashSet<string>();
                            foreach (var item in inputModel.statusEffectiveYear)
                            {
                                if (!hsstatusEffectiveYear.Add(item.ToString()))
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = GlobalVariable.CONST_ERR_EMPLOYEE_CANNOT_HAVE_SAME_YEAR,
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMPLOYEE_CANNOT_HAVE_SAME_YEAR, language)
                                    });
                                }
                            }
                        }
                        #endregion

                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.taxInfoModel.Salary_Tax_Policy) || inputModel.taxInfoModel.Salary_Tax_Policy.ToUpper() == "SELECT")
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_SALARY_TAX_POLICY_IS_EMPTY,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_SALARY_TAX_POLICY_IS_EMPTY, language)
                            });
                        }
                    }
                    #endregion
                }

            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }


        #endregion

        #region Validation Address Contact Information

        public static bool ValidationAddressContactInformation(Global_Employee_Address inputModel, string language, string type, out List<Global_Error_Code> errMessage, Guid EmployeeID, Guid OrgID)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    var RecordData = GeneralCore.EmployeeAddressQuery().Where(p => p.Employee_ID == EmployeeID).FirstOrDefault();
                    if (RecordData != null)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_1_EMPLOYEE_ONLY_HAVE_1_RECORD_DATA,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_1_EMPLOYEE_ONLY_HAVE_1_RECORD_DATA, language)
                        });
                    }

                    //var EmpAddress = db.tbl_Employee_Address.Where(p => p.Organization_ID == OrgID).ToList();
                    //foreach (var item in EmpAddress)
                    //{
                    //    var corporateemailaddress = item.Coorporate_Email_Address;
                    //    if (!string.IsNullOrEmpty(corporateemailaddress))
                    //    {
                    //        if (inputModel.employeeAdressModel.Coorporate_Email_Address == corporateemailaddress)
                    //        {
                    //            status = false;
                    //            errMessage.Add(new Global_Error_Code()
                    //            {
                    //                Error_Code = GlobalVariable.CONST_ERR_CORPORATE_EMAIL_ADDRESS_ALREADY_EXIST,
                    //                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CORPORATE_EMAIL_ADDRESS_ALREADY_EXIST, language)
                    //            });
                    //        }
                    //    }
                    //}

                    var EmailOtherEmailExist = db.tbl_Employee_Address.Where(p => p.Coorporate_Email_Address == inputModel.employeeAdressModel.Coorporate_Email_Address).Count();
                    if (inputModel.Exist_Email != inputModel.employeeAdressModel.Coorporate_Email_Address)
                    {
                        if (EmailOtherEmailExist > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_CORPORATE_EMAIL_ADDRESS_ALREADY_EXIST,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CORPORATE_EMAIL_ADDRESS_ALREADY_EXIST, language)
                            });
                        }
                    }

                    if (!string.IsNullOrEmpty(inputModel.employeeAdressModel.Coorporate_Email_Address))
                    {
                        if (inputModel.employeeAdressModel.Coorporate_Email_Address.Contains(" "))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_CORPORATE_EMAIL_ADDRESS_CANNOT_CONTAIN_SPACE,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CORPORATE_EMAIL_ADDRESS_CANNOT_CONTAIN_SPACE, language)
                            });
                        }
                    }
                    

                    #region Employment Status Permanent

                    //var IdEmployeeAppointment = db.tbl_Employee_Appointment.Where(p => p.Employee_ID == EmployeeID).FirstOrDefault().id;
                    //var EmploymentStatus = db.tbl_Appointment_Status_Information.Where(p => p.Appointment_Id == IdEmployeeAppointment).FirstOrDefault().Employment_Status;

                    Guid IdEmployeeAppointment = Guid.NewGuid();
                    var EmployeeAppointment = db.tbl_Employee_Appointment.Where(p => p.Employee_ID == EmployeeID && !(p.Status_Code == CoreVariable.CONST_STATUS_DELETED && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED)).FirstOrDefault();
                    if (EmployeeAppointment != null)
                        IdEmployeeAppointment = EmployeeAppointment.id;

                    var EmploymentAppointmentStatus = db.tbl_Appointment_Status_Information.Where(p => p.Appointment_Id == IdEmployeeAppointment).FirstOrDefault();
                    var EmploymentStatus = "";
                    if (EmploymentAppointmentStatus != null)
                        EmploymentStatus = EmploymentAppointmentStatus.Employee_Status;

                    if (EmploymentStatus.Contains(GlobalVariable.CONST_PERMANENT) || EmploymentStatus.Contains(GlobalVariable.CONST_CONTRACT) || EmploymentStatus.Contains(GlobalVariable.CONST_PROBATION) || EmploymentStatus.Contains(GlobalVariable.CONST_PERMANENT_EXPATRIATE_PERMANENT))
                    {
                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.employeeAdressModel.Address))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_LEGAL_ADDRESS_IS_EMPTY,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_LEGAL_ADDRESS_IS_EMPTY, language)
                            });
                        }
                    }

                    #endregion
                }
                #endregion

                #region Edit
                if (type == GlobalVariable.CONST_EDIT)
                {
                    var EmailOtherEmailExist = db.tbl_Employee_Address.Where(p => p.Coorporate_Email_Address == inputModel.employeeAdressModel.Coorporate_Email_Address).Count();

                    if (!string.IsNullOrEmpty(inputModel.employeeAdressModel.Coorporate_Email_Address))
                    {
                        if (inputModel.Exist_Email != inputModel.employeeAdressModel.Coorporate_Email_Address)
                        {
                            if (EmailOtherEmailExist > 0)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.CONST_ERR_CORPORATE_EMAIL_ADDRESS_ALREADY_EXIST,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CORPORATE_EMAIL_ADDRESS_ALREADY_EXIST, language)
                                });
                            }
                        }

                        if (inputModel.employeeAdressModel.Coorporate_Email_Address.Contains(" "))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_CORPORATE_EMAIL_ADDRESS_CANNOT_CONTAIN_SPACE,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CORPORATE_EMAIL_ADDRESS_CANNOT_CONTAIN_SPACE, language)
                            });
                        }
                    }

                    #region Employment Status Permanent

                    Guid IdEmployeeAppointment = Guid.NewGuid();
                    var EmployeeAppointment = db.tbl_Employee_Appointment.Where(p => p.Employee_ID == EmployeeID && !(p.Status_Code == CoreVariable.CONST_STATUS_DELETED && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED)).FirstOrDefault();
                    if (EmployeeAppointment != null)
                        IdEmployeeAppointment = EmployeeAppointment.id;

                    var EmploymentAppointmentStatus = db.tbl_Appointment_Status_Information.Where(p => p.Appointment_Id == IdEmployeeAppointment).FirstOrDefault();
                    var EmploymentStatus = "";
                    if (EmploymentAppointmentStatus != null)
                        EmploymentStatus = EmploymentAppointmentStatus.Employee_Status;

                    if ((EmploymentStatus.Contains(GlobalVariable.CONST_PERMANENT) || EmploymentStatus.Contains(GlobalVariable.CONST_CONTRACT) || EmploymentStatus.Contains(GlobalVariable.CONST_PROBATION) || EmploymentStatus.Contains(GlobalVariable.CONST_PERMANENT_EXPATRIATE_PERMANENT)))
                    {
                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.employeeAdressModel.Address))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_LEGAL_ADDRESS_IS_EMPTY,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_LEGAL_ADDRESS_IS_EMPTY, language)
                            });
                        }
                    }

                    #endregion
                }
                #endregion
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }


        #endregion

        #region Validation HolidayCalendar
        public static bool ValidationHolidayCalendar(Global_Holiday_Calendar inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                DateTime inputDate;
                DateTime.TryParseExact(inputModel.strHolidayDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out inputDate);
                DateTime? dateTimeNow = DateTime.Now;

                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    inputModel.holidayCalendarList = GeneralCore.NationalHolidayCalender().Where(p => p.Holiday_Date == inputDate).ToList();
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.strHolidayDate))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "holidayDateEmpty",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HOLIDAY_DATE_IS_EMPTY, language)
                        });
                    }

                    if (inputModel.holidayCalendarList.Count > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Message",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HOLIDAY_DATE_ALREADY_EXIST, language)
                        });
                    }
                }
                #endregion

                #region Edit
                if (type == GlobalVariable.CONST_EDIT)
                {
                    inputModel.holidayCalendarList = GeneralCore.NationalHolidayCalender().Where(s => s.id == inputModel.holidayCalendarModels.id).ToList();
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.strHolidayDate))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "holidayDateEmpty",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HOLIDAY_DATE_IS_EMPTY, language)
                        });
                    }

                    if (inputModel.holidayCalendarList.FirstOrDefault().Holiday_Date != inputDate)
                    {
                        inputModel.holidayCalendarList = GeneralCore.NationalHolidayCalender().Where(s => s.Holiday_Date == inputDate).Take(1).ToList();
                        if (inputModel.holidayCalendarList.Count() > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Message",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HOLIDAY_DATE_ALREADY_EXIST, language)
                            });
                        }
                    }
                }
                #endregion
            }

            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation Payroll Calculate
        public static bool ValidationPayrolCalculate(Global_Payroll_Calculation inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            bool isPembetulan = false;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                var Calculation_Closed = db.tbl_Payroll_Closing.Where(p => p.Calculation_ID == inputModel.DataModel.id && p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_ACTIVE).Count();
                if (Calculation_Closed == 0)
                {
                    var Payroll_Closing = db.tbl_Payroll_Closing.Where(p => p.tbl_Payroll_Calculation.Calculation_Type == inputModel.DataModel.Calculation_Type && p.tbl_Payroll_Calculation.Organization_ID == inputModel.DataModel.Organization_ID && p.Authorize_Status == CoreVariable.CONST_UNAUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_ACTIVE).Count();
                    if (Payroll_Closing > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Payroll_Period_ID",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CALCULATION_SETUP_MUST_CLOSED_BEFORE, language)
                        });
                    }
                    else
                    {
                        var payroll_period = db.tbl_Payroll_Period_Detail.Where(p => p.id == inputModel.DataModel.Payroll_Period_ID).FirstOrDefault();
                        if (payroll_period == null)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Payroll_Period_ID",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CALCULATION_SETUP_PAYROLL_PERIOD_EMPTY, language)
                            });
                        }
                        else
                        {
                            if (payroll_period.Period_Start_Date.Month == 12)
                            {
                                var Tax_Period_Closed = db.tbl_Tax_Period_Month.Where(p => p.Tax_Period_ID == payroll_period.tbl_Payroll_Period.Tax_Period_ID && p.Tax_Period_Month == payroll_period.Tax_Period).FirstOrDefault();

                                if (inputModel.DataModel.Calculation_Type == "NonPermanent")
                                {
                                    var OrgPayroll_Period = db.Get_Payroll_Period_NP(inputModel.DataModel.Organization_ID).FirstOrDefault();
                                    if (Tax_Period_Closed.Closing_Date_NonPermanent != null && OrgPayroll_Period.Tax_Year != Tax_Period_Closed.tbl_Tax_Period.Tax_Year)
                                    {
                                        isPembetulan = true;
                                    }
                                }
                                else
                                {
                                    var OrgPayroll_Period = db.Get_Payroll_Period(inputModel.DataModel.Organization_ID).FirstOrDefault();
                                    if (Tax_Period_Closed.Closing_Date_Permanent != null && OrgPayroll_Period.Tax_Year != Tax_Period_Closed.tbl_Tax_Period.Tax_Year)
                                    {
                                        isPembetulan = true;
                                    }
                                }


                            }


                            if (!isPembetulan && db.tbl_Payroll_Closing.Where(p => p.Calculation_ID == inputModel.DataModel.id && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Count() > 0)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "Payroll_Period_ID",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CALCULATION_SETUP_PAYROLL_PERIOD_CLOSED, language)
                                });
                            }
                            else
                            {
                                var SumClosing = db.tbl_Payroll_Closing.Where(p => p.tbl_Payroll_Calculation.Payroll_Period_ID != inputModel.DataModel.Payroll_Period_ID && p.tbl_Payroll_Calculation.tbl_Payroll_Period_Detail.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year == payroll_period.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year && p.tbl_Payroll_Calculation.Calculation_Type == inputModel.DataModel.Calculation_Type && p.tbl_Payroll_Calculation.Organization_ID == inputModel.DataModel.Organization_ID && !(p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_DELETED) && p.Status_Code != GlobalVariable.CONST_STATUS_REJECT).ToList();
                                var SumCalculation = db.tbl_Payroll_Calculation.Where(p => p.id != inputModel.DataModel.id && p.tbl_Payroll_Period_Detail.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year == payroll_period.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year && p.Payroll_Period_ID != inputModel.DataModel.Payroll_Period_ID && p.Calculation_Type == inputModel.DataModel.Calculation_Type && p.Organization_ID == inputModel.DataModel.Organization_ID && !(p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_DELETED) && p.Status_Code != GlobalVariable.CONST_STATUS_REJECT).Count();

                                if (!isPembetulan && SumClosing.Count() != SumCalculation)
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = "Payroll_Period_ID",
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CALCULATION_SETUP_MUST_CLOSED_BEFORE, language)
                                    });
                                }

                                #region Create
                                if (type == GlobalVariable.CONST_CREATE)
                                { 
                                    //tidak boleh ada calculate jika ada calculate in progress di organisasi itu
                                    if (db.tbl_Payroll_Calculation.Where(p => p.Organization_ID == inputModel.DataModel.Organization_ID && p.Calculation_Type == inputModel.DataModel.Calculation_Type && p.Calculate_Status == GlobalVariable.CONST_CALCULATION_STATUS_IN_PROGRESS && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED)).Count() > 0)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = "Payroll_Period_ID",
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CALCULATION_SETUP_PAYROLL_PERIOD_ALREADY_RUNING, language)
                                        });
                                    }
                                    else
                                    {

                                        var ListExistCalculation = db.tbl_Payroll_Calculation.Where(p => p.Payroll_Period_ID == inputModel.DataModel.Payroll_Period_ID && p.Calculation_Type == inputModel.DataModel.Calculation_Type && p.Run == inputModel.DataModel.Run && !(p.Authorize_Status == CoreVariable.CONST_AUTHORIZED && p.Status_Code == CoreVariable.CONST_STATUS_DELETED)).ToList();
                                        //non permanent tidak boleh lebih dari 1 run
                                        if (!isPembetulan && ListExistCalculation.Count() > 0 && inputModel.DataModel.Calculation_Type == "NonPermanent")
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = "Payroll_Period_ID",
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CALCULATION_SETUP_MUST_EDIT, language).Replace("#BATCH", ListExistCalculation.FirstOrDefault().Batch)
                                            });
                                        }
                                        else
                                        {
                                            //permanent ada yg status nya selain reject
                                            if (!isPembetulan && ListExistCalculation.Count() > 0)
                                            {
                                                status = false;
                                                errMessage.Add(new Global_Error_Code()
                                                {
                                                    Error_Code = "Payroll_Period_ID",
                                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CALCULATION_SETUP_MUST_EDIT, language).Replace("#BATCH", ListExistCalculation.FirstOrDefault().Batch)
                                                });
                                            }
                                            else
                                            {
                                                if (inputModel.DataModel.Payroll_Period_ID == null)
                                                {
                                                    status = false;
                                                    errMessage.Add(new Global_Error_Code()
                                                    {
                                                        Error_Code = "Payroll_Period_ID",
                                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CALCULATION_SETUP_PAYROLL_PERIOD_EMPTY, language)
                                                    });
                                                }
                                                //else
                                                //{
                                                //    payroll_period = db.tbl_Payroll_Period_Detail.Where(p => p.id == inputModel.DataModel.Payroll_Period_ID).FirstOrDefault();
                                                //    if (payroll_period.Period_Start_Date.Month == 12)
                                                //    {
                                                //        inputModel.OrgPayroll_Period = db.Get_Payroll_Period(inputModel.DataModel.Organization_ID).FirstOrDefault();
                                                //        if ((inputModel.OrgPayroll_Period.Tax_Year - payroll_period.Period_End_Date.Year) == 1 && UICommonFunction.StatusPeriodPermanent(payroll_period.id) == true)
                                                //        {
                                                //            status = false;
                                                //            errMessage.Add(new Global_Error_Code()
                                                //            {
                                                //                Error_Code = "Payroll_Period_ID",
                                                //                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CALCULATION_NOT_ALLOWED, language)
                                                //            });
                                                //        }
                                                //    }
                                                //}
                                                //jika run kosong
                                                if (inputModel.DataModel.Run == null)
                                                {
                                                    status = false;
                                                    errMessage.Add(new Global_Error_Code()
                                                    {
                                                        Error_Code = "Run",
                                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CALCULATION_SETUP_RUN_EMPTY, language)
                                                    });
                                                }
                                                //jika permanent dan list kosong
                                                if (inputModel.DataModel.Calculation_Type == "Permanent")
                                                {
                                                    if (!inputModel.DataModel.All_Employee)
                                                    {
                                                        if (inputModel.ListEmployeeSelected == null)
                                                        {
                                                            status = false;
                                                            errMessage.Add(new Global_Error_Code()
                                                            {
                                                                Error_Code = "ListEmployeeSelected",
                                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CALCULATION_SETUP_EMPLOYEE_EMPTY, language)
                                                            });
                                                        }
                                                    }
                                                    if (!inputModel.DataModel.All_Component)
                                                    {
                                                        if (inputModel.ListComponentSelected == null)
                                                        {
                                                            status = false;
                                                            errMessage.Add(new Global_Error_Code()
                                                            {
                                                                Error_Code = "ListComponentSelected",
                                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CALCULATION_SETUP_COMPONENT_EMPTY, language)
                                                            });
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (inputModel.DataModel.Recalculate && inputModel.DataModel.Payroll_Period_ID != null)
                                    {
                                        string EventType = "A";
                                        if (db.Validate_Recalculate_OnHistory(EventType, inputModel.DataModel.Organization_ID.ToString(), payroll_period.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year).FirstOrDefault().Recalculate_Status == "N")
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = "Recalculate",
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PTKP_NOT_CHANGED, language)
                                            });
                                        }
                                    }
                                }
                                #endregion

                                #region Edit
                                if (type == GlobalVariable.CONST_EDIT)
                                {
                                    if (inputModel.DataModel.Payroll_Period_ID == null)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = "Payroll_Period_ID",
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CALCULATION_SETUP_PAYROLL_PERIOD_EMPTY, language)
                                        });
                                    }
                                    var ExistPeriod = db.tbl_Payroll_Calculation.Where(p => p.id == inputModel.DataModel.id).FirstOrDefault();
                                    if (ExistPeriod != null)
                                    {
                                        if (ExistPeriod.Calculate_Status == GlobalVariable.CONST_CALCULATION_STATUS_IN_PROGRESS)
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = "Payroll_Period_ID",
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CALCULATION_SETUP_PAYROLL_PERIOD_ALREADY_RUNING, language)
                                            });
                                        }

                                        var ExistPeriod_Severence = db.tbl_Payroll_Calculation.Where(p => p.Batch == ExistPeriod.Batch && p.Calculation_Type == "Severance").FirstOrDefault();
                                        if (ExistPeriod_Severence != null)
                                        {
                                            if (ExistPeriod_Severence.Calculate_Status == GlobalVariable.CONST_CALCULATION_STATUS_IN_PROGRESS)
                                            {
                                                status = false;
                                                errMessage.Add(new Global_Error_Code()
                                                {
                                                    Error_Code = "Payroll_Period_ID",
                                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CALCULATION_SETUP_PAYROLL_SEVERENCE_ALREADY_PROGRESS, language)
                                                });
                                            }
                                        }


                                        //jika run kosong
                                        if (inputModel.DataModel.Run == null)
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = "Run",
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CALCULATION_SETUP_RUN_EMPTY, language)
                                            });
                                        }

                                        //jika permanent dan list kosong
                                        if (inputModel.DataModel.Calculation_Type == "Permanent")
                                        {
                                            if (!inputModel.DataModel.All_Employee)
                                            {
                                                if (inputModel.ListEmployeeSelected == null)
                                                {
                                                    status = false;
                                                    errMessage.Add(new Global_Error_Code()
                                                    {
                                                        Error_Code = "ListEmployeeSelected",
                                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CALCULATION_SETUP_EMPLOYEE_EMPTY, language)
                                                    });
                                                }
                                            }
                                            if (!inputModel.DataModel.All_Component)
                                            {
                                                if (inputModel.ListComponentSelected == null)
                                                {
                                                    status = false;
                                                    errMessage.Add(new Global_Error_Code()
                                                    {
                                                        Error_Code = "ListComponentSelected",
                                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CALCULATION_SETUP_COMPONENT_EMPTY, language)
                                                    });
                                                }
                                            }
                                        }
                                    }

                                    if (inputModel.DataModel.Recalculate && inputModel.DataModel.Payroll_Period_ID != null)
                                    {
                                        string EventType = "M";
                                        if(db.tbl_Payroll_Calculation.Where(p=>p.id == inputModel.DataModel.id && p.Recalculate == false).Count() > 0)
                                        {
                                            EventType = "A";
                                        }
                                        if (db.Validate_Recalculate_OnHistory(EventType, inputModel.DataModel.Organization_ID.ToString(), payroll_period.tbl_Payroll_Period.tbl_Tax_Period.Tax_Year).FirstOrDefault().Recalculate_Status == "N")
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = "Recalculate",
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PTKP_NOT_CHANGED, language)
                                            });
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                }
                else
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "Payroll_Period_ID",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CALCULATION_SETUP_PAYROLL_PERIOD_CLOSED, language)
                    });
                }

            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation Payroll Closing
        public static bool ValidationPayrolClosing(Payroll_Closing inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            { 
                #region Create

                #endregion

                #region Edit

                #endregion
            } 
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

		#region Validation Payroll Slip
        public static bool ValidationPayrolSlip(Global_Payroll_Slip inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {

                    if (inputModel.DataModel.Payroll_Period_ID == null || inputModel.DataModel.Payroll_Period_ID == Guid.Empty)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Payroll_Period_ID",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                        });
                    }
                    else
                    {
                        if (inputModel.ListEmployeeSelected != null && inputModel.DataModel.Payslip_Distribution == "Email")
                        {
                            var Employee_Adddress_Email = db.tbl_Employee_Address.Where(p => inputModel.ListEmployeeSelected.Contains(p.Employee_ID) && !string.IsNullOrEmpty(p.Coorporate_Email_Address) && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && p.Status_Code == GlobalVariable.CONST_STATUS_ACTIVE).Select(p => p.Employee_ID).ToList();
                            if (inputModel.ListEmployeeSelected.Count() != Employee_Adddress_Email.Count())
                            {
                                List<Guid> Empty_Email = new List<Guid>();
                                foreach(var item in inputModel.ListEmployeeSelected)
                                {
                                    if(!Employee_Adddress_Email.Contains(item))
                                    {
                                        Empty_Email.Add(item);
                                    }
                                }

                                var list_employee_name = db.tbl_Employee.Where(p => p.Status_Code == GlobalVariable .CONST_STATUS_ACTIVE && p.Authorize_Status == GlobalVariable.CONST_AUTHORIZED && Empty_Email.Contains(p.id)).Select(p=>p.Full_Name).ToList();
                                if (list_employee_name.Count() > 0)
                                {
                                    string Employee_Name_Empty_Email = string.Join(",", list_employee_name);
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = "ListEmployeeSelected",
                                        Error_Description = Employee_Name_Empty_Email + " " + UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMPLOYEE_EMAIL_EMPTY, language)
                                    });
                                }
                            }
                        }
                    }

                    if (inputModel.DataModel.Run_From == 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Run_From",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                        });
                    }

                    if (inputModel.DataModel.Run_To == 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Run_To",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                        });
                    }

                    if (inputModel.DataModel.Payslip_Distribution == null)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Payslip_Distribution",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                        });
                    }
                    else if (inputModel.DataModel.Payslip_Distribution == "ALL" && inputModel.DataModel.Final_Version == true)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Payslip_Distribution",
                            Error_Description = "All Payslip Distribution only available for Draft Version"//UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                        });
                    }

                    if (inputModel.ListEmployeeSelected == null)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "ListEmployeeSelected",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CALCULATION_SETUP_EMPLOYEE_EMPTY, language)
                        });
                    }

                    if (inputModel.ListComponentSelected == null)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "ListComponentSelected",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CALCULATION_SETUP_COMPONENT_EMPTY, language)
                        });
                    }

                    if (inputModel.DataModel.Location != null && (inputModel.DataModel.Department != null || inputModel.ListEmployeeSelected != null))
                    {
                        status = false;                        
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Location",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PAYSLIP_FILTER_CRITERIA, language)
                        });                                                
                    }

                    if (inputModel.DataModel.Department != null && (inputModel.DataModel.Location != null || inputModel.ListEmployeeSelected != null))
                    {
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Department",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PAYSLIP_FILTER_CRITERIA, language)
                        });
                    }

                    if (inputModel.ListEmployeeSelected != null && (inputModel.DataModel.Location != null || inputModel.DataModel.Department != null))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "ListEmployeeSelected",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PAYSLIP_FILTER_CRITERIA, language)
                        });
                        
                    }

                    if (inputModel.DataModel.Final_Version == true && inputModel.DataModel.Payslip_Distribution == GlobalVariable.CONST_PAYSLIP_DISTIBUTION_EMAIL && string.IsNullOrEmpty(inputModel.DataModel.Email_Note))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Email_Note",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                        });

                    }

                    if (inputModel.DataModel.Final_Version == true && inputModel.DataModel.Payslip_Distribution == GlobalVariable.CONST_PAYSLIP_DISTIBUTION_EMAIL && string.IsNullOrEmpty(inputModel.DataModel.Email_Subject))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Email_Subject",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                        });

                    }


                }
                #endregion
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion
		
        #region validation Tax Period
        public static bool ValidationTaxPeriod(Global_Tax_Period inputModel, Guid Organizatiodselected, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                tbl_Tax_Period valTaxPEriodExist = new tbl_Tax_Period();
                valTaxPEriodExist = GeneralCore.TaxPeriodQuery().Where(p => p.Organization_ID == Organizatiodselected && p.Tax_Year == inputModel.taxPeriodModels.Tax_Year).FirstOrDefault();

                #region create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (valTaxPEriodExist != null)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Message",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TAX_PERIOD_ALREADY_EXISTS, language)
                        });
                    }

                    if (inputModel.taxPeriodModels.Tax_Year == null || inputModel.taxPeriodModels.Tax_Year == 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_TAX_YEAR_IS_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TAX_YEAR_IS_EMPTY, language)
                        });
                    }

                    DateTime NOW = DateTime.Now;
                    DateTime MONTHFROM = Convert.ToDateTime(inputModel.strTaxFrom);
                    DateTime MONTHTO = Convert.ToDateTime(inputModel.strTaxTo);

                    if (inputModel.taxPeriodModels.def == 0)
                    {
                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.strTaxFrom))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "FROM",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TAX_PERIOD_MONTH_IS_EMPTY, language)
                            });
                        }
                        else if (MONTHFROM.Year == NOW.Year && MONTHFROM.Month < NOW.Month || MONTHFROM.Year < NOW.Year)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "FROM",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TAX_PERIOD_TAX_PERIOD_FROM, language)
                            });
                        }

                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.strTaxTo))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "TO",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TAX_PERIOD_MONTH_IS_EMPTY, language)
                            });
                        }
                        else if (MONTHTO.Year == MONTHFROM.Year && MONTHTO.Month < MONTHFROM.Month || MONTHTO.Year < MONTHFROM.Year)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "TO",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TAX_PERIOD_TAX_PERIOD_TO, language)
                            });
                        }

                    }
                }
                #endregion

                #region edit
                if (type == GlobalVariable.CONST_EDIT)
                {


                    if (UICommonFunction.IsValidDuplicateMonth(inputModel.taxPeriodMonthList))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_MONTH_DUPLICATE,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_MONTH_DUPLICATE, language)
                        });
                    }

                    else if (UICommonFunction.IsValidMonthSorting(inputModel.taxPeriodMonthList))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.ERR_MONTH_INCREMENT,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_MONTH_INCREMENT, language)
                        });
                    }

                    //else if (!UICommonFunction.IsValidMonthSorting(inputModel.taxPeriodMonthList))
                    //{
                    //DateTime TaxDateFrom = inputModel.taxPeriodMonthList[0].Tax_Period_Month;
                    //int ExistPayrollPeriod = GeneralCore.PayrollPeriodDetailQuery().Where(p => p.Period_End_Date < TaxDateFrom).Count();

                    //if (ExistPayrollPeriod > 0)
                    //{
                    //    status = false;
                    //    errMessage.Add(new Global_Error_Code()
                    //    {
                    //        Error_Code = "Message",
                    //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TAX_PERIOD_AND_PAYROLL_PERIOD_EDIT, language)
                    //    });
                    //}
                    //}
                }
                #endregion

            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region validation Global Organization Account Group Maintenence
        public static bool ValidationOrganizationAccountGroupMaintenence(Global_Organization_Account_Group_Maintenance inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            InputValidation inputValidation = new InputValidation();
            User_Data USERDATA = new User_Data();
            APP_CORE.CoreVariable Core = new APP_CORE.CoreVariable();
            USERDATA = Core.CoreUserLogin();
            try
            {
                if (type == GlobalVariable.CONST_CREATE)
                {
                    List<tbl_Organization_Account_Group_Maintenence> List = GeneralCore.OrganizationAccountGroupMaintenenceQuery().Where(p => p.Account_No == inputModel.organizationAccountGroupMaintenenceModels.Account_No && p.Organization_ID == USERDATA.OrganizationSelected.id).ToList();
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationAccountGroupMaintenenceModels.Account_No))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "ACCOUNT",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ACCOUNT_NUMBBER_IS_EMPTY, language)
                        });
                    }
                    else if (List.Count > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "ACCOUNT",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ACCOUNT_NUMBBER_ALREADY_EXISTS, language)
                        });
                    }
                }

                if (type == GlobalVariable.CONST_EDIT)
                {
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.organizationAccountGroupMaintenenceModels.Account_No))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "ACCOUNT",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ACCOUNT_NUMBBER_IS_EMPTY, language)
                        });
                    }
                }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation Employee Maintenance
        //#region Payslip Information
        //public static bool ValidationEmployeePayslipInfo(Global_Employee_Payslip_Info inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        //{
        //    status = true;
        //    ModelEntitiesWebsite db = new ModelEntitiesWebsite();
        //    errMessage = new List<Global_Error_Code>();
        //    try
        //    {
        //        inputModel.employeePayslipInfoList = db.tbl_Payslip.Where(s => s.Username == inputModel.employeePayslipInfoModel.Username).ToList();
        //        if (type == GlobalVariable.CONST_CREATE)
        //        {
        //            if (inputModel.employeePayslipInfoList.Count > 0)
        //            {
        //                {
        //                    status = false;
        //                    errMessage.Add(new Global_Error_Code()
        //                    {
        //                        Error_Code = "usernameAlreadyExist",
        //                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BI_BANK_CODE_IS_ALREADY_EXIST, language)
        //                    });
        //                }
        //            }
        //        }

        //        if (type == GlobalVariable.CONST_EDIT)
        //        {
        //            if (inputModel.employeePayslipInfoList.Count > 0)
        //            {
        //                {
        //                    status = false;
        //                    errMessage.Add(new Global_Error_Code()
        //                    {
        //                        Error_Code = "usernameAlreadyExist",
        //                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BI_BANK_CODE_IS_ALREADY_EXIST, language)
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        status = false;
        //    }
        //    return status;
        //}
        //#endregion
        #endregion

        #region Role Menu Function
        public static bool ValidationRoleMenuFunction(Global_RoleMenuFunction inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                if (UICommonFunction.StringIsNullOrEmpty(inputModel.RoleModels.Role_Code))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "rolecode",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ROLE_CODE_IS_EMPTY, language)
                    });
                }
                else
                {
                    if (type == GlobalVariable.CONST_CREATE)
                    {
                        var dbRole = db.tbl_Role.Where(s => s.Organization_ID == inputModel.RoleModels.Organization_ID && s.Role_Code == inputModel.RoleModels.Role_Code).Count();
                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.RoleModels.Role_Code))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "rolecode",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ROLE_CODE_ALREADY_EXIST, language)
                            });
                        }
                    }
                }
                if (UICommonFunction.StringIsNullOrEmpty(inputModel.RoleModels.Role_Description))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "roledescription",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ROLE_DESCRIPTION_IS_EMPTY, language)
                    });
                }
                if (type == GlobalVariable.CONST_CREATE)
                {
                    var dbRole = db.tbl_Role.Where(s => s.Organization_ID == inputModel.RoleModels.Organization_ID && s.Role_Code == inputModel.RoleModels.Role_Code).Count();
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.RoleModels.Role_Code))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_ROLE_CODE_ALREADY_EXIST,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BANK_INFORMATION_BANK_IS_EMPTY, language)
                        });
                    }
                }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }

        #endregion

        #region Validation PayrollPeriod
        public static bool ValidationPayrollPeriod(Global_Payroll_Period inputModel, Guid Organizatiodselected, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();

            try
            {

                DateTime? dateTimeNow = DateTime.Now;

                var valPayrollPeriod = GeneralCore.PayrollPeriodDetailQuery().Where(p => p.tbl_Payroll_Period.Tax_Period_ID == inputModel.taxPeriodID).FirstOrDefault();

                var taxPeriodDateList = new List<DateTime>();
                for (int i = 0; i < inputModel.taxPeriod.Length; i++)
                {
                    string[] splitMonth = inputModel.taxPeriod[i].Split('-');
                    int year = Convert.ToInt16(splitMonth[1]);
                    int period = DateTime.ParseExact(splitMonth[0], "MMM", CultureInfo.CurrentCulture).Month;
                    DateTime taxPeriodDate = new DateTime(year, period, 1);
                    taxPeriodDateList.Add(taxPeriodDate);
                }

                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (valPayrollPeriod != null)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Message",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PAYROLL_PERIOD_ALREADY_EXISTS, language)
                        });
                    }

                    else if (inputModel.taxPeriodID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "errTaxPeriodIDEmpty",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TAX_PERIOD_ID_IS_EMPTY, language)
                        });
                    }

                    else if (UICommonFunction.IsValidDuplicateMonthPeriod(taxPeriodDateList))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "errMonthDuplicate",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_MONTH_TAX_PERIOD_DUPLICATE, language)
                        });
                    }

                    else if (UICommonFunction.IsValidMonthSortingPeriod(taxPeriodDateList))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "errerrMonthIncrement",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_MONTH_TAXPERIOD_INCREMENT, language)
                        });
                    }

                    for (int i = 0; i < inputModel.taxPeriod.Length; i++)
                    {
                        if (UICommonFunction.ConvertToDateTime(inputModel.periodStart[i]) == DateTime.Parse("1/1/0001 12:00:00 AM"))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "errPeriodStartEmpty",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PERIOD_START_IS_EMPTY, language)
                            });
                        }

                        else if (UICommonFunction.ConvertToDateTime(inputModel.periodEnd[i]) == DateTime.Parse("1/1/0001 12:00:00 AM"))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "errPeriodEndEmpty",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PERIOD_END_IS_EMPTY, language)
                            });
                        }
                    }

                    bool overlap = false;
                    bool interval = false;
                    int countYear = inputModel.taxPeriod.Length;
                    for (int i = 0; i < countYear - 1; i++)
                    {
                        if (UICommonFunction.ConvertToDateTime(inputModel.periodStart[i]) != DateTime.Parse("1/1/0001 12:00:00 AM") || UICommonFunction.ConvertToDateTime(inputModel.periodEnd[i]) != DateTime.Parse("1/1/0001 12:00:00 AM"))
                        {
                            if (UICommonFunction.ConvertToDateTime(inputModel.periodStart[i + 1]) < UICommonFunction.ConvertToDateTime(inputModel.periodEnd[i]).AddDays(1))
                            {
                                overlap = UICommonFunction.ConvertToDateTime(inputModel.periodStart[i]) <= UICommonFunction.ConvertToDateTime(inputModel.periodEnd[i + 1]) && UICommonFunction.ConvertToDateTime(inputModel.periodStart[i + 1]) <= UICommonFunction.ConvertToDateTime(inputModel.periodEnd[i]);
                                if (overlap)
                                {
                                    break;
                                }
                            }
                            if (UICommonFunction.ConvertToDateTime(inputModel.periodStart[i + 1]) > UICommonFunction.ConvertToDateTime(inputModel.periodEnd[i]).AddDays(1))
                            {
                                interval = UICommonFunction.ConvertToDateTime(inputModel.periodStart[i + 1]) != UICommonFunction.ConvertToDateTime(inputModel.periodEnd[i]).AddDays(1);
                                if (interval)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    if (overlap)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "errPeriodOverlap",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PAYROLL_PERIOD_CANT_OVERLAP, language)
                        });
                    }

                    if (interval)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "errPeriodInterval",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_INTERVAL_BETWEEN_PAYROLL_PERIOD_DATE, language)
                        });
                    }
                }
                #endregion

                #region Edit
                if (type == GlobalVariable.CONST_EDIT)
                {
                    if (UICommonFunction.IsValidDuplicateMonthPeriod(taxPeriodDateList))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "errMonthDuplicate",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_MONTH_TAX_PERIOD_DUPLICATE, language)
                        });
                    }

                    else if (UICommonFunction.IsValidMonthSortingPeriod(taxPeriodDateList))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "errerrMonthIncrement",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_MONTH_TAXPERIOD_INCREMENT, language)
                        });
                    }

                    bool overlap = false;
                    bool interval = false;
                    int countYear = inputModel.taxPeriod.Length;
                    for (int i = 0; i < countYear - 1; i++)
                    {
                        if (UICommonFunction.ConvertToDateTime(inputModel.periodStart[i]) != DateTime.Parse("1/1/0001 12:00:00 AM") || UICommonFunction.ConvertToDateTime(inputModel.periodEnd[i]) != DateTime.Parse("1/1/0001 12:00:00 AM"))
                        {
                            if (UICommonFunction.ConvertToDateTime(inputModel.periodStart[i + 1]) < UICommonFunction.ConvertToDateTime(inputModel.periodEnd[i]).AddDays(1))
                            {
                                overlap = UICommonFunction.ConvertToDateTime(inputModel.periodStart[i]) <= UICommonFunction.ConvertToDateTime(inputModel.periodEnd[i + 1]) && UICommonFunction.ConvertToDateTime(inputModel.periodStart[i + 1]) <= UICommonFunction.ConvertToDateTime(inputModel.periodEnd[i]);
                                if (overlap)
                                {
                                    break;
                                }
                            }
                            if (UICommonFunction.ConvertToDateTime(inputModel.periodStart[i + 1]) > UICommonFunction.ConvertToDateTime(inputModel.periodEnd[i]).AddDays(1))
                            {
                                interval = UICommonFunction.ConvertToDateTime(inputModel.periodStart[i + 1]) != UICommonFunction.ConvertToDateTime(inputModel.periodEnd[i]).AddDays(1);
                                if (interval)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    if (overlap)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "errPeriodOverlap",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PAYROLL_PERIOD_CANT_OVERLAP, language)
                        });
                    }

                    if (interval)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "errPeriodInterval",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_INTERVAL_BETWEEN_PAYROLL_PERIOD_DATE, language)
                        });
                    }
                }
                #endregion

            }

            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region New Upload
        public static bool ValidationNewUploadHeader(ListColoumnUpload inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                status = false;
                errMessage.Add(new Global_Error_Code()
                {
                    Error_Code = GlobalVariable.CONST_ERR_UPLOAD_HEADER_NOT_EXIST_IN_DATABASE,
                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_UPLOAD_HEADER_NOT_EXIST_IN_DATABASE, language)
                });
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation Upload Is Row Empty
        public static bool ValidationUploadIsRowEMpty(ListColoumnUpload inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                status = false;
                errMessage.Add(new Global_Error_Code()
                {
                    Error_Code = GlobalVariable.CONST_ERR_ROW_IS_EMPTY_UPLOAD,
                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ROW_IS_EMPTY_UPLOAD, language)
                });
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation Field New Upload
        public static List<checkUploadedFile_Result> ValidationNewUpload(List<ListColoumnUpload> dbTblUploadStaging, _uploadData uploadData, string myUploadType, string language)
        {
            bool Error = false;
            string strErrorMessage = "";
            string strColumnName = "";
            int intErrorLine = 0;

            List<checkUploadedFile_Result> ListErrorNewUpload = new List<checkUploadedFile_Result>();
            InputValidation Err_Valid = new InputValidation();
            try
            {
                foreach (var item in dbTblUploadStaging)
                {
                    strErrorMessage = "";
                    strColumnName = "";

                    if (myUploadType == "AdditionalPayrollPermanent" || myUploadType == "AdditionalPayrollNonPermanent" || myUploadType == "YearEndAdditionalIncome")
                    {
                        var CompCode = dbTblUploadStaging.Where(p => p.Col_Name == "Code" && p.Row_Line == item.Row_Line).FirstOrDefault();
                        if (CompCode != null)
                        {
                            if (item.Col_ID != "Full_Name" && item.Col_ID != "Payroll_Period_ID" && item.Col_ID != "Employee_No")
                                strColumnName = CompCode.Value.ToString() + "_" + item.Col_Name;
                            else
                                strColumnName = item.Col_Name;

                        }
                        else
                            strColumnName = item.Col_Name;


                        strErrorMessage = item.Record_Id + "|";
                        intErrorLine = item.Row_Line;

                    }
                    else
                    {
                        strColumnName = item.Col_Name;
                        intErrorLine = item.Row_Line;
                    }

                    if (item.Data_Type == "int")
                    {
                        if (!UICommonFunction.StringIsNullOrEmpty(item.Value))
                        {
                            if (Err_Valid.IsInteger(item.Value.ToString(), "0") == false)
                            {
                                strErrorMessage += UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DATA_MUST_BE_NUMERIC, language);
                                Error = true;
                                InsertErrorNewUpload(ref ListErrorNewUpload, strErrorMessage, intErrorLine, strColumnName);
                            }
                        }
                        //if (UICommonFunction.StringIsNullOrEmpty(item.Value) && item.Is_Mandatory.ToUpper() == "Y")
                        //if (UICommonFunction.StringIsNullOrEmpty(item.Value) && item.Is_Mandatory_Bool == true)
                        //{
                        //    strErrorMessage += UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DATA_MUST_BE_FILLED, language);
                        //    Error = true;
                        //    InsertErrorNewUpload(ref ListErrorNewUpload, strErrorMessage, intErrorLine, strColumnName);
                        //}
                    }
                    else if (item.Data_Type == "numeric")
                    {
                        if (!UICommonFunction.StringIsNullOrEmpty(item.Value))
                        {
                            if (Err_Valid.IsNumeric(item.Value) == false)
                            {
                                strErrorMessage += UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DATA_MUST_BE_NUMERIC, language);
                                Error = true;
                                InsertErrorNewUpload(ref ListErrorNewUpload, strErrorMessage, intErrorLine, strColumnName);
                            }
                        }
                        //if (UICommonFunction.StringIsNullOrEmpty(item.Value) && item.Is_Mandatory.ToUpper() == "Y")
                        //if (UICommonFunction.StringIsNullOrEmpty(item.Value) && item.Is_Mandatory_Bool == true)
                        //{
                        //    strErrorMessage += UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DATA_MUST_BE_FILLED, language);
                        //    Error = true;
                        //    InsertErrorNewUpload(ref ListErrorNewUpload, strErrorMessage, intErrorLine, strColumnName);
                        //}
                    }
                    else if (item.Data_Type == "date")
                    {
                        if (!UICommonFunction.StringIsNullOrEmpty(item.Value))
                        {
                            if (Err_Valid.IsDateTimeTest(item.Value) == false)
                            {
                                strErrorMessage += UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ERR_DATA_MUST_BE_DATETIME, language);
                                Error = true;
                                InsertErrorNewUpload(ref ListErrorNewUpload, strErrorMessage, intErrorLine, strColumnName);
                            }
                        }
                        //if (UICommonFunction.StringIsNullOrEmpty(item.Value) && item.Is_Mandatory.ToUpper() == "Y")
                        if (UICommonFunction.StringIsNullOrEmpty(item.Value) && item.Is_Mandatory_Bool == true)
                        {
                            strErrorMessage += UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DATA_MUST_BE_FILLED, language);
                            Error = true;
                            InsertErrorNewUpload(ref ListErrorNewUpload, strErrorMessage, intErrorLine, strColumnName);
                        }
                    }
                    else if (item.Data_Type == "datetime")
                    {
                        if (!UICommonFunction.StringIsNullOrEmpty(item.Value))
                        {
                            if (Err_Valid.IsDateTimeTest(item.Value) == false)
                            {
                                strErrorMessage += UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ERR_DATA_MUST_BE_DATETIME, language);
                                Error = true;
                                InsertErrorNewUpload(ref ListErrorNewUpload, strErrorMessage, intErrorLine, strColumnName);
                            }
                        }
                        //if (UICommonFunction.StringIsNullOrEmpty(item.Value) && item.Is_Mandatory.ToUpper() == "Y")
                        if (UICommonFunction.StringIsNullOrEmpty(item.Value) && item.Is_Mandatory_Bool == true)
                        {
                            strErrorMessage += UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_DATA_MUST_BE_FILLED, language);
                            Error = true;
                            InsertErrorNewUpload(ref ListErrorNewUpload, strErrorMessage, intErrorLine, strColumnName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strErrorMessage = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_SYSTEM_TEMPORARY_UNAVAILABLE, language);
                strColumnName = "System and Connection";
                Error = true;
                InsertErrorNewUpload(ref ListErrorNewUpload, strErrorMessage, intErrorLine, strColumnName);
                UIException.LogException(ex, "public static List<ErrorNewUpload> ValidationNewUpload()", "");
            }
            return ListErrorNewUpload;
        }
        #endregion

        #region General Upload
        //public static void InsertErrorNewUpload(List<Global_Upload_Data.ListColoumnUpload> dbTblUploadStaging, ref List<ErrorNewUpload> ListErrorNewUpload, string strErrorMessage, int intErrorLine, string strColumnName)
        public static void InsertErrorNewUpload(ref List<ErrorNewUpload> ListErrorNewUpload, string strErrorMessage, int intErrorLine, string strColumnName)
        {
            ListErrorNewUpload.Add(new ErrorNewUpload
            {
                ID = Guid.NewGuid().ToString("N"),
                ERROR_LINE = intErrorLine,
                COLUMN_NAME = strColumnName,
                ERROR_MESSAGE = strErrorMessage,
                STATUS = 0
            });
        }

        public static void InsertErrorNewUpload(ref List<checkUploadedFile_Result> ListErrorNewUpload, string strErrorMessage, int intErrorLine, string strColumnName)
        {
            ListErrorNewUpload.Add(new checkUploadedFile_Result
            {
                Id = Guid.NewGuid().ToString("N"),
                Error_line = intErrorLine,
                Column_name = strColumnName,
                Error_message = strErrorMessage,
                Type = 0
            });
        }
        #endregion

        #region validation Employee Payment Information
        public static bool ValidationEmployeePaymentInformation(Global_Employee_Payment_Information inputModel, User_Data USERDATA, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                //public const string ERR_EMP_PAYROLL_ALREADY_EXIST = "ERR_EMP_PAYROLL_ALREADY_EXIST" //employee can only have 1 (one) payroll payment type, either bank transfer or cash
                //public const string ERR_EMP_PAYROLL_MUST_HAVE_DETAIL = "ERR_EMP_PAYROLL_MUST_HAVE_DETAIL"  //employee should have at least 1 (one) bank account
                //public const string ERR_EMP_PAYROLL_DIFFRENT_CURRENCY = "ERR_EMP_PAYROLL_DIFFRENT_CURRENCY"; //The employee bank accounts can have the same currency code or different currency code
                //public const string ERR_EMP_PERCENTAGE_PAYROLL_100 = "ERR_EMP_PERCENTAGE_PAYROLL_100"; //the total of percentage must be 100%
                //public const string ERR_EMP_SAME_PRIORITY = "ERR_EMP_SAME_PRIORITY";  //Priority from one account with another account should be differentiated

                if (type == GlobalVariable.CONST_CREATE)
                {
                    var PaymentInformationCount = GeneralCore.EmployeePaymentInformationQuery().Where(p => p.Employee_ID == inputModel.DataModel.Employee_ID).Take(1).Count();
                    if (PaymentInformationCount > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Payment_Type_ID",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_ALREADY_EXIST, language)
                        });
                    }
                    else
                    {
                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.DataModel.Payment_Type_ID))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Payment_Type_ID",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_VALID_PAYMENT_TYPE, language)
                            });
                        }
                        if (inputModel.DataModel.Payment_Type_ID.ToUpper() != "CASH")
                        {
                            if (inputModel.DataModel.Payment_Bank_ID == null)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "Payment_Bank_ID",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_VALID_PAYMENT_BANK, language)
                                });
                            }
                            //if (inputModel.List.Where(p => p.Employee_Bank_ID == null).Count() > 0)
                            //{
                            //    status = false;
                            //    errMessage.Add(new Global_Error_Code()
                            //    {
                            //        Error_Code = "Employee_Bank_ID",
                            //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_VALID_EMPLOYEE_BANK, language)
                            //    });
                            //}
                            if (inputModel.List.Where(p => p.Employee_Bank_Branch_ID == null).Count() > 0)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "Employee_Bank_Branch_ID",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_VALID_EMPLOYEE_BANK_BRANCH, language)
                                });
                            }
                            if (inputModel.List.Where(p => p.Currency_Code == null).Count() > 0)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "Currency_Code",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_VALID_CURRENCY, language)
                                });
                            }
                            if (inputModel.List.Where(p => p.Account_Name == null).Count() > 0)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "Account_Name",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_VALID_ACCOUNT_NAME, language)
                                });
                            }

                            if (inputModel.List.Where(p => p.Employee_Bank_Account != null).Count() == 0)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "Employee_Bank_Account",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_MUST_HAVE_DETAIL, language)
                                });
                            }
                            else if (inputModel.List.Where(p => p.Employee_Bank_Account == null).Count() > 0)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "Employee_Bank_Account",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_VALID_ACCOUNT, language)
                                });
                            }

                            if (inputModel.List.Sum(p => p.Percentage) == 0 && inputModel.List.Sum(p => p.Amount) == 0)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "Percentage",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_VALID_PERCENTAGE, language)
                                });
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "Amount",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_VALID_AMOUNT, language)
                                });
                            }
                            else
                            {
                                //if percentage
                                if (inputModel.List.Where(p => p.Percentage != null).Count() > 0)
                                {
                                    if (inputModel.List.Sum(p => p.Percentage) == 0)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = "Percentage",
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_VALID_PERCENTAGE, language)
                                        });
                                    }
                                    else if (inputModel.List.Sum(p => p.Percentage) != 100)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = "Percentage",
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_PERCENTAGE_PAYROLL_100, language)
                                        });
                                    }
                                }
                                else
                                {
                                    if (inputModel.List.Sum(p => p.Amount) == 0)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = "Amount",
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_VALID_AMOUNT, language)
                                        });
                                    }
                                    if (inputModel.List.Where(p => p.Amount == null).Count() > 0)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = "Amount",
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_VALID_AMOUNT, language)
                                        });
                                    }


                                    if (inputModel.List.Where(p => p.Priority == null).Count() > 0)
                                    {
                                        status = false;
                                        errMessage.Add(new Global_Error_Code()
                                        {
                                            Error_Code = "Priority",
                                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_VALID_PRIORITY, language)
                                        });
                                    }
                                    else
                                    {
                                        bool isnotValidPriority = false;
                                        int i = 1;
                                        foreach (var item in inputModel.List.OrderBy(p => p.Priority))
                                        {
                                            if (i != item.Priority)
                                            {
                                                isnotValidPriority = true;
                                                break;
                                            }
                                            i++;
                                        }
                                        if (isnotValidPriority)
                                        {
                                            status = false;
                                            errMessage.Add(new Global_Error_Code()
                                            {
                                                Error_Code = "Priority",
                                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_JUMP_PRIORITY, language)
                                            });
                                        }
                                    }
                                }
                            }
                        }


                    }
                }
                else if (type == GlobalVariable.CONST_EDIT)
                {
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.DataModel.Payment_Type_ID))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Payment_Type_ID",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_VALID_PAYMENT_TYPE, language)
                        });
                    }
                    if (inputModel.DataModel.Payment_Type_ID.ToUpper() != "CASH")
                    {
                        //if (inputModel.List.Where(p => p.Employee_Bank_ID != null).Count() == 0)
                        //{
                        //    status = false;
                        //    errMessage.Add(new Global_Error_Code()
                        //    {
                        //        Error_Code = "Employee_Bank_ID",
                        //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_VALID_EMPLOYEE_BANK, language)
                        //    });
                        //}
                        if (inputModel.List.Where(p => p.Employee_Bank_Branch_ID == null).Count() > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Employee_Bank_Branch_ID",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_VALID_EMPLOYEE_BANK_BRANCH, language)
                            });
                        }
                        if (inputModel.List.Where(p => p.Employee_Bank_Account != null).Count() == 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Employee_Bank_Account",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_MUST_HAVE_DETAIL, language)
                            });
                        }
                        if (inputModel.List.Where(p => p.Employee_Bank_Account == null).Count() > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Employee_Bank_Account",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_VALID_ACCOUNT, language)
                            });
                        }
                        if (inputModel.List.Where(p => p.Currency_Code == null).Count() > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Currency_Code",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_VALID_CURRENCY, language)
                            });
                        }

                        //if percentage
                        if (inputModel.List.Where(p => p.Percentage != null).Count() > 0)
                        {
                            if (inputModel.List.Sum(p => p.Percentage) == 0)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "Percentage",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_VALID_PERCENTAGE, language)
                                });
                            }
                            if (inputModel.List.Sum(p => p.Percentage) != 100)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "Percentage",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_PERCENTAGE_PAYROLL_100, language)
                                });
                            }

                        }
                        else
                        {
                            if (inputModel.List.Sum(p => p.Amount) == 0)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "Amount",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_VALID_AMOUNT, language)
                                });
                            }
                            else if (inputModel.List.Where(p => p.Amount == null).Count() > 0)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "Amount",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_VALID_AMOUNT, language)
                                });
                            }


                            if (inputModel.List.Where(p => p.Priority == null).Count() > 0)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "Priority",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_VALID_PRIORITY, language)
                                });
                            }
                            else
                            {
                                bool isnotValidPriority = false;
                                int i = 1;
                                foreach (var item in inputModel.List.OrderBy(p => p.Priority))
                                {
                                    if (i != item.Priority)
                                    {
                                        isnotValidPriority = true;
                                        break;
                                    }
                                    i++;
                                }
                                if (isnotValidPriority)
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = "Priority",
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.ERR_EMP_PAYROLL_JUMP_PRIORITY, language)
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation PayrollSchedule
        public static bool ValidationPayrollSchedule(Global_Payroll_Schedule inputModel, Guid Organizatiodselected, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            User_Data USERDATA = new User_Data();
            APP_CORE.CoreVariable Core = new APP_CORE.CoreVariable();
            USERDATA = Core.CoreUserLogin();
            try
            {

                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    inputModel.payrollScheduleList = GeneralCore.PayrollScheduleQuery().ToList();
                    var PayrollPeriod = USERDATA.Payroll_Period_Id;
                    if (PayrollPeriod != null)
                    {
                        if (inputModel.payrollScheduleList.Count > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Message",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PAYROLL_SCHEDULE_IS_ALREADY_EXIST, language)
                            });
                        }
                        int PayrollEndDate = Convert.ToInt32(USERDATA.PayrollEndDate.Value.Day);
                        if (inputModel.payrollScheduleModel.Cut_Off_Time_Start == 1)
                        {
                            if (PayrollEndDate != inputModel.payrollScheduleModel.Cut_Off_Time_End)
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.CONST_ERR_PAYROLL_SCHEDULE_CUT_OFF_NOT_MATCH,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PAYROLL_SCHEDULE_CUT_OFF_NOT_MATCH, language)
                                });
                            }
                        }
                    }
                    else
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_PAYROLL_SCHEDULE_PERIOD_DOES_NOT_EXIST,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PAYROLL_SCHEDULE_PERIOD_DOES_NOT_EXIST, language)
                        });
                    }
                }
                #endregion

                #region Edit
                if (type == GlobalVariable.CONST_EDIT)
                {
                    inputModel.payrollScheduleList = db.tbl_Payroll_Schedule.Where(s => s.id == inputModel.payrollScheduleModel.id).ToList();

                    if (inputModel.payrollScheduleList.FirstOrDefault().Organization_ID != Organizatiodselected)
                    {
                        inputModel.payrollScheduleList = db.tbl_Payroll_Schedule.Where(s => s.Organization_ID == Organizatiodselected).Take(1).ToList();
                        if (inputModel.payrollScheduleList.Count() > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Message",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PAYROLL_SCHEDULE_IS_ALREADY_EXIST, language)
                            });
                        }
                    }
                    int PayrollEndDate = Convert.ToInt32(USERDATA.PayrollEndDate.Value.Day);
                    if (inputModel.payrollScheduleModel.Cut_Off_Time_Start == 1)
                    {
                        if (PayrollEndDate != inputModel.payrollScheduleModel.Cut_Off_Time_End)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_PAYROLL_SCHEDULE_CUT_OFF_NOT_MATCH,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PAYROLL_SCHEDULE_CUT_OFF_NOT_MATCH, language)
                            });
                        }
                    }
                }
                #endregion
            }

            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validasi email
        public bool EmailIsValid(string emailaddress)
        {
            try
            {
                MailAddress Email = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        #endregion

        #region Validation Loan
        public static bool ValidationLoan(Global_Loan inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                inputModel.viewLoanList = GeneralCore.vwLoanQuery().Where(p => p.Employee == inputModel.loanModels.Employee_ID && p.Component_Linkage_ID == inputModel.loanModels.Component_Linkage && p.Status_Code != CoreVariable.CONST_STATUS_INACTIVE).ToList();

                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (inputModel.viewLoanList.Count > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Message",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_LOAN_EMPLOYEE_AND_LINKAGE_ALREADY_EXISTS, language)
                        });
                    }
                    else
                    {
                        if (inputModel.loanModels.Tenor > 600)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "maximumTenor",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_LOAN_MAXIMUM_TENOR, language)
                            });
                        }
                        if (inputModel.loanModels.Employee_ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "employeeIDEmpty",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_LOAN_EMPLOYEE_ID_EMPTY, language)
                            });
                        }

                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.loanModels.Type_Loan))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "typeLoanEmpty",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TYPE_LOAN_EMPTY, language)
                            });
                        }

                        if (inputModel.loanModels.Component_Linkage == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "componentLinkageEmpty",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_LOAN_COMPONENT_LINKAGE_EMPTY, language)
                            });
                        }

                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.loanModels.Loan_Amount.ToString()) || inputModel.loanModels.Loan_Amount == 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "loanAmountEmpty",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_LOAN_AMOUNT_EMPTY, language)
                            });
                        }

                        //if (UICommonFunction.StringIsNullOrEmpty(inputModel.loanModels.Outstanding_Loan_Amount.ToString()))
                        //{
                        //    status = false;
                        //    errMessage.Add(new Global_Error_Code()
                        //    {
                        //        Error_Code = "outstandingLoanAmountEmpty",
                        //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_OUTSTANDING_LOAN_AMOUNT_EMPTY, language)
                        //    });
                        //}

                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.loanModels.Installment_Amount.ToString()) || inputModel.loanModels.Installment_Amount == 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "installmentAmountEmpty",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_LOAN_INSTALLMENT_AMOUNT_EMPTY, language)
                            });
                        }

                        if (UICommonFunction.StringIsNullOrEmpty(inputModel.loanModels.Tenor.ToString()) || inputModel.loanModels.Tenor == 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "tenorEmpty",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_LOAN_TENOR_EMPTY, language)
                            });
                        }

                        //if (UICommonFunction.StringIsNullOrEmpty(inputModel.loanModels.Outstanding_Tenor.ToString()))
                        //{
                        //    status = false;
                        //    errMessage.Add(new Global_Error_Code()
                        //    {
                        //        Error_Code = "outstandingTenorEmpty",
                        //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_LOAN_OUSTANDING_TENOR_EMPTY, language)
                        //    });
                        //}
                    }
                }
                #endregion

                #region Edit
                if (type == GlobalVariable.CONST_EDIT)
                {
                    //var input_Loan = inputModel.loanModels.Outstanding_Loan_Amount;
                    //var input_Installment = inputModel.loanModels.Installment_Amount;
                    //var Ousts_Tenor = input_Loan / input_Installment;
                    //var rounde_Ousts_Tenor = Math.Round(Ousts_Tenor, 0);
                    //if (inputModel.loanModels.Outstanding_Tenor != rounde_Ousts_Tenor)
                    //{
                    //    status = false;
                    //    errMessage.Add(new Global_Error_Code()
                    //    {
                    //        Error_Code = "oustNotValid",
                    //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_LOAN_OUSTANDING_TENOR_NOT_VALID, language)
                    //    });
                    //}
                    if (inputModel.loanModels.Tenor > 600)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "maximumTenor",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_LOAN_MAXIMUM_TENOR, language)
                        });
                    }
                    if (inputModel.loanModels.Employee_ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "employeeIDEmpty",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_LOAN_EMPLOYEE_ID_EMPTY, language)
                        });
                    }

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.loanModels.Type_Loan))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "typeLoanEmpty",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TYPE_LOAN_EMPTY, language)
                        });
                    }

                    if (inputModel.loanModels.Component_Linkage == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "componentLinkageEmpty",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_LOAN_COMPONENT_LINKAGE_EMPTY, language)
                        });
                    }

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.loanModels.Loan_Amount.ToString()) || inputModel.loanModels.Loan_Amount == 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "loanAmountEmpty",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_LOAN_AMOUNT_EMPTY, language)
                        });
                    }

                    //if (UICommonFunction.StringIsNullOrEmpty(inputModel.loanModels.Outstanding_Loan_Amount.ToString()))
                    //{
                    //    status = false;
                    //    errMessage.Add(new Global_Error_Code()
                    //    {
                    //        Error_Code = "outstandingLoanAmountEmpty",
                    //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_OUTSTANDING_LOAN_AMOUNT_EMPTY, language)
                    //    });
                    //}

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.loanModels.Installment_Amount.ToString()) || inputModel.loanModels.Installment_Amount == 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "installmentAmountEmpty",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_LOAN_INSTALLMENT_AMOUNT_EMPTY, language)
                        });
                    }

                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.loanModels.Tenor.ToString()) || inputModel.loanModels.Tenor == 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "tenorEmpty",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_LOAN_TENOR_EMPTY, language)
                        });
                    }

                    //if (UICommonFunction.StringIsNullOrEmpty(inputModel.loanModels.Outstanding_Tenor.ToString()))
                    //{
                    //    status = false;
                    //    errMessage.Add(new Global_Error_Code()
                    //    {
                    //        Error_Code = "outstandingTenorEmpty",
                    //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_LOAN_OUSTANDING_TENOR_EMPTY, language)
                    //    });
                    //}
                }
                #endregion
            }

            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation CompensationAndBenefit
        public static bool ValidationCompensationAndBenefit(Global_Compensation_Benefit inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                inputModel.viewCompensationBenefitList = GeneralCore.vwCompensationAndBenefitQuery().Where(p => p.Employee == inputModel.compensationBenefitModels.Employee_ID && p.Component_Linkage_ID == inputModel.compensationBenefitModels.Component_Linkage && p.Status_Code != CoreVariable.CONST_STATUS_INACTIVE).ToList();

                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (inputModel.viewCompensationBenefitList.Count > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Message",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BENEFIT_EMPLOYEE_AND_LINKAGE_ALREADY_EXISTS, language)
                        });
                    }
                }
                #endregion

                #region Edit
                if (type == GlobalVariable.CONST_EDIT)
                {
                }
                #endregion

                if (inputModel.compensationBenefitModels.Employee_ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "employeeIDEmpty",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BENEFIT_EMPLOYEE_ID_EMPTY, language)
                    });
                }

                if (UICommonFunction.StringIsNullOrEmpty(inputModel.compensationBenefitModels.Type_Compensation_Benefit))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "typeBenefitEmpty",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TYPE_BENEFIT_EMPTY, language)
                    });
                }

                if (inputModel.compensationBenefitModels.Component_Linkage == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "componentLinkageEmpty",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BENEFIT_COMPONENT_LINKAGE_EMPTY, language)
                    });
                }

                if (UICommonFunction.StringIsNullOrEmpty(inputModel.compensationBenefitModels.Budget.ToString()))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "periodBudgetEmpty",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_BENEFIT_BUDGET_EMPTY, language)
                    });
                }

                if (UICommonFunction.StringIsNullOrEmpty(inputModel.compensationBenefitModels.Period))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "periodBenefitEmpty",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PERIOD_BENEFIT_EMPTY, language)
                    });
                }
            }

            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation OrganizationEmailSetup
        public static bool ValidationOrganizationEmailSetup(Global_Organization_Email_Setup inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                inputModel.organizationEmailSetupList = GeneralCore.OrganizationEmailSetup().Where(p => p.Organization_ID == inputModel.organizationEmailSetupModels.Organization_ID).ToList();

                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (inputModel.organizationEmailSetupList.Count > 0)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Message",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ORGANIZATION_EMAIL_SETUP_EXISTS, language)
                            });
                        }
                    }
                    else
                    {
                        if (inputModel.organizationEmailSetupModels.Email_Address == null)
                        {
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "Email_Address",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                                });
                            }
                        }
                        if (inputModel.organizationEmailSetupModels.SMTP_Server == null)
                        {
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "SMTP_Server",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                                });
                            }
                        }
                        if (inputModel.organizationEmailSetupModels.Port == null)
                        {
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "Port",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                                });
                            }
                        }
                        if (inputModel.organizationEmailSetupModels.SMTP_User == null)
                        {
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "SMTP_User",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                                });
                            }
                        }
                        if (inputModel.organizationEmailSetupModels.SMTP_Password == null)
                        {
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "SMTP_Password",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                                });
                            }
                        }
                        if (inputModel.organizationEmailSetupModels.Email_Subject == null)
                        {
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "Email_Subject",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                                });
                            }
                        }
                        if (inputModel.organizationEmailSetupModels.Email_Note == null)
                        {
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "Email_Note",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                                });
                            }
                        }
                    }
                }
                #endregion

                #region Edit
                if (type == GlobalVariable.CONST_EDIT)
                {
                    if (inputModel.organizationEmailSetupModels.Email_Address == null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Email_Address",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (inputModel.organizationEmailSetupModels.SMTP_Server == null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "SMTP_Server",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (inputModel.organizationEmailSetupModels.Port == null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Port",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (inputModel.organizationEmailSetupModels.SMTP_User == null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "SMTP_User",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (inputModel.organizationEmailSetupModels.SMTP_Password == null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "SMTP_Password",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (inputModel.organizationEmailSetupModels.Email_Subject == null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Email_Subject",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (inputModel.organizationEmailSetupModels.Email_Note == null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Email_Note",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (inputModel.organizationEmailSetupModels.Status_Code == null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Authorize_Status",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                }
                #endregion
            }

            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation ValidationReport1721Bulanan
        public static bool ValidationReport1721Bulanan(Global_Report_1721_Bulanan inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (inputModel.selectedOrganizationTaxID == null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "selectedOrganizationTaxID",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }

                    if (inputModel.selectedPayrollPeriod == null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "selectedPayrollPeriod",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }

                    if (inputModel.selectedCorrection == null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "selectedCorrection",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }

                    if (inputModel.selected_All_Employee == null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "selected_All_Employee",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                }
                #endregion
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation Report A1
        public static bool ValidationReportA1(Global_Tax_Reporting_A1 inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (inputModel.selectedOrganizationTaxID == null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "selectedOrganizationTaxID",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (inputModel.selectedFromPayrollPeriod != null && inputModel.selectedToPayrollPeriod != null)
                    {
                        if (inputModel.selectedFromPayrollPeriod.Value.Year != inputModel.selectedToPayrollPeriod.Value.Year)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_TAX_PERIOD_FROM_AND_TAX_PERIOD_TO_SHOULD_BE_SAME_YEAR,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TAX_PERIOD_FROM_AND_TAX_PERIOD_TO_SHOULD_BE_SAME_YEAR, language)
                            });
                        }
                        if (inputModel.selectedToPayrollPeriod < inputModel.selectedFromPayrollPeriod)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_TAX_PERIOD_TO_LESS_THAN_TAX_PERIOD_FROM,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TAX_PERIOD_TO_LESS_THAN_TAX_PERIOD_FROM, language)
                            });
                        }
                    }
                    if (inputModel.selectedFromPayrollPeriod == null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "selectedFromPayrollPeriod",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (inputModel.selectedEmployeeStatus == null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "selectedEmployeeStatus",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (inputModel.dateSelected == null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "dateSelected",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    //if (inputModel.CheckIncludeSignature == false && (Convert.ToInt32(inputModel.OrganizationSignatureLogo) <= 0 || String.IsNullOrEmpty(inputModel.OrganizationSignatureLogo)))
                    if(inputModel.typeFileDownload == "PDF")
                    {
                        if (inputModel.CheckIncludeSignature == false && String.IsNullOrEmpty(inputModel.OrganizationSignatureLogo))
                        {
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.CONST_ERR_1721_A1_SIGNATURE_IS_EMPTY,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_1721_A1_SIGNATURE_IS_EMPTY, language)
                                });
                            }
                        }
                    }
                }
                #endregion
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation Report 1721 Final
        public static bool ValidationReport1721Final(Global_Report_1721_Final inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (inputModel.selectedOrganizationTaxID == null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "selectedOrganizationTaxID",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (inputModel.selectedPayrollPeriod == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "selectedPayrollPeriod",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                }
                #endregion
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation Report 1721 Tidak Final
        public static bool ValidationReport1721TidakFinal(Global_Report_1721_Not_Final inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (inputModel.selectedOrganizationTaxID == null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "selectedOrganizationTaxID",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (inputModel.selectedPayrollPeriod == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "selectedPayrollPeriod",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (inputModel.typeFileDownload == "PDF")
                    {
                        if (inputModel.CheckIncludeSignature == false && String.IsNullOrEmpty(inputModel.OrganizationSignatureLogo))
                        {
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = GlobalVariable.CONST_ERR_1721_A1_SIGNATURE_IS_EMPTY,
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_1721_A1_SIGNATURE_IS_EMPTY, language)
                                });
                            }
                        }
                    }
                }
                #endregion
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation Report FundRequisition
        public static bool ValidationReportFundRequisition(Global_Fund_Requisition inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (inputModel.selectedPayrollPeriod == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "selectedPayrollPeriod",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (inputModel.run == null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "run",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (inputModel.clientPICName == null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "clientPICName",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (inputModel.transferTo == null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "transferTo",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (inputModel.typeFileDownload == null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "typeFileDownload",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                }
                #endregion
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation ValidationReportSalaryJournal
        public static bool ValidationReportSalaryJournal(Global_Salary_Journal_Report inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (inputModel.selectedTaxPeriodID == null || inputModel.selectedTaxPeriodID == "")
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "selectedTaxPeriodID",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }

                    if (inputModel.selectedEmploymentStatus == null || inputModel.selectedEmploymentStatus == "")
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "selectedEmploymentStatus",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }

                    if (inputModel.selectedCalculationRun == null || inputModel.selectedCalculationRun == "")
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "selectedCalculationRun",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (inputModel.typeFileDownload == null || inputModel.typeFileDownload == "")
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "typeFileDownload",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                }
                #endregion
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation Report Payroll Comparative
        public static bool ValidationReportPayrollComparative(Global_Payroll_Comparative_Report inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (inputModel.selectedPayrollPeriod == null || inputModel.selectedPayrollPeriod == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "selectedPayrollPeriod",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (string.IsNullOrEmpty(inputModel.selectedComparativeType))
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "selectedComparativeType",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                }
                #endregion
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion
        
        #region Validation Report Additional Payroll
        public static bool ValidationReportAdditionalPayroll(Global_Generate_Template_Additional inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                #region Create
                
                if (string.IsNullOrEmpty(inputModel.selectedEmployeementStatus))
                {
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "MANDATORY_1",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                        });
                    }
                }

                if (inputModel.selectedPayrollPeriod == Guid.Empty)
                {
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "MANDATORY_2",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                        });
                    }
                }
                #endregion
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation Change Password
        public static bool ValidationChangePassword(Global_Change_Password inputModel, string language, string type, Guid userIDSelected, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                #region Edit
                if (type == GlobalVariable.CONST_EDIT)
                {
                    if (inputModel.currentPassword == null || inputModel.newPassword == null || inputModel.confirmNewPassword == null)
                    {
                        if (inputModel.currentPassword == null)
                        {
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "currentPassword",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                                });
                            }
                        }
                        if (inputModel.newPassword == null)
                        {
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "newPassword",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                                });
                            }
                        }
                        if (inputModel.confirmNewPassword == null)
                        {
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "confirmNewPassword",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                                });
                            }
                        }
                    }
                    else
                    {
                        var userEdit = db.tbl_User.Where(m => m.id == userIDSelected).FirstOrDefault();
                        if (userEdit != null)
                        {
                            var currentPassword = UICommonFunction.Decrypt(userEdit.Password);
                            if (inputModel.currentPassword != currentPassword)
                            {
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = "currentPassword2",
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CURRENTPASSWORD, language)
                                    });
                                }
                            }
                            if (inputModel.newPassword != inputModel.confirmNewPassword)
                            {
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = "confirmNewPassword2",
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CONFIRMNEWPASSWORD, language)
                                    });
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation Change Password
        public static bool ValidationESSChangePassword(Global_Change_Password inputModel, string language, string type, Guid userIDSelected, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                #region Edit
                if (type == GlobalVariable.CONST_EDIT)
                {
                    if (inputModel.currentPassword == null || inputModel.newPassword == null || inputModel.confirmNewPassword == null)
                    {
                        if (inputModel.currentPassword == null)
                        {
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "currentPassword",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                                });
                            }
                        }
                        if (inputModel.newPassword == null)
                        {
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "newPassword",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                                });
                            }
                        }
                        if (inputModel.confirmNewPassword == null)
                        {
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "confirmNewPassword",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                                });
                            }
                        }
                    }
                    else
                    {
                        tbl_Payslip userEdit = new tbl_Payslip();
                        var UserMobile = db.vw_User_Mobile.Where(p => p.id == userIDSelected).FirstOrDefault();
                        if (UserMobile != null)
                        {
                            userEdit = db.tbl_Payslip.Where(m => m.Employee_ID == UserMobile.Employee_Id).FirstOrDefault();
                        }
                        if (userEdit != null)
                        {
                            var currentPassword = UICommonFunction.Decrypt(userEdit.Password);
                            if (inputModel.currentPassword != currentPassword)
                            {
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = "currentPassword2",
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CURRENTPASSWORD, language)
                                    });
                                }
                            }
                            if (inputModel.newPassword != inputModel.confirmNewPassword)
                            {
                                {
                                    status = false;
                                    errMessage.Add(new Global_Error_Code()
                                    {
                                        Error_Code = "confirmNewPassword2",
                                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_CONFIRMNEWPASSWORD, language)
                                    });
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Payroll Report Generation
        public static bool ValidationPayroll(Global_Report_Employee_Payslip_Template inputModel, string language, User_Data inputData, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                if (inputModel.ReportName == null)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_REPORT_NAME_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_REPORT_NAME_EMPTY, language) //ubah
                    });
                }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region validation User
        public static bool ValidationMasterEmployeeLeave(Global_Employee_Leave inputModel, string language, string type, Guid OrganizationSelected, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();

            try
            {
                if (inputModel.employeeLeaveMaster.Leave_Type == null)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_LEAVE_TYPE_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_LEAVE_TYPE_EMPTY, language)
                    });
                }

                if (inputModel.employeeLeaveMaster.Entitlement == null)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_ENTITLEMENT_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ENTITLEMENT_EMPTY, language)
                    });
                }

                try
                {
                    if (inputModel.Strat == null)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_PERIOD_START_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PERIOD_START_EMPTY, language)
                        });
                    }

                    if (inputModel.End == null)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_PERIOD_END_EMPTY,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PERIOD_END_EMPTY, language) //ubah
                        });
                    }

                    if (inputModel.Strat != null && inputModel.End != null)
                    {
                        DateTime From = UICommonFunction.ConvertToDateTime(inputModel.Strat);
                        DateTime To = UICommonFunction.ConvertToDateTime(inputModel.End);
                        if (From > To)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_PERIOD_GRETER,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_PERIOD_GRETER, language) //ubah
                            });
                        }
                    }
                }
                catch (Exception E)
                {
                    status = false;
                    ErrorMessage = E.ToString();

                    if (E.InnerException != null)
                    {
                        ErrorMessage = ErrorMessage + E.InnerException.ToString();
                    }
                    UIException.LogException(ErrorMessage, E.StackTrace);
                }

                if (inputModel.Structure == null)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_STRUCTURE_FILTER_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_STRUCTURE_FILTER_EMPTY, language)
                    });
                }

                if (inputModel.ListEmployeeSelected == null)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_EMPLOYEE_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMPLOYEE_EMPTY, language) //ubah
                    });
                }
                
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation Report Emmployee Deduction Schedule
        public static bool ValidationReportEmmployeeDeductionSchedule(Global_Employee_Deduction_Schedule_Report inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    if (inputModel.empNoID == null || inputModel.empNoID == "")
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "empNoID",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (inputModel.comLinkageID == null || inputModel.comLinkageID == "")
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "comLinkageID",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (inputModel.typeFileDownload == null || inputModel.typeFileDownload == "")
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "typeFileDownload",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                }
                #endregion
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Payroll Report Generation
        public static bool ValidationTemplateConversion(Global_Template_Conversion inputModel, HttpPostedFileBase file, string language, User_Data inputData, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                if (String.IsNullOrEmpty(inputModel.Template_Type))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_TEMPLATE_TYPE_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TEMPLATE_TYPE_EMPTY, language) //ubah
                    });
                }
                if (file.ContentLength == 0)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_FILE_DOWNLOAD_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FILE_DOWNLOAD_EMPTY, language) //ubah
                    });
                }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation System Parameter
        public static bool ValidationSystemParameter(Global_System_Parameter inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                //#region Create
                //if (type == GlobalVariable.CONST_CREATE)
                //{
                //    inputModel.holidayCalendarList = GeneralCore.NationalHolidayCalender().Where(p => p.Holiday_Date == inputDate).ToList();
                //    if (UICommonFunction.StringIsNullOrEmpty(inputModel.strHolidayDate))
                //    {
                //        status = false;
                //        errMessage.Add(new Global_Error_Code()
                //        {
                //            Error_Code = "holidayDateEmpty",
                //            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HOLIDAY_DATE_IS_EMPTY, language)
                //        });
                //    }

                //    if (inputModel.holidayCalendarList.Count > 0)
                //    {
                //        status = false;
                //        errMessage.Add(new Global_Error_Code()
                //        {
                //            Error_Code = "Message",
                //            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_HOLIDAY_DATE_ALREADY_EXIST, language)
                //        });
                //    }
                //}
                //#endregion

                #region Edit
                if (type == GlobalVariable.CONST_EDIT)
                {
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.systemParameterModels.Param_Code))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Param_Code",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.systemParameterModels.Value))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Value",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                        });
                    }
                }
                #endregion
            }

            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Report Attendance
        public static bool ValidationReportAttendance(Global_Report_Attendance inputModel, string language, User_Data inputData, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                if (!String.IsNullOrEmpty(inputModel.StartFrom) && !String.IsNullOrEmpty(inputModel.StartTo))
                {
                    DateTime StartFrom = UICommonFunction.ConvertToDateTime(inputModel.StartFrom);
                    DateTime StartTo = UICommonFunction.ConvertToDateTime(inputModel.StartTo);

                    if (StartTo < StartFrom)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_START_TO_LESS_THAN_START_FROM,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_START_TO_LESS_THAN_START_FROM, language)
                        });
                    }
                }
                if (String.IsNullOrEmpty(inputModel.StartFrom))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_START_FROM_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_START_FROM_IS_EMPTY, language)
                    });
                }
                if (String.IsNullOrEmpty(inputModel.StartTo))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_START_TO_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_START_TO_IS_EMPTY, language)
                    });
                }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Report Leave
        public static bool ValidationReportLeave(Global_Report_Leave inputModel, string language, User_Data inputData, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                if (!String.IsNullOrEmpty(inputModel.StartFrom) && !String.IsNullOrEmpty(inputModel.StartTo))
                {
                    DateTime StartFrom = UICommonFunction.ConvertToDateTime(inputModel.StartFrom);
                    DateTime StartTo = UICommonFunction.ConvertToDateTime(inputModel.StartTo);

                    if (StartTo < StartFrom)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_START_TO_LESS_THAN_START_FROM,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_START_TO_LESS_THAN_START_FROM, language)
                        });
                    }
                }
                if (String.IsNullOrEmpty(inputModel.StartFrom))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_START_FROM_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_START_FROM_IS_EMPTY, language)
                    });
                }
                if (String.IsNullOrEmpty(inputModel.StartTo))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_START_TO_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_START_TO_IS_EMPTY, language)
                    });
                }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Report Claim
        public static bool ValidationReportClaim(Global_Report_Claim inputModel, string language, User_Data inputData, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                if (!String.IsNullOrEmpty(inputModel.StartFrom) && !String.IsNullOrEmpty(inputModel.StartTo))
                {
                    DateTime StartFrom = UICommonFunction.ConvertToDateTime(inputModel.StartFrom);
                    DateTime StartTo = UICommonFunction.ConvertToDateTime(inputModel.StartTo);

                    if (StartTo < StartFrom)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_START_TO_LESS_THAN_START_FROM,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_START_TO_LESS_THAN_START_FROM, language)
                        });
                    }
                }
                if (String.IsNullOrEmpty(inputModel.StartFrom))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_START_FROM_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_START_FROM_IS_EMPTY, language)
                    });
                }
                if (String.IsNullOrEmpty(inputModel.StartTo))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_START_TO_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_START_TO_IS_EMPTY, language)
                    });
                }
                if (String.IsNullOrEmpty(inputModel.FileType))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_GBF_FILE_TYPE,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_GBF_FILE_TYPE, language)
                    });
                }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Report Overtime
        public static bool ValidationReportOvertime(Global_Report_Overtime inputModel, string language, User_Data inputData, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                if (!String.IsNullOrEmpty(inputModel.StartFrom) && !String.IsNullOrEmpty(inputModel.StartTo))
                {
                    DateTime StartFrom = UICommonFunction.ConvertToDateTime(inputModel.StartFrom);
                    DateTime StartTo = UICommonFunction.ConvertToDateTime(inputModel.StartTo);

                    if (StartTo < StartFrom)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_START_TO_LESS_THAN_START_FROM,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_START_TO_LESS_THAN_START_FROM, language)
                        });
                    }
                }
                if (String.IsNullOrEmpty(inputModel.StartFrom))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_START_FROM_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_START_FROM_IS_EMPTY, language)
                    });
                }
                if (String.IsNullOrEmpty(inputModel.StartTo))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_START_TO_IS_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_START_TO_IS_EMPTY, language)
                    });
                }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion
		
		#region Validation Generate Online Payment
        public static bool ValidationGenerateOnlinePayment(Global_Generate_Online_Payment inputModel, string language, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();

            try
            {
                status = true;
                var BankName = inputModel.ListViewPayrollPaymentSummary.Where(p => p.Employee_Bank_Code != "Danamon").ToList();
                if (String.IsNullOrEmpty(inputModel.TrxBankTransfer.Transfer_Type) && BankName.Count() > 0)
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_GOP_TRANSFER_TYPE_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_GOP_TRANSFER_TYPE_EMPTY, language)
                    });
                }
                if (UICommonFunction.StringIsNullOrEmpty(inputModel.TrxBankTransfer.Source_Account_Bank))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_SOURCE_BANK_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_SOURCE_BANK_EMPTY, language)
                    });
                }
                //if (inputModel.Employee_Bank == null)
                //{
                //    status = false;
                //    errMessage.Add(new Global_Error_Code()
                //    {
                //        Error_Code = GlobalVariable.CONST_ERR_EMPLOYEE_BANK_EMPTY,
                //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMPLOYEE_BANK_EMPTY, language)
                //    });
                //}
                if (UICommonFunction.StringIsNullOrEmpty(inputModel.TrxBankTransfer.Transfer_Message))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_GOP_TRANSFER_MESS,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_GOP_TRANSFER_MESS, language)
                    });
                }
                if (!UICommonFunction.StringIsNullOrEmpty(inputModel.TrxBankTransfer.Transfer_Type))
                {
                    if (inputModel.TrxBankTransfer.Transfer_Type == "SKN")
                    {
                        var SysParamSKN = db.tbl_SysParam.Where(s => s.Param_Code == "Min_Max_Transfer_SKN").FirstOrDefault().Value.Split('|');
                        
                        if (inputModel.ListViewPayrollPaymentSummary.Where(p => Convert.ToDecimal(p.Employee_Salary_Transferred) < Convert.ToDecimal(SysParamSKN[0]) || Convert.ToDecimal(p.Employee_Salary_Transferred) > Convert.ToDecimal(SysParamSKN[0])).ToList().Count() > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_GOP_SKN_MIN_MAX,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_GOP_SKN_MIN_MAX, language)
                            });
                        }
                    }
                    if (inputModel.TrxBankTransfer.Transfer_Type == "RTGS")
                    {
                        var SysParamRTGS = db.tbl_SysParam.Where(s => s.Param_Code == "Min_Max_Transfer_RTGS").FirstOrDefault().Value;

                        if (inputModel.ListViewPayrollPaymentSummary.Where(p => Convert.ToDecimal(p.Employee_Salary_Transferred) < Convert.ToDecimal(SysParamRTGS)).ToList().Count() > 0)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = GlobalVariable.CONST_ERR_GOP_RTGS_MIN_MAX,
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_GOP_RTGS_MIN_MAX, language)
                            });
                        }
                    }
                }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion
		
		
		#region Validation Blog
        public static bool ValidationBlog(Global_Blog inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    inputModel.blogList = GeneralCore.blogQuery().Where(p => p.Title.ToLower() == inputModel.blogModels.Title.ToLower()).ToList();
                    if (inputModel.blogList.Count > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Message",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TITLE_ALREADY_EXIST, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.blogModels.Title))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Title",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                        });
                    }
                    if (inputModel.blogModels.Category_Id == null || inputModel.blogModels.Category_Id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Category_Id",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.blogModels.Tags))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Tags",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.blogModels.Post_Content))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Post_Content",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                        });
                    }
                }
                #endregion

                #region Edit
                if (type == GlobalVariable.CONST_EDIT)
                {
                    //inputModel.blogList = GeneralCore.blogQuery().Where(p => p.Title.ToLower() == inputModel.blogModels.Title.ToLower()).ToList();
                    //if (inputModel.blogList.Count > 0)
                    //{
                    //    status = false;
                    //    errMessage.Add(new Global_Error_Code()
                    //    {
                    //        Error_Code = "Message",
                    //        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_TITLE_ALREADY_EXIST, language)
                    //    });
                    //}
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.blogModels.Title))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Title",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.blogModels.Tags))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Tags",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                        });
                    }
                    if (UICommonFunction.StringIsNullOrEmpty(inputModel.blogModels.Post_Content))
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "Post_Content",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                        });
                    }
                }
                #endregion
            }

            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion
				
        #region Validation Request Demo
        public static bool ValidationRequestDemo(Global_Payroll_Portal inputModel, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    var valUserName = db.tbl_User.Where(o => o.Username == inputModel.email).FirstOrDefault();
                    if (valUserName != null)
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "errEmailAlreadyExist",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_EMAIL_ALREADY_EXISTS, language)
                            });
                        }
                    }
                    if (inputModel.fullName == null || inputModel.fullName == "")
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "fullName",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    //if (inputModel.userName == null || inputModel.userName == "")
                    //{
                    //    {
                    //        status = false;
                    //        errMessage.Add(new Global_Error_Code()
                    //        {
                    //            Error_Code = "userName",
                    //            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                    //        });
                    //    }
                    //}
                    if (inputModel.mobilePhone1 == null || inputModel.mobilePhone1 == "")
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "mobilePhone1",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (inputModel.email == null || inputModel.email == "")
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "email",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (inputModel.companyName == null || inputModel.companyName == "")
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "companyName",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                }
                #endregion
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation Contact Support
        public static bool ValidationSupport(Global_Customer_Notice inputModel, string language, string type, out List<Global_Error_Code> errMessage, string type_notif)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                #region Create
                if (type == GlobalVariable.CONST_CREATE && type_notif == "BecomeAPartner")
                {
                    if (string.IsNullOrEmpty(inputModel.Company_Name))
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Company Name",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (string.IsNullOrEmpty(inputModel.Contact_Name))
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Contact Name",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (string.IsNullOrEmpty(inputModel.Title))
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Title",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }

                    if (string.IsNullOrEmpty(inputModel.Email))
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Email",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (string.IsNullOrEmpty(inputModel.Phone))
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Phone",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (string.IsNullOrEmpty(inputModel.Phone))
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "Phone",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (string.IsNullOrEmpty(inputModel.Curently_Partner))
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "CurentlyPartner",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (string.IsNullOrEmpty(inputModel.Partnership_Interest))
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "PartnershipInterest",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                    if (string.IsNullOrEmpty(inputModel.Primary_Competitors))
                    {
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "PrimaryCompetitors",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_FIELD_IS_MANDATORY, language)
                            });
                        }
                    }
                }
                #endregion
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation OrderPaymentMidtrans
        public static bool ValidationOrderPayment(string inputPromotionCode, string language, string type, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                #region Create
                if (type == GlobalVariable.CONST_CREATE)
                {
                    //check promotion code
                    if (inputPromotionCode != "")
                    {
                        var listPromotionCode = db.tbl_Promotion.Where(q => q.Promotion_Code == inputPromotionCode).ToList();
                        if (listPromotionCode.Count == 0)
                        {
                            {
                                status = false;
                                errMessage.Add(new Global_Error_Code()
                                {
                                    Error_Code = "errNotFoundPromotionCode",
                                    Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_NOTFOUND_PROMOTION_CODE, language)
                                });
                            }
                        }
                    }
                }
                #endregion
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation Report Payroll Variable
        public static bool ValidationReportPayrollVariable(Global_Report_Payroll_Variable inputModel, string language, User_Data inputData, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                if (inputModel.Payroll_Period == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = GlobalVariable.CONST_ERR_REPORT_VARIABLE_CUT_OFF_CANNOT_BE_EMPTY,
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_REPORT_VARIABLE_CUT_OFF_CANNOT_BE_EMPTY, language)
                    });
                }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation Attendance Synchronization
        public static bool ValidationAttendanceSynchronization(Global_Attendance_Synchronization inputModel, string language, string type, User_Data UserData, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                if (type == GlobalVariable.CONST_CREATE)
                {
                    if(inputModel.lengthFile <= 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_UPLOAD_ATTENDANCE_SELECT_TEMPLATE,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_UPLOAD_ATTENDANCE_SELECT_TEMPLATE, language)
                        });
                    }
                    var StatusInProgress = db.tbl_Attendance_History_Upload.Where(p => p.Organization_ID == UserData.OrganizationSelected_Id && p.Sync_Status == GlobalVariable.CONST_UPLOAD_ATTENDANCE_STATUS_IN_PROGRESS).ToList();
                    if (StatusInProgress.Count() > 0)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_UPLOAD_ATTENDANCE_IN_PROGRESS_EXIST,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_UPLOAD_ATTENDANCE_IN_PROGRESS_EXIST, language)
                        });
                    }
                }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region Validation MyAttendance
        public static bool ValidateMyAttendance(GlobalMyAttendance inputModel, string language, string type, User_Data UserData, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                CultureInfo cultures = new CultureInfo("en-GB");
                if (inputModel.pageQuery.filterString3 == "search")
                {
                    if (inputModel.pageQuery.filterString != null && inputModel.pageQuery.filterString2 != null)
                    {
                        DateTime filter1 = Convert.ToDateTime(inputModel.pageQuery.filterString, cultures);
                        DateTime filter2 = Convert.ToDateTime(inputModel.pageQuery.filterString2, cultures);
                        if (filter1 > filter2)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "ErrMyAttendance",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ESS_ATTENDANCE_LIST_TO_LESS_FROM, language)
                            });
                        }
                    }
                    else
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = "ErrMyAttendance",
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ESS_ATTENDANCE_LIST_EMPTY_FILTER, language)
                        });
                    }
                }
                   
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion

        #region validation Organization
        public static bool ValidationESSLeave(Guid EmpID, DateTime? From_Date, DateTime? To_Date, string type, string language, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                List<tbl_Apply_Leave> merge = new List<tbl_Apply_Leave>();
                List<tbl_Apply_Leave> listAllMyLeave = db.tbl_Apply_Leave.Where(p => p.Employee_Id == EmpID).ToList();
                List<tbl_Apply_Leave> DataListValid = listAllMyLeave.Where(p => (p.Status_Code == 1 && p.Authorize_Status == 0) || (p.Status_Code == 1 && p.Authorize_Status == 1)).ToList();

                var Type1 = DataListValid.Where(p => p.From_Date < From_Date && p.To_Date <= To_Date && p.To_Date >= From_Date).OrderByDescending(p => p.To_Date).Take(1).ToList();
                var Type2 = DataListValid.Where(p => p.From_Date >= From_Date && p.To_Date <= To_Date).OrderByDescending(p => p.To_Date).Take(1).ToList();
                var Type3 = DataListValid.Where(p => p.From_Date >= From_Date && p.To_Date > To_Date && p.From_Date <= To_Date).OrderByDescending(p => p.To_Date).Take(1).ToList();
                var Type4 = DataListValid.Where(p => p.From_Date < From_Date && p.To_Date > To_Date).OrderByDescending(p => p.To_Date).Take(1).ToList();

                if (Type1.Count() > 0)
                    merge.AddRange(Type1);
                if (Type2.Count() > 0)
                    merge.AddRange(Type2);
                if (Type3.Count() > 0)
                    merge.AddRange(Type3);
                if (Type4.Count() > 0)
                    merge.AddRange(Type4);

                if (merge.Count > 0) // overlap
                {
                    status = false;
                    errMessage.Add(new Global_Error_Code()
                    {
                        Error_Code = "MVLC_1",
                        Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ESS_LEAVE_EXIST, language)
                        
                    });
                }

            }
            catch (Exception ex)
            {
                status = false;
                ErrorMessage = ex.ToString();

                if (ex.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + ex.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, ex.StackTrace);
            }
            return status;
        }
        #endregion

        #region validation MyLeave
        public static bool ValidateMyLeave(GlobalMyLeave inputModel,string language,string type, User_Data UserData,out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                CultureInfo cultures = new CultureInfo("en-GB");
            
                    if (inputModel.pageQuery.filterString != null && inputModel.pageQuery.filterString2 != null)
                    {
                        DateTime filter1 = Convert.ToDateTime(inputModel.pageQuery.filterString, cultures);
                        DateTime filter2 = Convert.ToDateTime(inputModel.pageQuery.filterString2, cultures);
                        if (filter1 > filter2)
                        {
                            status = false;
                            errMessage.Add(new Global_Error_Code()
                            {
                                Error_Code = "ErrMyLeave",
                                Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ESS_LEAVE_LIST_FROM_GREATER_TO, language)
                            });
                        }
                    }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion
		
		#region Validation ApprovalLeave
        public static bool ValidateApprovalLeave(GlobalApprovalLeave inputModel, string language, string type, User_Data UserData, out List<Global_Error_Code> errMessage)
        {
            status = true;
            ModelEntitiesWebsite db = new ModelEntitiesWebsite();
            errMessage = new List<Global_Error_Code>();
            try
            {
                CultureInfo cultures = new CultureInfo("en-GB");

                if (inputModel.pageQuery.filterString != null && inputModel.pageQuery.filterString2 != null)
                {
                    DateTime filter1 = Convert.ToDateTime(inputModel.pageQuery.filterString, cultures);
                    DateTime filter2 = Convert.ToDateTime(inputModel.pageQuery.filterString2, cultures);
                    if (filter1 > filter2)
                    {
                        status = false;
                        errMessage.Add(new Global_Error_Code()
                        {
                            Error_Code = GlobalVariable.CONST_ERR_ESS_APPROVAL_LEAVE_FROM_GREATER_TO,
                            Error_Description = UICommonFunction.GetErrorDescription(GlobalVariable.CONST_ERR_ESS_APPROVAL_LEAVE_FROM_GREATER_TO, language)
                        });
                    }
                }
            }
            catch (DbUpdateException E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            catch (Exception E)
            {
                status = false;
                ErrorMessage = E.ToString();

                if (E.InnerException != null)
                {
                    ErrorMessage = ErrorMessage + E.InnerException.ToString();
                }
                UIException.LogException(ErrorMessage, E.StackTrace);
            }
            return status;
        }
        #endregion
		
    }
}
