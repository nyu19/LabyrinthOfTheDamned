using LabyrinthOfTheDamned.Scenes.ActionScene.Components;
using LabyrinthOfTheDamned.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

            KeyModel playerOneKeys = new KeyModel()
            {
                Attack = Keys.LeftShift,
                Jump = Keys.W,
                Left = Keys.A,
                Right = Keys.D
            };

            TextureModel playerOneTexures = new TextureModel()
            {
                Attack = game.Content.Load<Texture2D>("images/hero/ATTACK"),
                Walk = game.Content.Load<Texture2D>("images/hero/WALK"),
                Hurt = game.Content.Load<Texture2D>("images/hero/HURT"),
                Idle = game.Content.Load<Texture2D>("images/hero/IDLE"),
                Death = game.Content.Load<Texture2D>("images/hero/DEATH")
            };
            

            Player p1 = new Player(game, sb, playerOneTexures, playerOneKeys);
            Components.Add(p1);

            KeyModel playerTwoKeys = new KeyModel()
            {
                Attack = Keys.RightShift,
                Jump = Keys.Up,
                Left = Keys.Left,
                Right = Keys.Right
            };

            TextureModel playerTwoTexures = new TextureModel()
            {
                Attack = game.Content.Load<Texture2D>("images/alter/ATTACK"),
                Walk = game.Content.Load<Texture2D>("images/alter/WALK"),
                Hurt = game.Content.Load<Texture2D>("images/alter/HURT"),
                Idle = game.Content.Load<Texture2D>("images/alter/IDLE"),
                Death = game.Content.Load<Texture2D>("images/alter/DEATH")
            };
            

            Player p2 = new Player(game, sb, playerTwoTexures, playerTwoKeys);
            Components.Add(p2);


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
