using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text.RegularExpressions;


namespace WebService7
{

    /// <summary>
    /// Сводное описание для Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
    // [System.Web.Script.Services.ScriptService]
    public interface IUser
    {

    }

    //Provides work with users
    public class Users
    {
        //RegForm Fields
        private string _username;
        private string _password;
        private string _email;
        //Inner UserId
        private string _uid;
        public string UserName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
  /*      public static string HashPassword(string password)
        {
            int a = password.GetHashCode();
            a.ToString();
            return "1";
        }*/

        public bool Login()
        {
            if (true) return true;

        }
            
        //Calculate md5 hash from string
          static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public bool Register()
        {
            //HashPassword("456");
            MD5 md5Hash = MD5.Create();
            string hash = GetMd5Hash(md5Hash, "123");
            if (true) return true;
        }
        //Move method realization in client application(optimize) 
        public bool CheckMail(string str)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(str,
                   @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }

    }

    public class ConnectDb
    {

    }

    //Method moved in class Users
   /* public class RegexUtilities
    {
        public static bool CheckMail(string email)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(email,
                   @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }
    }*/

    public class Service1 : System.Web.Services.WebService
    {
        [Table(Name = "HelloDB")]
        public class LinqReader
        {
            //[Column]
            //public string CustomerID { get; set; }
            [Column]
            public string Message { get; set; }

            public override string ToString()
            {
                return "You see this message, it means that Class LinqReader work correctly. \t" + Message;
            }
        }

        [WebMethod]
        public string HelloWorld()
        {
            //Connect to Database using LINQ
            DataContext db2 = new DataContext
                (@"Data Source=ПОЛЬЗОВАТЕЛЬ-ПК\CURS;Initial Catalog=chat;Integrated Security=True");
            //Connect to Database using ADO.NET
            DataContext db = new DataContext
                (@"Data Source=.\SQLEXPRESS;
                    AttachDbFilename=|DataDirectory|\NORTHWND.MDF;
                    Integrated Security=True;
                    User Instance=True");

            var results = from c in db2.GetTable<LinqReader>()
                          where c.Message.Length > 0
                          select c;
            ArrayList HelloArray = new ArrayList();
            string result = "";
            foreach (var c in results)
                result += c.Message + " <> ";
            //HelloArray.Add(c.GetMessage);
            //Console.WriteLine("{0}\t{1}", c.CustomerID, c.City);
            //Console.ReadKey();
            LinqReader linq = new LinqReader();
            linq.Message = "Hello ";
            //return result;

            //test overrided method
            return linq.ToString();
        }


        //regNewUser
        [WebMethod]
        public bool RegUser(string username, string password, string email)
        {
            Users user = new Users();
            user.UserName = username;
            user.Register();
            //user.password = 
            if (username.Length > 0 && password.Length >0 && user.CheckMail(email))
                return true;
            else
                return false;
        }
    }
}