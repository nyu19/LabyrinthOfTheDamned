using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.MediaFoundation;

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
        const int GROUND_Y = 600;
        int scaleFactor = 1;
        SpriteBatch sb;
        double delay, delayCounter;
        bool hasJumped;
        Vector2 origin;
        Texture2D texWalk, texAttack, texIdle, texDeath, texHurt, activeTex;
        int frameCounter = 0;
        Rectangle frame = new Rectangle(0, 0, FRAME_WIDTH, FRAME_HEIGHT);
        Rectangle dest;
        MainGame game;
        KeyboardState oldKeyboardState;
        SpriteEffects flip = SpriteEffects.None;
        State mCurrentState = State.Idling;
        Vector2 velocity;
        Rectangle hitbox;

        public Player(Game game, SpriteBatch sb, Texture2D texWalk, Texture2D texAttack, Texture2D texIdle, Texture2D texDeath, Texture2D texHurt) : base(game)
        {
            this.game = (MainGame)game;
            this.sb = sb;
            dest = new Rectangle(200, GROUND_Y, FRAME_WIDTH * scaleFactor, FRAME_HEIGHT * scaleFactor);
            this.texWalk = texWalk;
            this.texAttack = texAttack;
            this.texIdle = texIdle;
            this.texDeath = texDeath;
            this.texHurt = texHurt;
            this.activeTex = texHurt;
            hasJumped = true;
            //this.origin = new Vector2(activeTex.Bounds.Width/2, activeTex.Bounds.Height);
        }

        public override void Update(GameTime gameTime)
        {
            switch (mCurrentState)
            {

                //case State.Jumping:
                case State.Walking:
                    activeTex = texWalk;
                    break;
                case State.Attacking:
                    activeTex = texAttack;
                    break;
                case State.Dying:
                    activeTex = texDeath;
                    break;
                case State.Hurt:
                    activeTex = texHurt;
                    break;
                case State.Idling:
                    activeTex = texIdle;
                    break;
                default:
                    activeTex = texIdle;
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

            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.A) && mCurrentState != State.Attacking)
            {
                mCurrentState = State.Walking;
                flip = SpriteEffects.FlipHorizontally;
                velocity.X = -SPEED_FACTOR;
            }
            else if (ks.IsKeyDown(Keys.D) && mCurrentState != State.Attacking)
            {
                mCurrentState = State.Walking;
                flip = SpriteEffects.None;
                velocity.X = SPEED_FACTOR;
            }
            else
            {
                velocity.X = 0;
            }

            // Y
            JumpHandler(ks);

            // Attack
            if (ks.IsKeyDown(Keys.Space) && ks != oldKeyboardState)
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
            sb.Draw(activeTex, dest, frame, Color.White, 0f, origin, flip, 0);
            sb.End();

            base.Draw(gameTime);
        }
        private void JumpHandler(KeyboardState ks)
        {
            if (ks.IsKeyDown(Keys.W) && hasJumped == false)
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
    }
}
