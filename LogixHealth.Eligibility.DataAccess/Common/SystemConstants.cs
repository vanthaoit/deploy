using System;
using System.Collections.Generic;
using System.Text;

namespace LogixHealth.Eligibility.DataAccess.Common
{
    public static class SystemConstants
    {
        public const string HEADER_CONTENT_TYPE = "application/json";
        public const string URL_API_ENDPOINT_NAME = "BaseAPI";
        public const string APP_SETTING = "appsettings.json";
    }
    public static class TableNameConstants
    {
        public const string VALIDATION_CODE = "MdRequestValidation";
    }
    public static class UserActionConstants
    {
        public const string ADD_VALIDATION_CODE = "AddValidationCode";
        public const string EDIT_VALIDATION_CODE = "EditValidationCode";
        public const string USER_ID = "BF189BE2-D457-4424-A27E-61EA87395439";
    }
    public static class SchemaDatabaseCosntants
    {
        public const string SCHEMA_DATABASE = "eligibility";
    }
}
