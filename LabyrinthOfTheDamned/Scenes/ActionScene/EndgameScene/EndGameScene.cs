using LabyrinthOfTheDamned.Scenes.CreditScene;
using LabyrinthOfTheDamned.Scenes.HelpScene;
using LabyrinthOfTheDamned.Scenes.HighScoreScene;
using LabyrinthOfTheDamned.Scenes.StartScene;
using LabyrinthOfTheDamned.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthOfTheDamned.Scenes.ActionScene.EndgameScene
{
    public class EndGameScene : GameScene
    {
        MainGame game;
        Texture2D texture;
        SpriteBatch sb;
        MenuComponent selection;
        string[] menuItems = { "Back to Menu", "Exit" };

        public EndGameScene(Game game, Texture2D texture) : base(game)
        {
            this.game = (MainGame)game;
            this.texture = texture;
            sb = Shared._sb;
            selection = new MenuComponent(game, Shared.regularFonts, Shared.highlightFonts, menuItems,new Vector2((Shared.stageSize.X/2) - Shared.highlightFonts.MeasureString(menuItems[0]).X/2, 500));

            this.Components.Add(selection);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(texture, new Rectangle(0, 0, (int)Shared.stageSize.X, (int)Shared.stageSize.Y), Color.White);
            sb.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();

            selectedIndex = selection.SelectedIndex;
            if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
            {
                hideAllScenes();
                game.startScene.Show();
                MainGame.oldKeyboardState = ks;
                game.Components.Remove(this);
            }
            else if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
            {
                game.ExitGame();
            }
            

            base.Update(gameTime);
        }

        private void hideAllScenes()
        {
            foreach (GameComponent item in Components)
            {
                if (item is GameScene)
                {
                    GameScene gs = (GameScene)item;
                    gs.Hide();
                }
            }
        }
    }
}
