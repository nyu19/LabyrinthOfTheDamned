/*
 * HealthManager.cs
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

namespace LabyrinthOfTheDamned.Scenes.ActionScene.Components
{
    /// <summary>
    /// Enum for HealthManager starting position
    /// </summary>
    public enum Position
    {
        Left,
        Right
    }

    /// <summary>
    /// Health Manager Class
    /// </summary>
    public class HealthManager : DrawableGameComponent
    {
        Player linkedPlayer;
        Vector2 iconPosition, textPosition;
        SpriteBatch sb;
        Texture2D icon;
        const int FRAME = 68;
        int frameCounter;
        double delay, delayCounter;
        Rectangle frame;
        Color overlay;
        bool isFlashing;

        /// <summary>
        /// Constructor for HealthManager
        /// </summary>
        /// <param name="game">game instance</param>
        /// <param name="linkedPlayer">player to which the health manager is connnected</param>
        /// <param name="position">postion of the health counter</param>
        /// <param name="ps">postion of the health counter either left or right of the screen</param>
        public HealthManager(Game game, Player linkedPlayer, Vector2 position, Position ps) : base(game)
        {
            this.linkedPlayer = linkedPlayer;
            this.sb = Shared._sb;
            icon = game.Content.Load<Texture2D>("images/other/heart");
            frame = new Rectangle(0, 0, FRAME, FRAME);
            delay = 0f;
            isFlashing = false;
            overlay = Color.White;

            Vector2 tempVector = Shared.highlightFonts.MeasureString("100");
            if (ps == Position.Left)
            {
                iconPosition = position; 
                textPosition = new Vector2(iconPosition.X + FRAME + 5, (icon.Height - tempVector.Y)/3);
            }
            else if (ps == Position.Right)
            {
                textPosition = new Vector2(Shared.stageSize.X - tempVector.X - position.X, (icon.Height - tempVector.Y) / 3);
                iconPosition = new Vector2(Shared.stageSize.X - tempVector.X - position.X - FRAME - 5 , position.Y);
            }

        }


        /// <summary>
        /// Overridden Method for Update
        /// </summary>
        /// <param name="gameTime">instance of gametime from game</param>
        public override void Update(GameTime gameTime)
        {
            int hp = linkedPlayer.PlayerHealth;
            if (hp <= 100 && hp >= 75)
            {
                frameCounter = 0;
            }
            else if(hp <= 74 && hp >= 50)
            {
                frameCounter = 1;
            }
            else if(hp <= 49 && hp >= 25)
            {
                frameCounter = 2;
            }
            else if(hp <= 24 && hp >= 10)
            {
                isFlashing = true;
                delay = 2;
                frameCounter = 3;
            }
            else if(hp <= 9)
            {
                isFlashing = true;
                delay = 1;
                frameCounter = 4;
            }

            delayCounter += gameTime.ElapsedGameTime.TotalSeconds;
            if (delayCounter > delay && isFlashing) 
            {
                delayCounter = 0;
                if (overlay == Color.DarkRed) 
                    overlay = Color.White;
                else if (overlay == Color.White) 
                    overlay = Color.DarkRed;

            }
            frame = new Rectangle(FRAME * frameCounter, 0, FRAME, FRAME);

            base.Update(gameTime);
        }

        /// <summary>
        /// Overriden Method
        /// </summary>
        /// <param name="gameTime">gametime Instance of the Game</param>
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(icon,iconPosition,frame,overlay);
            sb.DrawString(Shared.highlightFonts, linkedPlayer.PlayerHealth < 0 ? "0" : linkedPlayer.PlayerHealth.ToString(), textPosition, overlay);
            sb.End();
            base.Draw(gameTime);
        }
    }
}
