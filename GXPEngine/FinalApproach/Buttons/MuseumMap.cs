using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
    class MuseumMap:DraggableElement
    {
        public MuseumMap(string background, int cols, int rows) : base(background, cols, rows) {
        }


        public void AddPainting(MuseumPainting painting) {
            AddChild(painting);
        }
    }
