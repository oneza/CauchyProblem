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
            int[]
                upperBounds = new int[ld.Dim],
                cnt = new int[ld.Dim];
            for (int i = 0; i < ld.Dim; i++)
            {
                upperBounds[i] = (int)(sizes[i] / steps[i]);
                cnt[i] = 0;
            }

            int curind;
            do
            {
                PhasePoint curpoint = new PhasePoint(ld, steps, cnt);
                grid.Add(curpoint, null);

                curind = 0;
                cnt[curind]++;
                while (curind < ld.Dim && cnt[curind] > upperBounds[curind])
                {
                    cnt[curind] = 0;
                    curind++;
                    if (curind < ld.Dim) cnt[curind]++;
                }
            } while (curind < ld.Dim);
            return grid;
        }

        // Сетка, накинутая на шар
        public static Grid BallGrid(PhasePoint center, double radius, PhasePoint steps)
        {
            Grid grid = new Grid();
            int[]
                lowerBounds = new int[center.Dim],
                upperBounds = new int[center.Dim],
                cnt = new int[center.Dim];

            lowerBounds[0] = -(int)(radius / steps[0]);
            upperBounds[0] = -lowerBounds[0];
            cnt[0] = lowerBounds[0]-1;

            int curind = 0;

            while (curind >= 0)
            {
                cnt[curind]++;
                if (cnt[curind] > upperBounds[curind]) curind--;
                else
                {
                    curind++;
                    if(curind < center.Dim)
                    {
                        lowerBounds[curind] = 
                            (int)Math.Ceiling(-Math.Sqrt(radius * radius - 
                                cnt.Take(curind).Select(x => x*x).Sum()) /steps[curind]);
                        upperBounds[curind] = -lowerBounds[curind];
                        cnt[curind] = lowerBounds[curind] - 1;
                    }
                    else
                    {
                        PhasePoint curpoint = new PhasePoint(center, steps, cnt);
                        grid.Add(curpoint, null);
                        curind--;
                    }
                }
            }

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
