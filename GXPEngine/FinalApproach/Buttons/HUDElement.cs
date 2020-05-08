using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using GXPEngine;
using TiledMapParser;


public delegate void Action();

public abstract class HUDElement : AnimationSprite,IInteractable
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

    public Text textobject;
   // public TextDialogBox textDialogBox;

    public void CreateText(string text)
    {
        textobject = new Text(text, (int)x - width / 2, (int)y - height / 2, width, height, 0);
        AddChild(textobject);
    }
    public void SetupText(Action textConfigurarion)
    {
        if (textobject == null) {
            Console.WriteLine("Text object does not exist");
        }
        textConfigurarion.Invoke();
        textobject.DrawText();
    }


 /*   public void CreateDialogBox(string text)
    {
        textobject = new Text(text, (int)x - width / 2, (int)y - height / 2, width, height, 0);
        AddChild(textobject);
    }
*/

    protected Action onClickAction;

    public HUDElement(string path, int cols, int rows) : base(path, cols, rows) {
        SetOrigin(width / 2, height / 2);
    }
    public abstract void OnClick();
    public abstract void OnHover();
    public abstract void OnHoverEnd();
    public abstract void OnClickRelease();
    public abstract void OnClickPressed();
}

