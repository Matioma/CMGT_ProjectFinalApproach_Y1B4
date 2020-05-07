using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

using GXPEngine;
class Text : GameObject
{
    EasyDraw easyDraw;

    public string _message = "Test";
    public string Message {
        get { return _message; }
        set {
            _message = value;
            DrawText();
        }
    }

    Color3 color = new Color3(255,0,0);
    string fontFile = "STENCIL.TTF";
    float textRotation = 0;
    float fontSize = 20;

    public Text(string message, int x , int y, int width, int height, int rotation) {
        easyDraw = new EasyDraw(width,height);
        this.x = x;
        this.y = y;
        Message = message;
        AddChild(easyDraw);
    }

    void DrawText() {
        var _foo = new PrivateFontCollection();
        _foo.AddFontFile(fontFile);

        easyDraw.TextFont(new Font((FontFamily)_foo.Families[0], fontSize));
        easyDraw.rotation= textRotation;
        easyDraw.Fill(color.x, color.y,color.z);

        //Draw message in the middle of the button
        easyDraw.Text(_message, easyDraw.width / 2 - (_message.Length / 2*fontSize), easyDraw.height/2+ fontSize);
    }

}

