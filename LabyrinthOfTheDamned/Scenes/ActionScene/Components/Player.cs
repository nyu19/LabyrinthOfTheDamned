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

namespace LabyrinthOfTheDamned.Scenes.ActionScene.Components
{
    enum State
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
        const int SPEED_FACTOR = 3;
        const int JUMP_FACTOR = -10;
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
        Rectangle dest;
        MainGame game;
        KeyboardState oldKeyboardState;
        SpriteEffects flip = SpriteEffects.None;
        State mCurrentState = State.Idling;
        Vector2 velocity;
        Rectangle hitbox;

        public Player(Game game, SpriteBatch sb, TextureModel playerTextures, KeyModel playerKeys) : base(game)
        {
            this.game = (MainGame) game;
            this.sb = sb;
            dest = new Rectangle((int)Shared.stageSize.X/2, GROUND_Y, FRAME_WIDTH * scaleFactor, FRAME_HEIGHT * scaleFactor);
            this.playerTextures = playerTextures;
            this.activeTex = playerTextures.Hurt;
            this.playerKeys = playerKeys;
            hasJumped = true;
            this.origin = new Vector2(FRAME_WIDTH/2, FRAME_HEIGHT);
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
            else
            {
                velocity.X = 0;
            }

            JumpHandler(ks);

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

            sb.Begin(SpriteSortMode.Immediate,BlendState.Opaque);
            //sb.Begin();
            sb.Draw(activeTex, dest, frame, Color.White, 0f, origin, flip, 0);
            sb.End();

            base.Draw(gameTime);
        }

        private void JumpHandler(KeyboardState ks)
        {
            if (ks.IsKeyDown(playerKeys.Jump) && hasJumped == false)
            {
                mCurrentState = State.Jumping;
                velocity.Y = JUMP_FACTOR;
                hasJumped = true;
            }

            dest.X += (int)velocity.X;
            dest.Y += (int)velocity.Y;

            if (hasJumped == true)
            {
                float i = 2;
                velocity.Y += 0.15f * i;
            }

            if (dest.Y >= GROUND_Y) // TODO: Replace with hitbox
            {
                hasJumped = false;
            }

            if (hasJumped == false)
            {
                velocity.Y = 0f;
            }
        }

        #region Collision

        //protected bool IsTouchingLeft()
        //{
        //    return this.dest.Right + this.velocity.X > sprite.Rectangle.Left &&
        //      this.Rectangle.Left < sprite.Rectangle.Left &&
        //      this.Rectangle.Bottom > sprite.Rectangle.Top &&
        //      this.Rectangle.Top < sprite.Rectangle.Bottom;
        //}

        //protected bool IsTouchingRight(Sprite sprite)
        //{
        //    return this.Rectangle.Left + this.Velocity.X < sprite.Rectangle.Right &&
        //      this.Rectangle.Right > sprite.Rectangle.Right &&
        //      this.Rectangle.Bottom > sprite.Rectangle.Top &&
        //      this.Rectangle.Top < sprite.Rectangle.Bottom;
        //}

        //protected bool IsTouchingTop(Sprite sprite)
        //{
        //    return this.Rectangle.Bottom + this.Velocity.Y > sprite.Rectangle.Top &&
        //      this.Rectangle.Top < sprite.Rectangle.Top &&
        //      this.Rectangle.Right > sprite.Rectangle.Left &&
        //      this.Rectangle.Left < sprite.Rectangle.Right;
        //}

        //protected bool IsTouchingBottom(Sprite sprite)
        //{
        //    return this.Rectangle.Top + this.Velocity.Y < sprite.Rectangle.Bottom &&
        //      this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
        //      this.Rectangle.Right > sprite.Rectangle.Left &&
        //      this.Rectangle.Left < sprite.Rectangle.Right;
        //}
        #endregion

    }
}
