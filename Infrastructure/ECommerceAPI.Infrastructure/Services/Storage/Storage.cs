using ECommerceAPI.Infrastructure.BasicServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string path,string fileName); //referans türlüdür. nesne oluşturulabilir.
        protected async Task<string> RenameFileAsync(string path, string saltFileName,HasFile hasFileMethod, int number = 1) //protected --> kullananın kalıtımsal olarak türetilmesi lazım
        {
            return await Task.Run(async () =>
            {
                string extension = Path.GetExtension(saltFileName); //image .jpeg
                string oldName = $"{Path.GetFileNameWithoutExtension(saltFileName)}-{number}"; //
                string newFileName = $"{NameService.CharacterRegulator(oldName)}{extension}";

                //if (File.Exists($"{path}\\{newFileName}"))
                if(hasFileMethod(path, newFileName))                
                    return await RenameFileAsync(path, $"{newFileName.Split($"-{number}")[0]}{extension}",hasFileMethod, ++number);
                
                return newFileName;
            });
        }
    }
}
