using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RygOgRejs.Entities
{
    public class Payer
    {
        private string firstName;
        private string lastName;
        private string ssn;
        
        public string FirstName { get => firstName;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(nameof(value));
                firstName = value;
            }
        }
        public string LastName { get => lastName;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(nameof(value));
                lastName = value;
            }
        }
        public string Ssn { get => ssn;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(nameof(value));
                string[] strArr = value.Split('-');
                if (int.TryParse(strArr[0], out int a) && strArr[0].Length == 3 &&
                    int.TryParse(strArr[1], out a) && strArr[1].Length == 2 &&
                    int.TryParse(strArr[2], out a) && strArr[2].Length == 4 &&
                    value.Length == 11)
                {
                    ssn = value;
                }
                else
                    throw new ArgumentException(nameof(value));
            }
        }

        public Payer(string firstName, string lastName, string ssn)
        {
            FirstName = firstName;
            LastName = lastName;
            Ssn = ssn;
        }
    }
}
