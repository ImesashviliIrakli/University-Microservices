namespace University.Portal.Utility
{
    public class SD
    {
        // API Urls
        public static string CouponAPIBase { get; set; }
        public static string AuthAPIBase { get; set; }
        public static string ProductAPIBase { get; set; }
        public static string ShoppingCartAPIBase { get; set; }
        public static string OrderAPIBase { get; set; }

        // API Types
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        // Roles
        public const string RoleAdmin = "ADMIN";
        
        // JWT
        public const string TokenCookie = "JwtToken";
     
        // Content Type
        public enum ContentType
        {
            Json,
            MultipartFormData,
        }
    }
}
