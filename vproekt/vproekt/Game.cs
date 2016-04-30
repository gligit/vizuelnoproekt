using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vproekt
{
    class Game
    {
        Map map;
        Greendo greendo;
        Monster_Manager monsters;

        public Game()
        {
            greendo = new Greendo(200, 300);
            map = new Map();
            monsters = new Monster_Manager(2, 1.5f);
            monsters.set_bck_rectangle(map.current.get_size());

            monsters.add_monster_image("bahamut");
            monsters.add_monster_image("barrissoffee");
            monsters.add_monster_image("ifrit");
            monsters.add_monster_image("leviathan");
            monsters.add_monster_image("magicpot");
            monsters.add_monster_image("odin");
            monsters.add_monster_image("phoenix");
            monsters.add_monster_image("ryuk");
            monsters.add_monster_image("siren");
            monsters.add_monster_image("zemus");

            monsters.add_spell_image("..\\..\\vproekt_data\\monsters\\spell", 6, true);
            
        }
        public void play(System.Drawing.Graphics g)
        {
            g.Clear(System.Drawing.Color.White);
            bool changed = map.get_background(greendo);
            if (changed)
            {
                monsters.clear();
            }
            g.DrawImage(map.current.background, 0, 0);
            monsters.set_bck_rectangle(map.current.get_size());

            monsters.check_monster_unreachables(map.check_monster_unreachables);
            monsters.animate(g, greendo);
            monsters.collision_detection(greendo);
            monsters.should_stop_spawning_randoms(greendo);

            greendo.animate(g);
            greendo.collision_detection(monsters.get_monsters());

            monsters.auto_spawn_random();
        }
        public void set_greendo_moving(bool moving)
        {
            greendo.moving = moving;
        }
        public void set_greendo_casting(bool casting)
        {
            greendo.casting = casting;
        }
        public void set_greendo_direction(Direction d)
        {
            greendo.dir = d;
        }
        public void set_greendo_spell_index(int sindex)
        {
            greendo.spell_index = sindex;
        }
    }
}
