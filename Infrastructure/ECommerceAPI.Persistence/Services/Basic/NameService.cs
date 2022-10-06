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
    }
}
