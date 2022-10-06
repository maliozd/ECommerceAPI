using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.BasicServices
{
    public class NameService
    {
        public static string CharacterRegulator(string name)
        {
            name.Replace("\"", "")
                 .Replace("!", "")
                 .Replace("'", "")
                 .Replace("^", "")
                 .Replace("+", "")
                 .Replace("%", "")
                 .Replace("&", "")
                 .Replace("/", "")
                 .Replace("(", "")
                 .Replace(")", "")
                 .Replace("=", "")
                 .Replace("?", "")
                 .Replace("_", "")
                 .Replace(" ", "-")
                 .Replace("@", "")
                 .Replace("€", "")
                 .Replace("¨", "")
                 .Replace("~", "")
                 .Replace(",", "")
                 .Replace(";", "")
                 .Replace(":", "")
                 .Replace(".", "-")
                 .Replace("Ö", "o")
                 .Replace("ö", "o")
                 .Replace("Ü", "u")
                 .Replace("ü", "u")
                 .Replace("ı", "i")
                 .Replace("İ", "i")
                 .Replace("ğ", "g")
                 .Replace("Ğ", "g")
                 .Replace("æ", "")
                 .Replace("ß", "")
                 .Replace("â", "a")
                 .Replace("î", "i")
                 .Replace("ş", "s")
                 .Replace("Ş", "s")
                 .Replace("Ç", "c")
                 .Replace("ç", "c")
                 .Replace("<", "")
                 .Replace(">", "")
                 .Replace("|", "");
            return name;
        }
        public static string RenameGoogleUser(string name)
        {
            var newGuid = Guid.NewGuid().ToString();
            var rangeToTakeFromNewGuid = new Random().Next(newGuid.Length);
            string newName = name.Split(' ')[0];
            return $"{newName[..1]}-{newName[1..]}{newGuid[..4].ToUpper()}{newGuid[..rangeToTakeFromNewGuid]}" + newName[..2];
        }
    }
}
