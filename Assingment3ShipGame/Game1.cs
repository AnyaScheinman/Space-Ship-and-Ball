//Anya Scheinman
//ascheinman4585@conestogac.on.ca

//Assignment 3-Ship Game
//Revision History
//Anya Scheinman, 2022.11.14: Created
//Anya Scheinman, 2022.11.15: Code Added
//Anya Scheinman, 2022.11.16: Code Added
//Anya Scheinman, 2022.11.17: Added Comments
//Anya Scheinman, 2022.11.17: Finished project

//Ship Image  
//Spaceship. (n.d.).Retrieved from https://www.pngfind.com/mpng/hRbRmJ_spaceship-pixel-art-spaceship-space-invaders-png-transparent/. 
//planet Image
//Red Circle. (n.d.). Retrieved from https://www.emoji.co.uk/view/956/. 

using System;
using System.Collections;
using System.Threading;
using System.Threading.Channels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Net.Mime.MediaTypeNames;



namespace Assingment3ShipGame
{
    public class Game1 : Game
    {
        //creating varables for the ship, font, planet and timer.
        GraphicsDeviceManager _graphics;
        SpriteBatch spriteBatch;

        private Ship ship;

        SpriteFont font;

        //planet 
        Texture2D planetTexture;
        Vector2 planetPosition = new Vector2(100, 100);
        const int planetRadius = 30;

        //score
        int score = 0;

        //timer 
        private double elapsedTime;
        private double interval = 500;
        private string message = "";
        double gameTimer = 6000;
        double milisecond = 0;




        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();



        }

        protected override void LoadContent()
        {

            //roundering the math timer
            gameTimer = Math.Round(gameTimer, 2);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //adding the images and ship postiition and speed
            Texture2D shipTexture = Content.Load<Texture2D>("Spaceship1.0"); Vector2 stage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            Vector2 shipPosition = new Vector2(stage.X / 2, stage.Y / 2);
            Vector2 shipSpeed = new Vector2(1, 1);
            //adding the ship
            ship = new Ship(this, spriteBatch, shipTexture, shipPosition, shipSpeed, stage);
            this.Components.Add(ship);

            //adding planet image and boundry
            planetTexture = Content.Load<Texture2D>("RedPlanet"); Vector2 stage2 = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            Vector2 planetPosition = new Vector2(stage2.X / 2, stage2.Y / 2);

            //adding the font for the program
            font = Content.Load<SpriteFont>("File");

        }


        protected override void Update(GameTime gameTime)
        {



            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //creating a rectangle for planet and ship
            Rectangle planetRect = new Rectangle((int)planetPosition.X, (int)planetPosition.Y, planetTexture.Width, planetTexture.Height);
            //calling the getBounds method i created in the ship class
            Rectangle shipRect = ship.bound();

            //creating logic for when the ship rectagle touches the the planet.
            //making the score add by 10 everytime the ship touches the planet. 
            //making sure the ship sizes up as well.
            if (planetRect.Intersects(shipRect))
            {
                score = score + 10;

                Random random = new Random();

                ship.sizeUp();

                planetPosition.X = random.Next(0, _graphics.PreferredBackBufferWidth);
                planetPosition.Y = random.Next(0, _graphics.PreferredBackBufferHeight);


            }


            // TODO: Add your update logic here

            
            //when the game timer reaches zero the planet postition goes to 0,0 and a message displays 
            //if points are 70 or under.
            //also shows a message if th points are greater then 100
            if (gameTimer == 0)
            {
                planetPosition.X = 0;
                planetPosition.Y = 0;
                if(score <= 70)
                {
                    message = "Better try next time";
                }
                if(score > 100)
                {
                    message = "Winner!";
                }

            }
            else
            //creating logic for if time lapse is greater then the interval.
            //changing the planet postion randomly
            {
                if (elapsedTime > interval)
                {
                    Random random = new Random();
                    elapsedTime -= interval;
                    planetPosition.X = random.Next(0, _graphics.PreferredBackBufferWidth);
                    planetPosition.Y = random.Next(0, _graphics.PreferredBackBufferHeight);
                   
                }
                //making the timer go down and rounding the timer so it counts down from 60.
                elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
                gameTimer--;
                milisecond = Math.Round(gameTimer / 100);

                base.Update(gameTime);
            }

       
        }

        protected override void Draw(GameTime gameTime)
        {
            //adding a begin, draw and draw string and end.
            //drawing the planet with the planet textue and radius 
            //adding the "label" for score timer and message to the user on how they did, win or loose.
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(planetTexture, new Vector2(planetPosition.X - planetRadius, planetPosition.Y - planetRadius), Color.White);
            spriteBatch.DrawString(font, "SCORE: " + score.ToString(), new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(font, "Timer: " + milisecond, new Vector2(10, 35), Color.White);
            spriteBatch.DrawString(font, message, new Vector2(10, 55), Color.White);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


    }
}