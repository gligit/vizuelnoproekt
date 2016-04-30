using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace vproekt
{
    class Ranged_Monster : Monster
    {
        int delay_cast, delay_cast_ref, projectile_speed, spell_index;
        public bool casting { get; set; }
        public bool moving { get; set; }
        Spell[] spells;
        //nemam casting animation

        public Ranged_Monster(Bitmap monster, Spell[] spells,Rectangle bck,int speed = 3, int delay = 9, int delay_cast = 6, int projectile_speed = 10)
            : base(monster,bck, speed, delay)
        {
            this.delay_cast = 0;
            this.delay_cast_ref = delay_cast;
            this.projectile_speed = projectile_speed;
            this.spells = spells;
            spell_index = 0;
        }

        override public void animate(Graphics g, Greendo greendo)
        {
            if (is_alive())
            {
                if (greendo.dead)
                {
                    moving = false;
                    casting = false;
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
                else if (casting)
                {
                    //nema cast animacija
                    g.DrawImage(base.monster, base.bounding_rectangle.X, base.bounding_rectangle.Y, new Rectangle(base.move_position, vertical * h, w, h), GraphicsUnit.Pixel);
                    spells[spell_index].should_cast = true;
                    spells[spell_index].prepare_spell(dir, base.bounding_rectangle);
                }
                else
                {
                    g.DrawImage(base.monster, base.bounding_rectangle.X, base.bounding_rectangle.Y, new Rectangle(base.move_position, vertical * h, w, h), GraphicsUnit.Pixel);
                }
                foreach (Spell s in spells)
                {
                    s.cast(g);
                }

            }
            else
            {
                base.death_animation(g);
            }
            base.health_string(g, "r ");

        }
        override protected void next_step(Rectangle greendo)
        {
            int offset = 20;
            Rectangle stop = new Rectangle(greendo.X + offset, greendo.Y + offset, greendo.Width - offset, greendo.Height - offset);
            if (Helper.check_rectangles(stop, base.bounding_rectangle))
            {
                moving = false;
                casting = true;
            }
            else if (base.bounding_rectangle.Y < greendo.Y + greendo.Height / 2 && base.bounding_rectangle.Y > greendo.Y)
            {
                if (greendo.X < base.bounding_rectangle.X)
                    base.dir = Direction.Left;
                else if (greendo.X >= base.bounding_rectangle.X)
                    base.dir = Direction.Right;

                delay_cast_method();
            }
            else if (base.bounding_rectangle.X > greendo.X && base.bounding_rectangle.X < greendo.X + greendo.Width / 2)
            {
                if (greendo.Y < base.bounding_rectangle.Y)
                    base.dir = Direction.Up;
                else if (greendo.Y >= base.bounding_rectangle.Y)
                    base.dir = Direction.Down;

                delay_cast_method();
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
                        casting = false;
                    }
                    else if (greendo.Y >= base.bounding_rectangle.Y)
                    {
                        base.dir = Direction.Down;
                        moving = true;
                        casting = false;
                    }
                }
                else
                {
                    if (greendo.X < base.bounding_rectangle.X)
                    {
                        base.dir = Direction.Left;
                        moving = true;
                        casting = false;
                    }
                    else if (greendo.X >= base.bounding_rectangle.X)
                    {
                        base.dir = Direction.Right;
                        moving = true;
                        casting = false;
                    }
                }
            }
        }
        override public void collision_detection(Greendo greendo)
        {
            int damage = 0;
            foreach (Spell s in spells)
            {
                damage += s.collision_detection(greendo.get_bounding_rectangle());
            }
           greendo.health -= damage;
        }
        private void delay_cast_method()
        {
            if (delay_cast == delay_cast_ref)
            {
                moving = false;
                casting = true;
                delay_cast = 0;
            }
            else
            {
                moving = true;
                casting = false;
            }
            ++delay_cast;
        }

    }
}
