﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthOfTheDamned.Scenes.StartScene
{
    public class StartScene : GameScene
    {
        public MenuComponent Menu { get; set; }
        private SpriteBatch sb;
        string[] menuItems = { "Start Game", "Help", "High Score", "Credit", "Quit" };
        public StartScene(Game game) : base(game)
        {
            MainGame g = (MainGame)game;
            
            SpriteFont regularFont = game.Content.Load<SpriteFont>("fonts/RegularFont");
            SpriteFont highlightFont = g.Content.Load<SpriteFont>("fonts/HighlightFont");

            Menu = new MenuComponent(game, regularFont, highlightFont, menuItems);

            this.Components.Add(Menu);
        }
    }
}
