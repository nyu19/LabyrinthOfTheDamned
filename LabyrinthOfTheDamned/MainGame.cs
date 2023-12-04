using LabyrinthOfTheDamned.Scenes;
using LabyrinthOfTheDamned.Scenes.ActionScene;
using LabyrinthOfTheDamned.Scenes.HelpScene;
using LabyrinthOfTheDamned.Scenes.StartScene;
using LabyrinthOfTheDamned.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LabyrinthOfTheDamned
{
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public StartScene startScene;
        public ActionScene actionScene;
        public HelpScene helpScene;
        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.Title = "Labyrinth Of The Damned | Nakul Upasani and Shahyar Fida";
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.PreferredBackBufferWidth = (int)(_graphics.PreferredBackBufferHeight * 1.78);
        }

        protected override void Initialize()
        {
            Shared.stageSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            // TODO: Add your initialization logic here

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

        }

        protected override void Update(GameTime gameTime)
        {
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();

            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    actionScene.Show();

                }
                else if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    helpScene.Show();
                }
                else if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }

            if (actionScene.Enabled || helpScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
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
    }
}
