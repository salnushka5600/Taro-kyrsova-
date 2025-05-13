using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taro_kyrsovaя
{
   public class Shedule
    {
        public int Id { get; set; }
        public string Idclients { get; set; }
        public Client Client { get; set; }
        public string Idservice { get; set; }
        public string Idmaster { get; set; }
        public Master Master { get; set; }
        public string Date { get; set; }
        public string Servicestatus { get; set; }

    }
}
