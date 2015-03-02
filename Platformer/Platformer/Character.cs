using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
   class Character
   {
      public Body Body;

      private Texture2D texture;

      public Vector2 DragPosition { get; private set; }

      public Vector2 Position { get; private set; }
      public bool IsBeingDragged { get; private set; }
     
      public Character(World world, Texture2D texture , Vector2 position)
      {
         this.texture = texture;

         Body = BodyFactory.CreateRectangle(world,
            ConvertUnits.ToSimUnits(texture.Width),
            ConvertUnits.ToSimUnits(texture.Height), 1f, position);

         Body.BodyType = BodyType.Dynamic;
      }

      public void Move(Vector2 newPosition)
      {

         Vector2 direction = Body.Position + newPosition;
         Body.Position = direction;
      }

      public void Launch()
      {
         IsBeingDragged = false;

         // Launch projectile towards the tower
         //Vector2 dragPosition = new Vector2(-2f, 2f);
         Vector2 direction = (Body.Position - DragPosition) * 1.25f;
         //Vector2 direction = new Vector2(1f, -1f);
         Body.ApplyLinearImpulse(direction);
      }

      /// <summary>
      /// Allows the game component to update itself.
      /// </summary>
      /// <param name="gameTime">Provides a snapshot of timing values.</param>
     
      public void Draw(SpriteBatch spriteBatch)
      {
         spriteBatch.Draw(texture,
            ConvertUnits.ToDisplayUnits(Body.Position),
            null,
            Color.White,
            Body.Rotation,
            new Vector2(texture.Width / 2, texture.Height / 2),
            1f,
            SpriteEffects.None,
            0f);
      }
   }
}
