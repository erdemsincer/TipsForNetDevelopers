using DataAccess.Context;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace EagerLazyLoading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppDbContext context = new();
            //lAZY LOADİNG
            IList<Product> products = context.Products.ToList();
            //Eager LOADİNG
            IList<Product> products1 = context.Products.Include(x => x.Category).ToList();
            Console.WriteLine("Hello, World!");
        }
    }
}
