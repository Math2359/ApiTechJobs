using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utils.Interface
{
    public interface IAwsService
    {
        Task<string> UploadFileAsync(IFormFile file, string folder);
        Task RemoveFileAsync(string fileKey);
        Task<string> PreSignedURL(string fileKey);
        Task SendEmailTemplate(string destinatario, string template, object templateData);
    }
}
