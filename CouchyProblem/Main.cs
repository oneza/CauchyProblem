using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Contracts;
using System.IO;

namespace CouchyProblem
{
  public partial class Test
  {
    static public void Main(string[] args)
    {
      
      double t1 = 0;
      double T = 5;
      double delta = 0.1;
      int nT = (int)Math.Ceiling((T - t1) / delta);
      double[] time = new double[nT+1];
      for (int i = 0; i <= nT; i++)
      {
        time[nT - i] = T - i * delta;
      }
      
      string DLLPath = "./bin/Debug/netcoreapp2.1/";
      
      LibLinking<IDynamics> loader = new LibLinking<IDynamics>();
      IDynamics dynam = loader.FindLib(DLLPath + "SimpleMotion.dll");
      
      double finalR = 1.0;
      double finalT = T;
      
      LibLinking<IChi> chiloader = new LibLinking<IChi>();
      IChi chi = chiloader.FindLib(DLLPath + "CylinderSet_NoConstraints.dll", finalR);
      
      LibLinking<IChi> chi1loader = new LibLinking<IChi>();
      IChi chi1 = chi1loader.FindLib(DLLPath + "CylinderSet.dll", finalR, finalT);
      
      string inPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
      string outPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

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
      List<string> inArgs = new List<string>();
      using (StreamReader sr = new StreamReader(Path.Combine(inPath, "In.txt")))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          inArgs.Add(line);
        }
      }

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
      
      //Grid grid = Grid.BallGrid(center, 3, steps);
      foreach (PhasePoint x in grid.Keys.ToList())
        grid[x] = new SortedDictionary<double, double> {{T, chi1.chi(T, x)}};
      
      for (int i = time.Length - 1; i > 0; i--)
      {
        foreach (PhasePoint x in grid.Keys)
        {
          double velocity = Minmax(dynam, time[i], x, P, Q, grid);
          grid[x].Add(time[i-1], Math.Min(chi1.chi(time[i-1], x), 
            grid[x][time[i]] + (time[i] - time[i-1]) * velocity));
        }
      }

      using (StreamWriter outputFile = new StreamWriter(Path.Combine(outPath, "Result2.txt")))
      {
        int i = 1;
        foreach (PhasePoint x in grid.Keys)
        {
          foreach (double t in grid[x].Keys)
          {
            outputFile.WriteLine(i+") "+"Coordinates:" + " " + x + " " + "Time:" + " " + t + " " + "Result:" + " "+ Math.Round(grid[x][t], 8));
            i++;
          }
        }
      }

      Console.WriteLine(grid);
      Console.ReadKey();
    }
  }
}