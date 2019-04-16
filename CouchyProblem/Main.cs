using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

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

            LibLinking<IDynamics> loader = new LibLinking<IDynamics>();
            IDynamics dynam = loader.FindLib("./bin/Debug/netcoreapp2.1/SimpleMotion.dll");

            PhasePoint
                u = new PhasePoint(new List<double> {1, 0}),
                v = new PhasePoint(new List<double> {-2, 1}),
                speed = dynam.f(0, new PhasePoint(), u, v);
            Console.WriteLine(speed);

            PhasePoint
                center = new PhasePoint(new List<double> {0, 0}),
                steps = new PhasePoint(new List<double> {0.25, 0.25});
            double radiusP = 1;
            double radiusQ = 0.5;
            ControlConstraints P = ControlConstraints.BallConstraints(center, radiusP, steps);
            ControlConstraints Q = ControlConstraints.BallConstraints(center, radiusQ, steps);
            List<double> time = new List<double> {0, 1, 2, 3, 4, 5};
            

            Console.ReadKey();
        }
    }
}