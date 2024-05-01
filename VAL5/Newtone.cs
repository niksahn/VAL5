using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAL5
{
    internal class Newtone
    {
        double[][] ixy;
        double[] divDif;
        double[] finDif;

       public Newtone(double[][] Ixy)
        {
            ixy = Ixy;
            divDif = DividedDifferences(ixy);
            finDif = FiniteDifferences(ixy);
        }
        private double Factorial(int n)
        {
            if (n == 0)
                return 1;
            else
                return n * Factorial(n - 1);
        }

        private double[] DividedDifferences(double[][] xy)
        {
            int n = xy.Length;
            double[,] diff = new double[n, n];

            for (int i = 0; i < n; i++)
                diff[i, 0] = xy[i][1];

            for (int j = 1; j < n; j++)
                for (int i = 0; i < n - j; i++)
                    diff[i, j] = (diff[i + 1, j - 1] - diff[i, j - 1]) / (xy[i + j][0] - xy[i][0]);

            double[] result = new double[n];
            for (int i = 0; i < n; i++)
                result[i] = diff[0, i];

            return result;
        }

        private double[] FiniteDifferences(double[][] xy)
        {
            int n = xy.Length;
            double[,] diff = new double[n, n];

            for (int i = 0; i < n; i++)
                diff[i, 0] = xy[i][1];

            for (int j = 1; j < n; j++)
                for (int i = 0; i < n - j; i++)
                    diff[i, j] = diff[i + 1, j - 1] - diff[i, j - 1];

            double[] result = new double[n];

            int _n = n;
            for (int i = 0; i < n; i++)
                result[i] = diff[_n-1, i];

            return result;
        }

        private double NewtonInterpolationFirstForm(double xVal, double[][] xy, double[] diff)
        {
            int n = xy.Length;
            double result = diff[0];

            for (int i = 1; i < n; i++)
            {
                double term = diff[i];
                for (int j = 0; j < i; j++)
                    term *= (xVal - xy[j][0]);
                result += term;
            }

            return result;
        }

        private double NewtonInterpolationSecondForm(double xVal, double[][] xy, double[] diffFinite)
        {
            int n = xy.Length;
            double result = diffFinite[0];

            for (int i = 1; i < n; i++)
            {
                double term = diffFinite[i];

                int _n = n - 1;
                for (int j = 0; j < i; j++)
                {
                    term *= (xVal - xy[_n--][0]);
                    term /= (xy[j + 1][0] - xy[j][0]);
                }
                term /= Factorial(i);

                result += term;
            }

            return result;
        }

        public double getNF(double x, double a, double b)
        {
            if (b - x > a - x)
            {
                return NewtonInterpolationFirstForm(x, ixy, divDif);

            }
            else
            {
                return NewtonInterpolationSecondForm(x,ixy, divDif);
            }
        }

        public double getNS(double x)
        {
            return NewtonInterpolationSecondForm(x, ixy, divDif);
        }

        //private double getDeltaY(double y, int i, int k)
        //{
        //    double res = ixy[i + k][1];
        //    for (int n = 0; n < k; n++)
        //    {
        //        double kk = 1;
        //        for (int ki = 0; ki <= n; ki++)
        //        {
        //            kk *= (k - ki)/(ki+1);
        //        }
        //        res += Math.Pow(-1, n+1) * kk * ixy[i + k - n-1][1];
        //    }
        //   // res += Math.Pow(-1, k) * ixy[i][1];
        //    return res;
        //}

        //private double fact(double n)
        //{
        //    double res = 1;
        //    for (double nn = n; nn > 0; nn--)
        //    {
        //        res *= nn;
        //    }
        //    return res;
        //}

        //private double firstForm(double x, double step)
        //{
        //    int n = ixy.Length;
        //    double t = Math.Abs((x - ixy[0][0]) / step);
        //    double res = ixy[0][1];
        //    for (int nn = 1; nn < n; nn++)
        //    {
        //        double tt = 1;
        //        for (int ki = 0; ki <= nn; ki++)
        //        {
        //            tt *= (t - ki)/(ki+1);
        //        }
        //        res += tt * getDeltaY(ixy[0][1], 0, nn);
        //    }
        //    return res;
        //}

        //private double secondForm(double x, double step)
        //{
        //    int n = ixy.Length;
        //    double t = Math.Abs((x - ixy[n-1][0]) / step);
        //    double res = ixy[n - 1][1];
        //    int i = 1;
        //    for (int nn = n - 2; nn > 0; nn--)
        //    {
        //        double tt = 1;
        //        for (int ki = 0; ki <= i; ki++)
        //        {
        //            tt *= (t + ki) / (ki + 1);
        //        }
        //        res += tt * getDeltaY(ixy[nn][1], nn, i);
        //        i++;
        //    }
        //    return res;
        //}
        //public double getN(double x, double b, double a)
        //{
        //    int n = ixy.Length;
        //    double step = ixy[1][0] - ixy[0][0];
        //    if (b - x - 1 > Math.Abs(x - a))
        //    {
        //        return secondForm(x,step);
        //    }
        //    else return secondForm(x,step);

        //}
    }
}
