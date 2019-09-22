using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace HRM.WebSite.Helpers
{
    public class UploadFileHelper
    {
        public static string ProcessUpload(HttpServerUtilityBase server, HttpPostedFileBase file)
        {
            try
            {
                var validTypes = new Dictionary<string, string> {
                        {"image/gif", "gif"},
                        {"image/jpeg", "jpg"},
                        {"image/pjpeg", "jpg"},
                        {"image/png", "png"},
                        {"application/pdf", "pdf"},
                        {"application/msword", "doc"},
                        {"application/vnd.openxmlformats-officedocument.wordprocessingml.document", "docx"}
                    };

                if (file != null
                    && file.ContentLength > 0
                    && validTypes.ContainsKey(file.ContentType))
                {
                    var uploadDir = "~/Uploads";
                    var uploadFile = $"{Guid.NewGuid().ToString()}.{validTypes[file.ContentType]}";
                    var documentPath = Path.Combine(server.MapPath(uploadDir), uploadFile);
                    file.SaveAs(documentPath);
                    return uploadFile;
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return string.Empty;
            }
        }
    }
}