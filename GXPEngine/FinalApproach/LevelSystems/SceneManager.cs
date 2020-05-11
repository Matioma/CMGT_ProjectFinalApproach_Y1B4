using GXPEngine;
using GXPEngine.FinalApproach;
using System;
using System.Collections.Generic;

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

    private  Controller controller;


    Random random = new Random();
    public SceneManager() {
        Instance = this;
        controller = new Controller();
        AddChild(controller);

        LoadLevel();
        OpenScene("MainMenu");
    }


    /// <summary>
    /// Opens the level
    /// </summary>
    /// <param name="index"></param>
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



    /// <summary>
    /// Creates the scenes
    /// </summary>
    void LoadLevel()
    {
        CreateMainMenu();

        AddLevel("Scene1",
            (sceneRef) => {
                
                var imageLayer = new AnimationSprite("art/Background1.png", 1, 1);
                imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                imageLayer.SetXY(game.width / 2, game.height / 2);
                sceneRef.AddChild(imageLayer);

                imageLayer = new AnimationSprite("art/paintStand.png", 1, 1);
                imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                imageLayer.SetXY(300, 450);
                sceneRef.AddChild(imageLayer);

                var character = new Character("art/vangoghpainting.png", 6, 1);
                character.SetXY(300, 450);
                sceneRef.AddChild(character);

              

                imageLayer = new AnimationSprite("art/ForeGround1.png", 1, 1);
                imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                imageLayer.SetXY(game.width / 2, game.height / 2);
                sceneRef.AddChild(imageLayer);

                var button = new Button("art/transparent.png", 1, 1,
                    () =>
                    {
                        Instance.OpenScene("VisitVanGogh");
                    });
                button.width=120;
                button.height=200;
            
                button.SetXY(350, game.height - 250);
                sceneRef.AddChild(button);
            });




        CreateGallery();
        CreateLevel1();
        CreatePuzzleLevel();
    }

    

    private void CreatePuzzleLevel()
    {
        AddLevel("Puzzle", (sceneRef) =>
        {
            var puzzleGame = Puzzle.Create(sceneRef,"Art/Puzzles/puzzle1_start/", 3,2, new Vec2(20,153));
            puzzleGame.OnPuzzleSolved = () =>
            {
                var button = new Button("art/Button.jpg", 2, 1,
                 () =>
                 {
                     Instance.OpenScene("Gallery");
                 });
                button.CreateText("Gallery");
                button.SetupText(() =>
                {
                    button.textobject.fontSize = 15;
                    button.textobject.color = new Color3(0, 255, 0);
                    button.textobject.textRotation = 0;
                });
                button.SetXY(game.width - 120, game.height - 60);
                sceneRef.AddChild(button);
                //Instance.OpenScene("Gallery");
            };
            AddChild(puzzleGame);
        });
    }

    void CreateMainMenu() {
        AddLevel("MainMenu",
           (sceneRef) => {
               
               
               var backgroundImage = new AnimatedImage();
               backgroundImage.AddAnimation("Art/MainMenuAnimation/Top/frame", ".png", 18, "top");
               backgroundImage.AddAnimation("Art/MainMenuAnimation/Bottom/frame", ".png", 16, "bottom");
               backgroundImage.AddAnimation("Art/MainMenuAnimation/Middle/frame", ".png", 18, "middle");
               backgroundImage.PlayAnimation("middle");
               sceneRef.AddChild(backgroundImage);

               var imageLayer = new Sprite("Art/Menu text.png");
               sceneRef.AddChild(imageLayer);

               var button = new Button("art/transparent.png", 1, 1,
                  () =>
                  {
                      Instance.OpenScene("Scene1");
                  });
               button.onHoverAction = () => {
                   foreach (var obj in Instance.CurrentLevel.GetChildren()) {
                       if (obj is AnimatedImage) {
                           var background =obj as AnimatedImage;
                           background.PlayAnimation("top");
                       }
                   }
               };
               button.SetXY(310, 150);
               button.width = 280;
               button.height = 120;
               sceneRef.AddChild(button);



               button = new Button("art/transparent.png", 1, 1,
                    () =>
                    {
                        Instance.OpenScene("Gallery");
                    });

               button.onHoverAction = () => {
                   foreach (var obj in Instance.CurrentLevel.GetChildren())
                   {
                       if (obj is AnimatedImage)
                       {
                           var background = obj as AnimatedImage;
                           background.PlayAnimation("middle");
                       }
                   }
               };
               button.SetXY(350, 330);
               button.width = 280;
               button.height = 120;
               sceneRef.AddChild(button);



               button = new Button("art/transparent.png", 1, 1,
                   () =>
                   {
                       System.Diagnostics.Process.Start("https://www.vangoghmuseum.nl/"); //Opens the Link
                });

               button.onHoverAction = () => {
                   foreach (var obj in Instance.CurrentLevel.GetChildren())
                   {
                       if (obj is AnimatedImage)
                       {
                           var background = obj as AnimatedImage;
                           background.PlayAnimation("bottom");
                       }
                   }
               };
               button.SetXY(310, 490);
               button.width = 280;
               button.height = 120;
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
               button.CreateText("Random");
               button.SetXY(250, 120);
               sceneRef.AddChild(button);
           });
    }
    void CreateLevel1()
    {
        AddLevel("VisitVanGogh",
            (sceneRef) => {
                var backgroundsample = new AnimationSprite("art/Background1.png", 1, 1);
                backgroundsample.SetOrigin(backgroundsample.width / 2, backgroundsample.height / 2);
                backgroundsample.SetXY(game.width / 2, game.height / 2);
                sceneRef.AddChild(backgroundsample);

                var imageLayer = new AnimationSprite("art/paintStand.png", 1, 1);
                imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                imageLayer.SetXY(300, 450);
                sceneRef.AddChild(imageLayer);

                var character = new Character("art/vangoghtalking.png", 3, 1);
                character.SetXY(200, 400);
                sceneRef.AddChild(character);

                var foreGround = new AnimationSprite("art/ForeGround1.png", 1, 1);
                foreGround.SetOrigin(backgroundsample.width / 2, backgroundsample.height / 2);
                foreGround.SetXY(game.width / 2, game.height / 2);
                sceneRef.AddChild(foreGround);


                var dialogBox = new TextDialogBox(" \n test \n test2 \n test2 \n test4", 500, 300, 300, 600);
                sceneRef.AddChild(dialogBox);         
    
                var textBox = new Button("art/transparent.png", 1, 1,
                () => {});
                //textBox.CreateText("I have asdae \n test asdfasr sda \n sdf aer sdf ae \n test srseffa fase \n asdfaesf\n asdf sdfasdf\n");
                //textBox.SetupText(() => {
                //    textBox.textobject.fontSize = 10;
                //    textBox.textobject.color = new Color3(0, 255, 0);
                //    textBox.textobject.textRotation = 0;
                //});

                textBox.SetXY(650, game.height - 450);
                sceneRef.AddChild(textBox);



                var button = new Button("art/Button.jpg", 2, 1,
                    () => {
                        Instance.OpenScene("Puzzle");
                    });
                button.CreateText("Go");
                button.SetupText(() => {
                    button.textobject.fontSize = 15;
                    button.textobject.color = new Color3(0, 255, 0);
                    button.textobject.textRotation = 0;
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

