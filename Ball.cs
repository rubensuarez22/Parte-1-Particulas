using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_particulas
{
    public class Ball
    {
        int index;
        Size space;
        public Color c;
        // Variables de posición
        public float x;
        public float y;

        // Variables de velocidad
        private float vx;
        private float vy;

        // Variable de radio
        public float radio;

        public int vida;
        // Evento BallRemoved
        public event EventHandler BallRemoved;


        // Constructor
        public Ball(Random rand, Size size, int index)
        {
            this.vida = rand.Next(100, 300);
            this.radio = rand.Next(20, 35);
            this.x = 50;
            this.y = rand.Next(0, size.Height / 2);
            c = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
            // Velocidades iniciales
            this.vx = 100;
            this.vy = rand.Next(50, 100);

            this.index = index;
            space = size;
        }


        // Método para actualizar la posición de la pelota en función de su velocidad
        public void Update(float deltaTime)
        {
            // Actualizamos la posición de la pelota en función de su velocidad actual
            this.x += this.vx * deltaTime;
            this.y += this.vy * deltaTime;

            // Verificamos si la pelota ha salido del PictureBox
            if (this.x + this.radio < 0 || this.x - this.radio > space.Width || this.y + this.radio < 0 || this.y - this.radio > space.Height)
            {
                // Invocamos el evento BallRemoved para eliminar la pelota
                BallRemoved?.Invoke(this, EventArgs.Empty);
            }
        }



    }


}
