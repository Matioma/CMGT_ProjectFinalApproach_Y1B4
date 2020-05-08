using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;
using GXPEngine;


public class Controller:GameObject
{
    public static Controller Instance;



    public readonly Cursor cursor;

    List<HUDElement> listOfHoveredObjects = new List<HUDElement>();

    HUDElement hoveredObject = null;
    public HUDElement interactedElement = null;


    public Controller()
    {
        Instance = this;
        cursor = new Cursor(60,60,0);
        AddChild(cursor);
    }
    void Update()
    {
        cursor.followMouse();
        CheckHoveredHudElements();
        InputToInteractedElement();

        listOfHoveredObjects = new List<HUDElement>(); //Reset the list of hovered
    }

    private void InputToInteractedElement()
    {
        if (listOfHoveredObjects.Count > 0)
        {
            var HudElement = listOfHoveredObjects[listOfHoveredObjects.Count - 1];
            HudElement.IsHovered = true;
            hoveredObject = HudElement;
        }
        if (Input.GetMouseButtonDown(0))
        {

            interactedElement = hoveredObject;
            if (interactedElement != null) {
                
                interactedElement.OnClick();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (interactedElement != null)
            {
                interactedElement.OnClickRelease();
                interactedElement = null;
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (interactedElement != null)
            {
                interactedElement.OnClickPressed();
                
            }
        }
    }

    private void CheckHoveredHudElements()
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
                if (mouseOverButton(child as HUDElement))
                {
                    listOfHoveredObjects.Add(child as HUDElement);
                }else{
                    var HudElement = child as HUDElement;
                    if (HudElement == hoveredObject)
                    {
                        hoveredObject.IsHovered = false;
                        hoveredObject = null;
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