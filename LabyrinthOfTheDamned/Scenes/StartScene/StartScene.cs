/*
 * StartScene.cs
 * Revision History:
 *  - Created By Nakul Upasani; Created: 1-Dec-2023
 * 
 */
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
    /// <summary>
    /// Scene for Start Menu
    /// </summary>
    public class StartScene : GameScene
    {
        MainGame game;
        public MenuComponent Menu { get; set; }
        private SpriteBatch sb;
        string[] menuItems = { "Start Game", "Help", "High Score", "Credits", "Quit" };
        Texture2D texture;
        public TimeSpan MusicSpan { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// Constructor for Start Scene
        /// </summary>
        /// <param name="game">instance of game</param>
        public StartScene(Game game) : base(game)
        {
            this.game = (MainGame)game;
            sb = Shared._sb;
            Menu = new MenuComponent(game, menuItems, new Vector2(375, 150));

            this.Components.Add(Menu);
            LoadContent();
        }

        /// <summary>
        /// Loads content
        /// </summary>
        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("images/scenes/Main");
            base.LoadContent();
        }

        /// <summary>
        /// Overriden Method
        /// </summary>
        /// <param name="gameTime">gametime Instance of the Game</param>
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(texture, new Rectangle(0, 0, (int)Shared.stageSize.X, (int)Shared.stageSize.Y), Color.White);
            sb.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Shows the Component
        /// </summary>
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
