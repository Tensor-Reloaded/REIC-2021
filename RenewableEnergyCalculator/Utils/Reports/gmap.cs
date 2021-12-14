using GMap.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gmap
{
    public partial class gmapForm : Form
    {
        public gmapForm()
        {
            InitializeComponent();
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {
            
        }

        private void gmapForm_Load(object sender, EventArgs e)
        {
            gmap.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gmap.Position = new PointLatLng(47.156116, 27.5169306);

            Thread.Sleep(5000);
            using (Bitmap bmp = new Bitmap(this.Width, this.Height))
            {
                gmap.DrawToBitmap(bmp, new Rectangle(Point.Empty, bmp.Size));
                bmp.Save(@"C:\Users\teodo\Desktop\Master\REIC_report\map.png", ImageFormat.Png);
            }
        }

    }
}
