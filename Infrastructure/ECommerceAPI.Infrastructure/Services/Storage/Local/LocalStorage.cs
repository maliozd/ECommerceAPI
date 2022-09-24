using ECommerceAPI.Application.Abstraction;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Infrastructure.Services.Storage.Local
{
    public class LocalStorage :  Storage ,ILocalStorage
    {
        readonly IWebHostEnvironment _webHostEnvironment;
        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task DeleteAsync(string path, string fileName)
        {
            File.Delete($"{path}//{fileName}");
        }

        public List<string> GetFiles(string path)
        {
            DirectoryInfo directoryInfo = new(path);
            return directoryInfo.GetFiles().Select(f => f.Name).ToList(); 
        }

        public bool HasFile(string path, string fileName)
        {
           return File.Exists($"{path}//{fileName}");
        }
        private async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {             
                using FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                //using --> bu fonksiyon son bulduğunda dispose edilir. blok içinde kullanırsan blok bittiğinde dispose edilir.
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<(string fileName, string pathOrContainer)>> UploadAsync(string path, IFormFileCollection files)
        {
            string rootPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }

            List<(string fileName, string path)> data = new();  //döndürülecek data.
            List<bool> results = new();     //birden fazla giriş olduğunda kontrol edilecek

            foreach (IFormFile file in files)
            {
                string newFileName = await RenameFileAsync(path, file.Name, HasFile);
                bool result = await CopyFileAsync($"{rootPath}\\{newFileName}", file);
                data.Add((newFileName, $"{rootPath}\\{newFileName}"));

            }
            if (results.TrueForAll(r => r.Equals(true)))
            {
                return data;
            }
            return null;
        }
    }
}
