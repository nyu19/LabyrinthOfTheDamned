﻿using LabyrinthOfTheDamned.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthOfTheDamned.Scenes.CreditScene
{
    public class CreditScene : GameScene
    {
        SpriteBatch sb;
        Texture2D texture;

        public CreditScene(Game game) : base(game)
        {
            Game g = (MainGame)game;
            sb = Shared._sb;
            LoadContent();
        }
        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("images/Scenes/Credits");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            if (texture != null)
            {
                sb.Draw(texture, Vector2.Zero, Color.White);

            }
            sb.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
