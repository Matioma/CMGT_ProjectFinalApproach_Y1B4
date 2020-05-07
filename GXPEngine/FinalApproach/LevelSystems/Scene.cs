using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.FinalApproach;

public class Scene:GameObject
{
    public Scene() {
        BuildLevel0();
    }

    public Scene(int i) {
        switch (i) {
            case 0:
                BuildLevel0();
                break;
            case 1:
                BuildLevel1();
                break;
            case 2:
                BuildLevel2();
                break;

        }
    }

    void BuildLevel0() {
        var button = new Button("art/Button.jpg", 2, 1, 
            ()=> {
                SceneManager.Instance.OpenScene("Level1"); 
            });
        button.AddText("Visit");
        button.SetXY(250, 120);
        AddChild(button);


        button = new Button("art/Button.jpg", 2, 1,
             () => {
                 SceneManager.Instance.OpenScene("Level2");
             });
        button.AddText("Galary");
        button.SetXY(250, 320);
        AddChild(button);


        button = new Button("art/Button.jpg", 2, 1,
            () => {
                System.Diagnostics.Process.Start("https://www.vangoghmuseum.nl/");
            });
        button.AddText("Museum");
        button.SetXY(250, 520);
        AddChild(button);
    }



    void BuildLevel1()
    {
        var backgroundsample = new AnimationSprite("art/backgroundsample.jpg", 1, 1);
        backgroundsample.SetOrigin(backgroundsample.width / 2, backgroundsample.height / 2);
        backgroundsample.SetXY(game.width / 2, game.height / 2);
        AddChild(backgroundsample);




        var character = new Character("art/vangoghtalking.png", 3,1);
        character.SetXY(200,400);
        
        AddChild(character);



        var button = new Button("art/Button.jpg", 2, 1,
            ()=> {
                SceneManager.Instance.OpenScene("MainMenu");
            });
        button.SetXY(250, game.height-120);
        AddChild(button);
    }


    void BuildLevel2()
    {
        var draggableElement = new DraggableElement("art/MuseumMap.jpg", 1, 1);
        draggableElement.width *= 2;
        draggableElement.height *= 2;
        AddChild(draggableElement);

        var button = new Button("art/Button.jpg", 2, 1,
            ()=> {
                SceneManager.Instance.OpenScene("MainMenu");
            });

        button.SetXY(250, 120);
        AddChild(button);
    }
}

