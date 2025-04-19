using Microsoft.Xna.Framework;

namespace MonoGameLibrary.Input;

public class InputManager
{
    public KeyboardInfo Keyboard { get; private set; }
    public MouseInfo Mouse { get; private set; }
    //4 Gamepads are supported 0-3
    public GamePadInfo[] GamePads { get; private set; }

    public InputManager()
    {
        Keyboard = new KeyboardInfo();
        Mouse = new MouseInfo();
        GamePads = new GamePadInfo[4];
        for (int i = 0; i < 4; i++)
        {
            GamePads[i] = new GamePadInfo((PlayerIndex)i);
        }
    }

    public void Update (GameTime gameTime)
    {
        Keyboard.Update();
        Mouse.Update();
        for (int i = 0; i < 4; i++)
        {
            GamePads[i].Update(gameTime);
        }
    }

}
