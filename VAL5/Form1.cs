using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VAL5
{
    public partial class Form1 : Form
    {
        double[][]N;
        double[][]L;
        const double a = 1;
        const double b = 5;

        const int DOTS = 500;
        Lagrange lagr;
        Newtone newt;

        public Form1()
        {
            InitializeComponent();
            chart1.ChartAreas[0].AxisX.Minimum = 1;
            chart1.ChartAreas[0].AxisX.Maximum = 6;
            chart1.ChartAreas[0].AxisX.Interval = 0.3;
            chart1.ChartAreas[0].AxisY.Minimum = -1.5;
      //      chart1.ChartAreas[0].AxisY.Maximum = 1.5;
       //     chart1.ChartAreas[0].AxisY.Interval = 0.3;
        }

        public void generateN()
        {
            N = new double[(int)numericUpDown6.Value][];
            double interval = (b - a) / (int) numericUpDown6.Value;
            N[0] = new double[2] { a+interval, f(a+interval) };
            for(int i = 1; i < numericUpDown6.Value; i++)
            {
                double x = N[i - 1][0] + interval;
                N[i] = new double[2] {x, f(x) };
            }
        }

        public void generateL()
        {
            L = new double [(int)numericUpDown6.Value][];
            var Random = new Random();
            double interval = (b - a) / (int)numericUpDown6.Value;
            L[0] = new double[2] { a, f(a) };
 
            for (int i = 1; i < numericUpDown6.Value-1; i++)
            {
                double x = N[i][0] - Random.NextDouble()/10;
                L[i] = new double[2] {x, f(x) };
            }
            L[(int)numericUpDown6.Value-1] = new double[2]{ b,f(b)};
           // Array.Sort(L);
        }

        double f(double x)
        {
            return 1-Math.Log(2*x/(1+x));
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            generateN();
            generateL();
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            for (int i = 0; i< (int)numericUpDown6.Value; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView2.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = N[i][0];
                dataGridView1.Rows[i].Cells[1].Value = N[i][1];
                dataGridView2.Rows[i].Cells[0].Value = L[i][0];
                dataGridView2.Rows[i].Cells[1].Value = L[i][1];
            }
            lagr = new Lagrange(L);
            newt = new Newtone(N);
            drawDiagram();

        }

        void drawDiagram()
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            //chart1.Series[3].Points.Clear();

            double interval = (b - a) / DOTS;
            for (double i = a; i< b; i+= interval) 
            {
                chart1.Series[2].Points.AddXY(i,f(i));
                double l =  lagr.getLagr(i);
                chart1.Series[1].Points.AddXY(i, lagr.getLagr(i));
                chart1.Series[0].Points.AddXY(i, newt.getNF(i,a,b));
             //   chart1.Series[3].Points.AddXY(i, newt.getNS(i));
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lagr == null) return;
            double x = (double)numericUpDown1.Value;
            nf1.Text = newt.getNF(x,a,b).ToString();
            lf1.Text = lagr.getLagr(x).ToString();
            real.Text = f(x).ToString();
            np1.Text = (Math.Abs(newt.getNF(x,a,b)-f(x))).ToString();
            lp1.Text = (Math.Abs(lagr.getLagr(x) - f(x))).ToString();
        }

        private void nf1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
