using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchyProblem
{
    public class Test
    {
        static void Main(string[] args)
        {
            List<double> c = new List<double>() { 1, 1, 0, 1.1, 1, 1 };
            List<double> c1 = new List<double>() { 1, 1, 0, 1, 1, 1 };
            PhasePoint p1 = new PhasePoint( c);
            PhasePoint p2 = new PhasePoint( c1);
            Console.WriteLine(p1.CompareTo(p2));
            Console.ReadKey();
        }
    }
}