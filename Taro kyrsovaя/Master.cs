using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taro_kyrsovaя
{
    public class Master
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Workexperience { get; set; }
        public string SurName { get; set; }
        public string Specializations { get; set; }
        public string Fullname => SurName + " " + Name;




    }
}
