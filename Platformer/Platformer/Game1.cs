using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FarseerPhysics.Dynamics;
using FarseerPhysics;

namespace Platformer
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        const int LEVEL_WIDTH = 89;
        const int LEVEL_HEIGHT = 53;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private char[,] levelDataArray = new char[LEVEL_HEIGHT,LEVEL_WIDTH];

        private List<Ground> grounds = new List<Ground>();
        private List<Platform> platforms = new List<Platform>();
        private World world;
        private string levelData;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            world = new World(new Vector2(0, 9.82f));
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            CreateGameComponents();
        }

        //Allows you to clear the world and create new GameComponents
        private void CreateGameComponents()
        {
            // Textures
            Texture2D groundTexture = Content.Load<Texture2D>("Images\\ground");
            Texture2D platformTexture = Content.Load <Texture2D>("Images\\platform");

            //Read in the text file
            var stream = TitleContainer.OpenStream("Levels/level1.txt");
            var reader = new StreamReader(stream);

            for(int col = 0; col < LEVEL_HEIGHT; col++)
            {
                //Read in a line from level.txt
                levelData = reader.ReadLine();
                for (int row = 0; row < LEVEL_WIDTH; row++)
                {
                    levelDataArray[col,row] = levelData[row];
                }
            }
            //Put the level data into a list according to the character
            for (int col = 0; col < LEVEL_HEIGHT; col++)
            {
                for (int row = 0; row < LEVEL_WIDTH; row++)
                {
                    if (levelDataArray[col, row] == 'x')
                    {
                        Vector2 position = new Vector2(ConvertUnits.ToSimUnits(row),
                                                ConvertUnits.ToSimUnits(col));

                        Ground ground = new Ground(world, groundTexture, position);

                        grounds.Add(ground);
                    }

                    if (levelDataArray[col, row] == 'o')
                    {
                        Vector2 position = new Vector2(ConvertUnits.ToSimUnits(row),
                                                ConvertUnits.ToSimUnits(col));

                        Platform platform = new Platform(world, platformTexture, position);

                        platforms.Add(platform);
                    }
                }
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null,
             null, Matrix.Identity);

            foreach (Platform p in platforms)
            {
                p.Draw(spriteBatch);
            }

            foreach (Ground g in grounds)
            {
                g.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
