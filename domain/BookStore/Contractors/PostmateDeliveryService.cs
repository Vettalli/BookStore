using System;
using System.Collections.Generic;

namespace BookStore.Contractors
{
    public class PostmateDeliveryService : IDeliveryService
    {
        private IReadOnlyDictionary<string, string> cities = new Dictionary<string, string>
        {
            { "1", "Somwhere"},
            { "2", "Nowhere"}
        };

        private IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> postmates = new Dictionary<string, IReadOnlyDictionary<string, string>>
        {
            {
                "1",
                new Dictionary<string, string>
                {
                    { "1", "Railway station"},
                    { "2", "Pyterochka"},
                    { "3", "Circus"}
                }
            },
            {
                "2",
                new Dictionary<string, string>
                {
                    { "4", "The edge"},
                    { "5", "Crater"},
                    { "6", "Uncharted"}
                }
            }
        };

        public string UniqueCode => "Postmate";

        public string Title => "Delivery from somewhere to nowhere";

        public Form CreateForm(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            return new Form(UniqueCode, order.Id, 1, false, new List<Field>
            {
                new SelectionField("City", "city","1", cities)
            });
        }

        public Form MoveNext(int orderId, int step, IReadOnlyDictionary<string, string> values)
        {
            if (step == 1)
            {
                if (values["city"] == "1")
                {
                    return new Form(UniqueCode, orderId, 2, false, new Field[]
                    {
                        new HiddenField("City","city","1"),
                        new SelectionField("Postmate", "postmate", "1", postmates["1"])
                    });
                }
                else if (values["city"] == "2")
                {
                    return new Form(UniqueCode, orderId, 2, false, new Field[]
                    {
                        new HiddenField("City", "city", "2"),
                        new SelectionField("Postmate", "postmate","4", postmates["2"])
                    });
                }
                else
                {
                    throw new InvalidOperationException("Invalid postmate city");
                }
            }
            else if (step == 2)
            {
                return new Form(UniqueCode, orderId, 3, true, new Field[]
                {
                    new HiddenField("City", "city", values["city"]),
                    new HiddenField("Postmate", "postmate", values["postmate"])
                });
            }
            else
            {
                throw new InvalidOperationException("Invalid postmate step");
            }
            
        }
    }
}