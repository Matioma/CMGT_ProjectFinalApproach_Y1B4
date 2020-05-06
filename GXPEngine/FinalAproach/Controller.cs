using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


public class Controller:GameObject
{
    readonly Cursor cursor;


        
    public Controller()
    {
        cursor = new Cursor(60,60);
        AddChild(cursor);
    }

    void Update() {
        cursor.SetXY(Input.mouseX, Input.mouseY);
    
    }
}