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
            // Implementing!
        }


        private void XMovementHandler(KeyboardState ks)
        {

            if (ks.IsKeyDown(playerKeys.Left) && mCurrentState != State.Attacking)
            {
                mCurrentState = State.Walking;
                flip = SpriteEffects.FlipHorizontally;
                velocity.X = -SPEED_FACTOR;
            }
            else if (ks.IsKeyDown(playerKeys.Right) && mCurrentState != State.Attacking)
            {
                mCurrentState = State.Walking;
                flip = SpriteEffects.None;
                velocity.X = SPEED_FACTOR;
            }
            else if (mCurrentState != State.Jumping || mCurrentState != State.Attacking)
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

        const int LeftMargin = 110;
        const int RightMargin = 288 - 180;
        const int TopMargin = 88;
        const int BottomMargin = 288 - 195;

        protected bool IsTouchingLeft(Player dgc)
        {
            return destRectangle.Right - RightMargin > dgc.DestRectangle.Left + LeftMargin &&
              destRectangle.Left + LeftMargin < dgc.DestRectangle.Left + LeftMargin &&
              destRectangle.Bottom - BottomMargin > dgc.DestRectangle.Top + TopMargin &&
              destRectangle.Top + TopMargin < dgc.DestRectangle.Bottom - BottomMargin;
        }

        protected bool IsTouchingRight(Player dgc)
        {
            return destRectangle.Left + LeftMargin < dgc.DestRectangle.Right - RightMargin &&
              destRectangle.Right - RightMargin > dgc.DestRectangle.Right - RightMargin &&
              destRectangle.Bottom - BottomMargin > dgc.DestRectangle.Top + TopMargin &&
              destRectangle.Top + TopMargin < dgc.DestRectangle.Bottom - BottomMargin;
        }

        protected bool IsTouchingTop(Player dgc)
        {
            return destRectangle.Bottom - BottomMargin > dgc.DestRectangle.Top + TopMargin &&
              destRectangle.Top + TopMargin < dgc.DestRectangle.Top + TopMargin &&
              destRectangle.Right - RightMargin > dgc.DestRectangle.Left + LeftMargin &&
              destRectangle.Left + LeftMargin < dgc.DestRectangle.Right - RightMargin;
        }

        protected bool IsTouchingBottom(Player dgc)
        {
            return destRectangle.Top + TopMargin < dgc.DestRectangle.Bottom - BottomMargin &&
              destRectangle.Bottom - BottomMargin > dgc.DestRectangle.Bottom - BottomMargin &&
              destRectangle.Right - RightMargin > dgc.DestRectangle.Left + LeftMargin &&
              destRectangle.Left + LeftMargin < dgc.DestRectangle.Right - RightMargin;
        }
        #endregion


    }
}
