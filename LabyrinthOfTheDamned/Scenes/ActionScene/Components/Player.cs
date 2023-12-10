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

    public partial class Player : DrawableGameComponent
    {
        public int id;
        const int FRAME_HEIGHT = 288;
        const int FRAME_WIDTH = 288;
        const int SPEED_FACTOR = 7;
        const int JUMP_FACTOR = -15;
        const int GROUND_Y = 800;
        const int STARTING_HEALTH = 100;
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
        Rectangle destRectangle, stageRect;
        MainGame game;
        KeyboardState oldKeyboardState;
        public SpriteEffects flip = SpriteEffects.None;
        public State mCurrentState = State.Idling;
        Vector2 velocity;
        SoundEffect sword, jump, damageTaken;
        int playerHealth;
        Random speedRandomize = new Random();

        public bool isDead { get { return PlayerHealth <= 0; } }
        public Rectangle Hitbox
        {
            get {
                return new Rectangle(DestRectangle.Left + LEFT_MARGIN, DestRectangle.Top + TOP_MARGIN, 93, 108);
            }
        }

        public Rectangle DestRectangle { get => destRectangle; set => destRectangle = value; }
        public int PlayerHealth { get => playerHealth; set => playerHealth = value; }

        public Player(int id,Game game, SpriteBatch sb, Vector2 startingPosition, TextureModel playerTextures, KeyModel playerKeys) : base(game)
        {
            this.id = id;
            this.game = (MainGame)game;
            this.sb = sb;
            this.destRectangle = new Rectangle((int)startingPosition.X, (int)startingPosition.Y, FRAME_WIDTH * scaleFactor, FRAME_HEIGHT * scaleFactor);
            this.playerTextures = playerTextures;
            this.activeTex = playerTextures.Hurt;
            this.playerKeys = playerKeys;
            this.hasJumped = true;
            this.origin = new Vector2(FRAME_WIDTH / 2, FRAME_HEIGHT);
            this.sword = game.Content.Load<SoundEffect>("sounds/sword");
            PlayerHealth = STARTING_HEALTH;
            stageRect = new Rectangle(0, 0, (int)Shared.stageSize.X, (int)Shared.stageSize.Y);
        }

        public override void Update(GameTime gameTime)
        {

            if (PlayerHealth <= 0)
            {
                mCurrentState = State.Dying;
                if (!isDead)
                {
                    frameCounter = 0;
                    delayCounter = 0;
                }
            }
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

            #region Frames
            
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

                if (frameCounter >= (playerTextures.Death.Width / FRAME_WIDTH)-1 && mCurrentState == State.Dying)
                {
                    ActionScene.gameEnded = true;
                }
            }
            if (isDead)
            {
                return;
            }
            #endregion

            #region Movement
            KeyboardState ks = Keyboard.GetState();

            XMovementHandler(ks);
            JumpHandler(ks);

            foreach (GameComponent item in ActionScene.Components)
            {
                if (item == this || item is not Player )
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

            #region Attack
            if (ks.IsKeyDown(playerKeys.Attack) && mCurrentState != State.Attacking && !oldKeyboardState.IsKeyDown(playerKeys.Attack))
            {
                mCurrentState = State.Attacking;
                frameCounter = 0;
                delayCounter = 0;
                oldKeyboardState = ks;
            }
            #endregion

            ManageHealth();

            oldKeyboardState = ks;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            sb.Begin();
            if (Enabled)
                sb.Draw(activeTex, DestRectangle, frame, Color.White, 0f, origin, flip, 0);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
