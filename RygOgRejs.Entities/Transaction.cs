using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RygOgRejs.Entities
{
    public struct Transaction
    {
        private int id;
        private decimal amount;
        private Journey journey;
        private Payer payer;
        private DateTime timeStamp;

        public int Id { get => id; }
        public Journey Journey { get => journey; }
        public Payer Payer { get => payer; }
        public DateTime TimeStamp { get => timeStamp; }

        public Transaction(decimal amount, Journey journey, Payer payer, int id)
        {
            this.journey = journey;
            this.payer = payer;
            this.amount = journey.GetCurrentTotal();
            timeStamp = DateTime.Now;
            this.id = id;
        }
    }
}
