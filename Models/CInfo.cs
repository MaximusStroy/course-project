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
