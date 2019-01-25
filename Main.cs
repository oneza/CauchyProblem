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
            //PhasePoint
            //    sizes = new PhasePoint(new List<double> { 3, 3, 3 }),
            //    steps = new PhasePoint(new List<double> { 1, 1, 1 }),
            //    ld = new PhasePoint(new List<double> { 1, 1, 1 });
            //Grid grid = Grid.BoxGrid(ld, sizes, steps);
            PhasePoint
                center = new PhasePoint(new List<double> { 0, 0, 0 }),
                steps = new PhasePoint(new List<double> { 1, 1, 1 });
            double radius = 4;
            Grid grid = Grid.BallGrid(center, radius, steps);
            Console.WriteLine(grid);
            Console.ReadKey();
        }
    }
}