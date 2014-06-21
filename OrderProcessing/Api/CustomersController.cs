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

namespace OrderProcessing.Api
{
    public class CustomersController : ApiController
    {
        private IOrderProcessingContext orderProcessingContext;

        public CustomersController(IOrderProcessingContext orderProcessingContext)
        {
            this.orderProcessingContext = orderProcessingContext;
        }

        // GET: api/Customers
        public IEnumerable<dynamic> GetCustomers()
        {
            return orderProcessingContext.Customers.Select(c => new { Id = c.Id, Name = c.Name });
        }

        // GET: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = orderProcessingContext.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Id)
            {
                return BadRequest();
            }

            orderProcessingContext.Update(customer);

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

            orderProcessingContext.Customers.Add(customer);
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