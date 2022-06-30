using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Exam_Back_End.Areas.FinalAdmin.Extensions
{
    public static class FileCheck
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image");
        }

        public static bool IsGreatest(this IFormFile file,int mb)
        {
            return file.Length < mb * 1024 * 1024;
        }

        public static bool IsOkay(this IFormFile file,int mb)
        {
            return IsImage(file) && IsGreatest(file, mb);
        }
    }
}
