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
            List<double> sizes = new List<double>() { 3, 3, 3 };
            List<double> ld = new List<double>() { 1, 1, 1 };
            List<double> steps = new List<double>() { 1, 1, 1, };
            PhasePoint p1 = new PhasePoint(sizes);
            PhasePoint p2 = new PhasePoint(steps);
            PhasePoint p3 = new PhasePoint(ld);
            Grid grid = Grid.BoxGrid(p3,p2,p1);
            Console.WriteLine(grid);
            Console.ReadKey();
        }
    }
}