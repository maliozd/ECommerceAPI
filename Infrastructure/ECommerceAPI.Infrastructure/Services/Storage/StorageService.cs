using ECommerceAPI.Application.Abstraction.Storage;
using Microsoft.AspNetCore.Http;


namespace ECommerceAPI.Infrastructure.Services.Storage
{
    public class StorageService : IStorageService //  -->LocalStorage düşecek
    {
        readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }
        public string StorageName { get => _storage.GetType().Name; }

        public async Task DeleteAsync(string path, string fileName)
        {
           await _storage.DeleteAsync(path, fileName);
        }

        public List<string> GetFiles(string path)
        {
            return _storage.GetFiles(path);
        }

        public bool HasFile(string path, string fileName)
        {
          return  _storage.HasFile(path, fileName);
        }

        public async Task<List<(string fileName, string pathOrContainer)>> UploadAsync(string path, IFormFileCollection files)
        {
          return await _storage.UploadAsync(path, files);
        }
    }
}
