using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class Form1 : Form
    {

        int speed = 5;
        int xLoc = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {            
            if (pictureBox1.Location.X <= -pictureBox1.Size.Width)
            {
                pictureBox1.Left = this.Size.Width;
            }
            Collision();
            pictureBox1.Left -= speed;
        }

        private void Collision()
        {
            var x1 = pictureBox1.Left;
            var y1 = pictureBox1.Top;
            Rectangle r1 = new Rectangle(x1, y1, pictureBox1.Width, pictureBox1.Height);

            var x2 = pictureBox2.Left;
            var y2 = pictureBox2.Top;
            Rectangle r2 = new Rectangle(x2, y2, pictureBox2.Width, pictureBox2.Height);

            if (r1.IntersectsWith(r2))
            {
                label1.Text = "collided";
            }
            else
            {
                label1.Text = "not collided";
            }
        }

    }
}
