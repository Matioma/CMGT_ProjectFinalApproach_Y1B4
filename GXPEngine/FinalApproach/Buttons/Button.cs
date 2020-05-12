using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;


public class Button : HUDElement
{



    public Button(string path, int cols, int rows) : base(path, cols, rows)
    {
    }


    /// <summary>
    /// Button Constructor
    /// </summary>
    /// <param name="path">SpriteSheet path </param>
    /// <param name="cols">number of columns</param>
    /// <param name="rows">number of rows</param>
    /// <param name="onClickAction">Actions should take place</param>
    public Button(string path, int cols, int rows, Action onClickAction=null) : this(path, cols, rows)
    {
        this.onClickAction += onClickAction;
    }

    public override void OnClick() {
      
    }
    public override void OnHover() {
        if (onHoverAction != null) {
            onHoverAction.Invoke();
        }
        SetFrame(1);
    }
    public override void OnHoverEnd() {
        if (onHoverEndAction != null)
        {
            onHoverEndAction.Invoke();
        }
        SetFrame(0);
    }
    public override void OnClickRelease()
    {
        if (AudioManager.Instance.NarratorSoundChannel != null) { 
            if (AudioManager.Instance.NarratorSoundChannel.IsPlaying)
            {
                return;
            }
        }

        if (onClickAction != null)
        {
            onClickAction.Invoke();
        }
        else
        {
            Console.WriteLine("Default onClick action");
        }
        //SceneManager.Instance.OpenScene(ButtonTarget);
    }
    public override void OnClickPressed()
    {
        //TO DO
    }
}

