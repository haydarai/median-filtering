using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Median_Filter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Choose an image";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBoxOriginal.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBoxOriginal.Image = new Bitmap(ofd.FileName);
                }
            }
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            if (pictureBoxOriginal.Image == null)
                return;
            else
                MedianFilter();
        }

        private void MedianFilter()
        {
            Bitmap original = new Bitmap(pictureBoxOriginal.Image);
            int width = original.Width;
            int height = original.Height;
            Bitmap edited = new Bitmap(width, height);
            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    Color[] window = new Color[9];
                    int[] windowR = new int[9];
                    int[] windowG = new int[9];
                    int[] windowB = new int[9];
                    int count = 0;
                    for (int i = x-1; i < x+2; i++)
                    {
                        for (int j = y-1; j < y+2; j++)
                        {
                            window[count] = original.GetPixel(i, j);
                            windowR[count] = window[count].R;
                            windowG[count] = window[count].G;
                            windowB[count] = window[count].B;
                            count++;
                        }
                    }
                    Array.Sort(windowR);
                    Array.Sort(windowG);
                    Array.Sort(windowB);
                    int r = windowR[9 / 2];
                    int g = windowG[9 / 2];
                    int b = windowB[9 / 2];
                    Color color = Color.FromArgb(r, g, b);
                    edited.SetPixel(x, y, color);
                }
            }
            pictureBoxEdited.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxEdited.Image = edited;
        }
    }
}
