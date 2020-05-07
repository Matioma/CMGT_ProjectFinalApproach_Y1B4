using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class Puzzle:GameObject
{
    AnimationSprite Background;

    public Dictionary<DraggableElement, Vec2> puzzleRelationships;


    public Puzzle(Dictionary<DraggableElement, Vec2> puzzleRelationships) {
        this.puzzleRelationships = puzzleRelationships;
    }

}
    