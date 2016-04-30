using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace vproekt
{
    abstract class Monster
    {
        public int health { get; set; }
        protected Bitmap monster;
        protected Rectangle bounding_rectangle;
        protected int move_position;
        protected int speed, delay, delay_ref;
        public Direction dir { get; set; }
        protected Random r;
        public bool dead { get; set; }
        protected int death_position;

        public Monster(Bitmap monster, Rectangle bck, int speed, int delay)
        {
            dead = false;
            health = 100;
            this.monster = monster;
            bounding_rectangle.Width = monster.Width / 4;
            bounding_rectangle.Height = monster.Height / 4;
            move_position = 0;
            r = new Random();
            dir = (Direction)r.Next(0, 4);
            generate_starting_position(bck);
            this.speed = speed;
            this.delay = 0;
            delay_ref = delay;
        }
        private void generate_starting_position(Rectangle bck)
        {
            bool random = r.Next(0, 2) == 0 ? true : false;

            if (random)
            {
                bounding_rectangle.X = r.Next(bck.X, bck.Width - bounding_rectangle.Width);
                bounding_rectangle.Y = 0;
            }
            else
            {
                bounding_rectangle.X = r.Next(bck.X, bck.Width - bounding_rectangle.Width);
                bounding_rectangle.Y = bck.Height - bounding_rectangle.Height;
            }

        }
        public void set_x_y_from_center(Point r)
        {
            bounding_rectangle.X = r.X - bounding_rectangle.Width / 2;
            bounding_rectangle.Y = r.Y - bounding_rectangle.Height / 2;
        }
        public void set_x_y(Point p)
        {
            bounding_rectangle.X = p.X;
            bounding_rectangle.Y = p.Y;
        }
        public Rectangle get_bounding_rectangle()
        {
            return bounding_rectangle;
        }
        public bool is_alive()
        {
            return health > 0 ? true : false;
        }
        protected void health_string(Graphics g, string s)
        {
            Font font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
            int wi = bounding_rectangle.X;
            int he = bounding_rectangle.Y + bounding_rectangle.Height;
            g.DrawString(s + health.ToString(), font, new SolidBrush(Color.Black), new Point(wi, he));
        }
        protected void death_animation(Graphics g)
        {
            if (death_position == 8)
            {
                death_position = 7;
                dead = true;
            }
            g.DrawImage(Helper.death[death_position], bounding_rectangle.X, bounding_rectangle.Y);
            ++death_position;
        }
        abstract public void animate(Graphics g, Greendo greendo);
        abstract protected void next_step(Rectangle greendo);
        abstract public void collision_detection(Greendo greendo);

    }

}

