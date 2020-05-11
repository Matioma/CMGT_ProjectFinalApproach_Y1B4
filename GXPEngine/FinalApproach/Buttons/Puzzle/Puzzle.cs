﻿using System;
using System.Collections.Generic;
using System.Dynamic;
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

    //Creates a puzzle on a specific scene
    public static Puzzle Create(Scene pTargetScene, string path, int cols, int rows, Vec2 startPosition) {
        Random random = new Random();


        var background = new Sprite(path + "background.png");
        pTargetScene.AddChild(background);

        //Build the puzzle
        var dictionary = new Dictionary<DraggableElement, Vec2>();
        List<PuzzleElement> puzzleElements = new List<PuzzleElement>();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                string fileName = path + "puzzle to put.png";

                var test = new PuzzleElement(fileName, cols, rows);
                test.SetFrame(i * cols + j);
                puzzleElements.Add(test);

                Vec2 realPosition = new Vec2();

                realPosition.x = startPosition.x + test.width / 2 + test.width * j;
                realPosition.y = startPosition.y + test.height / 2 + test.height * i;


                //test.SetXY(realPosition.x, realPosition.y);
                test.SetXY(random.Next((int)startPosition.x, 1024 - (int)startPosition.x), random.Next((int)startPosition.y, 768 - (int)startPosition.y));
                dictionary.Add(test, realPosition);

                pTargetScene.AddChild(test);
            }
        }
        var puzzleGame = new Puzzle(dictionary);
        return puzzleGame;
    }

}
    