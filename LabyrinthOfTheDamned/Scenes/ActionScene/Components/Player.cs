using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthOfTheDamned.Scenes.ActionScene.Components
{
    public class Player : DrawableGameComponent
    {
        const int FRAME_HEIGHT = 288;
        const int FRAME_WIDTH = 288;
        const int SPEED_FACTOR = 3;
        int scaleFactor = 1;
        SpriteBatch sb;
        double delay, delayCounter;
        Vector2 pos, origin;
        Texture2D texWalk, texAttack, texIdle, texDeath, texHurt, activeTex;
        int frameCounter = 0;
        Rectangle frame = new Rectangle(0, 0, FRAME_WIDTH, FRAME_HEIGHT);
        Rectangle dest;
        MainGame game;
        KeyboardState oldKeyboardState;
        SpriteEffects flip = SpriteEffects.None;
        Rectangle hitbox;

        public Player(Game game, SpriteBatch sb, Vector2 pos, Texture2D texWalk, Texture2D texAttack, Texture2D texIdle, Texture2D texDeath, Texture2D texHurt) : base(game)
        {
            this.game = (MainGame)game;
            this.sb = sb;
            dest = new Rectangle(100, 100, FRAME_WIDTH * scaleFactor, FRAME_HEIGHT * scaleFactor);
            this.texWalk = texWalk;
            this.texAttack = texAttack;
            this.texIdle = texIdle;
            this.texDeath = texDeath;
            this.texHurt = texHurt;
            this.activeTex = texHurt;
            //this.origin = new Vector2(activeTex.Bounds.Width/2, activeTex.Bounds.Height);
        }

        public override void Update(GameTime gameTime)
        {
            delay = 100 * 0.167 * (activeTex.Width / FRAME_WIDTH);
            delayCounter += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (delayCounter >= delay)
            {
                frame.X = frameCounter * FRAME_WIDTH;

                frameCounter++;
                if (frameCounter >= (activeTex.Width / FRAME_WIDTH))
                {
                    frameCounter = 0;
                    if (activeTex != texIdle)
                    {
                        activeTex = texIdle;
                    }
                }
                delayCounter = 0;

            }

            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.A) && activeTex != texAttack)
            {
                activeTex = texWalk;
                flip = SpriteEffects.FlipHorizontally;
                dest.X -= SPEED_FACTOR;
            }
            else if (ks.IsKeyDown(Keys.D) && activeTex != texAttack)
            {
                activeTex = texWalk;
                flip = SpriteEffects.None;
                dest.X += SPEED_FACTOR;
            }
            if (ks.IsKeyDown(Keys.Space) && ks != oldKeyboardState)
            {
                activeTex = texAttack;
                frameCounter = 0;
                delayCounter = 0;
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

    }
}
