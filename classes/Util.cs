using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace weetit_website.classes
{
    public class Util
    {
        public static void log(String s)
        {

            try
            {
                StreamWriter logWriter = File.AppendText(@".\Log.txt");
                logWriter.Write(s + "\t" + DateTime.Now.ToLongTimeString().ToString() + "\n");
                logWriter.Close();
            }
            catch
            {
            
            }
        }
    }
}