﻿using DataAccess.Context;
using DataAccess.Models;

namespace IQueryablee
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppDbContext context = new();
            IQueryable<Product> products = context.Products.AsQueryable();
            IList<Product> productsList=products.Where(p=>p.Id>50000 && p.Id<55000).ToList();

        }
    }
}
