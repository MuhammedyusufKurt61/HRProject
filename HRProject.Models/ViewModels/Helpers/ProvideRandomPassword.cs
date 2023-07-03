using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Models.ViewModels.Helpers
{
    public static class ProvideRandomPassword
    {
        public static string CreateRandomPassword(int length)
        {
            string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string digitChars = "0123456789";
            string specialChars = "!@#$%^&*";
            string allowedChars = lowerChars + upperChars + digitChars + specialChars;
            Random random = new Random();
            string password = "";
            int i;
            for (i = 0; i < length / 4; i++)
            {
                password += lowerChars[random.Next(lowerChars.Length)];
                password += upperChars[random.Next(upperChars.Length)];
                password += digitChars[random.Next(digitChars.Length)];
                password += specialChars[random.Next(specialChars.Length)];
            }
            // Şifrenin uzunluğu eşit olmasın diye kalan karakterleri rastgele ekle
            for (int j = 0; j < length % 4; j++)
            {
                password += allowedChars[random.Next(allowedChars.Length)];
            }

            return password;
        }

        public static string ReplaceTurkishCharacters(string turkishWord)
        {
            string source = "ığüşöçĞÜŞİÖÇ";
            string destination = "igusocGUSIOC";

            string result = turkishWord;

            for (int i = 0; i < source.Length; i++)
            {
                result = result.Replace(source[i], destination[i]);
            }

            return result;
        }
    }
}
