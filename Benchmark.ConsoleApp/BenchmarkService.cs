﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace Benchmark.ConsoleApp
{
    [ShortRunJob,Config(typeof(Config))]
    public class BenchmarkService
    {
        private class Config : ManualConfig 
        {
            public Config() 
            {
                SummaryStyle=
                    BenchmarkDotNet.Reports.SummaryStyle.Default.WithRatioStyle
                    (BenchmarkDotNet.Columns.RatioStyle.Trend);
            }
        }

        [Benchmark(Baseline =true)]

        public void GetAll()
        {
            AppDbContext context = new();
            context.Products.ToList();
        }

        [Benchmark]

        public void GetAllSqlRaw() 
        {
            AppDbContext context = new();
            context.Products.FromSqlRaw("Select*from Products").ToList();
        }

    }
}
