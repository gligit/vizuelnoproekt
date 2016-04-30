using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace vproekt
{
    class Greendo
    {
        Bitmap greendo;
        public int health { get; set; }
        int move_position;
        int cast_position;
        public int spell_index { get; set; }
        public Direction dir { get; set; }
        public bool moving { get; set; }
        public bool casting { get; set; }
        Bitmap[,] cast_imgs;
        Spell[] spells;
        Rectangle bounding_rectangle;
        public bool dead { get; set; }
        int death_position;
        int kills;

        public Greendo(int x = 0, int y = 0)
        {
            kills = 0;
            dead = false;
            health = 200;
            dir = Direction.Up;
            casting = false;
            moving = false;
            move_position = cast_position = 0;
            bounding_rectangle.X = x;
            bounding_rectangle.Y = y;
            bounding_rectangle.Width = 72;
            bounding_rectangle.Height = 100;
            load_images();
        }
        public void animate(Graphics g)
        {
            Helper.kills.Text = kills.ToString();
            if (is_alive())
            {
                int vertical = 0;
                int nextx = bounding_rectangle.X;
                int nexty = bounding_rectangle.Y;
                switch (dir)
                {
                    case Direction.Down:
                        vertical = 0;
                        nexty += 10;
                        break;
                    case Direction.Left:
                        vertical = 1;
                        nextx -= 10;
                        break;
                    case Direction.Right:
                        vertical = 2;
                        nextx += 10;
                        break;
                    case Direction.Up:
                        vertical = 3;
                        nexty -= 10;
                        break;
                }

                int w = greendo.Width / 4;
                int h = greendo.Height / 4;


                if (moving)
                {
                    if (move_position > 3)
                        move_position = 0;


                    bounding_rectangle.X = nextx;
                    bounding_rectangle.Y = nexty;
                    g.DrawImage(greendo, bounding_rectangle.X, bounding_rectangle.Y, new Rectangle(move_position * w, vertical * h, w, h), GraphicsUnit.Pixel);
                    ++move_position;
                }
                else if (casting)
                {
                    g.DrawImage(cast_imgs[vertical, cast_position], bounding_rectangle.X, bounding_rectangle.Y);
                    spells[spell_index].should_cast = true;
                    spells[spell_index].prepare_spell(dir, get_bounding_rectangle());
                    ++cast_position;
                    if (cast_position > 5)
                        cast_position = 0;
                }
                else
                {
                    g.DrawImage(greendo, bounding_rectangle.X, bounding_rectangle.Y, new Rectangle(move_position, vertical * h, w, h), GraphicsUnit.Pixel);
                }
                foreach (Spell s in spells)
                {
                    s.cast(g);
                }
            }
            else
            {
                if (death_position == 8)
                {
                    death_position = 7;
                    dead = true;
                }

                g.DrawImage(Helper.death[death_position], bounding_rectangle.X, bounding_rectangle.Y);

                ++death_position;
            }

             Font font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
             int wi = bounding_rectangle.X + bounding_rectangle.Width / 3;
             int he = bounding_rectangle.Y + bounding_rectangle.Height;
             g.DrawString(health.ToString(), font, new SolidBrush(Color.Black), new Point(wi, he));
        }
        public void collision_detection(List<Monster> monsters)
        {
            int damage = 0;
            foreach (Monster m in monsters)
            {
                foreach (Spell s in spells)
                {
                    damage += s.collision_detection(m.get_bounding_rectangle());
                }

                m.health -= damage;
                if (m.dead)
                {
                    ++kills;
                    health += 10;
                }
                damage = 0;
            }

        }
        private void load_images()
        {
            greendo = new Bitmap(Bitmap.FromFile("..\\..\\vproekt_data\\greendo\\greendo.png"));
            cast_imgs = new Bitmap[4, 6];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    cast_imgs[i, j] = new Bitmap(Bitmap.FromFile("..\\..\\vproekt_data\\greendo\\casting\\" + i.ToString() + "\\" + j.ToString() + ".png"));
                }
            }
            Rectangle r = get_bounding_rectangle();
            spells = new Spell[3];
            spells[0] = new Projectile_Spell("..\\..\\vproekt_data\\greendo\\spells\\spell0", 8);

            Spell_Fixer sf = new Spell_Fixer(new Point(bounding_rectangle.Width / 3, 0), new Point(bounding_rectangle.Width / 3, 20), new Point(10, +20), new Point(bounding_rectangle.Width / 2 + 20, 20));
            spells[0].sfix = sf;

            sf.right = new Point(bounding_rectangle.Width - 20, 10);
            sf.left = new Point(20, 10);
            spells[1] = new Non_Projectile_Spell("..\\..\\vproekt_data\\greendo\\spells\\spell1", 6);
            spells[1].sfix = sf;

            spells[2] = new Projectile_Spell("..\\..\\vproekt_data\\greendo\\spells\\spell2", 6);
            spells[2].sfix = sf;

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
        private bool is_alive()
        {
            return health > 0 ? true : false;
        }
    }
}
