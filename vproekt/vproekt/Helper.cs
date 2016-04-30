using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace vproekt
{
    public static class Helper
    {
        public static System.Windows.Forms.Label kills;
        public static Bitmap[] death;
        public static int fps = 30;
        static Helper()
        {
            death = new Bitmap[8];
            for (int i = 0; i < 8; i++)
            {
                death[i] = new Bitmap(Bitmap.FromFile("..\\..\\vproekt_data\\death\\" + i.ToString() + ".png"));
            }
        }
        public static bool check_rectangles(Rectangle l, Rectangle r)
        {
            int lx2 = l.X + l.Width;
            int ly2 = l.Y + l.Height;
            int rx2 = r.X + r.Width;
            int ry2 = r.Y + r.Height;

            if (l.X < rx2 && lx2 > r.X && l.Y < ry2 && ly2 > r.Y)
            {
                return true;
            }
            return false;
        }
    }
}
