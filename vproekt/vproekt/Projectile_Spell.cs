using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace vproekt
{
    class Projectile
    {
        public int x { get; set; }
        public int y { get; set; }
        public int position { get; set; }
        public Direction dir { get; set; }

        public Projectile(int x, int y, Direction dir)
        {
            this.x = x;
            this.y = y;
            position = 0;
            this.dir = dir;
        }
    }
    class Projectile_Spell : Spell
    {
        List<Projectile> projs;
        int speed;
        public Projectile_Spell(string file, int num_of_images,int speed = 10,int damage = 2) : base(file,num_of_images,damage)
        {
            projs = new List<Projectile>();
            this.speed = speed;
        }

        override public int collision_detection(Rectangle r)
        {
            int hits = 0;
            foreach (Projectile p in projs)
            {
                int width = base.width;
                int height = base.height;

                if (p.dir == Direction.Up || p.dir == Direction.Down)
                {
                    width = base.height;
                    height = base.width;
                }
                Rectangle l = new Rectangle(p.x, p.y, width, height);
               
                if (Helper.check_rectangles(l, r))
                    hits += damage;
            }
            return hits;
        }
           
        public override void cast(Graphics g)
        {
            update_projectiles();
            foreach (Projectile p in projs)
            {
                g.DrawImage(get_next_image(p), p.x, p.y);
            }
        }

        private void update_projectiles()
        {
            for (int i = 0; i < projs.Count; i++)
            {
                switch (projs[i].dir)
                {
                    case Direction.Right:
                        if(projs[i].x > 800)
                        {
                            projs.Remove(projs[i]);
                            break;
                        }
                        projs[i].x += speed;
                        break;
                    case Direction.Down:
                        if (projs[i].y > 600)
                        {
                            projs.Remove(projs[i]);
                            break;
                        }
                        projs[i].y += speed;
                        break;
                    case Direction.Up:
                        if (projs[i].y < -base.width)
                        {
                            projs.Remove(projs[i]);
                            break;
                        }
                        projs[i].y -= speed;
                        break;
                    case Direction.Left:
                        if (projs[i].x < -base.width)
                        {
                            projs.Remove(projs[i]);
                            break;
                        }
                        projs[i].x -= speed;
                        break;
                }
            }

        }

        private Bitmap get_next_image(Projectile p)
        {
            Bitmap b = new Bitmap(base.images[p.position]);

            switch (p.dir)
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
            update_position(p);
            return b;
        }
        public override void prepare_spell(Direction d, Rectangle r)
        {
            int x = 0;
            int y = 0;

            switch (d)
            {
                case Direction.Right:
                    x = r.X + base.sfix.right.X - speed;
                    y = r.Y + base.sfix.right.Y;
                    break;
                case Direction.Down:
                    x = r.X + base.sfix.down.X;
                    y = r.Y + base.sfix.down.Y - speed;
                    break;
                case Direction.Up:
                    x = r.X + base.sfix.up.X;
                    y = r.Y - base.width + base.sfix.up.Y + speed;
                    break;
                case Direction.Left:
                    x = r.X - base.width + base.sfix.left.X + speed;
                    y = r.Y + base.sfix.left.Y;
                    break;
            }
            projs.Add(new Projectile(x, y, d));
        }

        private void update_position(Projectile p )
        {
            ++p.position;
            if (p.position > base.images.Length - 1)
                p.position = 0;
        }
    }
}
