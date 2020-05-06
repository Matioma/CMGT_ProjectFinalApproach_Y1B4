using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;

class MenuButton:Button
{
    public MenuButton(string path, int cols, int rows) : base(path, cols, rows)
    {
    }

    public override void OnClick() {
        Console.WriteLine("Clicked");
    }
    public override void OnHover() {
        SetFrame(1);
        Console.WriteLine("Hovered");
    }
    public override void OnHoverEnd() {
        SetFrame(0);
        Console.WriteLine("Hover ended");
    }
    public override void OnClickRelease()
    {
        Console.WriteLine("OnClickRelease");
    }
    public override void OnClickPressed()
    {
        Console.WriteLine("OnClickPressed");
    }
}

