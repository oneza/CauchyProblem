using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchyProblem
    
{
    public class Grid
    {
        public static double Step = 0.1;

        static SortedDictionary<PhasePoint, Dictionary<double, double>> grid = new SortedDictionary<PhasePoint, Dictionary<double, double>>();

        public static bool HasNeighbour(PhasePoint p1, int dir)
        {
            PhasePoint p3 = new PhasePoint(p1);
            p3.coords[dir] += Step;
            return grid.ContainsKey(p3);
        }

    }
}
