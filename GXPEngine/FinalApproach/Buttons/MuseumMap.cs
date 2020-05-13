using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
    class MuseumMap:DraggableElement
    {
        public static MuseumMap Instance;
        Dictionary<MuseumPainting, Vec2> paintings;

        Vec2 mapPos {
            get {
                return new Vec2(x, y);
            }
        }  
            
        public MuseumMap(string background, int cols, int rows) : base(background, cols, rows) {
            Instance = this;
            paintings = new Dictionary<MuseumPainting, Vec2>();    
        }

        void Update()
        {
            base.Update();
            foreach (var pair in paintings)
            {
                Vec2 newPos = pair.Value + mapPos;
                pair.Key.SetXY(newPos.x, newPos.y);
            }
        }

        public void AddPainting(MuseumPainting painting) {
            Vec2 paintingPos = new Vec2(painting.x, painting.y);
            Vec2 thisPos = new Vec2(x, y);
            paintings.Add(painting, paintingPos-thisPos);
        }
}
