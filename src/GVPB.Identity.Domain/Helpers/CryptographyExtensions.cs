
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace GVPB.Identity.Domain.Helpers;

public static class CryptographyExtensions
{
    public static string md5Hash(this string senha)
    {
        Regex md5Regex = new Regex("^[0-9a-fA-F]{32}$");
        bool isHash =  md5Regex.IsMatch(senha);
        if(isHash)
        { return senha; }
        using (MD5 md5Hash = MD5.Create())
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(senha));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}

