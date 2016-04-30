using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace vproekt
{
    class Non_Projectile_Spell : Spell
    {
        int position;
        int x,y;
        Direction dir;
        bool casted;
        public Non_Projectile_Spell(string file, int num_of_images,int damage = 33) : base(file,num_of_images,damage)
        {
            casted = false;
            position = 0;
            should_cast = false;
        }
        override public int collision_detection(Rectangle r)
        {
            int hits = 0;
            if (casted)
            {
                int width = base.width;
                int height = base.height;

                if (dir == Direction.Up || dir == Direction.Down)
                {
                    width = base.height;
                    height = base.width;
                }
                Rectangle l = new Rectangle(x, y, width, height);
                if (Helper.check_rectangles(l, r))
                    hits = damage;
            }
            casted = false;
            return hits;
        }
        public override void cast(Graphics g)
        {
            if (base.should_cast)
            {
                g.DrawImage(get_next_image(), x, y);
                casted = true;
            }
            base.should_cast = false;
        }
        private Bitmap get_next_image()
        {
            Bitmap b = new Bitmap(base.images[position]);

            switch (dir)
            {
                case Direction.Right:
                    break;
                case Direction.Down:
                    b.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;
                case Direction.Up:
                    b.RotateFlip(RotateFlipType.Rotate90FlipXY);
                    break;
                case Direction.Left:
                    b.RotateFlip(RotateFlipType.Rotate180FlipY);
                    break;
            }
            update_position();
            return b;
        }
        public override void prepare_spell(Direction d, Rectangle r)
        {
            dir = d;
            switch (d)
            {
                case Direction.Right:
                    x = r.X + base.sfix.right.X;
                    y = r.Y + base.sfix.right.Y;
                    break;
                case Direction.Down:
                    x = r.X + base.sfix.down.X;
                    y = r.Y + base.sfix.down.Y;
                    break;
                case Direction.Up:
                    x = r.X + base.sfix.up.X;
                    y = r.Y - base.width + base.sfix.up.Y;
                    break;
                case Direction.Left:
                    x = r.X - base.width + base.sfix.left.X; ;
                    y = r.Y + base.sfix.left.Y;
                    break;
            }
        }
        private void update_position()
        {
            ++position;
            if (position > base.images.Length - 1)
                position = 0;
        }
    }
     
}
