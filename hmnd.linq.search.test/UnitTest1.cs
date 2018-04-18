using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace hmnd.linq.search.test
{
    public class UnitTest1
    {
        public List<SampleClass> sampleList { get; set; }

        public UnitTest1()
        {
            sampleList = new List<SampleClass>();
            sampleList.Add(new SampleClass(0, "Jonathan"));
            sampleList.Add(new SampleClass(10, "Foo"));
            sampleList.Add(new SampleClass(1, "Bar"));
        }

        [Fact]
        public void Test1()
        {
            var query = sampleList.AsQueryable();
            query = query.Search("jon");
            var result = query.ToList();

            Assert.True(result.Count() == 1, "should contain 1 element");
        }

        [Fact]
        public void Test2()
        {
            var query = sampleList.AsQueryable();
            query = query.Search("Foo");
            var result = query.ToList();

            Assert.True(result.Count() == 1, "should contain 1 element");
        }

        [Fact]
        public void Test3()
        {
            var query = sampleList.AsQueryable();
            query = query.Search(DateTime.Now.Year.ToString());
            var result = query.ToList();

            Assert.True(result.Count() == sampleList.Count(), "should contain " + sampleList.Count() + " element");
        }
    }
}
