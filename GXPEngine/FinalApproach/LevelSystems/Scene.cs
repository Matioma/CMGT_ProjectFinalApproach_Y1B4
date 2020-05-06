using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;

public class Scene:GameObject
{
    public Scene() {
        BuildLevel1();
    }

    public Scene(int i) {
    }

    void BuildLevel1() {
        var button = new MenuButton("art/Button.jpg", 2, 1);
        button.ButtonTarget = "Level1";
        button.SetXY(250, 120);
        AddChild(button);


        button = new MenuButton("art/Button.jpg", 2, 1);
        button.SetXY(250, 320);
        AddChild(button);


        button = new MenuButton("art/Button.jpg", 2, 1);
        button.ButtonTarget = "Level1";
        button.SetXY(250, 520);
        AddChild(button);
    }

}

