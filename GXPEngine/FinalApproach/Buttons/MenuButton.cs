using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;




class MenuButton : Button
{

    public MenuButton(string path, int cols, int rows) : base(path, cols, rows)
    {
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

