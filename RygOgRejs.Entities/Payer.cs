using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RygOgRejs.Entities
{
    public class Payer : IPersistable
    {
        private int id;
        private string firstName;
        private string lastName;
        private string ssn;

        public int Id { get => id;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
                id = value;
            }
        }
        protected string FirstName { get => firstName;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(nameof(value));
                firstName = value;
            }
        }
        protected string LastName { get => lastName;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(nameof(value));
                lastName = value;
            }
        }
        protected string Ssn { get => ssn;
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
                    firstName = value;
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
        public Payer(string firstName, string lastName, string ssn, int id)
        {
            FirstName = firstName;
            LastName = lastName;
            Ssn = ssn;
            Id = id;
        }
    }
}
