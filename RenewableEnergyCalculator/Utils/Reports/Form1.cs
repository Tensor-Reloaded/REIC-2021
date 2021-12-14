using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using System.Drawing.Imaging;
using REIC;

namespace chart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            report.Series[0].ChartType = SeriesChartType.Pie;

            EnergyCalculator energyCalculator = new EnergyCalculator();
            energyCalculator.totalEnergy = 7.409;
            energyCalculator.REenergy = 1.402;
            double per = energyCalculator.CalculatePercent(energyCalculator.totalEnergy, energyCalculator.REenergy);

            report.Series[0].Points.AddXY("Total Energy", "7.409");
            report.Series[0].Points.AddXY("Renewable Energy", "1.402");
          
            report.Legends[0].Enabled = true;
            report.ChartAreas[0].Area3DStyle.Enable3D = true;
            report.Titles.Add("Energy report");

            using (Bitmap bmp = new Bitmap(this.Width, this.Height))
            {
                report.DrawToBitmap(bmp, new Rectangle(Point.Empty, bmp.Size));
                bmp.Save(@"C:\Users\teodo\Desktop\Master\REIC_report\chart.png", ImageFormat.Png); 
            }
        }
        private void report_Click(object sender, EventArgs e)
        {

        }
    }
}
