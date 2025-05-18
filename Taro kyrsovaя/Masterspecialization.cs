using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taro_kyrsovaя
{
   public class Masterspecialization
    {
        public int Id { get; set; }
        public int IdMaster { get; set; }
        public Master Master { get; set; }
        public int Idspecialization { get; set; }
        public specialization Specialization { get; set; }
        
        
    }
}
