using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAL5
{
    internal class Lagrange
    {
        double[][] ixy;
      
        public double getLagr(double x)
        {
            double rez = 0;
            for (int n = 0; n < ixy.Length; n++)
            {
                double l = getl(x, n);
                rez += l * ixy[n][1];
            }
            return rez;
        }

        private double getl(double x, int i)
        {
            double up = 1;
            double down = 1;
            for(int n = 0; n<ixy.Length; n++)
            {
                if (n != i)
                {
                 up *= x - ixy[n][0];
                 down *= (ixy[i][0] - ixy[n][0]);
                }
            }
            return up / down;
        }
        public Lagrange(double[][] iXY) {
            ixy = iXY;
        }
    }
}
