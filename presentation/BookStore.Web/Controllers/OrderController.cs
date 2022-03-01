using BookStore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using BookStore.Messages;
using System.Text.RegularExpressions;
using System;
using Microsoft.AspNetCore.Http;

namespace BookStore.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly INotificationService _notificationService;

        public OrderController(IBookRepository bookRepository, IOrderRepository orderRepository, INotificationService notificationService)
        {
            _bookRepository = bookRepository;
            _orderRepository = orderRepository;
            _notificationService = notificationService;
        }

        [HttpPost]
        public IActionResult AddItem(int bookId, int count = 1)
        {
            (Order order, Cart cart) = GetOrCreateOrderAndCart();

            var book = _bookRepository.GetById(bookId);

            order.AddOrUpdateItem(book, count);

            SaveOrderAndCart(order, cart);

            return RedirectToAction("Index", "Book", new { id = bookId});
        }

        [HttpPost]
        public IActionResult UpdateItem(int bookId, int count)
        {
            (Order order, Cart cart) = GetOrCreateOrderAndCart();

            order.GetItem(bookId).Count = count;

            SaveOrderAndCart(order, cart);

            return RedirectToAction("Index", "Order");
        }

        [HttpGet]
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

        [HttpPost]
        public ActionResult RemoveItem(int bookId)
        {
            (Order order, Cart cart) = GetOrCreateOrderAndCart();

            order.RemoveItem(bookId);

            SaveOrderAndCart(order, cart);

            return RedirectToAction("Index", "Order");
        }

        private OrderModel Map(Order order)
        {
            var bookIds = order.Items.Select(item => item.BookId);
            var books = _bookRepository.GetAllByIds(bookIds);

            var itemModels = from item in order.Items
                             join book in books on item.BookId equals book.Id
                             select new OrderItemModel
                             {
                                 BookId = book.Id,
                                 Title = book.Title,
                                 Author = book.Author,
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

        private (Order, Cart) GetOrCreateOrderAndCart()
        {
            Order order;

            if (HttpContext.Session.TryGetCart(out Cart cart))
            {
                order = _orderRepository.GetById(cart.OrderId);
            }
            else 
            {
                order = _orderRepository.Create();
                cart = new Cart(order.Id);
            }

            return (order, cart);
        }

        private void SaveOrderAndCart(Order order, Cart cart)
        {
            _orderRepository.Update(order);

            cart.TotalCount = order.TotalAmount;
            cart.TotalPrice = order.TotalPrice;

            HttpContext.Session.Set(cart);
        }

        [HttpPost]
        public IActionResult SendConfirmationCode(string cellPhone, int id)
        {
            var order = _orderRepository.GetById(id);
            var model = Map(order);

            if (!IsValidCellPhone(cellPhone))
            {
                model.Errors["cellPhone"] = "Your phone number doesn't fit to the standart +7XXXXXXXXXX";

                return View("Index", model);
            }

            Random random = new Random();
            var code = random.Next(1000, 9999);
            HttpContext.Session.SetInt32(cellPhone, code);
            _notificationService.SendNotificationCode(cellPhone, code);            

            return View("Confirmation", new ConfirmationModel { OrderId = id, CellPhone = cellPhone});
        }

        private bool IsValidCellPhone(string cellPhone)
        {
            if (cellPhone == null)
            {
                return false;
            }

            cellPhone = cellPhone.Replace(" ", "")
                                 .Replace("-", "");

            return Regex.IsMatch(cellPhone, @"^\+?\d{11}$");
        }

        [HttpPost]
        public IActionResult StartDelivery(int id, string cellPhone, int code)
        {
            int? storedCode = HttpContext.Session.GetInt32(cellPhone);

            if (storedCode == null)
            {
                return View("Confirmation",
                    new ConfirmationModel
                    {
                        OrderId = id,
                        CellPhone = cellPhone,
                        Errors = new Dictionary<string, string>
                        {
                            { "code", "The code not found! Try another code"}
                        }
                    });
            }

            if (storedCode != code)
            {
                return View("Confirmation",
                    new ConfirmationModel
                    {
                        OrderId = id,
                        CellPhone = cellPhone,
                        Errors = new Dictionary<string, string>
                        {
                            {"code","This is not the right code!" }
                        }
                    });                
            }

            return View();
        }
    }
}
