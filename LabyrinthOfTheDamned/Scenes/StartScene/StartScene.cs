using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;

namespace LabyrinthOfTheDamned.Scenes.StartScene
{
    public class StartScene : GameScene
    {
        MainGame game;
        public MenuComponent Menu { get; set; }
        private SpriteBatch sb;
        string[] menuItems = { "Start Game", "Help", "High Score", "Credit", "Quit" };
        public TimeSpan MusicSpan { get; set; } = TimeSpan.Zero;
        public StartScene(Game game) : base(game)
        {
            this.game = (MainGame)game;

            SpriteFont regularFont = game.Content.Load<SpriteFont>("fonts/RegularFont");
            SpriteFont highlightFont = game.Content.Load<SpriteFont>("fonts/HighlightFont");

            Menu = new MenuComponent(game, regularFont, highlightFont, menuItems);

            this.Components.Add(Menu);
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
