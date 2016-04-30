using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace vproekt
{
    struct Spell_Fixer
    {
        public Point up { get; set; }
        public Point down { get; set; }
        public Point left { get; set; }
        public Point right { get; set; }
        public Spell_Fixer(Point up, Point down, Point left, Point right) : this()
        {
            this.up = up;
            this.down = down;
            this.left = left;
            this.right = right;
        }
    }

    abstract class Spell
    {
        public Spell_Fixer sfix;
        protected Bitmap[] images;
        protected int width, height,damage;
        public bool should_cast { get; set; }

        public Spell(string file, int num_of_images,int damage = 1)
        {
            this.damage = damage;
            images = new Bitmap[num_of_images];
            for (int i = 0; i < num_of_images; i++)
            {
                images[i] = new Bitmap(Bitmap.FromFile(file + "\\" + i.ToString() + ".png"));
            }
            width = images[0].Width;
            height = images[0].Height;
        }
        abstract public void cast(Graphics g);
        abstract public int collision_detection(Rectangle r);
        abstract public void prepare_spell(Direction d, Rectangle r);
       
    }
}
