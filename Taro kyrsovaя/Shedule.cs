using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taro_kyrsovaя
{
   public class Shedule 
    {
        public int Id { get; set; }
        public int IDClients { get; set; }
        public Client Client { get; set; }
        public int IDService { get; set; }
        public Service Service { get; set; }
        public int IDMaster { get; set; }
        public Master Master { get; set; }
        public DateTime Date { get; set; }



       
    }
}

