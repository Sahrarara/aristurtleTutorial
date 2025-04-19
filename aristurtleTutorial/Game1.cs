using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Input;
using System;

namespace aristurtleTutorial;

public class Game1 : Core
{
    private AnimatedSprite _slime;
    private AnimatedSprite _bat;
    //tracks slime position
    private Vector2 _slimePosition;
    //speed multiplier when moving
    private const float MOVEMENT_SPEED = 5.0f;
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

        _slime = atlas.CreateAnimatedSprite("slime");

        _bat = atlas.CreateAnimatedSprite("bat");

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        //Update the slime and bat animated sprite
        _slime.Update(gameTime);
        _bat.Update(gameTime);

        CheckKeyBoardInput();
        CheckGamePadInput();

        base.Update(gameTime);
    }

    private void CheckGamePadInput()
    {
        GamePadInfo gamePadOne = Input.GamePads[(int)PlayerIndex.One];

        float speed = MOVEMENT_SPEED;

        //A button held down for increased movement speed and vibrating controller for feedbad
        if (gamePadOne.IsButtonDown(Buttons.A))
        {
            speed *= 1.5f;
            gamePadOne.SetVibration(1.0f,TimeSpan.FromSeconds(1));
        }
        else
        {
            gamePadOne.StopVibration();
        }

        if (gamePadOne.LeftThumbStick != Vector2.Zero)
        {
            _slimePosition.X += gamePadOne.LeftThumbStick.X * speed;
            _slimePosition.Y -= gamePadOne.LeftThumbStick.Y * speed;
        }
        else
        {
            if (gamePadOne.IsButtonDown(Buttons.DPadUp))
            {
                _slimePosition.Y -= speed;
            }
            if (gamePadOne.IsButtonDown(Buttons.DPadDown))
            {
                _slimePosition.Y += speed;
            }
            if (gamePadOne.IsButtonDown(Buttons.DPadLeft))
            {
                _slimePosition.X -= speed;
            }
            if (gamePadOne.IsButtonDown(Buttons.DPadRight))
            {
                _slimePosition.X += speed;
            }
        }
    }

    private void CheckKeyBoardInput()
    {
        //if the left shift key is held down, the movement speed increases by 1.5
        float speed = MOVEMENT_SPEED;
        if (Input.Keyboard.IsKeyDown(Keys.LeftShift))
        {
            speed *= 1.5f;
        }

        if (Input.Keyboard.IsKeyDown(Keys.W) || Input.Keyboard.IsKeyDown(Keys.Up))
        {
            _slimePosition.Y -= speed;
        }
        if (Input.Keyboard.IsKeyDown(Keys.S) || Input.Keyboard.IsKeyDown(Keys.Down))
        {
            _slimePosition.Y += speed;
        }
        if (Input.Keyboard.IsKeyDown(Keys.A) || Input.Keyboard.IsKeyDown(Keys.Left))
        {
            _slimePosition.X -= speed;
        }
        if (Input.Keyboard.IsKeyDown(Keys.D) || Input.Keyboard.IsKeyDown(Keys.Right))
        {
            _slimePosition.X += speed;
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        //Being the sprite batch to prepare for rendering
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        //Draw Slime and bat texture region
        _slime.Draw(SpriteBatch, _slimePosition);
        _bat.Draw(SpriteBatch, new Vector2(_slime.Width + 10, 0));

        SpriteBatch.End();


        base.Draw(gameTime);
    }
}
