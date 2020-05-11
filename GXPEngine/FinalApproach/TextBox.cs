using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using GXPEngine;

public class TextBox:GameObject
{
    int width = 0;
    int height = 0;

    public TextDialogBox dialogBox;
     public TextBox(Vec2 position, int width, int height) {
        this.x = position.x;
        this.y = position.y;
        this.width = width;
        this.height = height;

        dialogBox = new TextDialogBox(0,0,width,height);
        AddChild(dialogBox);
    }

    public TextBox(Button button):this(new Vec2(button.x-button.width/4, button.y-button.height/3),button.width,button.height) { 
    }

    public TextBox(Button button, bool alignedLeft) : this(new Vec2(button.x - button.width / 2, button.y - button.height / 3), button.width, button.height)
    {
    }
    public void Configure(Action configuration) {
        if (configuration == null) {
            return;
        }
        configuration.Invoke();
    }


    public void EndConfigure() {
        dialogBox.DrawText();
    }

}
