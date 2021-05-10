using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Media;

namespace FlappyBird
{
    public partial class Form1 : Form
    {
        int speed = 5;

        int fallSpeed = 0;
        int gravity = 1;
        int jumpVelocity = -10;

        List<PictureBox> pictureBoxList = new List<PictureBox>();

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            for (int i = 0; i < pictureBoxList.Count; i++)
            {
                
                if (pictureBoxList[i].Location.X <= -pictureBoxList[i].Size.Width)
                {
                    pictureBoxList[i].Left = this.Size.Width;                    
                }
                pictureBoxList[i].Left -= speed;

            }

            Collision();
            Gravity();
        }

        private void Gravity()
        {
            pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y + fallSpeed);
            fallSpeed += gravity;          
            
        }

        private void Collision()
        {
            var x1 = pictureBox1.Left;
            var y1 = pictureBox1.Top;
            Rectangle r1 = new Rectangle(x1, y1, pictureBox1.Width, pictureBox1.Height);

            for (int i = 0; i < pictureBoxList.Count; i++)
            {
                Rectangle r = new Rectangle(pictureBoxList[i].Left, pictureBoxList[i].Top, pictureBox2.Width, pictureBox2.Height);

                if (r1.IntersectsWith(r))
                {
                    label1.Visible = true;
                    timer1.Stop();
                }
            }


        }

        private void playSimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"C:\Users\Owner\Documents\FlappyBird\FlappyBird\Bounce.wav");
            simpleSound.Play();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                fallSpeed = jumpVelocity;
                playSimpleSound();
            } 
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            for (int i = 2; i < 10; i++)
            {
                pictureBoxList.Add((PictureBox)Controls.Find("pictureBox" + i, true)[0]);               
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            label3.Visible = false;
        }
    }
}
