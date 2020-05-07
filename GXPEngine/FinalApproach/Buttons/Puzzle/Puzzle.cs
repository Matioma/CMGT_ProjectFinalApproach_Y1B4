using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class Puzzle:GameObject
{

    public Dictionary<DraggableElement, Vec2> puzzleRelationships;



    public Action OnPuzzleSolved=null;
    public Puzzle(Dictionary<DraggableElement, Vec2> puzzleRelationships) {
        this.puzzleRelationships = puzzleRelationships;
    }

    public bool isSolved() {
        bool puzzleIsSolved = true;
        foreach (var element in puzzleRelationships) {
            var puzzleElement = element.Key as PuzzleElement;
            if (puzzleElement == null){
                Console.WriteLine("PuzzleELement does not exist");
                return false;
            }
            if (!puzzleElement.onRightPosition) {
                return false;
            }
        }
        return puzzleIsSolved;
        

    }

}
    