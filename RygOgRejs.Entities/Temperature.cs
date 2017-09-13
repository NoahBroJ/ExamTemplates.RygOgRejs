using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RygOgRejs.Entities
{
    public class Temperature
    {
        private string temp;

        public string Temp
        {
            get { return temp; }
        }

        public Temperature(Dictionary<string, double> main)
        {
            temp = main["temp"] - 273.15 + " °C";
        }
    }
}
