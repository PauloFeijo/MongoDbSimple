using MongoDB.Driver;
using MongoDbSimple.Entities;
using System;

namespace MongoDb
{
    class Program
    {
        static void Main(string[] args)
        {
            var customer = new Customer()
            {
                Id = Guid.NewGuid(),
                Name = "Paul",
                Address = "Street blue bird, 457",
                Phone = "1234567489"
            };

            // Create
            SaveCustomerOnMongoDb(customer);

            // Get
            var customer2 = GetCustomerFromMongoDb(customer.Id);

            // Update
            customer2.Name = "Joe";
            UpdateCustomerOnMongoDb(customer2);

            // Delete
            DeleteCustomerOnMongoDb(customer2);
        }

        private static IMongoCollection<Customer> CreateCollecion()
        {
            var connectionString = "mongodb://root:root@localhost:27017";
            var databaseName = "local";
            var mongoCLient = new MongoClient(connectionString);
            var dataBase = mongoCLient.GetDatabase(databaseName);
            return dataBase.GetCollection<Customer>(nameof(Customer));
        }

        private static void SaveCustomerOnMongoDb(Customer customer)
        {
            CreateCollecion().InsertOne(customer);
            Console.WriteLine($"Customer {customer.Name} inserted on mongoDb");
        }

        private static void UpdateCustomerOnMongoDb(Customer customer)
        {
            CreateCollecion().ReplaceOne(x => x.Id == customer.Id, customer);
            Console.WriteLine($"Customer {customer.Name} updated on mongoDb");
        }

        private static Customer GetCustomerFromMongoDb(Guid id)
        {
            var customer = CreateCollecion().Find(x => x.Id == id).FirstOrDefault();
            Console.WriteLine($"Customer {customer.Name} getted from mongoDb");
            return customer;
        }

        private static void DeleteCustomerOnMongoDb(Customer customer)
        {
            CreateCollecion().DeleteOne(x => x.Id == customer.Id);
            Console.WriteLine($"Customer {customer.Name} deleted from mongoDb");
        }
    }
}
