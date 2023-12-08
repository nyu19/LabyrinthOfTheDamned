using LabyrinthOfTheDamned.Scenes.ActionScene.Components;
using LabyrinthOfTheDamned.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct3D9;
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
        MainGame game;
        SpriteBatch sb;
        public static bool gameEnded;
        private static List<GameComponent> components;
        public static new List<GameComponent> Components { get => components; set => components = value; }
        Texture2D texture;

        public ActionScene(Game game) : base(game)
        {
            this.game = (MainGame)game;
            sb = Shared._sb;
            components = new List<GameComponent>();
            LoadContent();
            KeyModel playerOneKeys = new KeyModel()
            {
                Attack = Keys.F,
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
            

            Player p1 = new Player(game, sb, new Vector2(Shared.stageSize.X/4,Shared.stageSize.Y), playerOneTexures, playerOneKeys);

            KeyModel playerTwoKeys = new KeyModel()
            {
                Attack = Keys.RightControl,
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
            
            Player p2 = new Player(game, sb, new Vector2(Shared.stageSize.X - (Shared.stageSize.X / 4), Shared.stageSize.Y), playerTwoTexures, playerTwoKeys);
            p2.flip = SpriteEffects.FlipHorizontally;

            HealthManager h1 = new HealthManager(game, p1, new Vector2(10, 10), Position.Left);
            HealthManager h2 = new HealthManager(game, p2, new Vector2(10, 10), Position.Right);


            Components.Add(p2);
            Components.Add(h2);
            Components.Add(p1);
            Components.Add(h1);

        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("images/Scenes/Battle");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(texture, new Rectangle(0, 0, (int)Shared.stageSize.X, (int)Shared.stageSize.Y), Color.White);
            sb.End();
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
                if (gameEnded && item is Player)
                {
                    ((Player)item).Enabled = false;
                    ((Player)item).Dispose();
                }
            }
            base.Draw(gameTime);

        }

        public override void Show()
        {
            MediaPlayer.Stop();
            Song actionBgMx = game.Content.Load<Song>("sounds/actionScene");
            MediaPlayer.Volume = 0.25f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(actionBgMx);
            base.Show();
        }
    }
}
