using Microsoft.AspNetCore.Http;

namespace IKEA.BLL.Common.Services.Attachments;

public class AttachmentServices : IAttachmentServices
{
    private readonly List<string> _allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
    private const long _maxFileSize = 2 * 1024 * 1024; // 2 MB
    public string UploadImage(IFormFile file, string folderName)
    {
        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        if (!_allowedExtensions.Contains(fileExtension))
        {
            throw new InvalidOperationException("Invalid file type. Only .jpg, .jpeg, and .png are allowed.");
        }
        if (file.Length > _maxFileSize)
        {
            throw new InvalidOperationException("File size exceeds the maximum limit of 2 MB.");
        }
        var folderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "files", folderName);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        var FileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(folderPath, FileName);
        using var fs = new FileStream(filePath, FileMode.Create);
        file.CopyTo(fs);
        return FileName;
    }
    public bool DeleteImage(string FilePath)
    {
        if (File.Exists(FilePath))
        {
            File.Delete(FilePath);
            return true;
        }
        return false;
    }


}
