using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;
using GXPEngine;


public class Controller:GameObject
{
    readonly Cursor cursor;


        
    public Controller()
    {
        cursor = new Cursor(60,60);
        AddChild(cursor);
    }

    void Update()
    {
        cursor.followMouse();
        CheckHoveredButtons();
    }

    private void CheckHoveredButtons()
    {
        foreach (var child in parent.GetChildren())
        {
            if (child is IButton)
            {
                bool objIsHovered = mouseOverButton(child as Button);
                var button = child as Button;
                button.IsHovered = objIsHovered;


                if (objIsHovered)
                {
                    if (Input.GetMouseButton(0))
                    {
                        button.OnClick();
                    }
                }
            }
        }
    }


    //Check if a mouse is over a button
    bool mouseOverButton(Button button) {
        int width = button.width;
        int height = button.height;
        int xMin = (int)button.x - width / 2;
        int yMin = (int)button.y - height / 2;


        if (Input.mouseX > xMin && Input.mouseX < xMin+width) {
            if (Input.mouseY > yMin && Input.mouseY < yMin + height) {
                return true;
            }
        }
        return false;
        
    
    
    
    }





}