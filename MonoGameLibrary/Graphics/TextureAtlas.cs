using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary.Graphics;

public class TextureAtlas 
{
    private Dictionary<string, TextureRegion> _regions;
    //gets or sets the source texture represented by this texture atlas
    public Texture2D Texture { get; set; }
    private Dictionary<string, Animation> _animations;

    //Creates a new texture atlas.
    public TextureAtlas()
    {
        _regions = new Dictionary<string, TextureRegion>();
        _animations = new Dictionary<string, Animation>();
    }
    //Creates a new texture atlas instance using the given texture
    public TextureAtlas(Texture2D texture)
    {
        Texture = texture;
        _regions = new Dictionary<string, TextureRegion>();
        _animations = new Dictionary<string, Animation>();
    }
    //creates a new region and adds it to this texture atlas
    //<param name="name">The name to give the texture region.</param>
    //<param name="x">The top-left x-coordinate position of the region boundary relative to the top-left corner of the source texture boundary.</param>
    //<param name="y">The top-left y-coordinate position of the region boundary relative to the top-left corner of the source texture boundary.</param>
    //<param name="width">The width, in pixels, of the region.</param>
    //<param name="height">The height, in pixels, of the region.</param>
    public void AddRegion(string name, int x, int y, int width, int height)
    {
        TextureRegion region = new TextureRegion(Texture, x, y, width, height);
        _regions.Add(name, region);
    }
    //gets the region from this texture atlas with the specified name
    public TextureRegion GetRegion(string name)
    {
        return _regions[name];
    }
    //removes the region from this tecture atlas with the specified name
    public bool RemoveRegion(string name)
    {
        return _regions.Remove(name);
    }
    //removes all regions from this tecture atlas
    public void Clear()
    {
        _regions.Clear();
    }
    //creates a new texture atlas based on a texture atlas xml configuration file.
    public static TextureAtlas FromFile(ContentManager content, string fileName)
    {
        TextureAtlas atlas = new TextureAtlas();
        string filePath = Path.Combine(content.RootDirectory, fileName);
        using (Stream stream = TitleContainer.OpenStream(filePath))
        {
            using (XmlReader reader = XmlReader.Create(stream))
            {
                XDocument doc = XDocument.Load(reader);
                XElement root = doc.Root;
                //The <Texture> element contains the content path for the Texture2D to load.
                //so we'll retrieve the calue then use the content manager to load the texture.
                string texturePath = root.Element("Texture").Value;
                atlas.Texture = content.Load<Texture2D>(texturePath);

                var regions = root.Element("Regions")?.Elements("Region");
                if (regions != null)
                {
                    foreach(var region in regions)
                    {
                        string name = region.Attribute("name")?.Value;
                        int x = int.Parse(region.Attribute("x")?.Value ?? "0");
                        int y = int.Parse(region.Attribute("y")?.Value ?? "0");
                        int width = int.Parse(region.Attribute("width")?.Value ?? "0");
                        int height = int.Parse(region.Attribute("height")?.Value ?? "0");

                        if (!string.IsNullOrEmpty(name))
                        {
                            atlas.AddRegion(name, x, y, width, height);
                        }
                    }
                }

                var animationElements = root.Element("Animations")?.Elements("Animation");
                if (animationElements != null)
                {
                    foreach (var animationElement in animationElements)
                    {
                        string name = animationElement.Attribute("name")?.Value;
                        float delayInMilliseconds = float.Parse(animationElement.Attribute("delay")?.Value ?? "0");
                        TimeSpan delay = TimeSpan.FromMilliseconds(delayInMilliseconds);

                        List<TextureRegion> frames = new List<TextureRegion>();

                        var frameElements = animationElement.Elements("Frame");

                        if (frameElements != null)
                        {
                            foreach (var frameELement in frameElements)
                            {
                                string regionName = frameELement.Attribute("region").Value;
                                TextureRegion region = atlas.GetRegion(regionName);
                                frames.Add(region);
                            }
                        }
                        Animation animation = new Animation(frames, delay);
                        atlas.AddAnimation(name, animation);
                    }
                }

                return atlas;
            }
        }
    }
    //creates a new sprite using the region from this texture atlas with the specified name.
    //returns new Sprite using the texture region with the specified name "regionName"
    public Sprite CreateSprite(string regionName)
    {
        TextureRegion region = GetRegion(regionName);
        return new Sprite(region);
    }
    //adds the given animation to this texture atlas
    public void AddAnimation(string animationName, Animation animation)
    {
        _animations.Add(animationName, animation);

    }
    public Animation GetAnimation(string animationName)
    {
        return _animations[animationName];
    }
    public bool RemoveAnimation(string animationName)
    {
        return _animations.Remove(animationName);
    }
    public AnimatedSprite CreateAnimatedSprite(string animationName)
    {
        Animation animation = GetAnimation(animationName);
        return new AnimatedSprite(animation);
    }

}
