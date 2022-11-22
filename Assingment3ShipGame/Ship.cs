//Anya Scheinman
//ascheinman4585@conestogac.on.ca
//8154585
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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks.Sources;


namespace Assingment3ShipGame
{
    public class Ship : DrawableGameComponent
    {
        //setting up the variables for the ship
        SpriteBatch spriteBatch;
        Texture2D shipTexture;
        Vector2 shipPosition;
        Vector2 shipSpeed;
        //creating the variables for the ship movmnts and size and location.
        float shipRotation;
        Rectangle screenRectangle;
        Vector2 origin;
        float shipSize = 0.1f;
        Vector2 position;
        Vector2 speed;
        public Vector2 location;
        private int rectSize = 10;
        
        public Vector2 Speed { get => speed; set => speed = value; }
        public Vector2 Position { get => position; set => position = value; }
      
        public Ship(Game game, SpriteBatch spriteBatch, Texture2D shipTexture, Vector2 shipPosition, Vector2 shipSpeed, Vector2 stage) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.shipTexture = shipTexture;
            this.shipPosition = shipPosition;
            this.shipSpeed = shipSpeed;
            //making the screen rectagle 
            screenRectangle = new Rectangle(0, 0, shipTexture.Width, shipTexture.Height);
            //creating a origin with the ship width
            origin = new Vector2(shipTexture.Width / 2, 0);
        }

        public override void Draw(GameTime gameTime)
        {
            //drawing the ship to the game.
            spriteBatch.Begin();
            spriteBatch.Draw(shipTexture, shipPosition, screenRectangle, Color.White, shipRotation, origin, shipSize, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
         
            //making the ship move with the click of the left mouse button. 
            MouseState mState = Mouse.GetState();
            if (mState.LeftButton == ButtonState.Pressed)
            {
                Vector2 target = new Vector2(mState.X, mState.Y);
                float xDiffrence = target.X - shipPosition.X;
                float yDiffrence = target.Y - shipPosition.Y;

               
                //making the ship rotate correctly so the front of the ship moves with the mouse.
                shipPosition.X += xDiffrence * shipSpeed.X * 0.05f;
                shipPosition.Y += yDiffrence * shipSpeed.Y * 0.05f;
                shipRotation = (float)Math.Atan2(xDiffrence, -yDiffrence);

            }


            base.Update(gameTime);
        }
        //creating a bounds method for the rectle for ship postion
        public Rectangle bound()
        {
            return new Rectangle((int)shipPosition.X, (int)shipPosition.Y, rectSize, rectSize);
        }

        //making a method to size up the ship.
        public void sizeUp()
        {
            shipSize = (float)((double)shipSize * 1.05);
            rectSize = rectSize + (int)(rectSize / 2);
        }

    }
}

