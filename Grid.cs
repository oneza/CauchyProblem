using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchyProblem
{
    public class Grid : SortedDictionary<PhasePoint, Dictionary<double, double>>
    {
        public PhasePoint Steps { get; protected set; }

        //readonly GridStorage grid = new GridStorage();

        // Фабрики

        // Сетка, накинутая на прямоугольный параллелепипед
        public static Grid BoxGrid(PhasePoint ld, PhasePoint sizes, PhasePoint steps)
        {
            Grid grid = new Grid();
            // !!!!!! Писать тут !!!!!!
            return null;
        }

        // Сетка, накинутая на шар
        public static Grid BallGrid(PhasePoint center, double radius, PhasePoint steps)
        {
            Grid grid = new Grid();
//            for (int i = 0; i <)
//            {
//
//            }
            return grid;
        }

        public static Grid SegmentGrid(PhasePoint beginning, PhasePoint end, PhasePoint steps)
        {
            Grid grid = new Grid();
            PhasePoint currentpoint = beginning;
            while (currentpoint.CompareTo(end) != 0)
            {
                grid.Add(currentpoint, null);
                currentpoint += steps;
            }
            return grid;
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