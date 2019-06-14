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

      string DLLPath = "./bin/Debug/netcoreapp2.1/";
      LibLinking<IDynamics> loader = new LibLinking<IDynamics>();
      IDynamics dynam = loader.FindLib(DLLPath + "SimpleMotion.dll");
      
//      LibLinking<ISigma> sigmaloader = new LibLinking<ISigma>();
//      ISigma sigma = sigmaloader.FindLib(DLLPath + "Sigma.dll");
      
      double finalR = 1.0;
      LibLinking<IChi> chiloader = new LibLinking<IChi>();
      IChi chi = chiloader.FindLib(DLLPath + "CylinderSet_NoConstraints.dll", finalR);

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
        xSteps = new PhasePoint(new List<double> {0.2, 0.2}),
        pSteps = xSteps,
        qSteps = new PhasePoint(new List<double> {0.1, 0.1});
      double radiusP = 1;
      double radiusQ = 0.5;
      
      Grid grid = Grid.BallGrid(center, 3.0, xSteps);
      ControlConstraints P = ControlConstraints.BallConstraints(center, radiusP, pSteps);
      ControlConstraints Q = ControlConstraints.BallConstraints(center, radiusQ, qSteps);
      List<PhasePoint> res = new List<PhasePoint>();
      
      double t1 = 0;
      double T = 5;
      double delta = 0.1;
      int nT = (int)Math.Ceiling((T - t1) / delta);
      double[] time = new double[nT+1];
      for (int i = 0; i <= nT; i++)
      {
        time[nT - i] = T - i * delta;
      }
      
      //Grid grid = Grid.BallGrid(center, 3, steps);
      foreach (PhasePoint x in grid.Keys.ToList())
        grid[x] = new SortedDictionary<double, double> {{T, chi.chi(T, x)}};
      
      for (int i = time.Length - 1; i > 0; i--)
      {
        foreach (PhasePoint x in grid.Keys)
        {
          double velocity = Minmax(dynam, time[i], x, P, Q, grid);
          grid[x].Add(time[i-1], Math.Min(chi.chi(time[i-1], x), 
            grid[x][time[i]] + (time[i] - time[i-1]) * velocity));
        }
      }

      Console.WriteLine(grid);
      Console.ReadKey();
    }
  }
}