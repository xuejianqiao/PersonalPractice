using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test111
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<TestClass>() { new TestClass() { Id = 3, Name = "123" }, new TestClass() { Id = 4, Name = "123" } };
            for(int i=0;i<=5;i++)
            {
                if (list.Count <= 0)
                {
                  

                }
                else {
                    var model = list[0];
                    if (model != null)
                    {
                        list.Remove(model);
                    }
                }
            }
        }
    }


    public class TestClass
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
