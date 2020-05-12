using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class MuseumPainting:HUDElement
{
    bool _isAvailable=false;
    public bool IsAvailable {
        get { return _isAvailable; }
        set {
            if (value)
            {
                SetFrame(1);
            }
            else {
                SetFrame(0);
            }
            _isAvailable = value; 
        }
    }

    public MuseumPainting(string path, int cols, int rows) : base(path, cols, rows) {
        IsAvailable = true;
    }

    public override void OnClick()
    {
        Console.WriteLine("Clicked");
    }

    public override void OnHover()
    {
        Console.WriteLine("Hovered");
    }

    public override void OnHoverEnd()
    {
        Console.WriteLine("Hover end");
    }

    public override void OnClickRelease()
    {
        Console.WriteLine("On CLick release");
    }

    public override void OnClickPressed()
    {
        Console.WriteLine("On CLick pressed");
    }
}
