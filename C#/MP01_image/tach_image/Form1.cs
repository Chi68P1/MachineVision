using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;

namespace tach_image
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string img = @"C:\Users\Asus\source\lena_goc.png";
            //Read Image
            Image<Bgr, byte> imgor = new Image<Bgr, byte>(img);
            //Load original image in picturebox1
            imageBox1.Image = imgor;
            //Get image dimension
            //int width = imgor.Width;
            //int height = imgor.Height;

            //3 copy pictures for red green blue image
            Image<Bgr, byte> rbmp = new Image<Bgr, byte>(imgor.Width, imgor.Height);
            Image<Bgr, byte> gbmp = new Image<Bgr, byte>(imgor.Width, imgor.Height);
            Image<Bgr, byte> bbmp = new Image<Bgr, byte>(imgor.Width, imgor.Height);
            //Red green blue image
            for (int y = 0; y < imgor.Height; y++)
            {
                for (int x = 0; x < imgor.Width; x++)
                {
                    //Get pixel value
                    Bgr pixel = imgor[y, x];
                    //Extract ARGB value from pixel
                    byte b = (byte)pixel.Blue;
                    byte g = (byte)pixel.Green;
                    byte r = (byte)pixel.Red;

                    //Set red image pixel
                    rbmp[y, x] = new Bgr(0, 0, r);
                    gbmp[y, x] = new Bgr(0, g, 0);
                    bbmp[y, x] = new Bgr(b, 0, 0);

                }
            }

            //Load red image
            imageBox2.Image = rbmp;
            //Load green image
            imageBox3.Image = gbmp;
            //Load blue image
            imageBox4.Image = bbmp;
        }
    }
}
