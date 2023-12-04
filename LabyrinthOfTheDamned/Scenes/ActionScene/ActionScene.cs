using LabyrinthOfTheDamned.Scenes.ActionScene.Components;
using LabyrinthOfTheDamned.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthOfTheDamned.Scenes.ActionScene
{
    public class ActionScene : GameScene
    {
        SpriteBatch sb;
        private List<GameComponent> components;
        public List<GameComponent> Components { get => components; set => components = value; }

        public ActionScene(Game game) : base(game)
        {
            MainGame g = (MainGame)game;
            sb = Shared._sb;
            components = new List<GameComponent>();

            Texture2D attack = game.Content.Load<Texture2D>("images/alter/ATTACK");
            Texture2D walk = game.Content.Load<Texture2D>("images/hero/WALK");
            Texture2D hurt = game.Content.Load<Texture2D>("images/alter/HURT");
            Texture2D idle = game.Content.Load<Texture2D>("images/alter/IDLE");
            Texture2D death = game.Content.Load<Texture2D>("images/alter/DEATH");


            Player p1 = new Player(game, sb, walk, attack, idle, death, hurt);
            Components.Add(p1);


        }

        public override void Draw(GameTime gameTime)
        {
            foreach (GameComponent item in components)
            {
                if (item is DrawableGameComponent)
                {
                    DrawableGameComponent comp = (DrawableGameComponent)item;
                    if (comp.Visible)
                    {
                        comp.Draw(gameTime);
                    }
                }
            }
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent item in components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }
            base.Draw(gameTime);
        }
    }
}
