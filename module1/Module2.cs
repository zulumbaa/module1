//Створення простого застосунку для керування замовленнями
//Ваша програма повинна містити наступні елементи:
//Створення інтерфейсу IOrder, який містить методи для додавання товарів, видалення товарів та отримання загальної вартості замовлення.
//Створення класу Order, який реалізує інтерфейс IOrder та містить методи для роботи з замовленнями.
//Побудова ієрархії класів для товарів: базовий клас Product, який містить загальні властивості, та похідні класи, наприклад, FoodProduct, ElectronicProduct тощо.
//Використання конструкторів для ініціалізації об'єктів класів та деструкторів для звільнення ресурсів.
//Визначення події для сповіщення про зміну статусу замовлення та організація взаємодії об'єктів через цю подію.
//Реалізація узагальненого класу для зберігання списку товарів у замовленні.
//Створення класів винятків для обробки помилок під час роботи з замовленнями.
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace module1
{


    class Module2
    {
        //Визначення події для сповіщення про зміну статусу замовлення та організація взаємодії об'єктів через цю подію.
        public delegate void OrderChangedEventHandler(object s, OrderEvent e);

        public class OrderEvent : EventArgs
        {
            public string changed_status { get; set; }
            public OrderEvent(string changed_status)
            {
                this.changed_status = changed_status;
            }
        }
        //Створення інтерфейсу IOrder, який містить методи для додавання товарів, видалення товарів та отримання загальної вартості замовлення.
        interface IOrder<T>
        {
            public void Add(T product);
            public T Delete(T product);
            public double GetTotatCost();
            event OrderChangedEventHandler OrderChanged;

        }
        //Створення класу Order, який реалізує інтерфейс IOrder та містить методи для роботи з замовленнями.
        class Order<T> : IOrder<T> where T : Product
        {
            public ProductList<T> pl;
            private string event_message;
            public event OrderChangedEventHandler OrderChanged;
            public Order()
            {
                pl = new ProductList<T>();
                event_message = "Order was created";
            }

            public void Add(T product)
            {
                pl.List.Add(product);
                InvokeEvent($"A new product {product.Name} was added");
            }

            public T Delete(T product)
            {
                if (pl.List.Contains(product))
                {
                    throw new IsNotFoundProductEx($"{product.Name} is not exist in this order");
                }
                pl.List.Remove(product);
                InvokeEvent($"A product {product.Name} was deleted");
                return product;
            }

            public double GetTotatCost()
            {
                double sum = 0;
                foreach (T product in pl.List)
                {
                    sum += product.Cost;
                }
                return sum;
            }

            private void InvokeEvent(string new_massage)
            {
                event_message = new_massage;
                OrderChanged?.Invoke(this, new OrderEvent(event_message));
            }
        }
        //Побудова ієрархії класів для товарів: базовий клас Product, який містить загальні властивості, та похідні класи, наприклад, FoodProduct, ElectronicProduct тощо.
        class Product
        {
            public string Name { get; set; }
            public double Cost { get; set; }

            public Product(string n = "", double c = 0)
            {
                this.Name = n;
                this.Cost = c;
            }

        }

        class FoodProduct : Product
        {
            public DateTime ExpDate { get; set; }

            public FoodProduct(string n, double c, DateTime ed) : base(n, c)
            {
                this.ExpDate = ed;
            }
        }

        class ElectronicProduct : Product
        {
            public int YearsOfWarranty { get; set; }

            public ElectronicProduct(string n = "", double c = 0, int y = 0) : base(n, c)
            {
                this.YearsOfWarranty = y;
            }
        }
        //Реалізація узагальненого класу для зберігання списку товарів у замовленні.
        class ProductList<T> where T : Product
        {
            public List<T> List { get; set; }
            public ProductList()
            {
                List = new List<T>();
            }
        }
        //Створення класів винятків для обробки помилок під час роботи з замовленнями.
        public class IsNotFoundProductEx : Exception
        {
            public IsNotFoundProductEx(string message) : base(message) { }
        }

        static void Main(string[] args)
        {
            Order<Product> order = new Order<Product>();
            try
            {
                order.Add(new Product("Soap", 32));
                order.Add(new ElectronicProduct("Samsung Gallexy M8", 6300, 2));
                order.Add(new FoodProduct("Apples Golden", 12, DateTime.Now.AddDays(30)));

                Product p = new Product("Pan", 5);
                order.Delete(p);
            }
            catch(IsNotFoundProductEx e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
