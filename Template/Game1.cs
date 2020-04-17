
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace Template
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D AK; //bilden på skeppet
        Vector2 AKpos = new Vector2(400, 200); //position
        Texture2D bullets;
        Vector2 bulletspos = new Vector2(100, 800);
        Rectangle hitbox = new Rectangle(400, 500, 100, 65);
        List<Vector2> AKbulletsPos = new List<Vector2>();
        List<Enemies> enemies = new List<Enemies>();
        Random random = new Random();

        //Game World
        List<Enemies> enemies = new List<Enemies>();
        Random random = new Random();

        KeyboardState kNewstate;
        KeyboardState kOldstate;
        private object spriteBach;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            AK = Content.Load<Texture2D>("AK");
            bullets = Content.Load<Texture2D>("bullets");




            // TODO: use this.Content to load your game content here 
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            int counter = 1; //timern till spelet
            int limit = 50;
            float countDuration = 2f;
            float currentTime = 0f;

            currentTime += (float)GameTime.ElapsedGameTime.TotalSeconds;

            if (currentTime >= countDuration)
            {
                counter++;
                currentTime -= countDuration;
            }
            if (counter >= limit)
            {
                counter = 0;
            }
            // TODO: Unload any non ContentManager content here
        }

        float spawn = 0;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState kstate = Keyboard.GetState();
            KeyboardState kNewstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Right))
                AKpos.X += 10;
            if (kstate.IsKeyDown(Keys.Left))
                AKpos.X -= 10;
            if (kstate.IsKeyDown(Keys.Up))
                AKpos.Y -= 10;
            if (kstate.IsKeyDown(Keys.Down))
                AKpos.Y += 10;
            if (kNewstate.IsKeyDown(Keys.Space) && kOldstate.IsKeyUp(Keys.Space))
            {
                AKbulletsPos.Add(bulletspos);
            }

            for (int i = 0; i < AKbulletsPos.Count; i++)
            {
                AKbulletsPos[i] = AKbulletsPos[i] - new Vector2(0, 1);
            }

            RemoveObjects();//tar bort objekt säkert

            kOldstate = kNewstate;

            //Enemy spawn
            {
                spawn += (float)gameTime.ElapsedGameTime.TotalSeconds;

                foreach (spaceshooter.Enemies enemy in enemies)
                    enemy.Update(graphics.GraphicsDevice);

                base.Update(gameTime);
            }




            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public void LoadEnemies() 
        {
            int randY = random.Next(100, 400);

            if (spawn >= 1)
            {
                spawn = 0;
                if(enemies.Count() < 4)
                    enemies.Add(new Enemies(Content.Load<Texture2D>("Enemyship"), new Vector2(1100, randY)));
            }
            for(int i = 0; i < enemies.Count; i++)
                if(!enemies[i].isVisible)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(AK, new Rectangle(AKpos.ToPoint(), new Point(100, 100)), Color.White);
            spriteBatch.Draw(bullets, new Rectangle(bulletspos.ToPoint(), new Point(100, 100)), Color.White);
            foreach (Vector2 bulletspos in AKbulletsPos)
            {
                Rectangle rec = new Rectangle();
                rec.Location = bulletspos.ToPoint();
                rec.Size = new Point(30, 30);
                spriteBach.Draw(AK, rec, Color.Red);
            }

            spriteBatch.End();





            base.Draw(gameTime);
        }
        void RemoveObjects()
        {
            List<Vector2> temp = new List<Vector2>();
            foreach (var item in AKbulletsPos)
            {
                if (item.Y >= 50)
                {
                    temp.Add(item);
                }
            }

            AKbulletsPos = temp;
        }
    }
}


