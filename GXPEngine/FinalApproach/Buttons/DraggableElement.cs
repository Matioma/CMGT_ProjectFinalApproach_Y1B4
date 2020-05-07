using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;

class DraggableElement : HUDElement
{
    Vec2 _mapOffset;
    bool IsClicked = false; 




    public DraggableElement(string path, int cols, int rows) : base(path, cols, rows){

    }

    public void Update() {
        if(IsClicked)
            DragImage();
    }




    public void DragImage() {
        Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);
        Vec2 newMapPos =mousePos + _mapOffset;

        SetXY(newMapPos.x, newMapPos.y);
    }

    public override void OnClick()
    {
        //Compute the painting offset from mouse
        Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);
        Vec2 mapPos = new Vec2(x, y);
        _mapOffset = mapPos - mousePos;

        IsClicked = true;
    }

    public override void OnClickPressed()
    {
    }

    public override void OnClickRelease()
    {
        IsClicked = false;
    }

    public override void OnHover()
    {
    }

    public override void OnHoverEnd()
    {
    }
}

