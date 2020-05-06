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
        Console.WriteLine("Hovered");
    }
    public override void OnHoverEnd() {
        Console.WriteLine("Hover ended");
    }
}

