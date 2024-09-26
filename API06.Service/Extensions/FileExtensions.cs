using Microsoft.AspNetCore.Http;

namespace API06.Service.Extensions;


public static class FileExtensions
{
    public static bool IsImage(this IFormFile file)
    {
        return  file.ContentType.Contains("image");
    }

    public static bool IsSizeOk(this IFormFile file, int mb) 
    {
        return file.Length /1024 /1024 <= mb;
    }

    public static async Task<string> SaveFileAsync(this IFormFile file, string root, string path)
    {
        string filename = Guid.NewGuid().ToString() + file.FileName;
        string fullpath = Path.Combine(root, path, filename);
        using(FileStream stream = new FileStream(fullpath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return filename;
    }

}