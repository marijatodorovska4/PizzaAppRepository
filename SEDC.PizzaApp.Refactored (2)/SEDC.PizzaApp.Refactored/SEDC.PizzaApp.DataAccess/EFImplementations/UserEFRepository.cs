using SEDC.PizzaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.PizzaApp.DataAccess.Implementations
{
    public class UserEFRepository : IRepository<User>
    {
        private PizzaAppDbContext _pizzaAppDbContext;

        public UserEFRepository(PizzaAppDbContext pizzaAppDbContext)
        {
            _pizzaAppDbContext = pizzaAppDbContext;
        }

        public List<User> GetAll()
        {
            //return domain models (all records from the table in DB)
            return _pizzaAppDbContext.Users.ToList();

        }

        public User GetById(int id)
        {
            //returns one record from a table in DB (by id)
            return _pizzaAppDbContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public int Insert(User entity)
        {
            _pizzaAppDbContext.Users.Add(entity);
            _pizzaAppDbContext.SaveChanges();

            return entity.Id;
        }

        public void Update(User entity)
        {
            _pizzaAppDbContext.Users.Update(entity);
            _pizzaAppDbContext.SaveChanges();
        }

        public void DeleteById(int id)
        {
            User userDb = _pizzaAppDbContext.Users.FirstOrDefault(x => x.Id == id);
            if (userDb == null)
            {
                throw new Exception($"The user with id {id} was not found!");
            }

            _pizzaAppDbContext.Users.Remove(userDb);
            _pizzaAppDbContext.SaveChanges();

        }
    }
}
