﻿namespace University.Portal.Utility
{
    public class SD
    {
        // API Urls
        public static string CourseAPIBase { get; set; }
        public static string AuthAPIBase { get; set; }

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
        public const string RoleStudent = "STUDENT";
        public const string RoleTeacher = "TEACHER";

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
