using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace vproekt
{

    class Monster_Manager
    {
        public delegate void monster_unreachables(Monster m);
        List<Monster> monsters;
        List<Bitmap> monster_images;
        List<Spell> monster_spells;
        Rectangle bck;
        Random r;
        public int ratio_m_r { get; set; }
        int period,spawn_counter;
        bool spawn;  

        public Monster_Manager(int ratio_m_r,float monster_spawn_in_sec)
        {
            spawn = true;
            this.ratio_m_r = ratio_m_r;
            r = new Random();
            spawn_counter = 0;
            monsters = new List<Monster>();
            monster_images = new List<Bitmap>();
            monster_spells = new List<Spell>();
            this.period = get_cycles_from_seconds(monster_spawn_in_sec);
        }
        public void clear()
        {
            monsters.Clear();
            spawn_counter = 0;
        }
        private int get_cycles_from_seconds(float seconds)
        {
            int q  = (int)(seconds * Helper.fps);
            return q;
        }
        public void auto_spawn_random()
        {
            if (spawn)
            {
                if (spawn_counter == period)
                {
                    spawn_counter = 0;
                    spawn_random_monster();
                }
                ++spawn_counter;
            }
        }
        public void should_stop_spawning_randoms(Greendo grendo)
        {
            if (grendo.dead)
                spawn = false;
        }
        public void stop_spawning_randoms()
        {
            spawn = false;
        }
        public void spawn_random_monster()
        {
            int q = r.Next(ratio_m_r);
            int mindex = r.Next(monster_images.Count);
            int speed = r.Next(1, 7);
            int delay = r.Next(4, 12);

            if (q == 0)
            {
                //ima samo 1 spell
                int[] si = new int[1];
                si[0] = 0;
                int delay_cast = r.Next(2, 7);
                int proj_speed = r.Next(8, 15);
                speed = r.Next(2, 6);
                spawn_ranged_monster(mindex, si, speed, delay, delay_cast, proj_speed);
            }
            else
            {
                spawn_melee_monster(mindex,speed, delay);
            }

        }
        public void collision_detection(Greendo greendo)
        {
            if(!greendo.dead)
            foreach (Monster m in monsters)
            {
                m.collision_detection(greendo);
            }
        }
        public void spawn_ranged_monster(int monster_image_index, int[] spell_indices, int speed = 3, int delay = 9, int delay_cast = 6, int projectile_speed = 10)
        {
            Spell[] sp = new Spell[spell_indices.Length];
            for (int i = 0; i < sp.Length; i++)
            {
                sp[i] = monster_spells[spell_indices[i]];
            }

            monsters.Add(new Ranged_Monster(monster_images[monster_image_index], sp, bck, speed, delay, delay_cast, projectile_speed));
        }
        public void spawn_melee_monster(int monster_image_index, int speed = 3, int delay = 9)
        {
            monsters.Add(new Melee_Monster(monster_images[monster_image_index], bck, speed, delay));
        }
        public void check_monster_unreachables(monster_unreachables mu)
        {
            foreach (Monster m in monsters)
            {
                mu(m);
            }
        }
        public void animate(Graphics g, Greendo greendo)
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].dead)
                    monsters.Remove(monsters[i]);
            }

            foreach (Monster m in monsters)
            {
                m.animate(g, greendo);
            }
        }
        public void add_monster_image(string monster_name)
        {
            monster_images.Add(new Bitmap(Bitmap.FromFile("..\\..\\vproekt_data\\monsters\\" + monster_name + ".png")));
        }
        public void add_spell_image(string spell_location, int num_of_images, bool projectile)
        {
            if (projectile)
            {
                monster_spells.Add(new Projectile_Spell(spell_location, num_of_images));
            }
            else
            {
                monster_spells.Add(new Non_Projectile_Spell(spell_location, num_of_images));
            }
        }
        public List<Monster> get_monsters()
        {
            return monsters;
        }
        public void set_bck_rectangle(Rectangle r)
        {
            bck = r;
        }
    }
}
