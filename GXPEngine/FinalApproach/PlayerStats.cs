using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;

public class PlayerStats
{
    public int Score { get; private set; } = 0;

    Dictionary<PaintingsIdentifiers, MuseumPainting> paintingsSolved;


    public PlayerStats()
    {
        paintingsSolved = new Dictionary<PaintingsIdentifiers, MuseumPainting>();
    }

    public void AddScore(int amount)
    {
        Score += amount;
    }



    public void AddPainting(PaintingsIdentifiers type, MuseumPainting museumPainting) {
        paintingsSolved.Add(type, museumPainting);
    }

    public void PuzzleSolved(PaintingsIdentifiers key)
    {
        paintingsSolved[key].IsAvailable = true;
    }

}
