using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Services
{
    public static class RenameNewUser
    {
        public static string RenameGoogleUser(string name)
        {
            var newGuid = Guid.NewGuid().ToString();
            var rangeToTakeFromNewGuid = new Random().Next(newGuid.Length);
            string newName = GetFirstWord(name);
            return $"{newName[..1]}-{newName[1..]}{newGuid[..4].ToUpper()}{newGuid[..rangeToTakeFromNewGuid]}" + newName[..2];
        }

        private static string GetFirstWord(string words)
        {
            string[] newWords = words.Split(' ');
            return newWords[0];
        }
    }
}
