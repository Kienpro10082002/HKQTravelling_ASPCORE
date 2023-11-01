using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;
using HKQTravelling.Models;

namespace HKQTravelling.Extension
{
    public static class CheckAccountInformation
    {
        public static bool checkUsername(ApplicationDBContext data, string username)
        {
            return data.users.Count(u => u.Username == username) > 0;
        }
        public static bool checkEmail(ApplicationDBContext data, string email)
        {
            return data.userDetails.Count(u => u.Email == email) > 0;
        }
        public static bool IsEmailValid(string email)
        {
            string pattern = @"^[\w\.-]+@[\w\.-]+\.\w+$";
            return Regex.IsMatch(email, pattern);
        }
        public static bool checkSurname(string surname)
        {
            if (surname == null)
            {
                return false;
            }
            return true;
        }
        public static bool checkName(string name)
        {
            if (name == null)
            {
                return false;
            }
            return true;
        }
        public static bool existetPhoneNumber(ApplicationDBContext data, string phoneNumber)
        {
            return data.userDetails.Count(u => u.PhoneNumber == phoneNumber) > 0;
        }
        public static bool IsVietnamesePhoneNumber(string phoneNumber)
        {
            // Sử dụng biểu thức chính quy cho số điện thoại Việt Nam
            // Biểu thức này kiểm tra số điện thoại di động Việt Nam bắt đầu bằng 0, sau đó là 9 số.
            string pattern = @"^0\d{9}$";

            // Kiểm tra số điện thoại sử dụng biểu thức chính quy
            return Regex.IsMatch(phoneNumber, pattern);
        }
        public static bool existetNINumber(ApplicationDBContext data, string niNumber)
        {
            return data.userDetails.Count(u => u.NiNumber == niNumber) > 0;
        }
        public static bool numberOfNINumber(string niNumber)
        {
            int count = niNumber.Length;

            if (count == 12)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
