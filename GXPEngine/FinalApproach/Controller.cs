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
        cursor = new Cursor(60,60,0);
        AddChild(cursor);
    }

    void Update()
    {
        cursor.followMouse();
        CheckHoveredButtons();
    }

    private void CheckHoveredButtons()
    {
        

        var currentScene = SceneManager.Instance.CurrentLevel;

        if(currentScene == null)
        {
            return;
        }
        // Go through Active Children
        foreach (var child in currentScene.GetChildren())
        {
            if (child is IInteractable)
            {
                bool objIsHovered = mouseOverButton(child as HUDElement);
                var button = child as HUDElement;
                button.IsHovered = objIsHovered;


                if (objIsHovered)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        button.OnClick();
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        button.OnClickRelease();
                    }
                    if (Input.GetMouseButton(0))
                    {
                        button.OnClickPressed();
                    }
                }
            }
        }
    }


    //Check if a mouse is over a button
    bool mouseOverButton(HUDElement button) {
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