using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Bitmap bmp1; 
        Bitmap bmp2;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "1.png";
            textBox2.Text = "2.png";
        }
   

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" & textBox2.Text == "")
            {
                MessageBox.Show("please input the image file name (include folders)");
                return;
            }

            bmp1 = new Bitmap(textBox1.Text.ToString());
            bmp2 = new Bitmap(textBox2.Text.ToString());
            Graphics g = CreateGraphics();
            g.DrawImage(bmp1,new Rectangle(10,120,bmp1.Width,bmp1.Height));
            g.DrawImage(bmp2, new Rectangle(10+bmp1.Width+10, 120, bmp2.Width, bmp2.Height));
            g.Dispose();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" & textBox2.Text == "") {
                MessageBox.Show("please input the image file name (include folders)");
                return;
            }
            bmp1 = new Bitmap(textBox1.Text.ToString());
            bmp2 = new Bitmap(textBox2.Text.ToString());
            byte[] info1 = GetImagePixel(bmp1);
            byte[] info2 = GetImagePixel(bmp2);
            byte[] info3 = info1;

            for (int num=0;num<info3.Length;num+=3)
            {
                info3[num] = 125;
                info3[num + 2] = 125;
                if (info2[num + 1] != 0 & info1[num + 1] != 0)
                {
                    Console.WriteLine( info2[num + 1] / info1[num + 1]);
                    var t = info2[num + 1] / info1[num + 1];
                    info3[num + 1] = (byte)(t*20);
                }
            }
            Bitmap bmp3 = SetImagePixel(bmp2, info3);
            Graphics g = this.CreateGraphics();
            g.DrawImage(bmp2, new Rectangle(10 + bmp1.Width+10 +bmp1.Width+ 10, 120, bmp2.Width, bmp2.Height));
            g.Dispose();
        }


        public byte[] GetImagePixel(Bitmap img)
        {
            byte[] result = new byte[img.Width * img.Height * 3];
            int n = 0;
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    result[n] = img.GetPixel(i, j).R;
                    result[n + 1] = img.GetPixel(i, j).G;
                    result[n + 2] = img.GetPixel(i, j).B;
                    n += 3;
                    
                }
            }
            return result;
        }

        public Bitmap SetImagePixel(Bitmap img,byte[] info)
        {
           
            int n = 0;
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                  
                    img.SetPixel(i, j, Color.FromArgb(info[n],info[n+1],info[n+2]));
                    n += 3;
                
                }
            }
            return img;
         }
    }
}
