using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;




class Button : HUDElement
{

    public Button(string path, int cols, int rows) : base(path, cols, rows)
    {
        //AddText("Random");
    }


    public void AddText(string text) {
        var textMassage = new Text(text, (int)x - width / 2, (int)y - height / 2, width, height, 0);
        AddChild(textMassage);
    }



    /// <summary>
    /// Button Constructor
    /// </summary>
    /// <param name="path">SpriteSheet path </param>
    /// <param name="cols">number of columns</param>
    /// <param name="rows">number of rows</param>
    /// <param name="onClickAction">Actions should take place</param>
    public Button(string path, int cols, int rows, Action onClickAction) : this(path, cols, rows)
    {
        this.onClickAction += onClickAction;
    }

    public override void OnClick() {
        if (onClickAction != null)
        {
            onClickAction.Invoke();
        }
        else {
            Console.WriteLine("Default onClick action");
        }
    }
    public override void OnHover() {
        SetFrame(1);
    }
    public override void OnHoverEnd() {
        SetFrame(0);
    }
    public override void OnClickRelease()
    {
        //SceneManager.Instance.OpenScene(ButtonTarget);
    }
    public override void OnClickPressed()
    {
        //TO DO
    }
}

