using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RygOgRejs.Entities
{
    public class Journey
    {
        private int adults;
        private int children;
        private PriceDetails currentPriceDetails;
        private DateTime departureDate;
        private Destination destination;
        private bool isFirstClass;
        private double luggageAmount;

        public int Adults { get => adults;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
                adults = value;
            }
        }
        public int Children { get => children;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
                children = value;
            }
        }
        public PriceDetails CurrentPriceDetails { get => currentPriceDetails; set => currentPriceDetails = value; }
        public DateTime DepartureDate { get => departureDate; set => departureDate = value; }
        public Destination Destination { get => destination; set => destination = value; }
        public bool IsFirstClass { get => isFirstClass; set => isFirstClass = value; }
        public double LuggageAmount { get => luggageAmount;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
                luggageAmount = value;
            }
        }

        public void AddLuggage(double amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            LuggageAmount += amount;
            UpdateCurrentPriceDetails();
        }
        public void AddPersons(int adults, int children)
        {
            if (adults < 0)
                throw new ArgumentOutOfRangeException(nameof(adults));
            Adults += adults;

            if (children < 0)
                throw new ArgumentOutOfRangeException(nameof(children));
            Children += children;

            UpdateCurrentPriceDetails();
        }
        public void ChangeDestinationTo(Destination newDestination)
        {
            destination = newDestination;
            UpdateCurrentPriceDetails();
        }
        public decimal GetCurrentDestinationPrice()
        {
            decimal price = 0;
            for (int i = 0; i < Adults; i++)
            {
                switch (Destination)
                {
                    case Destination.Billund:
                        price += 395;
                        break;
                    case Destination.Copenhagen:
                        price += 1595;
                        break;
                    case Destination.PalmaDeMallorca:
                        price += 4995;
                        break;
                    default:
                        break;
                }
            }
            for (int i = 0; i < Children; i++)
            {
                switch (Destination)
                {
                    case Destination.Billund:
                        price += 295;
                        break;
                    case Destination.Copenhagen:
                        price += 1395;
                        break;
                    case Destination.PalmaDeMallorca:
                        price += 3099;
                        break;
                    default:
                        break;
                }
            }
            return price;
        }
        public decimal GetCurrentFirstClassPrice()
        {
            decimal price = 0;
            if (IsFirstClass)
            {
                for (int i = 0; i < Adults + Children; i++)
                {
                    price += 1699;
                }
            }
            return price;
        }
        public decimal GetCurrentLuggagePrice()
        {
            decimal price = 0;
            if (LuggageAmount > 25)
            {
                price += Math.Ceiling(Convert.ToDecimal(LuggageAmount) - 25) * 290;
            }
            return price;
        }
        public decimal GetCurrentTotal()
        {
            return CurrentPriceDetails.GetTotalWithTax();
        }
        public Journey(Destination destination, DateTime departureDate, bool isFirstClass,
            int adults, int children, double luggageAmount)
        {
            Destination = destination;
            DepartureDate = departureDate;
            IsFirstClass = isFirstClass;
            Adults = adults;
            Children = children;
            LuggageAmount = luggageAmount;
            UpdateCurrentPriceDetails();
        }
        public void RemoveLuggage(double amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            LuggageAmount -= amount;

            UpdateCurrentPriceDetails();
        }
        public void RemovePersons(int adults, int children)
        {
            if (adults < 0 || adults > Adults)
                throw new ArgumentOutOfRangeException(nameof(adults));
            Adults -= adults;

            if (children < 0 || children > Children)
                throw new ArgumentOutOfRangeException(nameof(children));
            Children -= children;

            UpdateCurrentPriceDetails();
        }
        public void UpdateCurrentPriceDetails()
        {
            currentPriceDetails = new PriceDetails(GetCurrentDestinationPrice(), GetCurrentFirstClassPrice(), GetCurrentLuggagePrice());
        }
    }
}
