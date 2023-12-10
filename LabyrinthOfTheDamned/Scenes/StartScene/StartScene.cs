using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;
using LabyrinthOfTheDamned.Utility;

namespace LabyrinthOfTheDamned.Scenes.StartScene
{
    public class StartScene : GameScene
    {
        MainGame game;
        public MenuComponent Menu { get; set; }
        private SpriteBatch sb;
        string[] menuItems = { "Start Game", "Help", "High Score", "Credits", "Quit" };

        Texture2D texture;

        public TimeSpan MusicSpan { get; set; } = TimeSpan.Zero;

        public StartScene(Game game) : base(game)
        {
            this.game = (MainGame)game;
            sb = Shared._sb;
            Menu = new MenuComponent(game, Shared.regularFonts, Shared.highlightFonts, menuItems, new Vector2(375, 150));

            this.Components.Add(Menu);
            LoadContent();
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("images/scenes/Main");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(texture, new Rectangle(0, 0, (int)Shared.stageSize.X, (int)Shared.stageSize.Y), Color.White);
            sb.End();

            base.Draw(gameTime);
        }
        public override void Show()
        {
            Song actionBgMx = game.Content.Load<Song>("sounds/startScene");
            MediaPlayer.Volume = 0.5f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(actionBgMx,MusicSpan);

            base.Show();
        }
    }
}
