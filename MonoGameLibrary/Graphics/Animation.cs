using System;
using System.Collections.Generic;

namespace MonoGameLibrary.Graphics;

public class Animation 
{
    //texture regions that make up the frames of the animation
    //the order of the collection is the order that the frames should be displayed in
    public List<TextureRegion> Frames { get; set; }
    //the amount of time to delay between each frame before moving to the next frame
    public TimeSpan Delay { get; set; }

    //creates a new animation
    public Animation()
    {
        Frames = new List<TextureRegion>();
        Delay = TimeSpan.FromMilliseconds(100);
    }
    //Creates a new animation with the specified frames and delay
    public Animation(List<TextureRegion> frames, TimeSpan delay)
    {
        Frames = frames;
        Delay = delay;
    }
}

