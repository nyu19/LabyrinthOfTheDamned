using LabyrinthOfTheDamned.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthOfTheDamned.Scenes.ActionScene
{
    public class ActionScene : GameScene
    {
        SpriteBatch sb;
        public ActionScene(Game game) : base(game)
        {
            MainGame g = (MainGame)game;
            sb = Shared._sb;
        }

        public override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
