using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Company : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address FirstAddress { get; set; }
        public Address SecondAddress { get; set; }
        public JuristicalNature JuristicalNature { get; set; }
        public UseFrequency UseFrequency { get; set; }
    }
}
