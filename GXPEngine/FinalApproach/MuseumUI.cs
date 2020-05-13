using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
public class MuseumUI : GameObject
{
    public MuseumUI() { 

    }


    public void RemoveChildren() {
        foreach (var obj in GetChildren()) {
            obj.LateRemove();
        }
    }
}
