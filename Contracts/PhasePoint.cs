using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public static class Tools
    {
        public static double Eps = 1e-7;

        public static bool EQ(double a, double b)
        {
            return Math.Abs(a - b) < Eps;
        }
        public static bool NE(double a, double b)
        {
            return Math.Abs(a - b) > Eps;
        }
        public static bool LT(double a, double b)
        {
            return a < b - Eps;
        }
        public static bool LE(double a, double b)
        {
            return a < b + Eps;
        }
        public static bool GT(double a, double b)
        {
            return a > b + Eps;
        }
        public static bool GE(double a, double b)
        {
            return a > b - Eps;
        }
    }

    public struct PhasePoint : IEnumerable<double>, IComparable<PhasePoint>
    {
        public List<double> coords;
        public double this[int i]
        {
            get { return coords[i];}
        }

        public int Dim
        {
            get { return coords.Count; }
        }

        public PhasePoint(int newDim)
        {
            coords = new List<double>(newDim);
            for (int i = 0; i < newDim; i++)
                coords.Add(0);
        }

        public PhasePoint(List<double> c)
        {
            coords = c;
        }

        public PhasePoint(PhasePoint prevPoint)
        {
            coords = new List<double>(prevPoint.coords);
        }

        public PhasePoint(PhasePoint basePoint, PhasePoint step, int[] indices)
        {
            coords = new List<double>(basePoint.coords);
            for (int i = 0; i < coords.Count; i++)
                coords[i] += indices[i] * step[i];
        }

        public static PhasePoint operator +(PhasePoint p1, PhasePoint p2)
        {
            PhasePoint p3 = new PhasePoint();
            p3.coords = new List<double>(p1.Dim);
            for (int i = 0; i < p1.coords.Count; i++)
            {
                p3.coords.Add(p1.coords[i] + p2.coords[i]);
            }
            return p3;
        }

        public static PhasePoint operator -(PhasePoint p1, PhasePoint p2)
        {
            PhasePoint p3 = new PhasePoint();
            p3.coords = new List<double>(p1.Dim);
            for (int i = 0; i < p1.coords.Count; i++)
            {
                p3.coords.Add(p1.coords[i] - p2.coords[i]);
            }
            return p3;
        }

        public static PhasePoint operator *(double alpha, PhasePoint p1)
        {
            return p1 * alpha;
        }

        public static PhasePoint operator *(PhasePoint p1, double alpha)
        {
            PhasePoint p3 = new PhasePoint();
            p3.coords = new List<double>(p1.Dim);
            for (int i = 0; i < p1.coords.Count; i++)
            {
                p3.coords.Add(alpha * p1.coords[i]);
            }
            return p3;
        }

        public static PhasePoint operator /(PhasePoint p1, double alpha)
        {
            PhasePoint p3 = new PhasePoint();
            p3.coords = new List<double>(p1.Dim);
            for (int i = 0; i < p1.coords.Count; i++)
            {
                p3.coords.Add(p1.coords[i] / alpha);
            }
            return p3;
        }

        public static double operator *(PhasePoint p1, PhasePoint p2)
        {
            double s = 0;
            for (int i = 0; i < p1.coords.Count; i++)
            {
                s = s + (p1.coords[i] * p2.coords[i]);
            }
            return s;
        }

        public int CompareTo(PhasePoint that)
        {
            int res = 0;
            for (int i = 0; i < that.coords.Count && res == 0; i++)
            {
                if (Tools.LT(this.coords[i], that.coords[i])) res = -1;
                else if (Tools.GT(this.coords[i], that.coords[i])) res = +1;
            }
            return res;
        }

        public IEnumerator<double> GetEnumerator()
        {
            return coords.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return coords.GetEnumerator();
        }

        public override string ToString()
        {
            string res = "(";
            for (int i = 0; i < Dim; i++)
            {
                if (i > 0) res += ",";
                res += coords[i];
            }
            res += ")";
            return res;
        }
    }
}