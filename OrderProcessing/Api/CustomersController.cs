using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OrderProcessing.Models;
using OrderProcessing.Models.Entities;
using OrderProcessing.Models.Context;

namespace OrderProcessing.Api
{
    public class CustomersController : ApiController
    {
        private IOrderProcessingContext orderProcessingContext;
        private const string CustomersUri = "api/customers/{0}/{1}/{2}";

        public CustomersController(IOrderProcessingContext orderProcessingContext)
        {
            this.orderProcessingContext = orderProcessingContext;
        }

        // GET: api/Customers
        [Route("api/customers")]
        [Route("api/customers/{pageNumber}/{pageSize}")]
        public PageResult<CustomerModel> GetCustomers(int pageNumber = 1, int pageSize = 10)
        {
            var pageIndex = pageNumber - 1;

            //NOTE: Total item count for the PageResult
            var totalCustomers = orderProcessingContext.Customers.Count();
            
            if (pageIndex < 0 || pageNumber * pageSize > totalCustomers)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var customers = orderProcessingContext.Customers.OrderBy(c => c.Id).Skip(pageIndex * pageSize).Take(pageSize).Select(c => new CustomerModel { Id = c.Id, Name = c.Name });
            return new PageResult<CustomerModel>(customers, totalCustomers, pageIndex == 0 ? string.Empty : string.Format(CustomersUri, pageNumber - 1, pageSize, ""), pageNumber * pageSize <= totalCustomers ? string.Format(CustomersUri, pageNumber + 1, pageSize, "") : string.Empty);
        }

        // GET: api/Customers/5
        [ResponseType(typeof(CustomerModel))]
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = orderProcessingContext.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(new CustomerModel { Id = customer.Id, Name = customer.Name });
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(int id, CustomerModel customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Id)
            {
                return BadRequest();
            }

            orderProcessingContext.Update(new Customer() { Id = id, Name = customer.Name });

            try
            {
                orderProcessingContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Customers
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            orderProcessingContext.Customers.Add(new Customer() { Name = customer.Name });
            orderProcessingContext.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = orderProcessingContext.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            orderProcessingContext.Customers.Remove(customer);
            orderProcessingContext.SaveChanges();

            return Ok(customer);
        }

        //NOTE: Use of Route Attribute
        [ResponseType(typeof(OrderModel))]
        [Route("api/Customers/{id}/Orders")]
        public IEnumerable<OrderModel> GetOrders(int id)
        {
            return orderProcessingContext.Orders
                .Where(o => o.Customer.Id == id)
                .Select(o => new OrderModel()
                {
                    Id = o.Id,
                    OrderNumber = o.OrderNumber,
                    CreatedDate = o.CreatedDate,
                    LastUpdatedDate = o.LastUpdatedDate
                });
        }

        [ResponseType(typeof(OrderModel))]
        [Route("api/Customers/{id}/Orders/{orderId}")]
        public IHttpActionResult GetOrders(int id, int orderId)
        {
            Order order = orderProcessingContext.Orders.Where(o => o.Customer.Id == id && o.Id == orderId).SingleOrDefault();
            if (order == null)
            {
                return NotFound();
            }

            return Ok(new OrderModel 
            { 
                Id = order.Id, 
                OrderNumber = order.OrderNumber, 
                CreatedDate = order.CreatedDate, 
                LastUpdatedDate = order.LastUpdatedDate
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return orderProcessingContext.Customers.Count(e => e.Id == id) > 0;
        }
    }
}