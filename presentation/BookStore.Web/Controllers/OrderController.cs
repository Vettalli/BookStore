using BookStore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderController(IBookRepository bookRepository, IOrderRepository orderRepository)
        {
            _bookRepository = bookRepository;
            _orderRepository = orderRepository;
        }

        public IActionResult AddItem(int id)
        {
            Cart cart;
            Order order;
            //Если cart есть, то она загружается. Если в сессии её нет, то создаем пустую новую.
            if(HttpContext.Session.TryGetCart(out cart))
            {
                order = _orderRepository.GetById(cart.OrderId);
            }
            else
            {
                order = _orderRepository.Create();
                cart = new Cart(order.Id);
            }

            var book = _bookRepository.GetById(id);
            order.AddItem(book, 1);
            _orderRepository.Update(order);


            cart.TotalCount = order.TotalAmount;
            cart.TotalPrice = order.TotalPrice;
            HttpContext.Session.Set(cart);

            return RedirectToAction("Index", "Book", new { id });
        }

        public IActionResult Index()
        {
            if(HttpContext.Session.TryGetCart(out Cart cart))
            {
                var order = _orderRepository.GetById(cart.OrderId);
                var model = Map(order);

                return View(model);
            }

            return View("Empty");
        }

        private OrderModel Map(Order order)
        {
            var bookIds = order.Items.Select(i => i.BookId);
            var books = _bookRepository.GetAllByIds(bookIds);

            var itemModels = from item in order.Items
                             join book in books on item.BookId equals book.Id
                             select new OrderItemModel
                             {
                                 BookId = item.BookId,
                                 Author = book.Author,
                                 Title = book.Title,
                                 Count = item.Count,
                                 Price = item.Price
                             };

            return new OrderModel
            {
                Id = order.Id,
                Items = new List<OrderItemModel>(itemModels),
                TotalCount = order.TotalAmount,
                TotalPrice = order.TotalPrice
            };
        }
    }
}
