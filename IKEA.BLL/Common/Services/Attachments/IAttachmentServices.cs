using Microsoft.AspNetCore.Http;

namespace IKEA.BLL.Common.Services.Attachments;

public interface IAttachmentServices
{
    public string UploadImage(IFormFile file, string folderName);
    public bool DeleteImage(string FilePath);
}
