using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1_Termometro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double x, y, angulo;
            int magnitud = 80;
            int XS, YS;
            double grados = (int)numericUpDown1.Value;


            angulo = grados*3;
            angulo = angulo - 90;
            angulo = (angulo * Math.PI) / 180;

            x = magnitud * Math.Cos(angulo);
            y = magnitud * Math.Sin(angulo);

            XS = Convert.ToInt32(x);
            YS = Convert.ToInt32(y);

            Image newImage = Image.FromFile("C:\\Users\\chino\\Downloads\\grados.gif");
            Pen plumaNegra = new Pen(Color.Red, 2f);
           
            Graphics superficie = CreateGraphics();
            
            superficie.DrawImage(newImage, 84, 84, 234, 234);
            superficie.DrawLine(plumaNegra, 200,200, (XS + 200), (YS + 200));
            superficie.Dispose();
            label1.Text = (numericUpDown1.Value + 20).ToString();
        }

        private void buttonOn_Click(object sender, EventArgs e)
        {
            Image newImage = Image.FromFile("C:\\Users\\chino\\Downloads\\grados.gif");
            Graphics superficie = CreateGraphics();
            superficie.DrawImage(newImage, 84, 84, 234, 234);
            Pen plumaNegra = new Pen(Color.Red, 2f);
            superficie.DrawLine(plumaNegra, 200, 200, 200, 120);
            label1.Text = 20.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonOff_Click(object sender, EventArgs e)
        {
            Image newImage = Image.FromFile("C:\\Users\\chino\\Downloads\\grados.gif");
            Graphics superficie = CreateGraphics();
            superficie.DrawImage(newImage, 84, 84, 234, 234);
            label1.Text = " ";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                serialPort1.Open();
            }
            else
            {
                string buffer;
                buffer = serialPort1.ReadLine();
                buffer = buffer.Substring(0, 2);
                if (buffer.Length <= 8)
                {
                    int temp = Convert.ToInt32(buffer);
                    numericUpDown1.Value = temp - 20;
                    label1.Text = buffer;
                    int aux;
                    aux = Convert.ToInt32(buffer);
                    label1.Text = (aux).ToString();
                    //this.Refresh();
                }
            }
        }
    }
}
