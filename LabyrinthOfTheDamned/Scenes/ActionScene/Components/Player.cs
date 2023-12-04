using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.MediaFoundation;
using SharpDX.Direct3D9;
using LabyrinthOfTheDamned.Utility;
using Microsoft.Xna.Framework.Audio;

namespace LabyrinthOfTheDamned.Scenes.ActionScene.Components
{
    public enum State
    {
        Jumping,
        Walking,
        Idling,
        Attacking,
        Dying,
        Hurt
    }

    public class Player : DrawableGameComponent
    {
        const int FRAME_HEIGHT = 288;
        const int FRAME_WIDTH = 288;
        const int SPEED_FACTOR = 7;
        const int JUMP_FACTOR = -15;
        const int GROUND_Y = 1000;
        int scaleFactor = 1;
        SpriteBatch sb;
        double delay, delayCounter;
        bool hasJumped;
        Vector2 origin;
        KeyModel playerKeys;
        TextureModel playerTextures;
        Texture2D activeTex;
        int frameCounter = 0;
        Rectangle frame = new Rectangle(0, 0, FRAME_WIDTH, FRAME_HEIGHT);
        Rectangle destRectangle;
        MainGame game;
        KeyboardState oldKeyboardState;
        public SpriteEffects flip = SpriteEffects.None;
        public State mCurrentState = State.Idling;
        Vector2 velocity;
        Rectangle hitbox;
        SoundEffect sword, jump, damageTaken;

        public Rectangle DestRectangle { get => destRectangle; set => destRectangle = value; }

        public Player(Game game, SpriteBatch sb,Vector2 startingPosition, TextureModel playerTextures, KeyModel playerKeys) : base(game)
        {
            this.game = (MainGame)game;
            this.sb = sb;
            this.destRectangle = new Rectangle((int)startingPosition.X,(int)startingPosition.Y, FRAME_WIDTH * scaleFactor, FRAME_HEIGHT * scaleFactor);
            this.playerTextures = playerTextures;
            this.activeTex = playerTextures.Hurt;
            this.playerKeys = playerKeys;
            this.hasJumped = true;
            this.origin = new Vector2(FRAME_WIDTH / 2, FRAME_HEIGHT);
            this.sword = game.Content.Load<SoundEffect>("sounds/sword");
        }

        public override void Update(GameTime gameTime)
        {
            switch (mCurrentState)
            {
                case State.Walking:
                    activeTex = playerTextures.Walk;
                    break;
                case State.Attacking:
                    activeTex = playerTextures.Attack;
                    break;
                case State.Dying:
                    activeTex = playerTextures.Death;
                    break;
                case State.Hurt:
                    activeTex = playerTextures.Hurt;
                    break;
                case State.Idling:
                    activeTex = playerTextures.Idle;
                    break;
                default:
                    activeTex = playerTextures.Idle;
                    break;
            }

            delay = 100 * 0.167 * (activeTex.Width / FRAME_WIDTH);
            delayCounter += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (delayCounter >= delay)
            {
                frame.X = frameCounter * FRAME_WIDTH;
                frameCounter++;
                if (frameCounter == 3 && mCurrentState == State.Attacking)
                {
                    sword.Play();
                }
                if (frameCounter >= (activeTex.Width / FRAME_WIDTH))
                {
                    frameCounter = 0;
                    if (mCurrentState != State.Idling)
                    {
                        mCurrentState = State.Idling;
                    }
                }
                delayCounter = 0;

            }

            #region Movement
            KeyboardState ks = Keyboard.GetState();

            XMovementHandler(ks);
            JumpHandler(ks);

            foreach (GameComponent item in ActionScene.Components)
            {
                if (item == this || item is not Player)
                    continue;
                Player sprite = item as Player;
                if ((this.velocity.X > 0 && this.IsTouchingLeft(sprite)) ||
                    (this.velocity.X < 0 & this.IsTouchingRight(sprite)))
                    this.velocity.X = 0;

                //if ((this.velocity.Y > 0 && this.IsTouchingTop(sprite)))
                //    this.velocity.Y = 0;
                
            }

            destRectangle.X += (int)velocity.X;
            destRectangle.Y += (int)velocity.Y;

            #endregion 

            // Attack
            if (ks.IsKeyDown(playerKeys.Attack) && mCurrentState != State.Attacking && !oldKeyboardState.IsKeyDown(playerKeys.Attack))
            {
                mCurrentState = State.Attacking;
                frameCounter = 0;
                delayCounter = 0;
                oldKeyboardState = ks;
            }

            oldKeyboardState = ks;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            sb.Begin();
            sb.Draw(activeTex, DestRectangle, frame, Color.White, 0f, origin, flip, 0);
            sb.End();

            base.Draw(gameTime);
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
            return destRectangle.Right - RightMargin > dgc.DestRectangle.Left + LeftMargin&&
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
