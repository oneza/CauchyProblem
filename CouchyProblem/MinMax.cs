using System.Collections.Generic;
using System.Linq;
using Contracts;

namespace CouchyProblem
{
  public class MinMax
  {
    public double Minmax(IDynamics dynam, double t, PhasePoint x, ControlConstraints u, 
      ControlConstraints v, Grid grid)
    {
      double maxmin = -1e38;
      PhasePoint neigh;
      foreach (var b in v.Keys)
      {
        double min = 1e38;
        foreach (var a in u.Keys)
        {
          PhasePoint speed = dynam.f(t, x, a, b);
          double sum = 0;
          for (int i = 0; i < x.Dim; i++)
          {
            if (speed[i] > 0)
            {
              if (grid.GetNeighbour(x, -i, out neigh))
              {
                sum += speed[i] * (grid[x][t] - grid[neigh][t]) / grid.Steps[i];
              }
              else
              {
                // Что делать??? Как аппроксимировать на краю сетки???
                sum += 0;
              }
            }
            else 
            {
              if (grid.GetNeighbour(x, i, out neigh))
              {
                sum += speed[i] * (grid[neigh][t] - grid[x][t]) / grid.Steps[i];
              }
              else
              {
                // Что делать??? Как аппроксимировать на краю сетки???
                sum += 0;
              }
            }
          }

          sum += grid[x][t];

          if (min > sum) min = sum;
        }

        if (maxmin < min) maxmin = min;
      }

      return maxmin;
    }
  }
}