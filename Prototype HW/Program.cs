using System;

namespace Prototype_HW
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Customer C1 = new Customer("Ron","Pat",13);

            Bill B1 = new Bill(C1.clone(), DateTime.Now, 13.5f, 3.1f);

            Bill BForCustomer = B1.clone();
            C1.FirstName = "Dor";
            B1.Amount = 14.5f;

            Customer C2 = new Customer("Yoram","Tal",14);
            Bill B2 = new Bill(DeepCopy(C2),DateTime.Now,56.3f,13.7f);

            Bill BForCustomer2 = DeepCopy(B2);
        }


        public static T DeepCopy<T>(T t)
        {
            var Json = System.Text.Json.JsonSerializer.Serialize(t);
            T MyObject = System.Text.Json.JsonSerializer.Deserialize<T>(Json);
            return MyObject;
        }
    }

    

    interface IPrototype<T>
    {
        T clone();
    }

    public class Customer :IPrototype<Customer>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }

        public Customer(string firstName, string lastName, int id)
        {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
        }

        public Customer clone()
        {
            return new Customer(this.FirstName,this.LastName,this.Id);
        }
    }

    public class Bill : IPrototype<Bill>
    {
        public Customer Customer { get; set; }
        public DateTime Date { get; set; }
        public float Amount { get; set; }
        public float Tax { get; set; }

        public Bill(Customer customer, DateTime date, float amount, float tax)
        {
            Customer = customer;
            Date = date;
            Amount = amount;
            Tax = tax;
        }

        public Bill clone()
        {
            return new Bill(this.Customer.clone(),this.Date,this.Amount,this.Tax);
        }
    }
}
