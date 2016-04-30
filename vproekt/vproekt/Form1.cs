using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace vproekt
{
    enum Direction { Left, Right, Up, Down }
    public partial class Form1 : Form
    {
        Timer t;
        Game game;
        bool help;
        bool map;
        Bitmap imap;

        public Form1()
        {
            help = false;
            map = false;
            InitializeComponent();
            Helper.kills = label2;
            Width = 800;
            Height = 600;
            imap = new Bitmap(Bitmap.FromFile("..\\..\\vproekt_data\\map.png"));
            game = new Game();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            t = new Timer();
            t.Tick += new EventHandler(refresh);
            t.Interval = 1000 / Helper.fps;
            t.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            if (map || help)
            {
                if (map)
                {
                    e.Graphics.DrawImage(imap, 0, 0);
                }
            }
            else
                game.play(e.Graphics);
        }

        private void refresh(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            game.set_greendo_moving(true);

            switch (e.KeyCode)
            {
                case Keys.W:
                    game.set_greendo_direction(Direction.Up);
                    break;
                case Keys.S:
                    game.set_greendo_direction(Direction.Down);
                    break;
                case Keys.A:
                    game.set_greendo_direction(Direction.Left);
                    break;
                case Keys.D:
                    game.set_greendo_direction(Direction.Right);
                    break;
                case Keys.NumPad1:
                    game.set_greendo_casting(true);
                    game.set_greendo_spell_index(0);
                    game.set_greendo_moving(false);
                    break;
                case Keys.NumPad2:
                    game.set_greendo_casting(true);
                    game.set_greendo_spell_index(1);
                    game.set_greendo_moving(false);
                    break;
                case Keys.NumPad3:
                    game.set_greendo_casting(true);
                    game.set_greendo_spell_index(2);
                    game.set_greendo_moving(false);
                    break;
                case Keys.N:
                    game = new Game();
                    break;
                case Keys.H:
                    if (map)
                        map = false;
                    help = help ? false : true;
                    helpm(help);
                    break;
                case Keys.M:
                    if (help)
                        help = false;
                    helpm(help);
                    map = map ? false : true;
                    break;
                default:
                    game.set_greendo_casting(false);
                    game.set_greendo_moving(false);
                    break;
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            game.set_greendo_casting(false);
            game.set_greendo_moving(false);
        }

        private void helpm(bool help)
        {
            if (help)
                richTextBox1.Visible = true;
            else
                richTextBox1.Visible = false;
        }



    }
}
