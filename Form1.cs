using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Proyecto_particulas
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Graphics g;
        Ball ball;
        public List<Ball> balls;
        SolidBrush brush;
        static float deltaTime;



        Timer timer = new Timer();

        public Form1()
        {
            InitializeComponent();

            bmp = new Bitmap(PCT_CANVAS.Width, PCT_CANVAS.Height);
            g = Graphics.FromImage(bmp);

            brush = new SolidBrush(Color.White);

            Random rand = new Random(); // declare and initialize the 'rand' variable

            balls = new List<Ball>();
            for (int b = 0; b < 5; b++)
            {
                balls.Add(new Ball(rand, PCT_CANVAS.Size, b));
            }

            // Suscribirse al evento BallRemoved
            foreach (Ball ball in balls)
            {
                ball.BallRemoved += new EventHandler(BallRemoved);
            }

            PCT_CANVAS.Image = bmp;
            timer.Interval = 10;
            timer.Tick += new EventHandler(TIMER_Tick);
            timer.Start();
        }



        // Método para agregar una nueva partícula
        public void AddNewBall()
        {
            Random rand = new Random();
            Ball newBall = new Ball(rand, PCT_CANVAS.Size, balls.Count);
            newBall.BallRemoved += new EventHandler(BallRemoved);
            balls.Add(newBall);
        }

        // Método para manejar el evento BallRemoved
        private void BallRemoved(object sender, EventArgs e)
        {
            Ball ball = (Ball)sender;
            RemoveBall(ball);
            AddNewBall();
        }

        public void RemoveBall(Ball ball)
        {
            balls.Remove(ball);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            AddNewBall();
        }




        private void TIMER_Tick(object sender, EventArgs e)
        {
            g.Clear(Color.Black);

            Parallel.For(0, balls.Count, b =>// Actualizamos en paralelo la posición de cada partícula
            {
                balls[b].Update(0.1f);
            });

            // Pintamos cada partícula en secuencia
            foreach (Ball p in balls)
            {
                g.FillEllipse(new SolidBrush(p.c), p.x - p.radio, p.y - p.radio, p.radio * 2, p.radio * 2);
            }

            PCT_CANVAS.Invalidate();
        }






    }

}


