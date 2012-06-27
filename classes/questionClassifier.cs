using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace weetit_website
{
    class questionClassifier
    {
        public enum types {question,comparison,relate};
        private string opText;
        public types ipType;
        private string[] buff;
        private List<string> objects = new List<string>();
        private string[] separators = File.ReadAllLines(HttpContext.Current.Server.MapPath("~/files/Separators.txt"));
        private string[] removesKeywords = File.ReadAllLines(HttpContext.Current.Server.MapPath("~/files/RemoveRegularExpression.txt"));

        public questionClassifier(String ip)
        {
            ip = ip.Trim();
            opText = ip;
            ipType = getSentenceType();

        }
        private types getSentenceType()
        {
            string[] comparisonKeyWords = File.ReadAllLines(HttpContext.Current.Server.MapPath("~/files/ComparissonRegularExpression.txt"));
            Regex[] comparisonRegex = new Regex[comparisonKeyWords.Length];
            for (int i = 0; i < comparisonKeyWords.Length; i++ )
            {
                comparisonRegex[i] = new Regex(comparisonKeyWords[i]);
            }         
            foreach (Regex regx in comparisonRegex)
            {              
                if (regx.IsMatch(opText))
                {
                    return(types.comparison);
                }
            }
            string[] relateKeyWords = File.ReadAllLines(HttpContext.Current.Server.MapPath("~/files/relateRegularExpression.txt"));
            Regex[] relateRegex = new Regex[relateKeyWords.Length];
            for (int i = 0; i < relateKeyWords.Length; i++)
            {
                relateRegex[i] = new Regex(relateKeyWords[i]);
            }
            foreach (Regex regx in relateRegex)
            {
                if (regx.IsMatch(opText))
                {
                    return (types.relate);
                }
            }
            return types.question;        
        }



        public List<string> getObjects()
        {

            foreach (String remove in removesKeywords)
            {
                opText = Regex.Replace(opText, remove," ");
            }
            foreach (String separator in separators)
            {
                opText = opText.Replace(separator, ",");
            }
            buff = opText.Split(',');
            for (int i = 0; i < buff.Length; i++)
                buff[i] = buff[i].Trim();
            foreach(String temp in buff)
            {
                if (temp!="")
                {
                    objects.Add(temp);
                }
            }            
            return objects;
        }
    }
}