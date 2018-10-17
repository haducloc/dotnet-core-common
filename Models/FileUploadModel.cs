using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace NetCore.Common.Models
{
    public class FileUploadModel
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
