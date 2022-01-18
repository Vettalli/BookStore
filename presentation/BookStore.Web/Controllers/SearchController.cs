﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Controllers
{
    public class SearchController : Controller
    {
        private BookService _bookService;

        public SearchController(BookService bookService)
        {
             _bookService = bookService;
        }

        public IActionResult Index(string query)
        {
            var books = _bookService.GetAllByQuery(query);

            return View(books);
        }
    }
}
