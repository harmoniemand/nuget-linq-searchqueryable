using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class SampleClass
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public SampleClass(int id, string name)
        {
            this.Id = id;
            this.Name = name;
            this.Created = DateTime.Now;
        }
    }
}
