using IBatisNet.DataMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var bbb = new BBB();
            var c = bbb.MyTest();
            var d = MyClass.MyTest(bbb);
        }
    }

    public interface AAA{

    }

    public class BBB : AAA { 
    }

    public static class MyClass{
        public static String MyTest(this AAA aaa) {
            return aaa.ToString();
        }
    } 
}
