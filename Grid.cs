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
            double[] lim = sizes.ToArray<double>();
            int len = lim.Length;

            int[] cnt = new int[len];
            for (int i = 0; i < len; i++)
            {
                cnt[i] = 0;
            }

            List<double> c = new List<double>();
            for (int i = 0; i < len; i++)
            {
                c.Add(0);
            }

            int curind = 0;
            while (cnt[curind] < lim[curind]- 1)
            {
                PhasePoint curpoint = new PhasePoint(c);
                for (int i = 0; i < len; i++)
                    if (i != cnt[curind])
                    {
                        curpoint.coords.Add(ld[i]);
                    }
                    else
                    {
                        curpoint.coords.Add(ld[i] + steps[i]);
                    }
                grid.Add(curpoint, null);
                while (curind < len)
                {
                    cnt[curind] = 0;
                    curind++;
                    if (curind < len) cnt[curind]++;
                }
            }
            return grid;
        }

        // Сетка, накинутая на шар
        public static Grid BallGrid(PhasePoint center, double radius, PhasePoint steps)
        {
            Grid grid = new Grid();

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
            Grid grid = new Grid();
            int absDir = Math.Abs(dir);
            PhasePoint p3 = new PhasePoint(p1);
            p3.coords[absDir] += Steps[absDir] * Math.Sign(dir);
            return grid.ContainsKey(p3);
        }
    }
}