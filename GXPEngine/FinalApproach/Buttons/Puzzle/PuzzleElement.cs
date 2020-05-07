﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;

class PuzzleElement:DraggableElement
{
    float snapDistance = 50;
    bool onRightPosition=false;


    public PuzzleElement(string file, int cols, int rows) : base(file, cols, rows) {
        SetOrigin(width / 2, height / 2);
    }



    public Puzzle FindPuzzleObject() {
        foreach (var obj in SceneManager.Instance.GetChildren()) {
            if (obj is Puzzle) {
                return obj as Puzzle;
            }
        }
        return null;
    }


    public override void OnClick()
    {
        if (!onRightPosition)
        {
            base.OnClick();
        }
        else { 
        
        
        }
        
    }

    public override void OnClickPressed()
    {
        if (!onRightPosition)
        {
            base.OnClickPressed();
        }
    }

    public override void OnClickRelease()
    {
        if (!onRightPosition)
        {
            base.OnClickRelease();

            Vec2 puzzleElementPoz = new Vec2(x, y);

            Vec2 targetPosition = FindPuzzleObject().puzzleRelationships[this];
            if ((puzzleElementPoz - targetPosition).Length() <= snapDistance)
            {
                SetXY(targetPosition.x, targetPosition.y);
                onRightPosition = true;
            }
        }
    }

    public override void OnHover()
    {
        base.OnHover();
    }

    public override void OnHoverEnd()
    {
        base.OnHoverEnd();
    }
}
