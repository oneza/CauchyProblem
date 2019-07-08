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
      string DLLPath = "./bin/Debug/netcoreapp2.1/";
      string inPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
      string outPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

      double t1 = 0;
      double T = 5;
      double delta = 0.025;
      int nT = (int) Math.Ceiling((T - t1) / delta);
      double[] time = new double[nT + 1];
      for (int i = 0; i <= nT; i++)
      {
        time[nT - i] = T - i * delta;
      }

/*      List<string> inArgs = new List<string>();
      using (StreamReader sr = new StreamReader(Path.Combine(inPath, "In.txt")))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          inArgs.Add(line);
        }
      }
      
      double T = Convert.ToDouble(inArgs[0]);
      double t1 = Convert.ToDouble(inArgs[1]);
      double delta = Convert.ToDouble(inArgs[2]);
      int nT = (int)Math.Ceiling((T - t1) / delta);
      double[] time = new double[nT+1];
      for (int i = 0; i <= nT; i++)
      {
        time[nT - i] = T - i * delta;
      }

      PhasePoint
        center = new PhasePoint(new List<double> {Convert.ToDouble(inArgs[4]),
          Convert.ToDouble(inArgs[5])}),
        xSteps = new PhasePoint(new List<double> {Convert.ToDouble(inArgs[7]),
          Convert.ToDouble(inArgs[8])}),
        pSteps = xSteps,
        qSteps = new PhasePoint(new List<double> {Convert.ToDouble(inArgs[19]),
          Convert.ToDouble(inArgs[20])});
      double radiusX = Convert.ToDouble(inArgs[6]);
      double radiusP = Convert.ToDouble(inArgs[12]);
      double radiusQ = Convert.ToDouble(inArgs[18]);
      if (inArgs[3] == "Ball")
      {
        Grid grid = Grid.BallGrid(center, Convert.ToDouble(inArgs[0]), xSteps);
      }
      if (inArgs[9] == "Ball")
      {
        ControlConstraints P = ControlConstraints.BallConstraints(center, radiusP, pSteps);
      }
      if (inArgs[15] == "Ball")
      {
        ControlConstraints Q = ControlConstraints.BallConstraints(center, radiusQ, qSteps);
      }
*/
      LibLinking<IDynamics> loader = new LibLinking<IDynamics>();
      IDynamics dynam = loader.FindLib(DLLPath + "SimpleMotion.dll");

      double finalR = 1.0;
      double finalT = T;

//      LibLinking<IChi> chi1loader = new LibLinking<IChi>();
//      IChi chi1 = chi1loader.FindLib(DLLPath + "CylinderSet.dll", finalR, finalT);

//      LibLinking<ITheta> theta1loader = new LibLinking<ITheta>();
//      ITheta theta1 = theta1loader.FindLib(DLLPath + "CylinderSet.dll", finalR, finalT);

/*      double t1 = 0;
      double T = 5;
      double delta = 0.1;
      int nT = (int)Math.Ceiling((T - t1) / delta);
      double[] time = new double[nT+1];
      for (int i = 0; i <= nT; i++)
      {
        time[nT - i] = T - i * delta;
      }
*/
      LibLinking<IChi> chiloader = new LibLinking<IChi>();
      IChi chi = chiloader.FindLib(DLLPath + "CylinderSet_NoConstraints.dll", finalR);

      LibLinking<ITheta> thetaloader = new LibLinking<ITheta>();
      ITheta theta = thetaloader.FindLib(DLLPath + "CylinderSet_NoConstraints.dll", finalR);

      /*          ControlConstraints cc =
                    ControlConstraints.BallConstraints(new PhasePoint(2), 1,
                        new PhasePoint(new List<double> {0.1, 0.1}));
                Grid g = Grid.BallGrid(new PhasePoint(2), 1,
                    new PhasePoint(new List<double> {0.1, 0.1}));
    
                PhasePoint
                    u = new PhasePoint(new List<double> {1, 0}),
                    v = new PhasePoint(new List<double> {-2, 1}),
                    speed = dynam.f(0, new PhasePoint(), u, v);
                Console.WriteLine(speed);      
                */

      PhasePoint
        center = new PhasePoint(new List<double> {0, 0}),
        xSteps = new PhasePoint(new List<double> {0.1, 0.1}),
        pSteps = xSteps,
        qSteps = new PhasePoint(new List<double> {0.1, 0.1});
      double radiusX = 4.6;
      double radiusP = 1;
      double radiusQ = 0.5;
      Grid grid = Grid.BallGrid(center, radiusX, xSteps);
      ControlConstraints P = ControlConstraints.BallConstraints(center, radiusP, pSteps);
      ControlConstraints Q = ControlConstraints.BallConstraints(center, radiusQ, qSteps);

      foreach (PhasePoint x in grid.Keys.ToList())
        grid[x] = new SortedDictionary<double, double> {{T, chi.chi(T, x)}};

      int length = time.Length;
      
 /*     Parallel.For(0, length  , i =>
      {
        foreach (PhasePoint x in grid.Keys)
        {
          double velocity = Minmax(dynam, time[length - i], x, P, Q, grid);
          grid[x].Add(time[length - i - 1], Math.Max(Math.Min(chi.chi(time[length - i - 1], x),
              grid[x][time[length - i]] + (time[length - i] - time[length - i - 1]) * velocity),
            theta.theta(time[length - i - 1], x)));
        }
      });*/


      for (int i = time.Length - 1; i > 0; i--)
      {
        foreach(PhasePoint x in grid.Keys)
        {
          double velocity = Minmax(dynam, time[i], x, P, Q, grid);
          grid[x].Add(time[i-1], Math.Max(Math.Min(chi.chi(time[i-1], x), 
            grid[x][time[i]] + (time[i] - time[i-1]) * velocity), 
            theta.theta(time[i-1], x)));
        }
      }

      using (StreamWriter outputFile = new StreamWriter(Path.Combine(outPath, "Result6.txt")))
      {
//        int i = 1;
        foreach (PhasePoint x in grid.Keys)
        {
          foreach (double t in grid[x].Keys)
          {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            //outputFile.WriteLine(i+") "+"Coordinates:" + " " + x + " " + "Time:" + " " + t + " " + "Result:" + " "+ Math.Round(grid[x][t], 8));
            outputFile.WriteLine(x.First() + " " + x.Last() +  " " + t + " " + Math.Round(grid[x][t], 8));
//            i++;
          }
        }
      }

      Console.WriteLine(grid);
      Console.ReadKey();
    }
  }
}