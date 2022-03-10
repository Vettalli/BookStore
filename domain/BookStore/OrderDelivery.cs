using System;
using System.Collections.Generic;

namespace BookStore
{
    public class OrderDelivery  //object value ddd
    {
        public string UniqueCode { get; }

        public string Description { get; }

        public decimal DeliveryPrice { get; }

        public IReadOnlyDictionary<string, string> Parameters { get; }

        public OrderDelivery(string uniqueCode, string description, decimal deliveryPrice,IReadOnlyDictionary<string, string> parameters)
        {
            if (string.IsNullOrWhiteSpace(uniqueCode))
            {
                throw new ArgumentException(nameof(uniqueCode));
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException(nameof(description));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            UniqueCode = uniqueCode;
            Description = description;
            DeliveryPrice = deliveryPrice;
            Parameters = parameters;
        }
    }
}
