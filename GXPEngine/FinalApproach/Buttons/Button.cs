using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using GXPEngine;
using TiledMapParser;

abstract class Button : AnimationSprite,IInteractable
{
    bool _isHovered=false;

    public bool IsHovered {
        get { return _isHovered; }
        set { 
            //Check if state changeds
            if (value != _isHovered) {
                _isHovered = value;
                if (_isHovered)
                {
                    OnHover();
                }
                else {
                    OnHoverEnd();
                }
            } 
        }
    }



    public Button(string path, int cols, int rows) : base(path, cols, rows) {
        SetOrigin(width / 2, height / 2);

        var textObject = new Text("Test", (int)x - width / 2, (int)y - height / 2, width, height, 0);
        AddChild(textObject);
        
    }


    public void Update()
    {
    }
    public virtual void OnClick()
    {
        throw new NotImplementedException();
    }

    public virtual void OnHover()
    {
        throw new NotImplementedException();
    }

    public virtual void OnHoverEnd()
    {
        throw new NotImplementedException();
    }

    public virtual void OnClickRelease()
    {
        throw new NotImplementedException();
    }

    public virtual void OnClickPressed()
    {
        throw new NotImplementedException();
    }
}

