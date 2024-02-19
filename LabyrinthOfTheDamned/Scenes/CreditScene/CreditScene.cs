/*
 * CreditScene.cs
 * Revision History:
 *  - Created By Shahyar Fida; Created: 1-Dec-2023
 */
using LabyrinthOfTheDamned.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthOfTheDamned.Scenes.CreditScene
{
    /// <summary>
    /// Credit scene
    /// </summary>
    public class CreditScene : GameScene
    {
        SpriteBatch sb;
        Texture2D texture;

        /// <summary>
        /// Constructor for the Credit Scene
        /// </summary>
        /// <param name="game"></param>
        public CreditScene(Game game) : base(game)
        {
            Game g = (MainGame)game;
            sb = Shared._sb;
            LoadContent();
        }
        /// <summary>
        /// Loads content
        /// </summary>
        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("images/scenes/Credits");
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

        /// <summary>
        /// Overridden Method for Update
        /// </summary>
        /// <param name="gameTime">instance of gametime from game</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
