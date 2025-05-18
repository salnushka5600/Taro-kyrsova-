using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taro_kyrsovaя
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Dateregistration { get; set; }
        public int Age { get; set; }
        public List<Shedule> Shedules { get; set; } = new();

    }
}
