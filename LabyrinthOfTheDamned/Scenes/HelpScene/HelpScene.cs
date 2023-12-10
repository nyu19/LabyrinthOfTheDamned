/*
 * Names:
 *  - Nakul Upasani
 *  - Shahyar Fida
 * Revision History:
 *  - Created By Nakul Upasani; Created: 1-Dec-2023
 * 
 */
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
    /// <summary>
    /// Help Scene Class
    /// </summary>
    public class HelpScene : GameScene
    {
        SpriteBatch sb;
        Texture2D texture;

        /// <summary>
        /// Constructor for Help Scene
        /// </summary>
        /// <param name="game">instace of game</param>
        public HelpScene(Game game) : base(game)
        {
            Game g = (MainGame)game;
            sb = Shared._sb;
            LoadContent();
        }
        
        /// <summary>
        /// load's constent
        /// </summary>
        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("images/Scenes/Controls");
            base.LoadContent();
        }

        /// <summary>
        /// Overriden Method
        /// </summary>
        /// <param name="gameTime">gametime Instance of the Game</param>
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(texture,new Rectangle(0,0,(int)Shared.stageSize.X,(int)Shared.stageSize.Y),Color.White);
            sb.End();

            base.Draw(gameTime);
        }

    }
}
