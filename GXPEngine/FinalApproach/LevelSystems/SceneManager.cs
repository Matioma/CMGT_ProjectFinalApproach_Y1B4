using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.FinalApproach;

public class SceneManager : GameObject
{
    delegate void ActionBuilder(Scene scene);


    public static SceneManager _instance;
    public static SceneManager Instance{
        get {
            if (_instance != null)
            {
                return _instance;
            }
            else {
                Console.WriteLine("Scene Manager does not exist");
                return null;
            }        
        }
        set {
            if (_instance == null)
            {
                _instance = value;
            }
            else if( _instance !=value){
                value.LateDestroy();
            }
        }
    }

    //Scene Manager Fields
    List<Scene> scenes=new List<Scene>();


    Dictionary<string,Scene> levels= new Dictionary<string,Scene>();
    public Scene CurrentLevel { get; private set; } = null;


    public SceneManager() {
        Instance = this;
        AddChild(new Controller());

        LoadLevel();
        OpenScene(0);
    }

    public void OpenScene(int index) {
        if (index >= 0 && index < scenes.Count)
        {
            if (CurrentLevel != null)
            {
                RemoveChild(CurrentLevel);
            }
            CurrentLevel = scenes[index];
            AddChildAt(CurrentLevel,0);
        }
        else {
            Console.WriteLine("Index out of bounds when Openning scene in SceneManager");
        }
    }
    public void OpenScene(string key)
    {
        if (CurrentLevel != null)
        {
            RemoveChild(CurrentLevel);
        }
        if (levels.ContainsKey(key))
        {
            CurrentLevel = levels[key];
            AddChildAt(CurrentLevel, 0);
        }
        else {
            Console.WriteLine("Filed to load Key");
        }

       
    }
    void LoadLevel() {
        CreateMainMenu();
        CreateGallery();
        CreateLevel1();
    }
    void CreateMainMenu() {
        AddLevel("MainMenu",
           (sceneRef) => {
               var button = new Button("art/Button.jpg", 2, 1,
                  () =>
                  {
                      Instance.OpenScene("VisitVanGogh");
                  });
               button.AddText("Visit");
               button.SetXY(250, 120);
               sceneRef.AddChild(button);



               button = new Button("art/Button.jpg", 2, 1,
                    () =>
                    {
                        Instance.OpenScene("Gallery");
                    });
               button.AddText("Galary");
               button.SetXY(250, 320);
               sceneRef.AddChild(button);



               button = new Button("art/Button.jpg", 2, 1,
                   () =>
                   {
                       System.Diagnostics.Process.Start("https://www.vangoghmuseum.nl/"); //Opens the Link
                });
               button.AddText("Museum");
               button.SetXY(250, 520);
               sceneRef.AddChild(button);
           });
    }
    void CreateGallery()
    {
        AddLevel("Gallery",
           (sceneRef) => {
               var draggableElement = new DraggableElement("art/MuseumMap.jpg", 1, 1);
               draggableElement.width *= 2;
               draggableElement.height *= 2;
               sceneRef.AddChild(draggableElement);

               var button = new Button("art/Button.jpg", 2, 1,
                   () => {
                       Instance.OpenScene("MainMenu");
                   });

               button.SetXY(250, 120);
               sceneRef.AddChild(button);
           });
    }
    void CreateLevel1()
    {
        AddLevel("VisitVanGogh",
            (sceneRef) => {
                var backgroundsample = new AnimationSprite("art/backgroundsample.png", 1, 1);
                backgroundsample.SetOrigin(backgroundsample.width / 2, backgroundsample.height / 2);
                backgroundsample.SetXY(game.width / 2, game.height / 2);
                sceneRef.AddChild(backgroundsample);

                var character = new Character("art/vangoghtalking.png", 3, 1);
                character.SetXY(200, 400);
                sceneRef.AddChild(character);

                var foreGround = new AnimationSprite("art/ForeGround.png", 1, 1);
                foreGround.SetOrigin(backgroundsample.width / 2, backgroundsample.height / 2);
                foreGround.SetXY(game.width / 2, game.height / 2);
                sceneRef.AddChild(foreGround);



                var button = new Button("art/Button.jpg", 2, 1,
                    () => {
                        Instance.OpenScene("MainMenu");
                    });
                button.SetXY(250, game.height - 120);
                sceneRef.AddChild(button);
        });
    }

    void AddLevel(string Name , ActionBuilder sceneDataActions) {
        Scene scene = new Scene();
        sceneDataActions.Invoke(scene);

        levels.Add(Name, scene);
        scenes.Add(scene);
    }



}

