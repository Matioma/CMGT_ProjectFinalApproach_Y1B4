using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

using GXPEngine;

public class Cursor:AnimationSprite
{
    static string cursorsSpriteSheet = "Art/Cursors.png";
        //;

    public Cursor():base(cursorsSpriteSheet, 2, 1) {
       
    }
    public Cursor(int width, int height) : this() {
        this.width = width;
        this.height = height;
    }
    public Cursor(int width, int height, int cursorIndex) : this(width,height)
    {
        SetCursor(cursorIndex);
    }


    public void SetCursor(int index)
    {
        SetFrame(index);
    }

    public void followMouse() {
        SetXY(Input.mouseX,Input.mouseY);
    }
}
