using GXPEngine;
using GXPEngine.FinalApproach;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public enum SceneType { 
    MainMenu,
    Gallery,
    Scene1,
    Scene2,
    Scene3,
    Scene4,
    Puzzle1
}

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

    Scene lastScene = null;
    public void OpenLastScene()
    {
        if(lastScene != null)
        {
            OpenScene(lastScene);
        }
    }
    Dictionary<SceneType, Scene> scenesDictionary = new Dictionary<SceneType, Scene>();

    public Scene CurrentLevel { get; private set; } = null;

    private  Controller controller;
    readonly Random random = new Random();
    public SceneManager() {
        Instance = this;
        controller = new Controller();
        AddChild(controller);

        AudioManager audioManager = new AudioManager();
        AddChild(audioManager);

        
        LoadLevels();
        OpenScene(SceneType.MainMenu);
        
    }


    public void OpenScene(Scene scene) {
     
        lastScene = CurrentLevel;
        RemoveChild(CurrentLevel);
        CurrentLevel = scene;
        AddChildAt(CurrentLevel, 0);
    }

    public void OpenScene(SceneType sceneType) {
        if (CurrentLevel != null)
        {
            lastScene = CurrentLevel;
            RemoveChild(CurrentLevel);
        }
        if (scenesDictionary.ContainsKey(sceneType))
        {
            CurrentLevel = scenesDictionary[sceneType];
            AddChildAt(CurrentLevel, 0);
        }
        else {
            Console.WriteLine("Undefined Scene");
        }

        if (CurrentLevel.OnSceneOpen != null) {
            CurrentLevel.OnSceneOpen.Invoke();
        }
    }
    void LoadLevels()
    {
        CreateMainMenu();
        CreateGallery();

        Scene1();
        Scene2();
        Scene3();
        Scene4();
        CreatePuzzle1Level();
    }

    void CreateMainMenu() {
        AddLevel(SceneType.MainMenu,
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
                      AudioManager.Instance.StopSounds();
                      AudioManager.Instance.AddEnvironementSound("Environment/7 wheatfield with crows");
                      //Instance.OpenScene("Scene1");
                      Instance.OpenScene(SceneType.Scene1);
                      Controller.Instance.SetCursor(CursorType.HAND);
                  });
               button.onHoverAction = () => {
                   foreach (var obj in Instance.CurrentLevel.GetChildren())
                   {
                       if (obj is AnimatedImage)
                       {
                           var background = obj as AnimatedImage;
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
                        Instance.OpenScene(SceneType.Gallery);
                        //Controller.Instance.SetCursor(CursorType.HAND);
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


               sceneRef.OnSceneOpen = () =>
               {
                   Controller.Instance.SetCursor(CursorType.BRUSH);
                   AudioManager.Instance.StopSounds();
                   AudioManager.Instance.AddEnvironementSound("Environment/Menu screen", ".mp3");
               };
           });
    }
    void CreateGallery()
    {
        
        AddLevel(SceneType.Gallery,
          (sceneRef) => {
              var museumMap = new MuseumMap("art/MuseumMap.png", 1, 1);
              museumMap.width *= 2;
              museumMap.height *= 2;
              sceneRef.AddChild(museumMap);


              var painting = new MuseumPainting("Art/OriginalImages/painting1.png", 2, 1);
              painting.SetOrigin(painting.width / 2, painting.height / 2);
              painting.SetScaleXY(0.1f, 0.1f);
              painting.SetXY(95, 155);
              museumMap.AddPainting(painting);

              AddBackButton(sceneRef);


              sceneRef.OnSceneOpen = () =>
              {
                  Controller.Instance.SetCursor(CursorType.HAND);
              };

          });


    }
    private void Scene1()
    {
        AddLevel(SceneType.Scene1,
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


                imageLayer = new AnimationSprite("art/Background1.png", 1, 1);
                imageLayer.SetScaleXY(0.25f, 0.25f);
                imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                imageLayer.SetXY(220, 350);
                sceneRef.AddChild(imageLayer);


                var character = new Character("art/vangoghpainting.png", 6, 1);
                character.SetXY(300, 450);
                sceneRef.AddChild(character);

                var handHelper = new Character("art/HandCursor.png", 1, 2);
                handHelper.SetScaleXY(0.8f, 0.8f);
                handHelper.SetXY(400, 500);
                sceneRef.AddChild(handHelper);

                imageLayer = new AnimationSprite("art/ForeGround1.png", 1, 1);
                imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                imageLayer.SetXY(game.width / 2, game.height / 2);
                sceneRef.AddChild(imageLayer);

                var button = new Button("art/transparent.png", 1, 1,
                    () =>
                    {
                        Instance.OpenScene(SceneType.Scene2);
                        AudioManager.Instance.PlaySound("VoiceoverLines/scene 1");
                    });
                button.width = 120;
                button.height = 200;
                button.SetXY(350, game.height - 250);
                sceneRef.AddChild(button);

                //UI Buttons
                AddBackButton(sceneRef);
                AddGallaryButton(sceneRef);

                sceneRef.OnSceneOpen = () =>
                {
                    Controller.Instance.SetCursor(CursorType.HAND);
                };

            });
    }
    private void Scene2()
    {
        
        AddLevel(SceneType.Scene2,
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
                   });
                button.SetScaleXY(0.8f, 0.8f);
                button.SetXY(game.width - button.width / 2 - 20, game.height - 190);
                sceneRef.AddChild(button);

                var dialogBox = new TextBox(button, true);
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
                        Instance.OpenScene(SceneType.Puzzle1);
                        //Instance.OpenScene("Puzzle");
                        Controller.Instance.SetCursor(CursorType.BRUSH);
                        AudioManager.Instance.PlaySound("VoiceoverLines/scene 2");

                    });
                button.SetScaleXY(0.5f, 0.5f);
                button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                sceneRef.AddChild(button);

                dialogBox = new TextBox(button, true);
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
                AddBackButton(sceneRef);
                AddGallaryButton(sceneRef);

                sceneRef.OnSceneOpen = () =>
                {
                    Controller.Instance.SetCursor(CursorType.HAND);
                };
            });
    }
    private void Scene3()
    {
        AddLevel(SceneType.Scene3,
                    (sceneRef) =>
                    {
                        var background = new AnimationSprite("art/Backgrounds/SunFlowers.png", 1, 1);
                        background.SetOrigin(background.width / 2, background.height / 2);
                        background.SetXY(game.width / 2, game.height / 2);
                        sceneRef.AddChild(background);


                        var character = new Character("art/vangoghtalking.png", 3, 1);
                        character.SetXY(200, 400);
                        sceneRef.AddChild(character);

                       
                        //Van gogh text
                        var button = new Button("art/Dialog Background.png", 1, 1,
                           () => {

                        });
                        button.SetScaleXY(0.8f, 1f);
                        button.SetXY(game.width - button.width / 2 - 20, game.height - 190);
                        sceneRef.AddChild(button);

                        var dialogBox = new TextBox(button, true);
                        dialogBox.y += 10;
                        dialogBox.x += 50;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = "I was born on the 30 of March, 1853 in Zundert,The\nNetherlands and I was the eldest son of the Protestant\nclergyman Theodorus van Gogh and Anna Cornelia \nCarbentus.";
                            dialogBox.dialogBox.fontSize = 10;
                            dialogBox.dialogBox.TextOffset = new Vec2(0, 10);
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);

                        //Response
                        button = new Button("art/Dialog Background.png", 1, 1,
                            () => {
                                Instance.OpenScene(SceneType.Scene4);
                                
                            });
                        button.SetScaleXY(0.5f, 0.5f);
                        button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                        sceneRef.AddChild(button);

                        dialogBox = new TextBox(button, true);
                        dialogBox.y += 10;
                        dialogBox.x += 85;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = "What happened next?";
                            dialogBox.dialogBox.fontSize = 10;
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);


                        //UI Buttons  
                        AddBackButton(sceneRef);


                        AddGallaryButton(sceneRef);

                        sceneRef.OnSceneOpen = () =>
                        {
                            Controller.Instance.SetCursor(CursorType.HAND);
                            AudioManager.Instance.StopSounds();
                        };
                    });
    }
    private void Scene4()
    {
        AddLevel(SceneType.Scene4,
                    (sceneRef) =>
                    {
                        var backgroundsample = new AnimationSprite("art/Backgrounds/SunFlowers.png", 1, 1);
                        backgroundsample.SetOrigin(backgroundsample.width / 2, backgroundsample.height / 2);
                        backgroundsample.SetXY(game.width / 2, game.height / 2);
                        sceneRef.AddChild(backgroundsample);


                        var character = new Character("art/vangoghtalking.png", 3, 1);
                        character.SetXY(200, 400);
                        sceneRef.AddChild(character);


                        //Van gogh text
                        var button = new Button("art/Dialog Background.png", 1, 1,
                           () => {

                           });
                        button.SetScaleXY(0.8f, 1f);
                        button.SetXY(game.width - button.width / 2 - 20, game.height - 190);
                        sceneRef.AddChild(button);

                        var dialogBox = new TextBox(button, true);
                        dialogBox.y += 10;
                        dialogBox.x += 50;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = "As a kid, I used to dream of becoming a pastor just like \nmy father. I was changing schools a lot and honestly I \nwasn't the best student, which led me to abandoning \nthat dream.";
                            dialogBox.dialogBox.fontSize = 10;
                            dialogBox.dialogBox.TextOffset = new Vec2(0, 10);
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);

                        //Response
                        button = new Button("art/Dialog Background.png", 1, 1,
                            () => {

                            });
                        button.SetScaleXY(0.5f, 0.5f);
                        button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                        sceneRef.AddChild(button);

                        dialogBox = new TextBox(button, true);
                        dialogBox.y += 5;
                        dialogBox.x += 15;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = "Sounds interesting! Tell me about \nyour school years.";
                            dialogBox.dialogBox.fontSize = 10;
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);


                        //UI Buttons
                        AddBackButton(sceneRef);
                        AddGallaryButton(sceneRef);


                        sceneRef.OnSceneOpen = () =>
                        {
                            Controller.Instance.SetCursor(CursorType.HAND);
                        };
                    });
    }
    private void CreatePuzzle1Level()
    {
        AddLevel(SceneType.Puzzle1, (sceneRef) =>
        {
            TextBox scoreTextDialog = null;

            var puzzleGame = Puzzle.Create(sceneRef, "Art/Puzzles/puzzle1_start/", 2, 2, new Vec2(20, 153));
            puzzleGame.OnPuzzleSolved = () =>
            {
                Controller.Instance.stats.AddScore(1);
                if (scoreTextDialog != null)
                {
                    scoreTextDialog.UpdateText(Controller.Instance.stats.Score.ToString());
                }

                //
                AudioManager.Instance.PlaySound("VoiceoverLines/scene 3");
                var button = new Button("art/Dialog Background.png", 1, 1,
                   () => {
                       AudioManager.Instance.PlaySound("VoiceoverLines/scene 4");
                       Instance.OpenScene(SceneType.Scene3);
                       
                       //Instance.OpenScene("Scene3");
                       Controller.Instance.SetCursor(CursorType.HAND);

                   });
                button.SetScaleXY(0.5f, 0.5f);
                button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                sceneRef.AddChild(button);

                var dialogBox = new TextBox(button, true);
                dialogBox.x += 50;
                dialogBox.y += 10;
                dialogBox.Configure(() =>
                {
                    dialogBox.dialogBox.Message = "Amazing, tell me more about you!";
                    dialogBox.dialogBox.fontSize = 10;
                    dialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                dialogBox.EndConfigure();
                sceneRef.AddChild(dialogBox);
            };
            sceneRef.AddChild(puzzleGame);

            //UI Buttons
            AddBackButton(sceneRef);
            AddGallaryButtonPuzzle(sceneRef);
            scoreTextDialog = AddSunflowerTextBox(sceneRef);


            sceneRef.OnSceneOpen = () =>
            {
                AudioManager.Instance.PlaySound("VoiceoverLines/scene 5");
                Controller.Instance.SetCursor(CursorType.BRUSH);
                AudioManager.Instance.ReduceVolume(0.1f);
            };
        });
    }

    void AddBackButton(Scene scene)
    {
        var button = new Button("art/BackButton.png", 1, 1,
                   () => {
                       Instance.OpenScene(SceneType.MainMenu);
                   });
        button.SetXY(button.width / 2 - 30, button.height / 2 - 30);
        scene.AddChild(button);

    }
    void AddGallaryButton(Scene scene) {
        var button = new Button("art/Gallery.png", 1, 1,
           () => {
               Instance.OpenScene(SceneType.Gallery);
           });
        button.SetScaleXY(0.75f, 0.75f);
        button.SetXY(button.width / 2 - 15, button.height);
        scene.AddChild(button);
    }
    void AddGallaryButtonPuzzle(Scene scene)
    {
        var button = new Button("art/Gallery.png", 1, 1,
           () => {
               Instance.OpenScene(SceneType.Gallery);
           });
        button.SetScaleXY(0.75f, 0.75f);
        button.SetXY(button.width + 20, button.height / 2 - 20);
        scene.AddChild(button);
    }

    TextBox AddSunflowerTextBox(Scene scene) {
        TextBox textBoxToReturn = null;

        var UIButton = new Button("art/SunFlowerBG.png", 1, 1,
           () => {
           });
        UIButton.SetScaleXY(0.75f, 0.75f);
        UIButton.SetXY(game.width - UIButton.width / 2, UIButton.height / 2 - 23);
        scene.AddChild(UIButton);

        var flowerCount = new TextBox(UIButton);
        flowerCount.y += UIButton.height / 2;
        flowerCount.Configure(() =>
        {
            flowerCount.dialogBox.Message = Controller.Instance.stats.Score.ToString();
            flowerCount.dialogBox.fontSize = 10;
            flowerCount.dialogBox.color = new Color3(255, 255, 255);
        });
        flowerCount.EndConfigure();
        scene.AddChild(flowerCount);
        textBoxToReturn = flowerCount;


        UIButton = new Button("art/SunFlower.png", 1, 1,
           () => {
           });
        UIButton.SetScaleXY(0.75f, 0.75f);
        UIButton.SetXY(game.width - UIButton.width / 2 - 50, UIButton.height / 2 + 20);
        scene.AddChild(UIButton);

        return textBoxToReturn;
    }

    void AddLevel(SceneType sceneType, ActionBuilder sceneDataActions)
    {
        Scene scene = new Scene();
        sceneDataActions.Invoke(scene);

        scenesDictionary.Add(sceneType, scene);
    }



}

