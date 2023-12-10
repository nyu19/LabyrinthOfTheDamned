/*
 * Names:
 *  - Nakul Upasani
 *  - Shahyar Fida
 * Revision History:
 *  - Created By Nakul Upasani; Created: 1-Dec-2023
 * 
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthOfTheDamned.Scenes
{
    /// <summary>
    /// Base Class for each Game Scene
    /// </summary>
    public class GameScene : DrawableGameComponent
    {
        private List<GameComponent> components;
        public List<GameComponent> Components { get => components; set => components = value; }
        protected Song bgMusic;

        /// <summary>
        /// Show's the component
        /// </summary>
        public virtual void Show()
        {
            this.Enabled = true;
            this.Visible = true;
        }

        /// <summary>
        /// Hides the component
        /// </summary>
        public virtual void Hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        /// <summary>
        /// Constructor for GameScene
        /// </summary>
        /// <param name="game">Instance of Game</param>
        protected GameScene(Game game) : base(game)
        {
            components = new List<GameComponent>();
            Hide();
        }

        /// <summary>
        /// Overriden Method
        /// </summary>
        /// <param name="gameTime">gametime Instance of the Game</param>

        public override void Draw(GameTime gameTime)
        {
            foreach (GameComponent item in components)
            {
                if (item is DrawableGameComponent)
                {
                    DrawableGameComponent comp = (DrawableGameComponent)item;
                    if (comp.Visible)
                    {
                        comp.Draw(gameTime);
                    }
                }
            }
            base.Draw(gameTime);
        }


        /// <summary>
        /// Overridden Method for Update
        /// </summary>
        /// <param name="gameTime">instance of gametime from game</param>

        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent item in components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }
            base.Draw(gameTime);
        }

    }
}
