using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;

namespace Northwind.Controllers
{
    public class APIController : Controller
    {
        // this controller depends on the NorthwindRepository (dependency injection)
        private INorthwindRepository repository;
        public APIController(INorthwindRepository repo) => repository = repo;

        [HttpGet, Route("api/product")] // This brings JSON (test in Postman)
        // returns list of all products
        public IEnumerable<Product> Get() => repository.Products.OrderBy(p => p.ProductName);

        [HttpGet, Route("api/product/{id}")]
        // returns specific product
        public Product Get(int id) => repository.Products.FirstOrDefault(p => p.ProductId == id);

        [HttpGet, Route("api/product/discontinued/{discontinued}")]
        // returns all products where discontinued = true/false
        public IEnumerable<Product> GetDiscontinued(bool discontinued) => repository.Products.Where(p => p.Discontinued == discontinued).OrderBy(p => p.ProductName);

        [HttpGet, Route("api/category/{CategoryId}/product")] 
        // returns all products in a specific category
        public IEnumerable<Product> GetByCategory(int CategoryId) => repository.Products.Where(p => p.CategoryId == CategoryId).OrderBy(p => p.ProductName);

        [HttpGet, Route("api/category/{CategoryId}/product/discontinued/{discontinued}")]
        // returns all products in a specific category where discontinued = true/false
        public IEnumerable<Product> GetByCategoryDiscontinued(int CategoryId, bool discontinued) => repository.Products.Where(p => p.CategoryId == CategoryId && p.Discontinued == discontinued).OrderBy(p => p.ProductName);

        [HttpGet, Route("api/category/{CategoryId}/product/discontinued/{discontinued}/maxprice/{maxprice}")]
        // returns all products in a specific category where discontinued = true/false
        // and where unitprice <= maxprice
        public IEnumerable<Product> GetByCategoryDiscontinuedPrice(int CategoryId, bool discontinued, int maxprice) => repository.Products.Where(p => p.CategoryId == CategoryId && p.Discontinued == discontinued && p.UnitPrice <= maxprice).OrderBy(p => p.ProductName);

        [HttpGet, Route("api/order/notshipped")] //?
        // returns list of all orders not shipped
        public IEnumerable<Order> GetOrdersNotShippedYet() => repository.Orders.Where(o => o.ShippedDate == null).OrderBy(o => o.OrderDate);

        //[HttpGet, Route("api/order")] //?
        // returns list of all orders not shipped
        //public IEnumerable<Order>GetOrders() => repository.Orders.OrderBy(o => o.RequiredDate);
        //public IEnumerable<Order> GetOrders() => repository.Orders.Where(o => o.ShippedDate == null && o.RequiredDate <= (DateTime.Now.AddDays(7))).OrderBy(o => o.RequiredDate);
        //public IEnumerable<Order> GetOrders() => repository.Orders.Where(o => o.ShippedDate == null && o.RequiredDate <= DateTime.Now).OrderBy(o => o.RequiredDate);

        [HttpGet, Route("api/order")] //?
        // returns list of all orders not shipped
        //public IEnumerable<Order>GetOrders() => repository.Orders.OrderBy(o => o.RequiredDate);
        //public IEnumerable<Order> GetOrders() => repository.Orders.Where(o => o.ShippedDate == null && o.RequiredDate <= (DateTime.Now.AddDays(7))).OrderBy(o => o.RequiredDate);
        public IEnumerable<Order> GetOrders() => repository.Orders.OrderBy(o => o.RequiredDate);

        [HttpGet, Route("api/order/take/{num}")] //?
        // returns list of all orders not shipped
        //public IEnumerable<Order>GetOrders() => repository.Orders.OrderBy(o => o.RequiredDate);
        //public IEnumerable<Order> GetOrders() => repository.Orders.Where(o => o.ShippedDate == null && o.RequiredDate <= (DateTime.Now.AddDays(7))).OrderBy(o => o.RequiredDate);
        public IEnumerable<Order> GetOrdersTakeX(int num) => repository.Orders.Take(num).OrderBy(o => o.RequiredDate);

        //FIX THIS TO BE REQUIRED WEEK
        //[HttpGet, Route("api/order/required/{num1}")] 
        // returns list of all orders ordered by shipped date required in a week ??
        //public IEnumerable<Order> GetOrdersRequiredSoon(int num1) => repository.Orders.Where(o => o.ShippedDate == null).Where(o => o.RequiredDate <= DateTime.Now.AddDays(num1) && o.RequiredDate >= DateTime.Now).OrderBy(o => o.OrderDate);


        [HttpGet, Route("api/order/required/{num1}")]
        // returns list of all orders ordered by shipped date required in a week ??
        public IEnumerable<Order> GetOrdersRequiredSoon2(int num1) => repository.Orders.Where(o => o.ShippedDate == null && o.RequiredDate <= DateTime.Now.AddDays(num1) && o.RequiredDate >= DateTime.Now.Date).OrderBy(o => o.OrderDate);


        [HttpGet, Route("api/order/requiredtoday")]
        // returns list of all orders ordered by shipped date required in a week ??
        public IEnumerable<Order> GetOrdersRequiredSoon() => repository.Orders.Where(o => o.ShippedDate == null && o.RequiredDate == DateTime.Today).OrderBy(o => o.OrderDate);

        [HttpGet, Route("api/order/required/overdue")]
        // returns list of orders overdue (past required date)
        public IEnumerable<Order> GetOrdersOverdue() => repository.Orders.Where(o => o.ShippedDate == null && o.RequiredDate < DateTime.Today).OrderBy(o => o.OrderDate);

        /*[HttpGet, Route("api/order/required/{num1}")]
        // returns list of all orders ordered by shipped date required in a week ??
        public IEnumerable<Order> GetOrdersPastRequired(int num1) => repository.Orders.Where(o => o.ShippedDate == null).Where(o => o.RequiredDate >= DateTime.Now.AddDays(num1)).OrderBy(o => o.OrderDate);
        */
        //[HttpGet, Route("api/order/required/{num2}")] 
        // returns list of all orders ordered by shipped date required today
        //public IEnumerable<Order> GetOrdersRequiredToday(int num2) => repository.Orders.Where(o => o.ShippedDate == null).Where(o => o.RequiredDate == DateTime.Now.w.AddDays(num1)).OrderBy(o => o.OrderDate);



    }
}
