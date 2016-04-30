using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace vproekt
{
    class Background
    {
        public Bitmap background { get; set; }
        public Background left { get; set; }
        public Background right { get; set; }
        public Background up { get; set; }
        public Background down { get; set; }
        Rectangle size;
        public Point spawn;
        public List<Rectangle> unreachables { get; set; }

        public Background(string file, Rectangle size)
        {
            spawn = new Point(400, 300);
            background = new Bitmap(Bitmap.FromFile(file));
            this.size = size;
            unreachables = new List<Rectangle>();
        }
        public Background(Background b)
        {
            this.background = b.background;
            this.size = b.size;
            this.unreachables = b.unreachables;
        }
        public Rectangle get_size()
        {
            return size;
        }
        public Point get_spawn()
        {
            return spawn;
        }
        public Point check_unreachables(Rectangle r,Direction d)
        {
            foreach(Rectangle l in unreachables)
            {
                if (Helper.check_rectangles(l, r))
                {
                    switch (d)
                    {
                        case Direction.Down:
                            return new Point(r.X, l.Y + l.Height);
                        case Direction.Up:
                            return new Point(r.X, l.Y - r.Height);
                        case Direction.Left:
                            return new Point(l.X - r.Width, r.Y);
                        case Direction.Right:
                            return new Point(l.X + l.Width,r.Y);
                    }
                    
                }
            }
            return new Point(r.X, r.Y);
        }
        public Background check_for_background_change(Rectangle character)
        {
            Point center = new Point();
            center.X = character.X + character.Width / 2;
            center.Y = character.Y + character.Height / 2;

            if (center.X < size.X)
                if (left != null)
                {
                    if ((string)this.background.Tag == "bck0")
                    {
                        left.spawn.X = 120;
                        left.spawn.Y = center.Y;
                    }
                    else
                    {
                        left.spawn.X = left.size.Width;
                        left.spawn.Y = center.Y;
                    }
                    return left;
                }
                else
                {
                    this.spawn.X = this.size.X;
                    this.spawn.Y = center.Y;
                    return this;
                }

            if (center.X > size.Width)
                if (right != null)
                {
                    if ((string)this.background.Tag == "bck0")
                    {
                        right.spawn.X = 800-120;
                        right.spawn.Y = center.Y;
                    }
                    else
                    {
                        right.spawn.X = right.size.X;
                        right.spawn.Y = center.Y;
                    }
                    return right;
                }
                else
                {
                    this.spawn.X = this.size.Width;
                    this.spawn.Y = center.Y;
                    return this;
                }


               if (center.Y < size.Y)
                   if (up != null)
                   {
                       up.spawn.X = center.X;
                       up.spawn.Y = up.size.Height;
                       return up;
                   }
                   else
                   {
                       this.spawn.X = center.X;
                       this.spawn.Y = this.size.Y;

                       return this;
                   }

               if (center.Y > size.Height)
                   if (down != null)
                   {
                       down.spawn.X = center.X;
                       down.spawn.Y = down.size.Y;
                       return down;
                   }
                   else
                   {
                       this.spawn.X = center.X;
                       this.spawn.Y = this.size.Height;
                       return this;
                   }

            return null;
        }
    

    }
}
