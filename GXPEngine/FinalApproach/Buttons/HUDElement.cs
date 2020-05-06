using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using GXPEngine;
using TiledMapParser;

abstract class HUDElement : AnimationSprite,IInteractable
{
    bool _isHovered = false;
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


    ///
    public string ButtonTarget{ set; get; }="MainMenu";



    public HUDElement(string path, int cols, int rows) : base(path, cols, rows) {
        SetOrigin(width / 2, height / 2);

        
    }
    public abstract void OnClick();
    public abstract void OnHover();
    public abstract void OnHoverEnd();
    public abstract void OnClickRelease();
    public abstract void OnClickPressed();
}

