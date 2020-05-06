using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;



public delegate void Action();

abstract class Button : AnimationSprite,IButton
{
    public Button(string path, int cols, int rows) : base(path, cols, rows) {
        SetOrigin(width / 2, height / 2);
    }


    public void Update()
    {
        //OnClick();
        //OnHover();
        //OnHoverEnd();
    }
    //protected void PlaySound()
    //{
    //    Console.WriteLine("Ai Play sound");
    //}
    //protected void ChangeColor()
    //{
    //    Console.WriteLine("Change sprite");
    //}
    public virtual void OnClick()
    {
        throw new NotImplementedException();
    }

    public virtual void OnHover()
    {
        throw new NotImplementedException();
    }

    public virtual void OnHoverEnd()
    {
        throw new NotImplementedException();
    }
}

