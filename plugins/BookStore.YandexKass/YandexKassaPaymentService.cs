using BookStore.Contractors;
using BookStore.Web.Contractors;
using System.Collections.Generic;

namespace BookStore.YandexKass
{
    public class YandexKassaPaymentService : IPaymentService, IWebContractorService
    {
        public string UniqueCode => "YandexKassa";

        public string Title => "Payment by card";

        public string GetUri => "/YandexKassa/";  //если бы подключение было настоящим, то здесь был бы адрес из документации. В текущем случае по данному URL будет доступен этот проект

        public Form CreateForm(Order order)
        {
            return new Form(UniqueCode, order.Id, 1, false, new Field[0]);
        }

        public OrderPayment GetPayment(Form form)
        {
            return new OrderPayment(UniqueCode, "Payment by card", new Dictionary<string, string>());
        }

        public Form MoveNextForm(int orderId, int step, IReadOnlyDictionary<string, string> values)
        {
            return new Form(UniqueCode, orderId, 2, true, new Field[0]);
        }
    }
}
