using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using GXPEngine;
class AnimatedImage : GameObject
{
    Dictionary<string, Animation> animationDictionary = new Dictionary<string, Animation>();


    string animationToPlay =null;


    List<Sprite> sprites = new List<Sprite>();
    Sprite currentImage;

    int currentFrame = 0;


    float FrameRate = 0.083f;
    float timer = 0f;


    public AnimatedImage() {
        //AddAnimation("Art/MainMenuAnimation/Top/frame", ".png", 5, "test");
    
    }

    public AnimatedImage(string pathStart, string fileType, int numberOfImages)
    {
        LoadSprites(pathStart, fileType, numberOfImages);
        SetFrame(0);
    }


    /// <summary>
    /// Adds a new Animation
    /// </summary>
    /// <param name="pathStart"></param>
    /// <param name="fileType"></param>
    /// <param name="numberOfImages"></param>
    /// <param name="animationName">Animation name used for recording</param>
    public void AddAnimation(string pathStart, string fileType, int numberOfImages, string animationName) {
        Animation animation = new Animation();
        animation.LoadAnimationSprites(pathStart, fileType, numberOfImages);
        animationDictionary.Add(animationName, animation);
    }
    public void PlayAnimation(string animationKey) {
        animationToPlay = animationKey;
    }

    //public void StopAnimation() {
    //    animationToPlay = null;
    //}
 



    void Update() {
        Animate();
    }



    void Animate() {
        timer += (float)Time.deltaTime / (float)1000;

        if (timer >= FrameRate)
        {
            if (animationToPlay != null)
            {
                animationDictionary[animationToPlay].Update();
                SetFrame(animationDictionary[animationToPlay].GetCurrentFrame());
            }

            timer = 0;
        }
    }


    public void SetFrame(int i)
    {
        if (i < 0 || i >= sprites.Count) {
            Console.WriteLine("Animated image tried to acces a sprite out the box");
            return;
        }
        if (currentImage != null)
        {
            currentImage.LateRemove();
        }
        currentImage = sprites[i];
        currentFrame = i;
        AddChild(currentImage);
    }


    public void SetFrame(Sprite sprite) {
        if (currentImage != null)
        {
            currentImage.LateRemove();
        }
        currentImage = sprite;
        AddChild(currentImage);

    }



    public void Next() {
        currentFrame++;

        if (currentFrame>=sprites.Count)
        {
            currentFrame = 0;
        }
        SetFrame(currentFrame);
    }

    void LoadSprites(string pathStart, string fileType, int numberOfImages)
    {
        for (int i = 1; i <= numberOfImages; i++)
        {
            sprites.Add(new Sprite(pathStart + i + fileType));
        }
    }

    void LoadSpritesTo(string pathStart, string fileType, int numberOfImages, List<Sprite> sprites)
    {
        for (int i = 1; i <= numberOfImages; i++)
        {
            sprites.Add(new Sprite(pathStart + i + fileType));
        }
    }
}

class Animation {
    List<Sprite> animations =new List<Sprite>();
    int currentFrame = 0;

    float FrameRate = 0.083f;
    float timer = 0f;

    public Sprite GetCurrentFrame()
    {
        return animations[currentFrame];
    }

    public Animation() {
    }

    public void LoadAnimationSprites(string pathStart, string fileType, int numberOfImages)
    {
        for (int i = 1; i <= numberOfImages; i++)
        {
            animations.Add(new Sprite(pathStart + i + fileType));
        }
    }

    public void Next()
    {
        currentFrame++;
        if (currentFrame >= animations.Count)
        {
            currentFrame = 0;
        }
        SetFrame(currentFrame);
    }
    public void SetFrame(int i)
    {
        if (i < 0 || i >= animations.Count)
        {
            Console.WriteLine("Animated image tried to acces a sprite out the box");
            return;
        }
        currentFrame = i;
    }


    public void Animate() {
        timer += (float)Time.deltaTime / (float)1000;

        if (timer >= FrameRate)
        {
            Next();

            timer++;
            timer = 0;
        }

    }

    public void Update() {
        Next();

        //Next();
    }
}