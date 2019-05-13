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
    static public void Main(string[] args)
    {
      //PhasePoint
      //    sizes = new PhasePoint(new List<double> { 3, 3, 3 }),
      //    steps = new PhasePoint(new List<double> { 1, 1, 1 }),
      //    ld = new PhasePoint(new List<double> { 1, 1, 1 });
      //Grid grid = Grid.BoxGrid(ld, sizes, steps);

      LibLinking<IDynamics> loader = new LibLinking<IDynamics>();
      IDynamics dynam = loader.FindLib("../SimpleMotion/bin/Debug/netcoreapp2.1/SimpleMotion.dll");
      
      LibLinking<ISigma> anotherloader = new LibLinking<ISigma>();
      ISigma sigma = anotherloader.FindLib("../Sigma/bin/Debug/netcoreapp2.1/Sigma.dll");
      

//            ControlConstraints cc =
//                ControlConstraints.BallConstraints(new PhasePoint(2), 1,
//                    new PhasePoint(new List<double> {0.1, 0.1}));
//            Grid g = Grid.BallGrid(new PhasePoint(2), 1,
//                new PhasePoint(new List<double> {0.1, 0.1}));

//            PhasePoint
//                u = new PhasePoint(new List<double> {1, 0}),
//                v = new PhasePoint(new List<double> {-2, 1}),
//                speed = dynam.f(0, new PhasePoint(), u, v);
//            Console.WriteLine(speed);

      PhasePoint
        center = new PhasePoint(new List<double> {0, 0}),
        steps = new PhasePoint(new List<double> {0.2, 0.2});
      double radiusP = 1;
      double radiusQ = 0.5;
      
      ControlConstraints P = ControlConstraints.BallConstraints(center, radiusP, steps);
      ControlConstraints Q = ControlConstraints.BallConstraints(center, radiusQ, steps);
      List<PhasePoint> res = new List<PhasePoint>();
      
      double t1 = 0;
      double T = 5;
      double delta = 0.25;
      double[] time = new double[21];
      for (int i = 0; i < time.Length; i++)
      {
        time[i] = t1 + i * delta;
      }
      
      Grid grid = Grid.BallGrid(center, 3, steps);
      foreach (PhasePoint x in grid.Keys)
        grid[x] = new SortedDictionary<double, double> {{T, sigma.sigma(x)}};
      
      for (int i = time.Length - 1; i >= 0; i--)
      {
        // Нужны два момента времени: текущий t и предыдущий t-delta,
        // на который пересчитываем
        {
          foreach (PhasePoint x in grid.Keys)
          {
            double velocity = Minmax(dynam, time[i], x, P, Q, grid);
            grid[x].Add(time[i] - delta, grid[x][time[i]] - delta * velocity);
          }
        }
      }

      Console.WriteLine(grid);
      Console.ReadKey();
    }
  }
}