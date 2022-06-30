using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Exam_Back_End.Areas.FinalAdmin.Utilities
{
    public static class FileUtilities
    { 
        public static string FileCreate(this IFormFile file,string root,string folder)
        {
            string filename = Guid.NewGuid() + file.FileName;
            string path = Path.Combine(root, folder);
            string fullpath = Path.Combine(path, filename);

            using (FileStream stream = new FileStream(fullpath,FileMode.Create))
            {
                file.CopyToAsync(stream);
            }

            return filename;  
        }
    }
}
