using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;
using GXPEngine;




public class Controller:GameObject
{
    public static Controller Instance;


    public PlayerStats stats;
    public readonly Cursor cursor;

    List<HUDElement> listOfHoveredObjects = new List<HUDElement>();

    HUDElement hoveredObject = null;
    public HUDElement interactedElement = null;


    Cursor currentCursor;
    Cursor brushCursor;
    Cursor handCursor;


    public Controller()
    {
        Instance = this;
        stats = new PlayerStats();
        addCursors();
    }

    public void SetCursor(CursorType cursorType)
    {
        switch (cursorType)
        {
            case CursorType.HAND:
                brushCursor.alpha = 0;
                handCursor.alpha = 1;
                currentCursor = handCursor;
                break;
            case CursorType.BRUSH:
                handCursor.alpha = 0;
                brushCursor.alpha = 1;
                currentCursor = brushCursor;
                break;
            default:
                Console.WriteLine("unkown brush set in Controller in SetCursor methods");
                break;

        }

    }

    void addCursors() {
        brushCursor = new Cursor("Art/Brush_cursor.png", 2, 1);
        brushCursor.SetCursor(0);
        AddChild(brushCursor);
        currentCursor = brushCursor;
        handCursor = new Cursor("Art/HandCursor.png", 1, 2);
        handCursor.SetCursor(1);
        handCursor.alpha = 0;
        AddChild(handCursor);
    }
    public void SwitchCursor() {
        if (currentCursor == brushCursor)
        {
            currentCursor.alpha = 0;
            currentCursor = handCursor;
            currentCursor.alpha = 1;
            
        }
        else
        {
            currentCursor.alpha = 0;
            currentCursor = brushCursor;
            currentCursor.alpha = 1;
        }
    }

    void Update()
    {
        currentCursor.followMouse();
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
            currentCursor.SetCursor(1);
            interactedElement = hoveredObject;
            if (interactedElement != null) {
                
                interactedElement.OnClick();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            currentCursor.SetCursor(0);
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