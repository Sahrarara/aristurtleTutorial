﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary.Graphics;

/// <summary>
/// Represents a rectangular region within a texture.
/// </summary>
public class TextureRegion {

    //Gets or Sets the source texture this texture region is part of.
    public Texture2D Texture { get; set; }
    //Gets or Sets the source rectangle boundary of this texture region within the source texture.
    public Rectangle SourceRectangle { get; set; }
    //Gets the width, in pixels, of this texture region.
    public int Width => SourceRectangle.Width;
    //Gets the height, in pixels, of this texture region.
    public int Height => SourceRectangle.Height;

    //Creates a new texture region
    public TextureRegion()
    {

    }
    //Creates a new texture region using specified source teture
    public TextureRegion(Texture2D texture, int x, int y, int width, int height)
    {

    }
    public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
    {
        Draw(spriteBatch, position, color, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
    {
        Draw(
            spriteBatch,
            position,
            color,
            rotation,
            origin,
            new Vector2(scale, scale),
            effects,
            layerDepth
            );
    }

    public void Draw (SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
    {
        spriteBatch.Draw(
            Texture,
            position,
            SourceRectangle,
            color,
            rotation,
            origin,
            scale,
            effects,
            layerDepth
            );
    }

}
