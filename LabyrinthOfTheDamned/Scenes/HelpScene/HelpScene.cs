using LabyrinthOfTheDamned.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthOfTheDamned.Scenes.HelpScene
{
    public class HelpScene : GameScene
    {
        SpriteBatch sb;
        Texture2D texture;

        public HelpScene(Game game) : base(game)
        {
            Game g = (MainGame)game;
            sb = Shared._sb;
            
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(texture, Vector2.Zero, Color.White);
            sb.End();

            base.Draw(gameTime);
        }

    }
}
