using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeWay.App_Code
{
    public class MyTools
    {
        public static string GetID(string t, int i)
        {
            string ID = "0000000" + i;
            string result = t + ID.Substring(ID.Length - 7, 7);
            return result;
        }
    }
}