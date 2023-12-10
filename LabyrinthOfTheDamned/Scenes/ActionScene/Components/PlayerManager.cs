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
    public partial class Player
    {
        private void ManageHealth()
        {
            // Add health deduction multipler
            foreach (GameComponent item in ActionScene.Components)
            {
                if (item == this || item is not Player)
                    continue;

                Player otherPlayer = (Player)item;
                
                Rectangle p1 = this.Hitbox;
                Rectangle p2 = otherPlayer.Hitbox;
                
                if (p1.Intersects(p2) && this.IsFacing(otherPlayer) && this.frameCounter == 6 && this.mCurrentState == State.Attacking)
                {
                    otherPlayer.PlayerHealth -= 5 * 20; // TO REMOVE
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


        private void XMovementHandler(KeyboardState ks)
        {

            if (ks.IsKeyDown(playerKeys.Left) && mCurrentState != State.Attacking && Hitbox.Left > 0)
            {
                mCurrentState = State.Walking;
                flip = SpriteEffects.FlipHorizontally;
                velocity.X = -(SPEED_FACTOR + speedRandomize.Next(-2,2));
            }
            else if (ks.IsKeyDown(playerKeys.Right) && mCurrentState != State.Attacking && Hitbox.Right < Shared.stageSize.X)
            {
                mCurrentState = State.Walking;
                flip = SpriteEffects.None;
                velocity.X = SPEED_FACTOR + speedRandomize.Next(-2, 2);
            }
            else if (mCurrentState != State.Jumping || mCurrentState != State.Attacking )
            {
                velocity.X = 0;
            }


        }

        private void JumpHandler(KeyboardState ks)
        {
            if (hasJumped)
            {
                float i = 2;
                velocity.Y += 0.15f * i;
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

        protected bool IsTouchingLeft(Player dgc)
        {
            return destRectangle.Right - RIGHT_MARGIN > dgc.DestRectangle.Left + LEFT_MARGIN &&
              destRectangle.Left + LEFT_MARGIN < dgc.DestRectangle.Left + LEFT_MARGIN &&
              destRectangle.Bottom - BOTTOM_MARGIN > dgc.DestRectangle.Top + TOP_MARGIN &&
              destRectangle.Top + TOP_MARGIN < dgc.DestRectangle.Bottom - BOTTOM_MARGIN;
        }

        protected bool IsTouchingRight(Player dgc)
        {
            return destRectangle.Left + LEFT_MARGIN < dgc.DestRectangle.Right - RIGHT_MARGIN &&
              destRectangle.Right - RIGHT_MARGIN > dgc.DestRectangle.Right - RIGHT_MARGIN &&
              destRectangle.Bottom - BOTTOM_MARGIN > dgc.DestRectangle.Top + TOP_MARGIN &&
              destRectangle.Top + TOP_MARGIN < dgc.DestRectangle.Bottom - BOTTOM_MARGIN;
        }

        protected bool IsTouchingTop(Player dgc)
        {
            return destRectangle.Bottom - BOTTOM_MARGIN > dgc.DestRectangle.Top + TOP_MARGIN &&
              destRectangle.Top + TOP_MARGIN < dgc.DestRectangle.Top + TOP_MARGIN &&
              destRectangle.Right - RIGHT_MARGIN > dgc.DestRectangle.Left + LEFT_MARGIN &&
              destRectangle.Left + LEFT_MARGIN < dgc.DestRectangle.Right - RIGHT_MARGIN;
        }

        protected bool IsTouchingBottom(Player dgc)
        {
            return destRectangle.Top + TOP_MARGIN < dgc.DestRectangle.Bottom - BOTTOM_MARGIN &&
              destRectangle.Bottom - BOTTOM_MARGIN > dgc.DestRectangle.Bottom - BOTTOM_MARGIN &&
              destRectangle.Right - RIGHT_MARGIN > dgc.DestRectangle.Left + LEFT_MARGIN &&
              destRectangle.Left + LEFT_MARGIN < dgc.DestRectangle.Right - RIGHT_MARGIN;
        }

        protected bool IsFacing(Player dgc)
        {
            return (this.Hitbox.Left < dgc.Hitbox.Left && this.flip == SpriteEffects.None) ||
                (this.Hitbox.Left > dgc.Hitbox.Left && this.flip == SpriteEffects.FlipHorizontally);
        }

        protected bool IsLeveled(Player dgc)
        {
            return ((this.Hitbox.Top + this.Hitbox.Bottom)/2 >= dgc.Hitbox.Top ) ||
                ((this.Hitbox.Top + this.Hitbox.Bottom) / 2 <= dgc.Hitbox.Bottom);
        }
        #endregion


    }
}
