using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;




class Button : HUDElement
{

    public Button(string path, int cols, int rows) : base(path, cols, rows)
    {
        var textObject = new Text("Test", (int)x - width / 2, (int)y - height / 2, width, height, 0);
        AddChild(textObject);
    }

    public override void OnClick() {
        //TO DO
    }
    public override void OnHover() {
        SetFrame(1);
    }
    public override void OnHoverEnd() {
        SetFrame(0);
    }
    public override void OnClickRelease()
    {
        SceneManager.Instance.OpenScene(ButtonTarget);
    }
    public override void OnClickPressed()
    {
        //TO DO
    }
}

