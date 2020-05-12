using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;

public class PlayerStats
{
    public int Score { get; private set; } = 0;
    
    public PlayerStats()
    {
    }

    public void AddScore(int amount)
    {
        Score += amount;
    }
}
