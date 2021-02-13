﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesTaxProblem
{
    public class Program
    {
        static void Main(string[] args)
        {
            IProductFactory productFactory = new ProductFactory();
            List<Product> cartItems = new List<Product>();
            int count = 0;

            while (true)
            {
                Console.WriteLine("----------------- Shopping Cart -------------");
                Console.WriteLine("Add Items to Cart");
                Console.WriteLine("");
                Console.WriteLine($"Item {++count}");
                Console.WriteLine("");

                Console.WriteLine("Name : ");
                var name = Console.ReadLine();

                Console.WriteLine("Price : ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    throw new Exception("Invalid price");
                }

                Console.WriteLine("Quantity : ");
                if (!int.TryParse(Console.ReadLine(), out int quantity))
                {
                    throw new Exception("Invalid quantity");
                }

                Console.WriteLine("Is imported : (Y/N) [any other key except y/Y is considered as N]");
                var isImported = Console.ReadLine().ToLower() == "y" ? true : false;

                Console.WriteLine("Type of Item (select one of below)\n1. Book\n2. Food\n3. Medical\n4. Other");

                if (!Enum.TryParse<GoodsType>(Console.ReadLine(), out GoodsType goodsType))
                {
                    throw new Exception("Invalid type");
                }

                Console.WriteLine("Do you want to add more items? (Y/N) [any other key except y/Y is considered as N]");
                var input = Console.ReadLine().ToLower();

                var product = productFactory.GetProduct(name, Convert.ToDecimal(price), Convert.ToInt32(quantity), goodsType, isImported);
                cartItems.Add(product);

                if (input == "y")
                {
                    continue;
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine("\n");
            Console.WriteLine("Your Billing details are below");
            Console.WriteLine("------- Date : " + DateTime.Now.ToString("dd/MM/yyyy") + " -------");
            Console.WriteLine("");
            Console.WriteLine("Item Name (Quantity)    Price   Is Imported    Tax");
            Console.WriteLine("");

            foreach (var item in cartItems)
            {
                Console.WriteLine(item.Name + " (" + item.Quantity + ")" + "                    "
                                            + item.Price + "    "
                                            + item.IsImported + "   "
                                            + item.CalculateTax());
            }

            Console.WriteLine("\n");

            Console.WriteLine($"----------------- Total Items : {cartItems.Count()} -----------------\n");
            Console.WriteLine("Price : " + cartItems.Sum(item => item.Price));
            Console.WriteLine("Service Tax (Imported Tax + Service Tax) : " + cartItems.Sum(item => item.CalculateTax()));

            Console.ReadLine();
        }
    }
}