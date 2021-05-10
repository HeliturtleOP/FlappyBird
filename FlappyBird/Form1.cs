using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;
using System.Resources;
using System.Reflection;

namespace FlappyBird
{
    public partial class Form1 : Form
    {
        int score = 0;
        int highscore;

        //pipe speed
        int speed = 5;

        //variables for bird
        int fallSpeed = 0;
        int gravity = 1;
        int jumpVelocity = -10;

        //list of pipes
        List<PictureBox> pipeList = new List<PictureBox>();

        ResourceManager rm = new ResourceManager("items", Assembly.GetExecutingAssembly());

        public Form1()
        {
            InitializeComponent();
        }
        
        //runs once at application start
        private void Form1_Load(object sender, EventArgs e)
        {
            //populates the list of pipes
            for (int i = 2; i < 10; i++)
            {
                pipeList.Add((PictureBox)Controls.Find("pictureBox" + i, true)[0]);
            }

        }

        //update Loop (Set timer to an interval of 17)
        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < pipeList.Count; i++)
            {
                //gives score
                ScoreUp(i);
                //randomises pipe placement
                PoolGeneration(i);
                //moves pipes to the left
                pipeList[i].Left -= speed;
            }
            //method for collision
            Collision();
            //method for gravity
            Gravity();
        }

        private void PoolGeneration(int i)
        {
            //looks in the pipe list
            if (pipeList[i].Location.X <= -pipeList[i].Size.Width)
            {
                //checks if pipe is on bottom
                if (i % 2 == 0)
                {
                    Random r = new Random();

                    //generats randim number between 157(inclusive) and 364(exclusive) and sets the bottm pipe to that location on the y axis
                    pipeList[i].Top = r.Next(157, 364);
                    //sets the location on the y axis off the top pipes to 450 pixels above
                    pipeList[i + 1].Top = pipeList[i].Top - 450;
                }
                //puts pipes off screen to the right on a multiple of 5
                pipeList[i].Left = (this.Size.Width) / 5 * 5;
            }
        }

        private void ScoreUp(int i)
        {
            //checks is bird is going through pipe
            if (pipeList[i].Left == pictureBox1.Left)
            {
                //adds score and sets label
                score++;
                label2.Text = "Points: " + (score / 2).ToString();
            }
        }

        private void Gravity()
        {
            //moves the bird down according to fall speed varable;
            pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y + fallSpeed);
            //increases fall speed by gravity to make velocity
            fallSpeed += gravity;                      
        }

        private void Collision()
        {
            //collision rectangle for bird
            var x1 = pictureBox1.Left;
            var y1 = pictureBox1.Top;
            Rectangle r1 = new Rectangle(x1, y1, pictureBox1.Width, pictureBox1.Height);

            //looks through pipe list
            for (int i = 0; i < pipeList.Count; i++)
            {
                //collision rectangle for all pipes;
                Rectangle r = new Rectangle(pipeList[i].Left, pipeList[i].Top, pictureBox2.Width, pictureBox2.Height);

                //checks if collision occurs with pipe or ground
                if (r1.IntersectsWith(r) || pictureBox1.Top >= 400)
                {
                    label1.Visible = true;
                    timer1.Stop();
                    Int32.TryParse(rm.GetString("HighScore"), out highscore);
                    if (highscore <= score){
                        highscore = score;
                    }
                    {

                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //checks if key pressed is space
            if (e.KeyCode == Keys.Space)
            {
                //sets fall speed to a large positive velocity to make bird "jump" up
                fallSpeed = jumpVelocity;
            } 
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //starts the game
            timer1.Enabled = true;
            label3.Visible = false;
        }
    }
}
