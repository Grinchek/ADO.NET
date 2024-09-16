using System;
using System.Collections.Generic;
using System.Linq;
namespace _02_linqToSql
{

    internal class Program
    {
        static void Main(string[] args)
        {
            
            int[] ints= { 1, 2,3,-23,123,-43, 56,76,-99,-1 };
            //=================================================
            IEnumerable<int> query = from i in ints
                                     where i > 0
                                     select i;
            var res=ints.Select(i => i>0);
            //=================================================
            double query1=(from i in ints
                                    where (i > 10&&i<100)
                                    select i).Average();
            var res2 =ints.Where(i =>( i>=10&&i<100)).Average();
            //=================================================
            int[] years = { 1998, 2002,2003, 2023, 1980, 2012, 1993, 2011, 1990, 2024 };
            IEnumerable<int> query3 = from i in years
                                      where i % 4 == 0
                                      orderby i
                                      select i;
            var res3 = years.Where(i => i % 4 == 0).OrderByDescending(i=>i);
            //=================================================
            int query4 = (from i in ints
                                      where i % 2 == 0
                                      select i).Max();
            var res4= ints.Where(i=>i%2==0).Max();
            //=================================================
            string[] strings = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "99" };
            IEnumerable<string> query5 = from i in strings
                                         select i + "!!!";
            var res5 = strings.Select(i =>i+"!!!");
            //foreach(var resu in query5)
            //{
            //    Console.WriteLine(resu);
            //}
            //=================================================
            char symb = '#';
            IEnumerable<string> query6 = from i in strings
                                         select i + symb;
            var res6 = strings.Select(i => i + symb);
            foreach (var resu in res6)
            {
                Console.WriteLine(resu);
            }
            //=================================================
            IEnumerable<IGrouping<int, string>> query7 = from i in strings
                                                         group i by i.Length;
            var res7 = strings.GroupBy(i => i.Length).Select(i=>i);
            foreach (var item in query7)
            {
                Console.WriteLine(item.Key);
                foreach (var item1 in item)
                {
                    Console.WriteLine(item1);
                }
            }
                                       


        }
    }
}
