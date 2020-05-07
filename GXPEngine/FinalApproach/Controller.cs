using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;
using GXPEngine;


public class Controller:GameObject
{
    public readonly Cursor cursor;

    List<HUDElement> listOfOverlapedObjects = new List<HUDElement>();

    public Controller()
    {
        cursor = new Cursor(60,60,0);
        AddChild(cursor);
    }



    
    void Update()
    {
        cursor.followMouse();
        CheckHoveredButtons();

        if(listOfOverlapedObjects.Count > 0)
        {
            var HudElement = listOfOverlapedObjects[listOfOverlapedObjects.Count - 1];


            HudElement.IsHovered = true;

            if (Input.GetMouseButtonDown(0))
            {
                HudElement.OnClick();
            }
            if (Input.GetMouseButtonUp(0))
            {
                HudElement.OnClickRelease();
                Console.WriteLine("Button Released");
            }
            if (Input.GetMouseButton(0))
            {
                HudElement.OnClickPressed();
            }
        }
        listOfOverlapedObjects = new List<HUDElement>();
    }

    private void CheckHoveredButtons()
    {
        var currentScene = SceneManager.Instance.CurrentLevel;

        if(currentScene == null)
        {
            return;
        }


        
        foreach (var child in currentScene.GetChildren())
        {
            if (child is IInteractable)
            {
                bool objIsHovered = mouseOverButton(child as HUDElement);

                if (objIsHovered)
                {
                    listOfOverlapedObjects.Add(child as HUDElement);
                }
                if (!objIsHovered) {
                    var HudElement = child as HUDElement;
                    HudElement.IsHovered = false;
                }

                
               

                //if (objIsHovered)
                //{
                //    if (Input.GetMouseButtonDown(0))
                //    {
                //        HudElement.OnClick();
                //    }
                //    if (Input.GetMouseButtonUp(0))
                //    {
                //        HudElement.OnClickRelease();
                //    }
                //    if (Input.GetMouseButton(0))
                //    {
                //        HudElement.OnClickPressed();
                //    }
                //}
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


    bool mouseOverButton(HUDElement button, Vec2 scaleCollider)
    {
        int width = button.width;
        int height = button.height;
        int xMin = (int)button.x - (width / 2 * (int)scaleCollider.x);
        int yMin = (int)button.y - (height / 2 * (int)scaleCollider.y);


        if (Input.mouseX > xMin && Input.mouseX < xMin + width)
        {
            if (Input.mouseY > yMin && Input.mouseY < yMin + height)
            {
                return true;
            }
        }
        return false;
    }


 
}