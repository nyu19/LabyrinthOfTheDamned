/*
 * PlayerManager.cs
 * Revision History:
 *  - Created By Nakul Upasani; Created: 1-Dec-2023
 * 
 */
using LabyrinthOfTheDamned.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthOfTheDamned.Scenes.ActionScene.Components
{
    /// <summary>
    /// Parital Player class
    /// </summary>
    public partial class Player
    {
        /// <summary>
        /// Health Manager
        /// </summary>
        private void ManageHealth()
        {

            foreach (GameComponent item in ActionScene.Components)
            {
                if (item == this || item is not Player)
                    continue;

                Player otherPlayer = (Player)item;
                
                Rectangle p1 = this.Hitbox;
                Rectangle p2 = otherPlayer.Hitbox;
                
                if (p1.Intersects(p2) && this.IsFacing(otherPlayer) && this.frameCounter == 6 && this.mCurrentState == State.Attacking)
                {
                    otherPlayer.PlayerHealth -= 5;
                    mCurrentState = State.Hurt;
                }

                p1.Inflate(20, 0); 
                p2.Inflate(20, 0); 

                if (p1.Intersects(p2) && this.IsFacing(otherPlayer) && this.frameCounter == 6 && this.mCurrentState == State.Attacking)
                {
                    otherPlayer.PlayerHealth -= 2;
                    mCurrentState = State.Hurt;
                }
            }
        }

        /// <summary>
        /// Handles Movement on the X Axis
        /// </summary>
        /// <param name="ks">Keyboard state of current update</param>
        private void XMovementHandler(KeyboardState ks)
        {
            if (ks.IsKeyDown(playerKeys.Left) && mCurrentState != State.Attacking && Hitbox.Left-(FRAME_WIDTH / 2) >= 0 )
            {
                mCurrentState = State.Walking;
                flip = SpriteEffects.FlipHorizontally;
                velocity.X = -(SPEED_FACTOR + speedRandomize.Next(-2,2));
            }
            else if (ks.IsKeyDown(playerKeys.Right) && mCurrentState != State.Attacking && Hitbox.Right-(FRAME_WIDTH/2) <= Shared.stageSize.X)
            {
                mCurrentState = State.Walking;
                flip = SpriteEffects.None;
                velocity.X = SPEED_FACTOR + speedRandomize.Next(-2, 2);
            }
            else if (mCurrentState != State.Jumping || mCurrentState != State.Attacking)
            {
                velocity.X = 0;
            }
        }

        /// <summary>
        /// Handles Jumping of the Player
        /// </summary>
        /// <param name="ks"></param>
        private void JumpHandler(KeyboardState ks)
        {
            if (hasJumped)
            {
                const float GRAVITY_FACTOR = 3;
                velocity.Y += 0.15f * GRAVITY_FACTOR;
            }

            if (DestRectangle.Y >= GROUND_Y) // TODO: Replace with hitbox
            {
                hasJumped = false;
            }

            if (!hasJumped)
            {
                velocity.Y = 0f;
            }

            if (ks.IsKeyDown(playerKeys.Jump) && hasJumped == false) // starts from here
            {
                mCurrentState = State.Jumping;
                velocity.Y = JUMP_FACTOR;
                hasJumped = true;
            }

        }

        #region Collision

        const int LEFT_MARGIN = 110;
        const int RIGHT_MARGIN = 288 - 180;
        const int TOP_MARGIN = 88;
        const int BOTTOM_MARGIN = 288 - 195;

        /// <summary>
        /// Checks if player's left is touching other player's left
        /// </summary>
        /// <param name="otherPlayer">other player</param>
        /// <returns>Returns true if player is touching other player</returns>
        protected bool IsTouchingLeft(Player otherPlayer)
        {
            return destRectangle.Right - RIGHT_MARGIN > otherPlayer.DestRectangle.Left + LEFT_MARGIN &&
              destRectangle.Left + LEFT_MARGIN < otherPlayer.DestRectangle.Left + LEFT_MARGIN &&
              destRectangle.Bottom - BOTTOM_MARGIN > otherPlayer.DestRectangle.Top + TOP_MARGIN &&
              destRectangle.Top + TOP_MARGIN < otherPlayer.DestRectangle.Bottom - BOTTOM_MARGIN;
        }
        
        /// <summary>
        /// Checks if player's right is touching other player's left
        /// </summary>
        /// <param name="otherPlayer">other player</param>
        /// <returns>Returns true if player is touching other player</returns>
        protected bool IsTouchingRight(Player otherPlayer)
        {
            return destRectangle.Left + LEFT_MARGIN < otherPlayer.DestRectangle.Right - RIGHT_MARGIN &&
              destRectangle.Right - RIGHT_MARGIN > otherPlayer.DestRectangle.Right - RIGHT_MARGIN &&
              destRectangle.Bottom - BOTTOM_MARGIN > otherPlayer.DestRectangle.Top + TOP_MARGIN &&
              destRectangle.Top + TOP_MARGIN < otherPlayer.DestRectangle.Bottom - BOTTOM_MARGIN;
        }

        /// <summary>
        /// Checks if player is facing enemy
        /// </summary>
        /// <param name="otherPlayer">other player</param>
        /// <returns>Returns true if player is facing the other player
        protected bool IsFacing(Player otherPlayer)
        {
            return (this.Hitbox.Left < otherPlayer.Hitbox.Left && this.flip == SpriteEffects.None) ||
                (this.Hitbox.Left > otherPlayer.Hitbox.Left && this.flip == SpriteEffects.FlipHorizontally);
        }

        /// <summary>
        /// Checks if player is on attack Level of other player
        /// </summary>
        /// <param name="otherPlayer">other player</param>
        /// <returns>Returns true if player levelled to hit other player</returns>
        protected bool IsLeveled(Player otherPlayer)
        {
            return ((this.Hitbox.Top + this.Hitbox.Bottom)/2 >= otherPlayer.Hitbox.Top ) ||
                ((this.Hitbox.Top + this.Hitbox.Bottom) / 2 <= otherPlayer.Hitbox.Bottom);
        }
        #endregion


    }
}
