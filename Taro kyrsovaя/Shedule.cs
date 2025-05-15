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
        public string IDClients { get; set; }
        public Client Client { get; set; }
        public string IDService { get; set; }
        public Service Service { get; set; }
        public string IDMaster { get; set; }
        public Master Master { get; set; }
        public DateTime Date { get; set; }
        

    }
}
