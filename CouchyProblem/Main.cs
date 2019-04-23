using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Contracts;

namespace CouchyProblem
{
    public partial class Test
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

            ControlConstraints cc =
                ControlConstraints.BallConstraints(new PhasePoint(2), 1,
                    new PhasePoint(new List<double> {0.1, 0.1}));
            Grid g = Grid.BallGrid(new PhasePoint(2), 1,
                new PhasePoint(new List<double> {0.1, 0.1}));

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
            Grid grid = Grid.BallGrid(center, 3, steps);
            List<PhasePoint> res = new List<PhasePoint>();
            foreach(double t in time) 
                // Нужны два момента времени: текущий t и предыдущий t-delta,
                // на который пересчитываем
            {
                foreach(PhasePoint x in grid.Keys)
                {
                    double velocity = Minmax(dynam, t, x, P, Q, grid);
                    grid[x].Add(t - delta, grid[x][t] +- delta * velocity);
                }
            }

            Console.ReadKey();
        }
    }
}