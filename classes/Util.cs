using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace weetit_website
{
    public class Util
    {
        public static void log(String s)
        {

            try
            {
                StreamWriter logWriter = new StreamWriter(HttpContext.Current.Server.MapPath("~/files/log.txt"), false);// File.AppendText(@".\Log.txt");
                logWriter.Write(s + "\t" + DateTime.Now.ToLongTimeString().ToString() + "\n");
                logWriter.Close();
            }
            catch
            {
            
            }
        }
    }
}