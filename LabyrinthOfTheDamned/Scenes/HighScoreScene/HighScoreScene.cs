using LabyrinthOfTheDamned.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthOfTheDamned.Scenes.HighScoreScene
{
    public class HighScoreScene : GameScene
    {
        SpriteBatch sb;
        Texture2D texture;
        Vector2 pos;
        public HighScoreScene(Game game) : base(game)
        {
            Game g = (MainGame)game;
            sb = Shared._sb;
            pos = new Vector2(Shared.stageSize.X/2 - 150, Shared.stageSize.Y/2-50);
            LoadContent();
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("images/scenes/HighScore");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(texture,new Rectangle(0,0,(int)Shared.stageSize.X,(int)Shared.stageSize.Y),Color.White);
            sb.DrawString(Shared.regularFonts,$"Player 1: {HighScoreManager.ScoreSet[1]}\nPlayer 2: {HighScoreManager.ScoreSet[2]}",pos,Color.White);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
