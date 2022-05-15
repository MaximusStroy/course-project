using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacancy.Models
{
    public class CInfo
    {
        public static int status;

        public static int id_vac;

        public static int id_app;

        public string convertMoneyToStr(string s)
        {
            string result = "";
            bool flag = true;
            for (int i = 0; i < s.Length; i++)
            {

                if (s[i] != ' ')
                {
                    result += s[i];
                    if (s[i] == ',')
                    {
                        break;
                        flag = false;
                    }
                }
                if (false == flag) break;
            }

            return result;
        }


        public string convertJsonToString(dynamic _dynamic)
        {
            string result = "";
            for (int i = 0; i < _dynamic.Length; i++)
            {
                result += _dynamic[i];
                if (i != _dynamic.Length - 1)
                    result += ", ";
            }
            return result;
        }
        public string convertStringToJson(string _str)
        {
            List<string> lst = new List<string>();
            string str = "";
            for (int i = 0; i < _str.Length; i++)
            {
                if(_str[i] != ',')
                {
                    str += _str[i];
                }
                else if (_str.Length-1 == i)
                {
                    lst.Add(str);
                    str = "";
                }
            }
            object[] strIsJson = new object[lst.Count];
            for (int i = 0; i < lst.Count; i++)
            {
                strIsJson[i] = lst[i];
            }
            return System.Web.Helpers.Json.Encode(strIsJson);
        }
        public string datatimeConvert(string _s1)
        {
            string str = "";

            for (int i = 0; i < _s1.Length; i++)
            {
                if (_s1[i].ToString() != " ")
                {
                    str += _s1[i];
                }
                else break;
            }

            return str;
        }
    }

}
