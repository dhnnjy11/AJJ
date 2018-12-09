using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Ajj.Services
{
    public class PasswordHasher
    {
        private static string _itoa64 = "./0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private static readonly string _passwordChars = "0123456789abcdefghijklmnopqrstuvwxyz";

        public static string MD5Encode(string password, string setting)
        {
            //            string str_randam = "AbCdEfGh";
            string str_randam;
            string str_hash = "$P$B";
            int length = 6;
            StringBuilder sb = new StringBuilder(length);
            Random r = new Random();

            for (int i = 0; i < length; i++)
            {
                //文字の位置をランダムに選択
                int pos = r.Next(_passwordChars.Length);
                //選択された位置の文字を取得
                char c = _passwordChars[pos];
                //パスワードに追加
                sb.Append(c);
            }

            str_randam = sb.ToString();

            string str_encode = Encode64(Encoding.ASCII.GetBytes(str_randam), 6);
            string hash = str_hash + str_encode;

            if (setting.Length > 0)
            {
                hash = setting;
            }

            string output = "*0";
            if (hash == null)
            {
                return output;
            }

            if (hash.StartsWith(output))
                output = "*1";

            string id = hash.Substring(0, 3);
            // We use "$P$", phpBB3 uses "$H$" for the same thing
            if (id != "$P$" && id != "$H$")
                return output;

            // get who many times will generate the hash
            int count_log2 = _itoa64.IndexOf(hash[3]);
            if (count_log2 < 7 || count_log2 > 30)
                return output;

            int count = 1 << count_log2;

            string salt = hash.Substring(4, 8);
            if (salt.Length != 8)
                return output;

            byte[] hashBytes = { };
            using (MD5 md5Hash = MD5.Create())
            {
                hashBytes = md5Hash.ComputeHash(Encoding.ASCII.GetBytes(salt + password));
                byte[] passBytes = Encoding.ASCII.GetBytes(password);
                do
                {
                    hashBytes = md5Hash.ComputeHash(hashBytes.Concat(passBytes).ToArray());
                } while (--count > 0);
            }

            output = hash.Substring(0, 12);
            string newHash = Encode64(hashBytes, 16);

            return output + newHash;
        }

        private static string Encode64(byte[] input, int count)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            do
            {
                int value = (int)input[i++];
                sb.Append(_itoa64[value & 0x3f]); // to uppercase
                if (i < count)
                    value = value | ((int)input[i] << 8);
                sb.Append(_itoa64[(value >> 6) & 0x3f]);
                if (i++ >= count)
                    break;
                if (i < count)
                    value = value | ((int)input[i] << 16);
                sb.Append(_itoa64[(value >> 12) & 0x3f]);
                if (i++ >= count)
                    break;
                sb.Append(_itoa64[(value >> 18) & 0x3f]);
            } while (i < count);

            return sb.ToString();
        }
    }

    public class CustomPasswordHasher<TUser> : PasswordHasher<TUser> where TUser : class
    {
        public override string HashPassword(TUser user, string password)
        {
            var newHasedPassword = PasswordHasher.MD5Encode(password,"");
            return newHasedPassword;
        }

        public override PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            if (hashedPassword == null) { throw new ArgumentNullException(nameof(hashedPassword)); }
            if (providedPassword == null) { throw new ArgumentNullException(nameof(providedPassword)); }

            var newHasedPassword = PasswordHasher.MD5Encode(providedPassword, hashedPassword);

            if (newHasedPassword.Equals(hashedPassword))
            {
                return PasswordVerificationResult.Success;
            }
            else
            {
                return PasswordVerificationResult.Failed;
            }
        }
    }
}