using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Services.Basic
{
    public class NameService
    {
        public static string RenameGoogleUser(string name)
        {
            var newGuid = Guid.NewGuid().ToString();
            var rangeToTakeFromNewGuid = new Random().Next(newGuid.Length);
            string newName = name.Split(' ')[0];
            return $"{newName[..1]}-{newName[1..]}{newGuid[..4].ToUpper()}{newGuid[..rangeToTakeFromNewGuid]}" + newName[..2];
        }
        public static string CreateOrderCode()
        {

            char[] charList = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(); //26
            var random = new Random();
            var randomNumber = random.Next(1000, 600000);
            var randomStartNumber = random.Next(100, 999);
            var randomCharIndex = random.Next(0, 26);
            var guid = Guid.NewGuid().ToString();
            guid = guid.Replace("-", "");
            //--
            var orderCode = $"OR-{randomStartNumber}{charList[randomCharIndex]}-{randomNumber.ToString()[2..]}{guid[..7].ToUpper()}";
            return orderCode;

        }
    }
}
