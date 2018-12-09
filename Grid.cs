using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchyProblem
{
    public class GridStorage : SortedDictionary<PhasePoint, Dictionary<double, double>>
    {
    }

    public class Grid
    {
        public PhasePoint Steps { get; protected set; }

        readonly GridStorage grid = new GridStorage();

        // Фабрики

        // Сетка, накинутая на прямоугольный параллелепипед
        public static Grid BoxGrid(PhasePoint ld, PhasePoint sizes, PhasePoint steps)
        {
            return null;
        }

        // Сетка, накинутая на шар
        public static Grid BallGrid(PhasePoint center, double radius, PhasePoint steps)
        {
            return null;
        }

        public static Grid SegmentGrid(PhasePoint beginning, PhasePoint end, PhasePoint steps)
        {
            return null;
        }

        // Может быть, еще что-то...

        public bool HasNeighbour(PhasePoint p1, int dir)
        {
            int absDir = Math.Abs(dir);
            PhasePoint p3 = new PhasePoint(p1);
            p3.coords[absDir] += Steps[absDir] * Math.Sign(dir);
            return grid.ContainsKey(p3);
        }
    }
}