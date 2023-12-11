/*
 * MenuComponent.cs
 * Revision History:
 *  - Created By Nakul Upasani; Created: 1-Dec-2023
 */
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Audio;

namespace LabyrinthOfTheDamned.Utility
{
    /// <summary>
    /// Menu Component
    /// </summary>
    public class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch sb;
        private SpriteFont regularFont;
        private SpriteFont highlightFont;
        private List<string> menuItems;
        public int SelectedIndex { get; set; }
        private Vector2 position;
        private Color regularColor = Color.White;
        private Color highlightColor = Color.DarkTurquoise;
        private KeyboardState oldState;
        private SoundEffect menuClick;
        private const float VOLUME = 0.05f;

        /// <summary>
        /// Constructor for Menu Components
        /// </summary>
        /// <param name="game">Maingame instance</param>
        /// <param name="menus">string array for menu items</param>
        /// <param name="pos">postion of the menu</param>
        public MenuComponent(Game game, string[] menus, Vector2 pos) : base(game)
        {
            sb = Shared._sb;
            this.regularFont = Shared.regularFonts;
            this.highlightFont = Shared.highlightFonts;
            menuItems = menus.ToList();
            position = pos;
            menuClick = game.Content.Load<SoundEffect>("sounds/interface");
        }

        /// <summary>
        /// Overriden Method
        /// </summary>
        /// <param name="gameTime">gametime Instance of the Game</param>
        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos = position;

            sb.Begin();

            for (int i = 0; i < menuItems.Count; i++)
            {
                if (i == SelectedIndex)
                {
                    sb.DrawString(highlightFont, menuItems[i], tempPos, highlightColor);
                    tempPos.Y += highlightFont.LineSpacing;
                }
                else
                {
                    sb.DrawString(regularFont, menuItems[i], tempPos, regularColor);
                    tempPos.Y += regularFont.LineSpacing;

                }
            }

            sb.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Overridden Method for Update
        /// </summary>
        /// <param name="gameTime">instance of gametime from game</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (oldState.IsKeyUp(Keys.Down) && ks.IsKeyDown(Keys.Down))
            {
                SelectedIndex++;
                menuClick.Play(VOLUME, 0, 0);
                if (SelectedIndex == menuItems.Count)
                {
                    SelectedIndex = 0;
                }
            }
            if (oldState.IsKeyUp(Keys.Up) && ks.IsKeyDown(Keys.Up))
            {
                SelectedIndex--;
                menuClick.Play(VOLUME, 0, 0);
                if (SelectedIndex == -1)
                {
                    SelectedIndex = menuItems.Count - 1;
                }
            }
            oldState = ks;
            base.Update(gameTime);
        }
    }
}