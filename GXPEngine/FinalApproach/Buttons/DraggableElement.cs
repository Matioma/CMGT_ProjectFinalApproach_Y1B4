using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;

class DraggableElement : HUDElement
{
    Vec2 _mapOffset;

    public DraggableElement(string path, int cols, int rows) : base(path, cols, rows){

    }


    void DragImage() {
        Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);
        Vec2 newMapPos =mousePos + _mapOffset;

        SetXY(newMapPos.x, newMapPos.y);
    }




    public override void OnClick()
    {

        Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);
        Vec2 mapPos = new Vec2(x, y);
        _mapOffset = mapPos - mousePos;
    }

    public override void OnClickPressed()
    {
        DragImage();
    }

    public override void OnClickRelease()
    {
        //throw new NotImplementedException();
    }

    public override void OnHover()
    {
        //throw new NotImplementedException();
    }

    public override void OnHoverEnd()
    {
        //throw new NotImplementedException();
    }
}

