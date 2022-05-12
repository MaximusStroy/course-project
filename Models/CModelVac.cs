using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacancy.Models
{
    class CModelVac
    {
        public string post { get; set; }

        public decimal salary { get; set; }

        public static CModelVac[] GetVacancy()
        {
            using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
            {
                var list = (from x in db.vacancy
                            select x).ToList();
                var result = new CModelVac[] {
                    new CModelVac(){ post = list[1].post.ToString(), salary = list[1].salary}

                };
                return result;

            }
        }    
    }
}
