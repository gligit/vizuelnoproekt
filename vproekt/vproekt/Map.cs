using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vproekt
{
    class Map
    {
        //za insta teleport
        Background[] bcks;
        public Background current { get; set; }
    
        public Map()
        {
            bcks = new Background[12];
            load_images();
            connect_images();
            current = bcks[0];
        }

        public bool get_background(Greendo hero)
        {
            hero.set_x_y(current.check_unreachables(hero.get_bounding_rectangle(), hero.dir));

            bool changed = false;
            Background b = current.check_for_background_change(hero.get_bounding_rectangle());
            if (b != null)
            {
                if( current != b)
                    changed = true;

                current = b;
                hero.set_x_y_from_center(b.get_spawn());
                
            }
            return changed;
        }

        public void check_monster_unreachables(Monster monster)
        {
            monster.set_x_y(current.check_unreachables(monster.get_bounding_rectangle(), monster.dir));
        }

        private void load_images()
        {
            //width i height se koristeni kako x2,y2 za da ne prepisuvam kod samo za edna slika
            bcks[0] = new Background("..\\..\\vproekt_data\\bcks\\bck0.png", new System.Drawing.Rectangle(120, 0, 680, 600));
            bcks[0].background.Tag = "bck0";
            bcks[0].unreachables.Add(new System.Drawing.Rectangle(380, 0, 60, 160));
            bcks[0].unreachables.Add(new System.Drawing.Rectangle(380, 402, 60, 220));

            bcks[1] = new Background("..\\..\\vproekt_data\\bcks\\bck1.png", new System.Drawing.Rectangle(0, 0, 800, 600));
            bcks[1].unreachables.Add(new System.Drawing.Rectangle(0,253,284,17));
            bcks[1].unreachables.Add(new System.Drawing.Rectangle(494, 253, 306, 17));

            bcks[2] = new Background("..\\..\\vproekt_data\\bcks\\bck2.png", new System.Drawing.Rectangle(0, 0, 800, 600));
            bcks[2].unreachables.Add(new System.Drawing.Rectangle(0, 270, 310, 10));
            bcks[2].unreachables.Add(new System.Drawing.Rectangle(514, 270, 286, 10));

            bcks[3] = new Background("..\\..\\vproekt_data\\bcks\\bck3.png", new System.Drawing.Rectangle(0, 0, 800, 600));

            bcks[4] = new Background("..\\..\\vproekt_data\\bcks\\bck4.png", new System.Drawing.Rectangle(0, 0, 800, 600));
            bcks[4].unreachables.Add(new System.Drawing.Rectangle(0, 264, 265, 10));
            bcks[4].unreachables.Add(new System.Drawing.Rectangle(554, 264, 246, 10));

            bcks[5] = new Background("..\\..\\vproekt_data\\bcks\\bck5.png", new System.Drawing.Rectangle(0, 0, 800, 600));
            bcks[6] = new Background("..\\..\\vproekt_data\\bcks\\bck6.png", new System.Drawing.Rectangle(0, 0, 800, 600));

            bcks[7] = new Background(bcks[1]);
            bcks[8] = new Background(bcks[2]);
            bcks[9] = new Background(bcks[3]);
            bcks[10] = new Background(bcks[4]);
            bcks[11] = new Background(bcks[5]);
        }
        private void connect_images()
        {
            bcks[0].up = bcks[6];
            bcks[0].down = null;
            bcks[0].left = bcks[1];
            bcks[0].right = bcks[7];

            bcks[1].up = null;
            bcks[1].down = null;
            bcks[1].left = bcks[2];
            bcks[1].right = bcks[8];

            bcks[2].up = bcks[4];
            bcks[2].down = null;
            bcks[2].left = null;
            bcks[2].right = bcks[1];

            bcks[3].up = null;
            bcks[3].down = bcks[4];
            bcks[3].left = null;
            bcks[3].right = bcks[5];

            bcks[4].up = bcks[3];
            bcks[4].down = bcks[2];
            bcks[4].left = null;
            bcks[4].right = null;

            bcks[5].up = null;
            bcks[5].down = null;
            bcks[5].left = bcks[3];
            bcks[5].right = bcks[6];

            bcks[6].up = null;
            bcks[6].down = bcks[0];
            bcks[6].left = bcks[5];
            bcks[6].right = bcks[11];

            bcks[7].up = null;
            bcks[7].down = null;
            bcks[7].left = bcks[2];
            bcks[7].right = bcks[8];

            bcks[8].up = bcks[10];
            bcks[8].down = null;
            bcks[8].left = bcks[7];
            bcks[8].right = null;

            bcks[9].up = null;
            bcks[9].down = bcks[10];
            bcks[9].left = bcks[11];
            bcks[9].right = null;

            bcks[10].up = bcks[9];
            bcks[10].down = bcks[8];
            bcks[10].left = null;
            bcks[10].right = null;

            bcks[11].up = null;
            bcks[11].down = null;
            bcks[11].left = bcks[6];
            bcks[11].right = bcks[9];
 
        }
    }
}
