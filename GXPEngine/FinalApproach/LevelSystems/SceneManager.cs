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

        AudioManager audioManager = new AudioManager();
        //AddChild(audioManager);

        LoadLevel();
        OpenScene("Puzzle");
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
        CreateGallery();

        Scene1();
        Scene2();
        CreatePuzzle1Level();
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


               //Setup Start game button
               var button = new Button("art/transparent.png", 1, 1,
                  () =>
                  {
                      Instance.OpenScene("Scene1");
                      Controller.Instance.SetCursor(CursorType.HAND);
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




               //Setup gallary button
               button = new Button("art/transparent.png", 1, 1,
                    () =>
                    {
                        Instance.OpenScene("Gallery");
                        Controller.Instance.SetCursor(CursorType.HAND);
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




               // Setup museum button
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
               var draggableElement = new DraggableElement("art/MuseumMap.png", 1, 1);
               draggableElement.width *= 2;
               draggableElement.height *= 2;
               sceneRef.AddChild(draggableElement);

               var button = new Button("art/BackButton.png", 1, 1,
                   () => {
                       Instance.OpenScene("MainMenu");
                       Controller.Instance.SetCursor(CursorType.BRUSH);
                       //Controller.Instance.SwitchCursor();
                   });
               button.CreateText("Random");
               button.SetXY(button.width/2-30, button.height/2-30);
               sceneRef.AddChild(button);
           });
    }
    private void Scene1()
    {
        AddLevel("Scene1",
            (sceneRef) =>
            {
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
                        Instance.OpenScene("Scene2");
                        AudioManager.Instance.PlaySound("VoiceoverLines/scene 1");
                    });
                button.width = 120;
                button.height = 200;
                button.SetXY(350, game.height - 250);
                sceneRef.AddChild(button);




                //UI Buttons
                button = new Button("art/BackButton.png", 1, 1,
                   () => {
                       Instance.OpenScene("MainMenu");
                       Controller.Instance.SetCursor(CursorType.HAND);
                       //Controller.Instance.SwitchCursor();
                   });
                button.SetXY(button.width / 2 - 30, button.height / 2 - 30);
                sceneRef.AddChild(button);


                button = new Button("art/Gallery.png", 1, 1,
                   () => {
                       Instance.OpenScene("Gallery");
                       Controller.Instance.SetCursor(CursorType.HAND);
                       //Controller.Instance.SwitchCursor();
                   });
                button.SetScaleXY(0.75f, 0.75f);
                button.SetXY(button.width / 2 - 15, button.height);
                sceneRef.AddChild(button);



                //var textBox = new TextBox(new Vec2(280,120),300,300);
                //textBox.Configure(() =>
                //{
                //    textBox.dialogBox.Message = "Here we go the first \nmessage that has to be\nwrapp around the dialog\nbox";
                //    textBox.dialogBox.fontSize = 10;
                //    textBox.dialogBox.color = new Color3(0, 0, 0);
                //});
                //textBox.EndConfigure();
                //sceneRef.AddChild(textBox);

            });
    }
    private void Scene2()
    {
        AddLevel("Scene2",
            (sceneRef) => {
                Sound sound = new Sound("Audio/VoiceoverLines/scene 1.wav");

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



                //Van gogh text
                var button = new Button("art/Dialog Background.png", 1, 1,
                   () => {
                       //Instance.OpenScene("Puzzle");
                       //Controller.Instance.SetCursor(CursorType.BRUSH);
                       //AudioManager.Instance.PlaySound("VoiceoverLines/scene 2");

                   });
                button.SetScaleXY(0.8f, 0.8f);
                button.SetXY(game.width - button.width / 2 - 20, game.height - 190);
                sceneRef.AddChild(button);

                var dialogBox = new TextBox(button,true);
                dialogBox.y += 20;
                dialogBox.x += 50;
                dialogBox.Configure(() =>
                {
                    dialogBox.dialogBox.Message = "Oh, I was waiting for you! Where do you want me to start?";
                    dialogBox.dialogBox.fontSize = 10;
                    dialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                dialogBox.EndConfigure();
                sceneRef.AddChild(dialogBox);





                //Response
                button = new Button("art/Dialog Background.png", 1, 1,
                    () => {
                        Instance.OpenScene("Puzzle");
                        Controller.Instance.SetCursor(CursorType.BRUSH);
                        AudioManager.Instance.PlaySound("VoiceoverLines/scene 2");
                        
                    });
                button.SetScaleXY(0.5f, 0.5f);
                button.SetXY(game.width-button.width/2-30, game.height - 70);
                sceneRef.AddChild(button);

                dialogBox = new TextBox(button,true);
                dialogBox.y += 10;
                dialogBox.x += 50;
                dialogBox.Configure(() =>
                {
                    dialogBox.dialogBox.Message = "What are you drawing there? ";
                    dialogBox.dialogBox.fontSize = 10;
                    dialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                dialogBox.EndConfigure();
                sceneRef.AddChild(dialogBox);


                //UI Buttons
                button = new Button("art/BackButton.png", 1, 1,
                   () => {
                       Instance.OpenScene("MainMenu");
                       Controller.Instance.SetCursor(CursorType.BRUSH);
                       //Controller.Instance.SwitchCursor();
                   });
                button.CreateText("Random");
                button.SetXY(button.width / 2 - 30, button.height / 2 - 30);
                sceneRef.AddChild(button);


                button = new Button("art/Gallery.png", 1, 1,
                   () => {
                       Instance.OpenScene("Gallery");
                       Controller.Instance.SetCursor(CursorType.HAND);
                       //Controller.Instance.SwitchCursor();
                   });
                button.SetScaleXY(0.75f, 0.75f);
                button.SetXY(button.width / 2 - 15, button.height);
                sceneRef.AddChild(button);

            });
    }
    private void CreatePuzzle1Level()
    {
        AddLevel("Puzzle", (sceneRef) =>
        {
            var puzzleGame = Puzzle.Create(sceneRef, "Art/Puzzles/puzzle1_start/", 2, 2, new Vec2(20, 153));
            puzzleGame.OnPuzzleSolved = () =>
            {
                AudioManager.Instance.PlaySound("VoiceoverLines/scene 3");

                var button = new Button("art/Dialog Background.png", 1, 1,
                   () => {
                       Instance.OpenScene("Gallery");
                       Controller.Instance.SetCursor(CursorType.HAND);

                   });
                button.SetScaleXY(0.5f, 0.5f);
                button.SetXY(game.width - 120, game.height - 60);
                sceneRef.AddChild(button);

                var dialogBox = new TextBox(button);
                dialogBox.Configure(() =>
                {
                    dialogBox.dialogBox.Message = "What are you \ndrawing there? ";
                    dialogBox.dialogBox.fontSize = 10;
                    dialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                dialogBox.EndConfigure();
                sceneRef.AddChild(dialogBox);



             
            };


            //UI Buttons
            var UIButton = new Button("art/BackButton.png", 1, 1,
               () => {
                   Instance.OpenScene("MainMenu");
                   Controller.Instance.SetCursor(CursorType.BRUSH);
                   });
            UIButton.CreateText("Random");
            UIButton.SetXY(UIButton.width / 2 - 30, UIButton.height / 2 - 30);
            sceneRef.AddChild(UIButton);


            UIButton = new Button("art/Gallery.png", 1, 1,
               () => {
                   Instance.OpenScene("Gallery");
                   Controller.Instance.SetCursor(CursorType.HAND);
                   });
            UIButton.SetScaleXY(0.75f, 0.75f);
            UIButton.SetXY(UIButton.width+20, UIButton.height / 2 - 23);
            sceneRef.AddChild(UIButton);

            //
            UIButton = new Button("art/SunFlowerBG.png", 1, 1,
               () => {
               });
            UIButton.SetScaleXY(0.75f, 0.75f);
            UIButton.SetXY(game.width -UIButton.width/2, UIButton.height / 2 - 23);
            sceneRef.AddChild(UIButton);

            var flowerCount = new TextBox(UIButton);
            flowerCount.y += UIButton.height/2;
            flowerCount.Configure(() =>
            {
                flowerCount.dialogBox.Message = "145";
                flowerCount.dialogBox.fontSize = 10;
                flowerCount.dialogBox.color = new Color3(255, 255, 255);
            });
            flowerCount.EndConfigure();
            sceneRef.AddChild(flowerCount);




            //
            UIButton = new Button("art/SunFlower.png", 1, 1,
               () => {
               });
            UIButton.SetScaleXY(0.75f, 0.75f);
            UIButton.SetXY(game.width - UIButton.width / 2 -50, UIButton.height / 2 +20);
            sceneRef.AddChild(UIButton);



            sceneRef.AddChild(puzzleGame);
        });
    }
    void AddLevel(string Name , ActionBuilder sceneDataActions) {
        Scene scene = new Scene();
        sceneDataActions.Invoke(scene);

        levels.Add(Name, scene);
        scenes.Add(scene);
    }



}

