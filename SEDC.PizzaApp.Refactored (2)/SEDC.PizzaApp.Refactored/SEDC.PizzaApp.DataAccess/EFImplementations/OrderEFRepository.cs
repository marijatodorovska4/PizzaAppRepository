using Microsoft.EntityFrameworkCore;
using SEDC.PizzaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.PizzaApp.DataAccess.Implementations
{
    public class OrderEFRepository : IRepository<Order>
    {
        private PizzaAppDbContext _pizzaAppDbContext;
        public OrderEFRepository(PizzaAppDbContext pizzaAppDbContext)
        {
            _pizzaAppDbContext = pizzaAppDbContext;
        }
        public void DeleteById(int id)
        {
            Order orderDb = _pizzaAppDbContext.Orders.FirstOrDefault(x => x.Id == id);
            if (orderDb == null)
            {
                throw new Exception($"Order with id {id} was not found!");
            }

            _pizzaAppDbContext.Orders.Remove(orderDb);
            _pizzaAppDbContext.SaveChanges();
        }

        public List<Order> GetAll()
        {
           
           var orderDb = _pizzaAppDbContext.Orders
                .Include(x => x.PizzaOrders)
                .ThenInclude(x => x.Pizza)
                .Include(x => x.User)
                .ToList();

            return orderDb;
        }

        public Order GetById(int id)
        {
            //return StaticDb.Orders.FirstOrDefault(x => x.Id == id);


            var orderDb = _pizzaAppDbContext.Orders
                 .Include(x => x.PizzaOrders)
                 .ThenInclude(x => x.Pizza)
                 .Include(x => x.User)
                 .FirstOrDefault(x => x.Id == id);
            return orderDb;
        }

        public int Insert(Order entity)
        {
            //first do the increment, then assign the value
            //entity.Id = ++StaticDb.OrderId;
            //StaticDb.Orders.Add(entity);
            //return entity.Id;

            _pizzaAppDbContext.Orders.Add(entity);
            _pizzaAppDbContext.SaveChanges();

            return entity.Id;
        }

        public void Update(Order entity)
        {
            _pizzaAppDbContext.Orders.Update(entity);
            _pizzaAppDbContext.SaveChanges();
        }
    }
}
