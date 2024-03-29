﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Factories;

namespace Platformer
{
    class Ground
    {
        public Body Body { get; private set; }

        private Texture2D texture;

        public Ground(World world, Texture2D texture, Vector2 position)
        {
            this.texture = texture;

            Body = BodyFactory.CreateRectangle(world, 
                ConvertUnits.ToSimUnits(texture.Width),
                ConvertUnits.ToSimUnits(texture.Height), 1f, position);
           Body.BodyType = BodyType.Static;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,
                ConvertUnits.ToDisplayUnits(Body.Position * 9),
                null,
                Color.White,
                0f,
                new Vector2(texture.Width / 2, texture.Height / 2),  // origin
                1f,
                SpriteEffects.None,
                0f);
        }
    }
}
