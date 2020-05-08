using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

using GXPEngine;
public class Text : GameObject
{
    EasyDraw easyDraw;

    public string _message = "Test";
    public string Message {
        get { return _message; }
        set {
            _message = value;
            //DrawText();
        }
    }

    public Color3 color = new Color3(255,0,0);
    public string fontFile = "STENCIL.TTF";
    public float textRotation = 0;
    public float fontSize = 20;

    public Text(string message, int x , int y, int width, int height, int rotation) {
        easyDraw = new EasyDraw(width,height);
        this.x = x;
        this.y = y;
        Message = message;
        AddChild(easyDraw);
    }

    public void DrawText() {
        var _foo = new PrivateFontCollection();
        _foo.AddFontFile(fontFile);

        easyDraw.TextFont(new Font((FontFamily)_foo.Families[0], fontSize));
        easyDraw.rotation= textRotation;
        easyDraw.Fill(color.x, color.y,color.z);


        int messsageLength = MessageWidth();
        Console.WriteLine(messsageLength);


        //Draw message in the middle of the button
        easyDraw.Text(_message, easyDraw.width / 2 - (messsageLength / 2*fontSize), easyDraw.height/2+ fontSize);
    }



    public int MessageWidth() {
        
        var lines =_message.Split('\n');

        int longest = lines[0].Length;

        foreach (var obj in lines) {
            if (obj.Length > longest) {
                longest = obj.Length;
            }
        }
        return longest;
    }

}


class TextDialogBox : GameObject {
    EasyDraw easyDraw;

    public string _message = "Test";
    public string Message
    {
        get { return _message; }
        set
        {
            _message = value;
            DrawText();
        }
    }

    public Color3 color = new Color3(255, 0, 0);
    public string fontFile = "STENCIL.TTF";
    public float textRotation = 0;
    public float fontSize = 20;


    public TextDialogBox(string message, int x, int y, int width, int height, int rotation)
    {
        easyDraw = new EasyDraw(width, height);
        this.x = x;
        this.y = y;
        Message = message;
        AddChild(easyDraw);
        //DrawText();
    }



    public void DrawText() {
        var _foo = new PrivateFontCollection();
        _foo.AddFontFile(fontFile);

        easyDraw.TextFont(new Font(_foo.Families[0], fontSize));
        easyDraw.rotation= textRotation;
        easyDraw.Fill(color.x, color.y,color.z);
        easyDraw.Text(_message, 0, 2*fontSize);
    }
}

