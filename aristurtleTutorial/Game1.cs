using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace aristurtleTutorial;

public class Game1 : Core
{
    private Sprite _slime;
    private Sprite _bat;
    public Game1() :base("aristurtle Tutorial",1280,720,false)
    {

    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        //load the atlas texture
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");

        _slime = atlas.CreateSprite("slime");

        _bat = atlas.CreateSprite("bat");

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        //Being the sprite batch to prepare for rendering
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        //Draw Slime and bat texture region
        _slime.Draw(SpriteBatch, Vector2.One);
        _bat.Draw(SpriteBatch, new Vector2(_slime.Width + 10, 0));

        SpriteBatch.End();


        base.Draw(gameTime);
    }
}
