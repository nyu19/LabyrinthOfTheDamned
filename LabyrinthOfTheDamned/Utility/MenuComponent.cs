using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Audio;

namespace LabyrinthOfTheDamned.Utility
{
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
        public MenuComponent(Game game, SpriteFont regularFont, SpriteFont highlightFont, string[] menus, Vector2 pos) : base(game)
        {
            sb = Shared._sb;
            this.regularFont = regularFont;
            this.highlightFont = highlightFont;
            menuItems = menus.ToList();
            position = pos;
            menuClick = game.Content.Load<SoundEffect>("sounds/interface");
        }

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