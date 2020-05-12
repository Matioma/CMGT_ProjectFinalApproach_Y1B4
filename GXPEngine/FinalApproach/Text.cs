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


public class TextDialogBox : GameObject {
    EasyDraw easyDraw;

    public string fontFile = "LEVIBRUSH.TTF";
    public string Message = "Test";
    public Color3 color = new Color3(255, 0, 0);
    
    public float fontSize = 20;

    public Vec2 TextOffset = new Vec2();


    public TextDialogBox(string message, int x, int y, int width, int height)
    {
        easyDraw = new EasyDraw(width, height);
        this.x = x;
        this.y = y;
        Message = message;
        AddChild(easyDraw);
        //DrawText();
    }
    public TextDialogBox(int x, int y, int width, int height)
    {
        easyDraw = new EasyDraw(width, height);
        this.x = x;
        this.y = y;
        AddChild(easyDraw);
    }


    private int numberOfLines()
    {
        var lines = Message.Split('\n');
        return lines.Length;
    }
    public void DrawText() {
        var _foo = new PrivateFontCollection();
        _foo.AddFontFile(fontFile);

        easyDraw.TextFont(new Font(_foo.Families[0], 15));
        easyDraw.Fill(color.x, color.y,color.z);
        easyDraw.Text(Message, 2*fontSize+TextOffset.x, (numberOfLines()+1)*2 *fontSize+2+ TextOffset.y);
    }
}

