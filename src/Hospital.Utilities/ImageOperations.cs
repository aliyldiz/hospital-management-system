using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Hospital.Utilities;

public class ImageOperations
{
    private readonly IWebHostEnvironment _env;

    public ImageOperations(IWebHostEnvironment env)
    {
        _env = env;
    }
    public string ImageUpload(IFormFile file)
    {
        string filename = null;
        if (file != null)
        {
            string fileDirectory = Path.Combine(_env.WebRootPath, "Images");
            filename = Guid.NewGuid() + "-" + file.FileName;
            string filePath = Path.Combine(fileDirectory, filename);
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(fs);
            }
        } 
        return filename;
    }
}