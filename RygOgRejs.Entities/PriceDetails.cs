using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RygOgRejs.Entities
{
    public struct PriceDetails
    {
        private decimal destinationPrice;
        private decimal firstClassPrice;
        private decimal luggagePrice;
        private double TaxRate;

        public decimal DestinationPrice { get => destinationPrice; }
        public decimal FirstClassPrice { get => firstClassPrice; }
        public decimal LuggagePrice { get => luggagePrice; }

        public decimal GetTaxAmount()
        {
            return GetTotalWithTax() - GetTotalWithoutTax();
        }
        public decimal GetTotalWithoutTax()
        {
            return DestinationPrice + FirstClassPrice + LuggagePrice;
        }
        public decimal GetTotalWithTax()
        {
            return GetTotalWithoutTax() * (1 + Convert.ToDecimal(TaxRate));
        }
        public PriceDetails(decimal destinationPrice, decimal firstClassPrice, decimal luggagePrice)
        {
            TaxRate = 0.231;
            if (destinationPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(destinationPrice));
            if (firstClassPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(firstClassPrice));
            if (luggagePrice < 0)
                throw new ArgumentOutOfRangeException(nameof(luggagePrice));
            this.destinationPrice = destinationPrice;
            this.firstClassPrice = firstClassPrice;
            this.luggagePrice = luggagePrice;
        }
    }
}
