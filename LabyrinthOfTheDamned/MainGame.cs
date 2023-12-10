﻿using LabyrinthOfTheDamned.Scenes;
using LabyrinthOfTheDamned.Scenes.ActionScene;
using LabyrinthOfTheDamned.Scenes.CreditScene;
using LabyrinthOfTheDamned.Scenes.HelpScene;
using LabyrinthOfTheDamned.Scenes.HighScoreScene;
using LabyrinthOfTheDamned.Scenes.StartScene;
using LabyrinthOfTheDamned.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LabyrinthOfTheDamned
{
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public StartScene startScene;
        public ActionScene actionScene;
        public HighScoreScene highscoreScene;
        public HelpScene helpScene;
        public CreditScene creditScene;
        public static KeyboardState oldKeyboardState;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.Title = "Labyrinth Of The Damned | Nakul Upasani and Shahyar Fida";
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.PreferredBackBufferWidth = (int)(_graphics.PreferredBackBufferHeight * 1.78);

        }

        protected override void Initialize()
        {
            Shared.stageSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            Shared.regularFonts = Content.Load<SpriteFont>("fonts/RegularFont");
            Shared.highlightFonts = Content.Load<SpriteFont>("fonts/HighlightFont");
            // TODO: Add your initialization logic here
            HighScoreManager.DeserializeHighscore();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Shared._sb = _spriteBatch;

            startScene = new StartScene(this);
            this.Components.Add(startScene);
            startScene.Show();

            actionScene = new ActionScene(this);
            this.Components.Add(actionScene);

            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);

            highscoreScene = new HighScoreScene(this);
            this.Components.Add(highscoreScene);


            creditScene = new CreditScene(this);
            this.Components.Add(creditScene);

        }

        protected override void Update(GameTime gameTime)
        {
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyUp(Keys.Enter))
                oldKeyboardState = ks;

            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter) && oldKeyboardState.IsKeyUp(Keys.Enter))
                {
                    hideAllScenes();
                    if (ActionScene.gameEnded)
                    {
                        this.Components.Remove(actionScene);
                        actionScene = new ActionScene(this);
                        this.Components.Add(actionScene);
                        ActionScene.gameEnded = false;
                    }
                    actionScene.Show();

                }
                else if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    helpScene.Show();
                }
                if(selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                { 
                    hideAllScenes();
                    highscoreScene.Show();
                
                }
                if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    creditScene.Show();
                }
                else if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    ExitGame();
                }
            }

            if (actionScene.Enabled || helpScene.Enabled || highscoreScene.Enabled || creditScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    if (actionScene.Enabled)
                    {
                        MediaPlayer.Stop();
                    }
                    startScene.MusicSpan = MediaPlayer.PlayPosition;
                    hideAllScenes();
                    startScene.Show();
                }
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkOliveGreen);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void hideAllScenes()
        {
            foreach (GameComponent item in Components)
            {
                if (item is GameScene)
                {
                    GameScene gs = (GameScene)item;
                    gs.Hide();
                }
            }
        }

        public void ExitGame()
        {
            HighScoreManager.SerializeHighscore();
            Exit();
        }
    }
}
