using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var sampleList = new List<SampleClass>();

            sampleList.Add(new SampleClass(0, "Jonathan"));
            sampleList.Add(new SampleClass(10, "Foo"));
            sampleList.Add(new SampleClass(1, "Bar"));


            var query = sampleList.AsQueryable();
            query = query.Search("2018");

            var result = query.ToList();

            Console.WriteLine("Hello World!");
        }
    }
}
