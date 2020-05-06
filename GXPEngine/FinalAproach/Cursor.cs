using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;

class Cursor:AnimationSprite
{
    public Cursor():base("Art/brush.png", 2, 1) {   
    }

    public Cursor(int width, int height) : this() {
        this.width = width;
        this.height = height;
    }

}
