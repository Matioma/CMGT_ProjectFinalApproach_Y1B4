using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.FinalApproach
{
    class Character:AnimationSprite
    {
        float animationTime=0.167f;

        float _timer = 0;



        public Character(string file, int cols, int rows) : base(file, cols, rows) {
            SetOrigin(width/2, height/2);
        }
        void Update() {
            _timer += (float)Time.deltaTime/1000;

            if (_timer >= animationTime) {
                NextFrame();
                _timer = 0;
            }     
        }
    }
}
