using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace vproekt
{
    class Melee_Monster : Monster
    {
        bool moving;
        bool atacking;
        //nemam attack animation
        public Melee_Monster(Bitmap monster,Rectangle bck, int speed = 3, int delay = 9)
            : base(monster,bck,speed, delay)
        {

        }
        public override void animate(Graphics g, Greendo greendo)
        {
            if (is_alive())
            {
                if (greendo.dead)
                {
                    moving = false;
                    atacking = false;
                }
                else
                {
                    if (base.delay == base.delay_ref)
                    {
                        next_step(greendo.get_bounding_rectangle());
                        base.delay = 0;
                    }
                    ++base.delay;
                }

                int vertical = 0;
                int nextx = base.bounding_rectangle.X;
                int nexty = base.bounding_rectangle.Y;
                switch (dir)
                {
                    case Direction.Down:
                        vertical = 0;
                        nexty += base.speed;
                        break;
                    case Direction.Left:
                        vertical = 1;
                        nextx -= base.speed;
                        break;
                    case Direction.Right:
                        vertical = 2;
                        nextx += base.speed;
                        break;
                    case Direction.Up:
                        vertical = 3;
                        nexty -= base.speed;
                        break;
                }

                int w = base.bounding_rectangle.Width;
                int h = base.bounding_rectangle.Height;
                if (moving)
                {
                    if (base.move_position > 3)
                        base.move_position = 0;

                    base.bounding_rectangle.X = nextx;
                    base.bounding_rectangle.Y = nexty;
                    g.DrawImage(base.monster, base.bounding_rectangle.X, base.bounding_rectangle.Y, new Rectangle(base.move_position * w, vertical * h, w, h), GraphicsUnit.Pixel);
                    ++base.move_position;
                }
                else if (atacking)
                {
                    //nema attack animacija
                    g.DrawImage(base.monster, base.bounding_rectangle.X, base.bounding_rectangle.Y, new Rectangle(base.move_position, vertical * h, w, h), GraphicsUnit.Pixel);
                }
                else
                {
                    g.DrawImage(base.monster, base.bounding_rectangle.X, base.bounding_rectangle.Y, new Rectangle(base.move_position, vertical * h, w, h), GraphicsUnit.Pixel);
                }
            }
            else
            {
                base.death_animation(g);
            }
            base.health_string(g, "m ");
        }
        protected override void next_step(Rectangle greendo)
        {
            int offset = 30;
            Rectangle stop = new Rectangle(greendo.X + offset, greendo.Y + offset, greendo.Width - offset, greendo.Height - offset);
            if (Helper.check_rectangles(stop, base.bounding_rectangle))
            {
                moving = false;
                atacking = true;
            }
            else if (base.bounding_rectangle.Y < greendo.Y + greendo.Height / 2 && base.bounding_rectangle.Y > greendo.Y)
            {
                if (greendo.X < base.bounding_rectangle.X)
                    base.dir = Direction.Left;
                else if (greendo.X >= base.bounding_rectangle.X)
                    base.dir = Direction.Right;
            }
            else if (base.bounding_rectangle.X > greendo.X && base.bounding_rectangle.X < greendo.X + greendo.Width / 2)
            {
                if (greendo.Y < base.bounding_rectangle.Y)
                    base.dir = Direction.Up;
                else if (greendo.Y >= base.bounding_rectangle.Y)
                    base.dir = Direction.Down;
            }
            else
            {
                bool random = base.r.Next(0, 2) == 0 ? true : false;
                if (random)
                {
                    if (greendo.Y < base.bounding_rectangle.Y)
                    {
                        base.dir = Direction.Up;
                        moving = true;
                        atacking = false;
                    }
                    else if (greendo.Y >= base.bounding_rectangle.Y)
                    {
                        base.dir = Direction.Down;
                        moving = true;
                        atacking = false;
                    }
                }
                else
                {
                    if (greendo.X < base.bounding_rectangle.X)
                    {
                        base.dir = Direction.Left;
                        moving = true;
                        atacking = false;
                    }
                    else if (greendo.X >= base.bounding_rectangle.X)
                    {
                        base.dir = Direction.Right;
                        moving = true;
                        atacking = false;
                    }
                }
            }
        }
        public override void collision_detection(Greendo greendo)
        {
            int damage = 25;
            int offset = 30;
            Rectangle r = greendo.get_bounding_rectangle();
            Rectangle stop = new Rectangle(r.X + offset, r.Y + offset, r.Width - offset, r.Height - offset);
           
            if( Helper.check_rectangles(base.get_bounding_rectangle(),stop))
            greendo.health -= damage;
        }
    }
}
