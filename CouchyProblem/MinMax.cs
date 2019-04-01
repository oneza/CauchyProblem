using System.Collections.Generic;
using System.Linq;
using Contracts;

namespace CouchyProblem
{
    public class MinMax
    {
        public double Minmax(ControlConstraints u, ControlConstraints v, Grid grid, double p)
        {
            List<double> row = new List<double>();
            List<List<double>> column = new List<List<double>>();
            double sum = 0;
            foreach (var b in v.Keys)
            {
                foreach (var a in u.Keys)
                {
                    for (int i = 0; i < v.Keys.Count; i++)
                    {
                        if (f(T - t, grid.ElementAt(i).Key, u, v) > 0)
                        {
                            if (Grid.HasNeighbour(grid.ElementAt(i).Key, i, grid))
                            {
                                PhasePoint point = new PhasePoint(grid.ElementAt(i).Key);
                                PhasePoint point1 = new PhasePoint(point);
                                point1[i] += ai * p;
                                sum += ((v(point1) - v(point) / ai * p)) * f(T - t, point, u, v);
                            }
                            else
                            {
                                break;
                            }
                        }
                        else if (f(T - t, grid.ElementAt(i).Key, u, v) < 0)
                        {
                            if (Grid.HasNeighbour(grid.ElementAt(i).Key, i, grid))
                            {
                                PhasePoint point = new PhasePoint(grid.ElementAt(i).Key);
                                PhasePoint point1 = new PhasePoint(point);
                                point1[i] -= ai * p;
                                sum += ((v(point) - v(point1) / ai * p)) * f(T - t, point, u, v);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    
                    row.Add(sum);
                    sum = 0;
                }
                column.Add(row);
            }

            double res = column
                .Select(r => r.Min())
                .Max();

            return res;
        }
    }
}