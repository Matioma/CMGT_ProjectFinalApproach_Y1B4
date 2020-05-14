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
    Scene5,
    Scene6,
    Scene7,
    Scene8,
    Scene9,
    Scene10,
    Scene11,
    Scene12,
    Puzzle1,
    Puzzle2,
    Puzzle3,
    Puzzle4,
    Puzzle5,
    Puzzle6,
    Puzzle7,
    Puzzle8
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


        CreateGallery();
        //LoadLevels();
        OpenScene(SceneType.Scene9);
        
    }


    public void OpenScene(Scene scene) {
     
        lastScene = CurrentLevel;
        RemoveChild(CurrentLevel);
        CurrentLevel = scene;
        AddChildAt(CurrentLevel, 0);

        if (CurrentLevel.OnSceneOpen != null)
        {
            CurrentLevel.OnSceneOpen.Invoke();
        }
    }

    public void OpenScene(SceneType sceneType) {
        if (CurrentLevel != null)
        {
            lastScene = CurrentLevel;
            RemoveChild(CurrentLevel);
        }
        if (!scenesDictionary.ContainsKey(sceneType)) {
            switch (sceneType)
            {
                case SceneType.MainMenu:
                    CreateMainMenu();
                    break;
                case SceneType.Gallery:
                    CreateGallery();
                    break;
                case SceneType.Scene1:
                    Scene1();
                    break;
                case SceneType.Scene2:
                    Scene2();
                    break;
                case SceneType.Puzzle1:
                    CreatePuzzle1Level();
                    break;
                case SceneType.Scene3:
                    Scene3();
                    break;
                case SceneType.Scene4:
                    Scene4();
                    break;
                case SceneType.Scene5:
                    Scene5();
                    break;
                case SceneType.Puzzle2:
                    CreatePuzzle2Level();
                    break;
                case SceneType.Scene6:
                    Scene6();
                    break;
                case SceneType.Puzzle3:
                    CreatePuzzle3Level();
                    break;
                case SceneType.Scene7:
                    Scene7();
                    break;
                case SceneType.Puzzle4:
                    CreatePuzzle4Level();
                    break;
                case SceneType.Scene8:
                    Scene8();
                    break;
                case SceneType.Puzzle5:
                    CreatePuzzle5Level();
                    break;
                case SceneType.Scene9:
                    Scene9();
                    break;
                case SceneType.Puzzle6:
                    CreatePuzzle6Level();
                    break;
                case SceneType.Scene10:
                    Scene10();
                    break;
                case SceneType.Scene11:
                    Scene11();
                    break;
                case SceneType.Puzzle7:
                    CreatePuzzle7Level();
                    break;
                case SceneType.Scene12:
                    Scene12();
                    break;
                case SceneType.Puzzle8:
                    CreatePuzzle8Level();
                    break;
            }
        }

        if (scenesDictionary.ContainsKey(sceneType))
        {
            CurrentLevel = scenesDictionary[sceneType];
            AddChildAt(CurrentLevel, 0);
        }
        else {
            Console.WriteLine("this scene is not existent");
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
                      AudioManager.Instance.PlayOnce("SoundEffect/ButtonMenu");
                      //AudioManager.Instance.PlaySound("SoundEffect/ButtonMenu");
                      //AudioManager.Instance.PlaySound("SoundEffect/ButtonMenu");


                      //AudioManager.Instance.StopSounds();
                      //AudioManager.Instance.AddEnvironementSound("Environment/7 wheatfield with crows");
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
                        //AudioManager.Instance.PlaySound("SoundEffect/ButtonMenu");
                        AudioManager.Instance.PlayOnce("SoundEffect/ButtonMenu");
                        Instance.OpenScene(SceneType.Gallery);
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
                       //AudioManager.Instance.PlaySound("SoundEffect/ButtonMenu");
                       AudioManager.Instance.PlayOnce("SoundEffect/ButtonMenu");
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

              var museumUI = new MuseumUI();
              sceneRef.AddChild(museumUI);


              // Creating the paintings
              var painting = new MuseumPainting("Art/OriginalImages/painting1.png", 2, 1 , PaintingsIdentifiers.Painting1);
              painting.SetOrigin(painting.width / 2, painting.height / 2);
              painting.SetScaleXY(0.15f, 0.15f);
              painting.SetXY(190, 314);
              museumMap.AddPainting(painting);
              sceneRef.AddChildAt(painting, GetChildren().Count - 1);


              painting = new MuseumPainting("Art/OriginalImages/painting2.png", 2, 1, PaintingsIdentifiers.Painting2);
              painting.SetOrigin(painting.width / 2, painting.height / 2);
              painting.SetScaleXY(0.15f, 0.15f);
              painting.SetXY(405, -440);
              museumMap.AddPainting(painting);
              sceneRef.AddChildAt(painting, GetChildren().Count - 1);


              painting = new MuseumPainting("Art/OriginalImages/painting3.png", 2, 1, PaintingsIdentifiers.Painting3);
              painting.SetOrigin(painting.width / 2, painting.height / 2);
              painting.SetScaleXY(0.15f, 0.15f);
              painting.SetXY(-40, -320);
              museumMap.AddPainting(painting);
              sceneRef.AddChildAt(painting, GetChildren().Count - 1);


              painting = new MuseumPainting("Art/OriginalImages/painting4.png", 2, 1, PaintingsIdentifiers.Painting4);
              painting.SetOrigin(painting.width / 2, painting.height / 2);
              painting.SetScaleXY(0.15f, 0.15f);
              painting.SetXY(-420, -530);
              museumMap.AddPainting(painting);
              sceneRef.AddChildAt(painting, GetChildren().Count - 1);

              painting = new MuseumPainting("Art/OriginalImages/painting5.png", 2, 1, PaintingsIdentifiers.Painting5);
              painting.SetOrigin(painting.width / 2, painting.height / 2);
              painting.SetScaleXY(0.15f, 0.15f);
              painting.SetXY(-638, 170);
              museumMap.AddPainting(painting);
              sceneRef.AddChildAt(painting, GetChildren().Count - 1);

              painting = new MuseumPainting("Art/OriginalImages/painting6.png", 2, 1, PaintingsIdentifiers.Painting6);
              painting.SetOrigin(painting.width / 2, painting.height / 2);
              painting.SetScaleXY(0.2f, 0.2f);
              painting.SetXY(-638, 500);
              museumMap.AddPainting(painting);
              sceneRef.AddChildAt(painting, GetChildren().Count - 1);

              painting = new MuseumPainting("Art/OriginalImages/painting7.png", 2, 1, PaintingsIdentifiers.Painting7);
              painting.SetOrigin(painting.width / 2, painting.height / 2);
              painting.SetScaleXY(0.15f, 0.15f);
              painting.SetXY(-190, 510);
              museumMap.AddPainting(painting);
              sceneRef.AddChildAt(painting, GetChildren().Count - 1);



              ///Tutorial Text
              var tutorialDialogBox = new TextBox(new Vec2(300, 0), 500, 500);
              //tutorialDialogBox.y += 20;
              //tutorialDialogBox.x += 50;
              tutorialDialogBox.Configure(() =>
              {
                  tutorialDialogBox.dialogBox.Message = "Click And Drag to move the map!";
                  tutorialDialogBox.dialogBox.fontSize = 20;
                  tutorialDialogBox.dialogBox.color = new Color3(0, 0, 0);
              });
              tutorialDialogBox.EndConfigure();
              sceneRef.AddChildAt(tutorialDialogBox, GetChildren().Count-1);

              PreviousSceneButton(sceneRef);





              sceneRef.OnSceneOpen = () =>
              {
                  Controller.Instance.SetCursor(CursorType.HAND);
                  AudioManager.Instance.StopSounds();
                  AudioManager.Instance.AddEnvironementSound("Environment/gallery music", ".wav");
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
                    AudioManager.Instance.StopSounds();
                    AudioManager.Instance.AddEnvironementSound("Environment/7 wheatfield with crows");
                    AudioManager.Instance.AddEnvironementSound("SoundEffect/BrushesPainting");
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
                dialogBox.y += 5;
                dialogBox.x += 30;
                dialogBox.Configure(() =>
                {
                    dialogBox.dialogBox.Message = "Oh, I was waiting for you! Where do you want me to start?";
                    dialogBox.dialogBox.fontSize = 15;
                    dialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                dialogBox.EndConfigure();
                sceneRef.AddChild(dialogBox);

                //Response
                button = new Button("art/Dialog Background.png", 1, 1,
                    () => {
                        Instance.OpenScene(SceneType.Puzzle1);
                        Controller.Instance.SetCursor(CursorType.BRUSH);
                        AudioManager.Instance.PlaySound("VoiceoverLines/scene 2");

                    });
                button.SetScaleXY(0.5f, 0.5f);
                button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                sceneRef.AddChild(button);

                dialogBox = new TextBox(button, true);
                dialogBox.y += -10;
                dialogBox.x += 50;
                dialogBox.Configure(() =>
                {
                    dialogBox.dialogBox.Message = "What are you drawing there? ";
                    dialogBox.dialogBox.fontSize = 15;
                    dialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                dialogBox.EndConfigure();
                sceneRef.AddChild(dialogBox);


                //UI Buttons
                AddBackButton(sceneRef);
                AddGallaryButton(sceneRef);

                sceneRef.OnSceneOpen = () =>
                {
                    AudioManager.Instance.StopSound("SoundEffect/BrushesPainting");
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
                        
                        dialogBox.x += 50;
                        dialogBox.y += -35;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = "I was born on the 30 of March, 1853 in Zundert,The\nNetherlands and I was the eldest son of the Protestant\nclergyman Theodorus van Gogh and Anna Cornelia \nCarbentus.";
                            dialogBox.dialogBox.fontSize = 15;
                            dialogBox.dialogBox.TextOffset = new Vec2(0, 10);
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);

                        //Response
                        button = new Button("art/Dialog Background.png", 1, 1,
                            () => {
                                Instance.OpenScene(SceneType.Scene4);
                                AudioManager.Instance.PlaySound("VoiceoverLines/scene 5");
                            });
                        button.SetScaleXY(0.5f, 0.5f);
                        button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                        sceneRef.AddChild(button);

                        dialogBox = new TextBox(button, true);
                       
                        dialogBox.x += 75;
                        dialogBox.y += -10;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = "What happened next?";
                            dialogBox.dialogBox.fontSize = 15;
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);


                        //UI Buttons  
                        AddBackButton(sceneRef);


                        AddGallaryButton(sceneRef);

                        sceneRef.OnSceneOpen = () =>
                        {
                            AudioManager.Instance.StopSounds();
                            //AudioManager.Instance.PlaySound("VoiceoverLines/scene 4");
                            Controller.Instance.SetCursor(CursorType.HAND);
                            
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
                        dialogBox.y += -37;
                        dialogBox.x += 50;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = "As a kid, I used to dream of becoming a pastor just like \nmy father. I was changing schools a lot and honestly I \nwasn't the best student, which led me to abandoning \nthat dream.";
                            dialogBox.dialogBox.fontSize = 15;
                            dialogBox.dialogBox.TextOffset = new Vec2(0, 10);
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);

                        //Response
                        button = new Button("art/Dialog Background.png", 1, 1,
                            () => {
                                AudioManager.Instance.PlaySound("VoiceoverLines/scene 6");
                                OpenScene(SceneType.Scene5);
                            });
                        button.SetScaleXY(0.5f, 0.5f);
                        button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                        sceneRef.AddChild(button);

                        dialogBox = new TextBox(button, true);
                        dialogBox.y += -25;
                        dialogBox.x += 15;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = "Sounds interesting! Tell me about \nyour school years.";
                            dialogBox.dialogBox.fontSize = 15;
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

    private void Scene5()
    {
        string backgroundPath = "art/Backgrounds/Background2.png";
        string characterAnimation = "art/vangoghpainting.png";
        string dialogVanGoghMessage = "Well, school was just not for me, so I moved to Belgium\n and chose to become a miner instead. Then I took my \nfirst art classes and got keen on learning from art \nbooks.";
        string userResponse = "Let me see closer!";

        string nextSceneVoiceOver = "VoiceoverLines/scene 7";
        SceneType TargetScene = SceneType.Puzzle2;
        AddLevel(SceneType.Scene5,
                    (sceneRef) =>
                    {
                        //string dialog 


                        var imageLayer = new AnimationSprite(backgroundPath, 1, 1);


                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(game.width / 2, game.height / 2);
                        sceneRef.AddChild(imageLayer);



                        imageLayer = new AnimationSprite("art/paintStand.png", 1, 1);
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(300, 450);
                        sceneRef.AddChild(imageLayer);


                        imageLayer = new AnimationSprite(backgroundPath, 1, 1);
                        imageLayer.SetScaleXY(0.25f, 0.25f);
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(220, 350);
                        sceneRef.AddChild(imageLayer);


                        var character = new Character(characterAnimation, 6, 1);
                        character.SetXY(300, 450);
                        sceneRef.AddChild(character);


                        //Van gogh text
                        var button = new Button("art/Dialog Background.png", 1, 1,
                           () => {

                           });
                        button.SetScaleXY(0.8f, 1f);
                        button.SetXY(game.width - button.width / 2 - 20, game.height - 190);
                        sceneRef.AddChild(button);

                        var dialogBox = new TextBox(button, true);
                        dialogBox.y += -37;
                        dialogBox.x += 50;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = dialogVanGoghMessage;
                            dialogBox.dialogBox.fontSize = 15;
                            dialogBox.dialogBox.TextOffset = new Vec2(0, 10);
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);

                        //Response
                        //
                        //
                        button = new Button("art/Dialog Background.png", 1, 1,
                            () => {
                                AudioManager.Instance.PlaySound(nextSceneVoiceOver);
                                OpenScene(TargetScene);
                            });
                        button.SetScaleXY(0.5f, 0.5f);
                        button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                        sceneRef.AddChild(button);

                        dialogBox = new TextBox(button, true);
                        dialogBox.y += -11;
                        dialogBox.x += 15;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = userResponse;
                            dialogBox.dialogBox.fontSize = 15;
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);


                        //UI Buttons
                        AddBackButton(sceneRef);
                        AddGallaryButton(sceneRef);


                        sceneRef.OnSceneOpen = () =>
                        {
                            AudioManager.Instance.StopSounds();
                            AudioManager.Instance.AddEnvironementSound("Environment/2 Stillife with the bible");
                            Controller.Instance.SetCursor(CursorType.HAND);
                        };
                    });
    }
    private void Scene6()
    {
        string backgroundPath = "art/Backgrounds/the bible background.png";
        string characterAnimation = "art/vangoghpainting.png";
        string dialogVanGoghMessage = "I've devoted my early twenties in reading the Bible\n and I found peace in the religion and art";
        string userResponse = "What happened to you afterwards?";

        string nextSceneVoiceOver = "";

        SceneType TargetScene =SceneType.Puzzle3;
        AddLevel(SceneType.Scene6,
                    (sceneRef) =>
                    {
                        //string dialog 


                        var imageLayer = new AnimationSprite(backgroundPath, 1, 1);
                        
                        
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(game.width / 2, game.height / 2);
                        sceneRef.AddChild(imageLayer);



                        imageLayer = new AnimationSprite("art/paintStand.png", 1, 1);
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(300, 450);
                        sceneRef.AddChild(imageLayer);


                        imageLayer = new AnimationSprite(backgroundPath, 1, 1);
                        imageLayer.SetScaleXY(0.25f, 0.25f);
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(220, 350);
                        sceneRef.AddChild(imageLayer);


                        var character = new Character(characterAnimation, 6, 1);
                        character.SetXY(300, 450);
                        sceneRef.AddChild(character);


                        //Van gogh text
                        var button = new Button("art/Dialog Background.png", 1, 1,
                           () => {

                           });
                        button.SetScaleXY(0.8f, 1f);
                        button.SetXY(game.width - button.width / 2 - 20, game.height - 190);
                        sceneRef.AddChild(button);

                        var dialogBox = new TextBox(button, true);
                        dialogBox.y += 0;
                        dialogBox.x += 50;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = dialogVanGoghMessage;
                            dialogBox.dialogBox.fontSize = 15;
                            dialogBox.dialogBox.TextOffset = new Vec2(0, 10);
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);

                        //Response
                        //
                        //
                        button = new Button("art/Dialog Background.png", 1, 1,
                            () => {
                                if(nextSceneVoiceOver!= "")
                                    AudioManager.Instance.PlaySound(nextSceneVoiceOver);
                                OpenScene(TargetScene);
                            });
                        button.SetScaleXY(0.5f, 0.5f);
                        button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                        sceneRef.AddChild(button);

                        dialogBox = new TextBox(button, true);
                        dialogBox.y += -5;
                        dialogBox.x += 15;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = userResponse;
                            dialogBox.dialogBox.fontSize = 15;
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);


                        //UI Buttons
                        AddBackButton(sceneRef);
                        AddGallaryButton(sceneRef);


                        sceneRef.OnSceneOpen = () =>
                        {
                            AudioManager.Instance.StopSounds();
                            AudioManager.Instance.AddEnvironementSound("Environment/2 Stillife with the bible");
                            Controller.Instance.SetCursor(CursorType.HAND);
                        };
                    });
    }
    private void Scene7()
    {
        string backgroundPath = "art/Backgrounds/background buildings.png";
        string characterAnimation = "art/vangoghpainting.png";
        string dialogVanGoghMessage = "I had to move to Belgium because I wanted \nto become a good artist. My brother sent me \nmoney and took care of me then.";
        string userResponse = "Must have been hard time for you!";

        string nextSceneVoiceOver = "";

        SceneType TargetScene = SceneType.Puzzle4;
        AddLevel(SceneType.Scene7,
                    (sceneRef) =>
                    {
                        //string dialog 


                        var imageLayer = new AnimationSprite(backgroundPath, 1, 1);
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(game.width / 2, game.height / 2);
                        sceneRef.AddChild(imageLayer);



                        imageLayer = new AnimationSprite("art/paintStand.png", 1, 1);
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(300, 450);
                        sceneRef.AddChild(imageLayer);


                        imageLayer = new AnimationSprite(backgroundPath, 1, 1);
                        imageLayer.SetScaleXY(0.25f, 0.25f);
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(220, 350);
                        sceneRef.AddChild(imageLayer);


                        var character = new Character(characterAnimation, 6, 1);
                        character.SetXY(300, 450);
                        sceneRef.AddChild(character);

                        var foreGround = new AnimationSprite("art/Backgrounds/forground buildings.png", 1, 1);
                        //foreGround.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        foreGround.SetXY(0, 20);
                        sceneRef.AddChild(foreGround);


                        //Van gogh text
                        var button = new Button("art/Dialog Background.png", 1, 1,
                           () => {

                           });
                        button.SetScaleXY(0.8f, 1f);
                        button.SetXY(game.width - button.width / 2 - 20, game.height - 190);
                        sceneRef.AddChild(button);

                        var dialogBox = new TextBox(button, true);
                        dialogBox.y += -20;
                        dialogBox.x += 50;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = dialogVanGoghMessage;
                            dialogBox.dialogBox.fontSize = 15;
                            dialogBox.dialogBox.TextOffset = new Vec2(0, 10);
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);

                        //Response
                        //
                        //
                        button = new Button("art/Dialog Background.png", 1, 1,
                            () => {
                                if (nextSceneVoiceOver != "")
                                    AudioManager.Instance.PlaySound(nextSceneVoiceOver);
                                OpenScene(TargetScene);
                            });
                        button.SetScaleXY(0.5f, 0.5f);
                        button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                        sceneRef.AddChild(button);

                        dialogBox = new TextBox(button, true);
                        dialogBox.y += -5;
                        dialogBox.x += 15;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = userResponse;
                            dialogBox.dialogBox.fontSize = 15;
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);


                        //UI Buttons
                        AddBackButton(sceneRef);
                        AddGallaryButton(sceneRef);


                        sceneRef.OnSceneOpen = () =>
                        {
                            AudioManager.Instance.StopSounds();
                            AudioManager.Instance.AddEnvironementSound("Environment/2 Stillife with the bible");
                            Controller.Instance.SetCursor(CursorType.HAND);
                        };
                    });
    }

    private void Scene8()
    {
        string backgroundPath = "art/Backgrounds/painting of theo.png";
        string characterAnimation = "art/vangoghpainting.png";
        string dialogVanGoghMessage = "I moved to Paris and I hoped I could finally become\n a painter and earn some money. Unfortunatelly, not \nas planned, I had to live with my brother and his wife";
        string userResponse = "You must have really counted on \nyour brother then!";

        string nextSceneVoiceOver = "VoiceoverLines/scene 11";

        SceneType TargetScene = SceneType.Puzzle5;
        AddLevel(SceneType.Scene8,
                    (sceneRef) =>
                    {
                        //string dialog 


                        var imageLayer = new AnimationSprite(backgroundPath, 1, 1);
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(game.width / 2, game.height / 2);
                        sceneRef.AddChild(imageLayer);



                        imageLayer = new AnimationSprite("art/paintStand.png", 1, 1);
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(300, 450);
                        sceneRef.AddChild(imageLayer);


                        imageLayer = new AnimationSprite(backgroundPath, 1, 1);
                        imageLayer.SetScaleXY(0.25f, 0.25f);
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(220, 350);
                        sceneRef.AddChild(imageLayer);


                        var character = new Character(characterAnimation, 6, 1);
                        character.SetXY(300, 450);
                        sceneRef.AddChild(character);

                        var foreGround = new AnimationSprite("art/Backgrounds/forground buildings.png", 1, 1);
                        //foreGround.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        foreGround.SetXY(0, 20);
                        sceneRef.AddChild(foreGround);


                        //Van gogh text
                        var button = new Button("art/Dialog Background.png", 1, 1,
                           () => {

                           });
                        button.SetScaleXY(0.8f, 1f);
                        button.SetXY(game.width - button.width / 2 - 20, game.height - 190);
                        sceneRef.AddChild(button);

                        var dialogBox = new TextBox(button, true);
                        dialogBox.y += -20;
                        dialogBox.x += 50;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = dialogVanGoghMessage;
                            dialogBox.dialogBox.fontSize = 15;
                            dialogBox.dialogBox.TextOffset = new Vec2(0, 10);
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);

                        //Response
                        //
                        //
                        button = new Button("art/Dialog Background.png", 1, 1,
                            () => {
                                if (nextSceneVoiceOver != "")
                                    AudioManager.Instance.PlaySound(nextSceneVoiceOver);
                                OpenScene(TargetScene);
                            });
                        button.SetScaleXY(0.5f, 0.5f);
                        button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                        sceneRef.AddChild(button);

                        dialogBox = new TextBox(button, true);
                        dialogBox.y += -25;
                        dialogBox.x += 15;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = userResponse;
                            dialogBox.dialogBox.fontSize = 15;
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);


                        //UI Buttons
                        AddBackButton(sceneRef);
                        AddGallaryButton(sceneRef);


                        sceneRef.OnSceneOpen = () =>
                        {
                            AudioManager.Instance.StopSounds();
                            AudioManager.Instance.AddEnvironementSound("Environment/2 Stillife with the bible");
                            Controller.Instance.SetCursor(CursorType.HAND);
                        };
                    });
    }


    private void Scene9()
    {
        string backgroundPath = "art/Backgrounds/background sunflowers.png";
        string characterAnimation = "art/vangoghpainting.png";
        string dialogVanGoghMessage = "I shared a lot of experiences with other painters such as \nToulouse Lautrec and Emile Bernard that had influenced me \nto see the beauty of colors.";
        string userResponse = "Show me.";

        string nextSceneVoiceOver = "";

        SceneType TargetScene = SceneType.Puzzle6;
        AddLevel(SceneType.Scene9,
                    (sceneRef) =>
                    {
                        //string dialog 


                        var imageLayer = new AnimationSprite(backgroundPath, 1, 1);
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(game.width / 2, game.height / 2);
                        sceneRef.AddChild(imageLayer);



                        imageLayer = new AnimationSprite("art/paintStand.png", 1, 1);
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(300, 450);
                        sceneRef.AddChild(imageLayer);


                        imageLayer = new AnimationSprite(backgroundPath, 1, 1);
                        imageLayer.SetScaleXY(0.25f, 0.25f);
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(220, 350);
                        sceneRef.AddChild(imageLayer);


                        var character = new Character(characterAnimation, 6, 1);
                        character.SetXY(300, 450);
                        sceneRef.AddChild(character);

                        var foreGround = new AnimationSprite("art/Backgrounds/forground sunflower.png", 1, 1);
                        //foreGround.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        foreGround.SetXY(0, 20);
                        sceneRef.AddChild(foreGround);


                        //Van gogh text
                        var button = new Button("art/Dialog Background.png", 1, 1,
                           () => {

                           });
                        button.SetScaleXY(0.8f, 1f);
                        button.SetXY(game.width - button.width / 2 - 20, game.height - 190);
                        sceneRef.AddChild(button);

                        var dialogBox = new TextBox(button, true);
                        dialogBox.y += -20;
                        dialogBox.x += 50;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = dialogVanGoghMessage;
                            dialogBox.dialogBox.fontSize = 15;
                            dialogBox.dialogBox.TextOffset = new Vec2(0, 10);
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);

                        //Response
                        //
                        //
                        button = new Button("art/Dialog Background.png", 1, 1,
                            () => {
                                if (nextSceneVoiceOver != "")
                                    AudioManager.Instance.PlaySound(nextSceneVoiceOver);
                                OpenScene(TargetScene);
                            });
                        button.SetScaleXY(0.5f, 0.5f);
                        button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                        sceneRef.AddChild(button);

                        dialogBox = new TextBox(button, true);
                        dialogBox.y += -25;
                        dialogBox.x += 15;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = userResponse;
                            dialogBox.dialogBox.fontSize = 15;
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);


                        //UI Buttons
                        AddBackButton(sceneRef);
                        AddGallaryButton(sceneRef);


                        sceneRef.OnSceneOpen = () =>
                        {
                            AudioManager.Instance.StopSounds();
                            AudioManager.Instance.AddEnvironementSound("Environment/2 Stillife with the bible");
                            Controller.Instance.SetCursor(CursorType.HAND);
                        };
                    });
    }

    private void Scene10()
    {
        string backgroundPath = "art/Backgrounds/background sunflowers.png";
        string characterAnimation = "art/vangoghtalking.png";
        string dialogVanGoghMessage = "For my whole life I couldn't manage to sell more \nthan one painting. Nothing was going as I \nhad expected. People didn't appreciate my art, which \nmade me sad. ";
        string userResponse = "How did you cope?";

        string nextSceneVoiceOver = "VoiceoverLines/scene 14";

        SceneType TargetScene = SceneType.Scene11;
        AddLevel(SceneType.Scene10,
                    (sceneRef) =>
                    {
                        //string dialog 


                        var imageLayer = new AnimationSprite(backgroundPath, 1, 1);
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(game.width / 2, game.height / 2);
                        sceneRef.AddChild(imageLayer);



                        imageLayer = new AnimationSprite("art/paintStand.png", 1, 1);
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(300, 450);
                        sceneRef.AddChild(imageLayer);


                        imageLayer = new AnimationSprite(backgroundPath, 1, 1);
                        imageLayer.SetScaleXY(0.25f, 0.25f);
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(220, 350);
                        sceneRef.AddChild(imageLayer);


                        var character = new Character(characterAnimation, 3, 1);
                        character.SetXY(300, 450);
                        sceneRef.AddChild(character);

                        var foreGround = new AnimationSprite("art/Backgrounds/forground sunflower.png", 1, 1);
                        //foreGround.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        foreGround.SetXY(0, 20);
                        sceneRef.AddChild(foreGround);


                        //Van gogh text
                        var button = new Button("art/Dialog Background.png", 1, 1,
                           () => {

                           });
                        button.SetScaleXY(0.8f, 1f);
                        button.SetXY(game.width - button.width / 2 - 20, game.height - 190);
                        sceneRef.AddChild(button);

                        var dialogBox = new TextBox(button, true);
                        dialogBox.y += -35;
                        dialogBox.x += 50;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = dialogVanGoghMessage;
                            dialogBox.dialogBox.fontSize = 15;
                            dialogBox.dialogBox.TextOffset = new Vec2(0, 10);
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);

                        //Response
                        //
                        //
                        button = new Button("art/Dialog Background.png", 1, 1,
                            () => {
                                if (nextSceneVoiceOver != "")
                                    AudioManager.Instance.PlaySound(nextSceneVoiceOver);
                                OpenScene(TargetScene);
                            });
                        button.SetScaleXY(0.5f, 0.5f);
                        button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                        sceneRef.AddChild(button);

                        dialogBox = new TextBox(button, true);
                        dialogBox.y += -15;
                        dialogBox.x += 15;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = userResponse;
                            dialogBox.dialogBox.fontSize = 15;
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);


                        //UI Buttons
                        AddBackButton(sceneRef);
                        AddGallaryButton(sceneRef);


                        sceneRef.OnSceneOpen = () =>
                        {
                            AudioManager.Instance.StopSounds();
                            AudioManager.Instance.AddEnvironementSound("Environment/2 Stillife with the bible");
                            Controller.Instance.SetCursor(CursorType.HAND);
                        };
                    });
    }

    private void Scene11()
    {
        string backgroundPath = "art/Backgrounds/IMG_0222.png";
        string characterAnimation = "art/vangoghpaintingnoear.png";
        string dialogVanGoghMessage = "Those thoughts led me to darker times, I lost \nmyself and cut my left ear once, in a burst of anger.";
        string userResponse = "Horrifying!";

        string nextSceneVoiceOver = "VoiceoverLines/scene 15";

        SceneType TargetScene = SceneType.Puzzle7;
        AddLevel(SceneType.Scene11,
                    (sceneRef) =>
                    {
                        //string dialog 


                        var imageLayer = new AnimationSprite(backgroundPath, 1, 1);
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(game.width / 2, game.height / 2);
                        sceneRef.AddChild(imageLayer);



                        imageLayer = new AnimationSprite("art/paintStand.png", 1, 1);
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(300, 450);
                        sceneRef.AddChild(imageLayer);


                        imageLayer = new AnimationSprite(backgroundPath, 1, 1);
                        imageLayer.SetScaleXY(0.25f, 0.25f);
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(220, 350);
                        sceneRef.AddChild(imageLayer);


                        var character = new Character(characterAnimation, 6, 1);
                        character.SetXY(300, 450);
                        sceneRef.AddChild(character);

                        //var foreGround = new AnimationSprite("art/Backgrounds/forground sunflower.png", 1, 1);
                        ////foreGround.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        //foreGround.SetXY(0, 20);
                        //sceneRef.AddChild(foreGround);


                        //Van gogh text
                        var button = new Button("art/Dialog Background.png", 1, 1,
                           () => {

                           });
                        button.SetScaleXY(0.8f, 1f);
                        button.SetXY(game.width - button.width / 2 - 20, game.height - 190);
                        sceneRef.AddChild(button);

                        var dialogBox = new TextBox(button, true);
                        dialogBox.y += -15;
                        dialogBox.x += 50;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = dialogVanGoghMessage;
                            dialogBox.dialogBox.fontSize = 15;
                            dialogBox.dialogBox.TextOffset = new Vec2(0, 10);
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);

                        //Response
                        //
                        //
                        button = new Button("art/Dialog Background.png", 1, 1,
                            () => {
                                if (nextSceneVoiceOver != "")
                                    AudioManager.Instance.PlaySound(nextSceneVoiceOver);
                                OpenScene(TargetScene);
                            });
                        button.SetScaleXY(0.5f, 0.5f);
                        button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                        sceneRef.AddChild(button);

                        dialogBox = new TextBox(button, true);
                        dialogBox.y += -15;
                        dialogBox.x += 15;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = userResponse;
                            dialogBox.dialogBox.fontSize = 15;
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);


                        //UI Buttons
                        AddBackButton(sceneRef);
                        AddGallaryButton(sceneRef);


                        sceneRef.OnSceneOpen = () =>
                        {
                            AudioManager.Instance.StopSounds();
                            AudioManager.Instance.AddEnvironementSound("Environment/2 Stillife with the bible");
                            Controller.Instance.SetCursor(CursorType.HAND);
                        };
                    });
    }


    private void Scene12()
    {
        string backgroundPath = "art/Background1.png";
        string characterAnimation = "art/vangoghtalkingnoear.png";
        string dialogVanGoghMessage = "Now that I am out from the asylim I live \ncurrently in Auvers-sur-Oise. Oh....that reminds \nme, come with me to check my last masterpiece.";
        string userResponse = "Let’s go!";

        string nextSceneVoiceOver = "";

        SceneType TargetScene = SceneType.Puzzle8;
        AddLevel(SceneType.Scene12,
                    (sceneRef) =>
                    {
                        //string dialog 


                        var imageLayer = new AnimationSprite(backgroundPath, 1, 1);
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(game.width / 2, game.height / 2);
                        sceneRef.AddChild(imageLayer);



                        imageLayer = new AnimationSprite("art/paintStand.png", 1, 1);
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(300, 450);
                        sceneRef.AddChild(imageLayer);


                        imageLayer = new AnimationSprite(backgroundPath, 1, 1);
                        imageLayer.SetScaleXY(0.25f, 0.25f);
                        imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        imageLayer.SetXY(220, 350);
                        sceneRef.AddChild(imageLayer);


                        var character = new Character(characterAnimation, 3, 1);
                        character.SetXY(300, 450);
                        sceneRef.AddChild(character);

                        //var foreGround = new AnimationSprite("art/Backgrounds/forground sunflower.png", 1, 1);
                        ////foreGround.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                        //foreGround.SetXY(0, 20);
                        //sceneRef.AddChild(foreGround);


                        //Van gogh text
                        var button = new Button("art/Dialog Background.png", 1, 1,
                           () => {

                           });
                        button.SetScaleXY(0.8f, 1f);
                        button.SetXY(game.width - button.width / 2 - 20, game.height - 190);
                        sceneRef.AddChild(button);

                        var dialogBox = new TextBox(button, true);
                        dialogBox.y += -15;
                        dialogBox.x += 50;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = dialogVanGoghMessage;
                            dialogBox.dialogBox.fontSize = 15;
                            dialogBox.dialogBox.TextOffset = new Vec2(0, 10);
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);

                        //Response
                        //
                        //
                        button = new Button("art/Dialog Background.png", 1, 1,
                            () => {
                                if (nextSceneVoiceOver != "")
                                    AudioManager.Instance.PlaySound(nextSceneVoiceOver);
                                OpenScene(TargetScene);
                            });
                        button.SetScaleXY(0.5f, 0.5f);
                        button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                        sceneRef.AddChild(button);

                        dialogBox = new TextBox(button, true);
                        dialogBox.y += -15;
                        dialogBox.x += 15;
                        dialogBox.Configure(() =>
                        {
                            dialogBox.dialogBox.Message = userResponse;
                            dialogBox.dialogBox.fontSize = 15;
                            dialogBox.dialogBox.color = new Color3(0, 0, 0);
                        });
                        dialogBox.EndConfigure();
                        sceneRef.AddChild(dialogBox);


                        //UI Buttons
                        AddBackButton(sceneRef);
                        AddGallaryButton(sceneRef);


                        sceneRef.OnSceneOpen = () =>
                        {
                            AudioManager.Instance.StopSounds();
                            AudioManager.Instance.AddEnvironementSound("Environment/2 Stillife with the bible");
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
                AudioManager.Instance.PlayOnce("SoundEffect/positive feedback");
                //Controller.Instance.stats.PuzzleSolved(PaintingsIdentifiers.Painting2);


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

                   });
                button.SetScaleXY(0.5f, 0.5f);
                button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                sceneRef.AddChild(button);

                var dialogBox = new TextBox(button, true);
                dialogBox.x += 20;
                dialogBox.y -= 10;
                dialogBox.Configure(() =>
                {
                    dialogBox.dialogBox.Message = "Amazing, tell me more about you!";
                    dialogBox.dialogBox.fontSize = 15;
                    dialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                dialogBox.EndConfigure();
                sceneRef.AddChild(dialogBox);


                ///
                ///Adding temporary dialog boxes
                ///
                sceneRef.RemoveTemporaryObjects();
                button = new Button("art/Dialog Background.png", 1, 1);
                button.SetScaleXY(0.7f, 0.7f);
                button.SetXY(button.width / 2, game.height - button.height / 2);
                sceneRef.AddChild(button);

                dialogBox = new TextBox(button, true);
                dialogBox.y -= 30;
                dialogBox.x += 20;
                dialogBox.Configure(() =>
                {
                    dialogBox.dialogBox.Message = "You are doing great, young artist!With common forces\nwe made it through half of the painting. Maybe you can \ncheck it out later to help me finish?";
                    dialogBox.dialogBox.fontSize = 15;
                    dialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                dialogBox.EndConfigure();
                sceneRef.AddTemporaryObject(button);
                sceneRef.AddTemporaryObject(dialogBox);
            };
            sceneRef.AddChild(puzzleGame);


            ///Tutorial Text
            var tutorialDialogBox = new TextBox(new Vec2(300, 50), 500, 500);
            //tutorialDialogBox.y += 20;
            //tutorialDialogBox.x += 50;
            tutorialDialogBox.Configure(() =>
            {
                tutorialDialogBox.dialogBox.Message = "Click And Drag to solve the Puzzle!";
                tutorialDialogBox.dialogBox.fontSize = 15;
                tutorialDialogBox.dialogBox.color = new Color3(255, 255, 255);
            });
            tutorialDialogBox.EndConfigure();
            sceneRef.AddChild(tutorialDialogBox);


            ///
            ///Adding temporary dialog boxes
            ///
            var TextMessageButton = new Button("art/Dialog Background.png", 1, 1);
            TextMessageButton.SetScaleXY(0.7f, 0.7f);
            TextMessageButton.SetXY(TextMessageButton.width / 2 , game.height - TextMessageButton.height/2);
            sceneRef.AddChild(TextMessageButton);

            var tempDialogBox = new TextBox(TextMessageButton, true);
            tempDialogBox.y -= 10;
            tempDialogBox.x += 20;
            tempDialogBox.Configure(() =>
            {
                tempDialogBox.dialogBox.Message = "That? ... That is going to be my last painting, \nchild. I should be done with it by the end of the tour.";
                tempDialogBox.dialogBox.fontSize = 15;
                tempDialogBox.dialogBox.color = new Color3(0, 0, 0);
            });
            tempDialogBox.EndConfigure();
            sceneRef.AddTemporaryObject(TextMessageButton);
            sceneRef.AddTemporaryObject(tempDialogBox);


            //UI Buttons
            AddBackButton(sceneRef);
            AddGallaryButtonPuzzle(sceneRef);
            scoreTextDialog = AddSunflowerTextBox(sceneRef);


            sceneRef.OnSceneOpen = () =>
            {
                //AudioManager.Instance.PlaySound("VoiceoverLines/scene 2");
                Controller.Instance.SetCursor(CursorType.BRUSH);
                AudioManager.Instance.ReduceVolume(0.1f);
            };
        });
    }
    private void CreatePuzzle2Level()
    {
        string NaratorTextPath = "I loved drawing what I see, as I say: “I want to \ntouch people with my art. I want them to say 'he feels \ndeeply, he feels tenderly'.” I wanted to sell my works. \nbecause my art could possibly help  peasants";
        string playerResponse = "Amazing!";




        AddLevel(SceneType.Puzzle2, (sceneRef) =>
        {
            TextBox scoreTextDialog = null;

            var puzzleGame = Puzzle.Create(sceneRef, "Art/Puzzles/puzzle2/", 2, 4, new Vec2(138, 99));
            puzzleGame.OnPuzzleSolved = () =>
            {
                AudioManager.Instance.PlayOnce("SoundEffect/positive feedback");
                Controller.Instance.stats.PuzzleSolved(PaintingsIdentifiers.Painting1);


                Controller.Instance.stats.AddScore(1);
                if (scoreTextDialog != null)
                {
                    scoreTextDialog.UpdateText(Controller.Instance.stats.Score.ToString());
                }

                var button = new Button("art/Dialog Background.png", 1, 1,
                   () => {
                       AudioManager.Instance.PlaySound("VoiceoverLines/scene 8");
                       Instance.OpenScene(SceneType.Scene6);

                   });
                button.SetScaleXY(0.5f, 0.5f);
                button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                sceneRef.AddChild(button);

                var dialogBox = new TextBox(button, true);
                dialogBox.x += 20;
                dialogBox.y -= 10;
                dialogBox.Configure(() =>
                {
                    dialogBox.dialogBox.Message = "Amazing!";
                    dialogBox.dialogBox.fontSize = 15;
                    dialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                dialogBox.EndConfigure();
                sceneRef.AddChild(dialogBox);

                sceneRef.RemoveTemporaryObjects();
            };
            sceneRef.AddChild(puzzleGame);


            ///Tutorial Text
            var tutorialDialogBox = new TextBox(new Vec2(300, 50), 500, 500);
            //tutorialDialogBox.y += 20;
            //tutorialDialogBox.x += 50;
            tutorialDialogBox.Configure(() =>
            {
                tutorialDialogBox.dialogBox.Message = "Click And Drag to solve the Puzzle!";
                tutorialDialogBox.dialogBox.fontSize = 15;
                tutorialDialogBox.dialogBox.color = new Color3(255, 255, 255);
            });
            tutorialDialogBox.EndConfigure();
            sceneRef.AddChild(tutorialDialogBox);



            ///
            ///Adding temporary dialog boxes
            ///
            var TextMessageButton = new Button("art/Dialog Background.png", 1, 1);
            TextMessageButton.SetScaleXY(0.7f, 0.7f);
            TextMessageButton.SetXY(TextMessageButton.width / 2, game.height - TextMessageButton.height / 2);
            sceneRef.AddChild(TextMessageButton);

            var tempDialogBox = new TextBox(TextMessageButton, true);
            tempDialogBox.y -= 35;
            tempDialogBox.x += 10;
            tempDialogBox.Configure(() =>
            {
                tempDialogBox.dialogBox.Message = NaratorTextPath;
                tempDialogBox.dialogBox.fontSize = 15;
                tempDialogBox.dialogBox.color = new Color3(0, 0, 0);
            });
            tempDialogBox.EndConfigure();
            sceneRef.AddTemporaryObject(TextMessageButton);
            sceneRef.AddTemporaryObject(tempDialogBox);


            //UI Buttons
            AddBackButton(sceneRef);
            AddGallaryButtonPuzzle(sceneRef);
            scoreTextDialog = AddSunflowerTextBox(sceneRef);


            sceneRef.OnSceneOpen = () =>
            {
                //AudioManager.Instance.PlaySound("VoiceoverLines/scene 2");
                Controller.Instance.SetCursor(CursorType.BRUSH);
                AudioManager.Instance.ReduceVolume(0.1f);
            };
        });
    }
    private void CreatePuzzle3Level()
    {
        string NaratorTextPath = "I loved drawing what I see, as I say: \n“I want to touch people with my art. I want them to say 'he feels deeply, he feels tenderly'.” I wanted to sell my works because my art could possibly help  peasants";
        string playerResponse = "Nice!";

        string nextSceneVoiceOver = "VoiceoverLines/scene 9";
        PaintingsIdentifiers solvedPuzzle = PaintingsIdentifiers.Painting2;

        SceneType nextScene = SceneType.Scene7;

        AddLevel(SceneType.Puzzle3, (sceneRef) =>
        {
            TextBox scoreTextDialog = null;

            var puzzleGame = Puzzle.Create(sceneRef, "Art/Puzzles/puzzle3/", 4, 2, new Vec2(138, 99));
            puzzleGame.OnPuzzleSolved = () =>
            {
                AudioManager.Instance.PlayOnce("SoundEffect/positive feedback");
                Controller.Instance.stats.PuzzleSolved(solvedPuzzle);


                Controller.Instance.stats.AddScore(1);
                if (scoreTextDialog != null)
                {
                    scoreTextDialog.UpdateText(Controller.Instance.stats.Score.ToString());
                }

                var button = new Button("art/Dialog Background.png", 1, 1,
                   () => {
                       AudioManager.Instance.PlaySound(nextSceneVoiceOver);
                       Instance.OpenScene(nextScene);

                   });
                button.SetScaleXY(0.5f, 0.5f);
                button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                sceneRef.AddChild(button);

                var dialogBox = new TextBox(button, true);
                dialogBox.x += 20;
                dialogBox.y -= 10;
                dialogBox.Configure(() =>
                {
                    dialogBox.dialogBox.Message = playerResponse;
                    dialogBox.dialogBox.fontSize = 15;
                    dialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                dialogBox.EndConfigure();
                sceneRef.AddChild(dialogBox);

                sceneRef.RemoveTemporaryObjects();
            };
            sceneRef.AddChild(puzzleGame);


            ///Tutorial Text
            var tutorialDialogBox = new TextBox(new Vec2(300, 50), 500, 500);
            //tutorialDialogBox.y += 20;
            //tutorialDialogBox.x += 50;
            tutorialDialogBox.Configure(() =>
            {
                tutorialDialogBox.dialogBox.Message = "Click And Drag to solve the Puzzle!";
                tutorialDialogBox.dialogBox.fontSize = 15;
                tutorialDialogBox.dialogBox.color = new Color3(255, 255, 255);
            });
            tutorialDialogBox.EndConfigure();
            sceneRef.AddChild(tutorialDialogBox);



            ///
            ///Adding temporary dialog boxes
            ///
            //var TextMessageButton = new Button("art/Dialog Background.png", 1, 1);
            //TextMessageButton.SetScaleXY(0.7f, 0.7f);
            //TextMessageButton.SetXY(TextMessageButton.width / 2, game.height - TextMessageButton.height / 2);
            //sceneRef.AddChild(TextMessageButton);

            //var tempDialogBox = new TextBox(TextMessageButton, true);
            //tempDialogBox.y -= 10;
            //tempDialogBox.x += 20;
            //tempDialogBox.Configure(() =>
            //{
            //    tempDialogBox.dialogBox.Message = NaratorTextPath;
            //    tempDialogBox.dialogBox.fontSize = 15;
            //    tempDialogBox.dialogBox.color = new Color3(0, 0, 0);
            //});
            //tempDialogBox.EndConfigure();
            //sceneRef.AddTemporaryObject(TextMessageButton);
            //sceneRef.AddTemporaryObject(tempDialogBox);


            //UI Buttons
            AddBackButton(sceneRef);
            AddGallaryButtonPuzzle(sceneRef);
            scoreTextDialog = AddSunflowerTextBox(sceneRef);


            sceneRef.OnSceneOpen = () =>
            {
                //AudioManager.Instance.PlaySound("VoiceoverLines/scene 2");
                Controller.Instance.SetCursor(CursorType.BRUSH);
                AudioManager.Instance.ReduceVolume(0.1f);
            };
        });
    }
    private void CreatePuzzle4Level()
    {
        string NaratorTextPath = "I loved drawing what I see, as I say: \n“I want to touch people with my art. I want them to say 'he feels deeply, he feels tenderly'.” I wanted to sell my works because my art could possibly help  peasants";
        string playerResponse = "Please, Continue";

        string nextSceneVoiceOver = "VoiceoverLines/scene 10";
        PaintingsIdentifiers solvedPuzzle = PaintingsIdentifiers.Painting4;

        SceneType nextScene = SceneType.Scene8;

        AddLevel(SceneType.Puzzle4, (sceneRef) =>
        {
            TextBox scoreTextDialog = null;

            var puzzleGame = Puzzle.Create(sceneRef, "Art/Puzzles/puzzle4/", 2, 4, new Vec2(277, 73));
            puzzleGame.OnPuzzleSolved = () =>
            {
                AudioManager.Instance.PlayOnce("SoundEffect/positive feedback");
                Controller.Instance.stats.PuzzleSolved(solvedPuzzle);


                Controller.Instance.stats.AddScore(1);
                if (scoreTextDialog != null)
                {
                    scoreTextDialog.UpdateText(Controller.Instance.stats.Score.ToString());
                }

                var button = new Button("art/Dialog Background.png", 1, 1,
                   () => {
                       AudioManager.Instance.PlaySound(nextSceneVoiceOver);
                       Instance.OpenScene(nextScene);

                   });
                button.SetScaleXY(0.5f, 0.5f);
                button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                sceneRef.AddChild(button);

                var dialogBox = new TextBox(button, true);
                dialogBox.x += 20;
                dialogBox.y -= 10;
                dialogBox.Configure(() =>
                {
                    dialogBox.dialogBox.Message = playerResponse;
                    dialogBox.dialogBox.fontSize = 15;
                    dialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                dialogBox.EndConfigure();
                sceneRef.AddChild(dialogBox);

                sceneRef.RemoveTemporaryObjects();
            };
            sceneRef.AddChild(puzzleGame);


            ///Tutorial Text
            var tutorialDialogBox = new TextBox(new Vec2(300, 50), 500, 500);
            //tutorialDialogBox.y += 20;
            //tutorialDialogBox.x += 50;
            tutorialDialogBox.Configure(() =>
            {
                tutorialDialogBox.dialogBox.Message = "Click And Drag to solve the Puzzle!";
                tutorialDialogBox.dialogBox.fontSize = 15;
                tutorialDialogBox.dialogBox.color = new Color3(255, 255, 255);
            });
            tutorialDialogBox.EndConfigure();
            sceneRef.AddChild(tutorialDialogBox);



            ///
            ///Adding temporary dialog boxes
            ///
            //var TextMessageButton = new Button("art/Dialog Background.png", 1, 1);
            //TextMessageButton.SetScaleXY(0.7f, 0.7f);
            //TextMessageButton.SetXY(TextMessageButton.width / 2, game.height - TextMessageButton.height / 2);
            //sceneRef.AddChild(TextMessageButton);

            //var tempDialogBox = new TextBox(TextMessageButton, true);
            //tempDialogBox.y -= 10;
            //tempDialogBox.x += 20;
            //tempDialogBox.Configure(() =>
            //{
            //    tempDialogBox.dialogBox.Message = NaratorTextPath;
            //    tempDialogBox.dialogBox.fontSize = 15;
            //    tempDialogBox.dialogBox.color = new Color3(0, 0, 0);
            //});
            //tempDialogBox.EndConfigure();
            //sceneRef.AddTemporaryObject(TextMessageButton);
            //sceneRef.AddTemporaryObject(tempDialogBox);


            //UI Buttons
            AddBackButton(sceneRef);
            AddGallaryButtonPuzzle(sceneRef);
            scoreTextDialog = AddSunflowerTextBox(sceneRef);


            sceneRef.OnSceneOpen = () =>
            {
                //AudioManager.Instance.PlaySound("VoiceoverLines/scene 2");
                Controller.Instance.SetCursor(CursorType.BRUSH);
                AudioManager.Instance.ReduceVolume(0.1f);
            };
        });
    }
    private void CreatePuzzle5Level()
    {
        string NaratorTextPath = "Yes, I was very thankful for all his help.\nHe was my only support all those years, \nso I drew him a portrait.";
        string playerResponse = "Please, Continue";

        string nextSceneVoiceOver = "VoiceoverLines/scene 12";
        PaintingsIdentifiers solvedPuzzle = PaintingsIdentifiers.Painting3;

        SceneType nextScene = SceneType.Scene9;

        AddLevel(SceneType.Puzzle5, (sceneRef) =>
        {
            TextBox scoreTextDialog = null;

            var puzzleGame = Puzzle.Create(sceneRef, "Art/Puzzles/puzzle5/", 4, 2, new Vec2(277, 73));
            puzzleGame.OnPuzzleSolved = () =>
            {
                AudioManager.Instance.PlayOnce("SoundEffect/positive feedback");
                Controller.Instance.stats.PuzzleSolved(solvedPuzzle);


                Controller.Instance.stats.AddScore(1);
                if (scoreTextDialog != null)
                {
                    scoreTextDialog.UpdateText(Controller.Instance.stats.Score.ToString());
                }

                var button = new Button("art/Dialog Background.png", 1, 1,
                   () => {
                       AudioManager.Instance.PlaySound(nextSceneVoiceOver);
                       Instance.OpenScene(nextScene);

                   });
                button.SetScaleXY(0.5f, 0.5f);
                button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                sceneRef.AddChild(button);

                var dialogBox = new TextBox(button, true);
                dialogBox.x += 20;
                dialogBox.y -= 20;
                dialogBox.Configure(() =>
                {
                    dialogBox.dialogBox.Message = playerResponse;
                    dialogBox.dialogBox.fontSize = 15;
                    dialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                dialogBox.EndConfigure();
                sceneRef.AddChild(dialogBox);

                sceneRef.RemoveTemporaryObjects();
            };
            sceneRef.AddChild(puzzleGame);


            ///Tutorial Text
            var tutorialDialogBox = new TextBox(new Vec2(300, 50), 500, 500);
            //tutorialDialogBox.y += 20;
            //tutorialDialogBox.x += 50;
            tutorialDialogBox.Configure(() =>
            {
                tutorialDialogBox.dialogBox.Message = "Click And Drag to solve the Puzzle!";
                tutorialDialogBox.dialogBox.fontSize = 15;
                tutorialDialogBox.dialogBox.color = new Color3(255, 255, 255);
            });
            tutorialDialogBox.EndConfigure();
            sceneRef.AddChild(tutorialDialogBox);



            ///
            ///Adding temporary dialog boxes
            ///
            var TextMessageButton = new Button("art/Dialog Background.png", 1, 1);
            TextMessageButton.SetScaleXY(0.7f, 0.7f);
            TextMessageButton.SetXY(TextMessageButton.width / 2, game.height - TextMessageButton.height / 2);
            sceneRef.AddChild(TextMessageButton);

            var tempDialogBox = new TextBox(TextMessageButton, true);
            tempDialogBox.y -= 28;
            tempDialogBox.x += 20;
            tempDialogBox.Configure(() =>
            {
                tempDialogBox.dialogBox.Message = NaratorTextPath;
                tempDialogBox.dialogBox.fontSize = 15;
                tempDialogBox.dialogBox.color = new Color3(0, 0, 0);
            });
            tempDialogBox.EndConfigure();
            sceneRef.AddTemporaryObject(TextMessageButton);
            sceneRef.AddTemporaryObject(tempDialogBox);


            //UI Buttons
            AddBackButton(sceneRef);
            AddGallaryButtonPuzzle(sceneRef);
            scoreTextDialog = AddSunflowerTextBox(sceneRef);


            sceneRef.OnSceneOpen = () =>
            {
                //AudioManager.Instance.PlaySound("VoiceoverLines/scene 2");
                Controller.Instance.SetCursor(CursorType.BRUSH);
                AudioManager.Instance.ReduceVolume(0.1f);
            };
        });
    }
    private void CreatePuzzle6Level()
    {
        string NaratorTextPath = "Yes, I was very thankful for all his help.\nHe was my only support all those years, \nso I drew him a portrait.";
        string playerResponse = "Nice one";

        string nextSceneVoiceOver = "VoiceoverLines/scene 13";
        PaintingsIdentifiers solvedPuzzle = PaintingsIdentifiers.Painting5;

        SceneType nextScene = SceneType.Scene10;

        AddLevel(SceneType.Puzzle6, (sceneRef) =>
        {
            TextBox scoreTextDialog = null;

            var puzzleGame = Puzzle.Create(sceneRef, "Art/Puzzles/puzzle6/", 3, 4, new Vec2(277, 73));
            puzzleGame.OnPuzzleSolved = () =>
            {
                AudioManager.Instance.PlayOnce("SoundEffect/positive feedback");
                Controller.Instance.stats.PuzzleSolved(solvedPuzzle);


                Controller.Instance.stats.AddScore(1);
                if (scoreTextDialog != null)
                {
                    scoreTextDialog.UpdateText(Controller.Instance.stats.Score.ToString());
                }

                var button = new Button("art/Dialog Background.png", 1, 1,
                   () => {
                       AudioManager.Instance.PlaySound(nextSceneVoiceOver);
                       Instance.OpenScene(nextScene);

                   });
                button.SetScaleXY(0.5f, 0.5f);
                button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                sceneRef.AddChild(button);

                var dialogBox = new TextBox(button, true);
                dialogBox.x += 20;
                dialogBox.y -= 20;
                dialogBox.Configure(() =>
                {
                    dialogBox.dialogBox.Message = playerResponse;
                    dialogBox.dialogBox.fontSize = 15;
                    dialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                dialogBox.EndConfigure();
                sceneRef.AddChild(dialogBox);

                sceneRef.RemoveTemporaryObjects();
            };
            sceneRef.AddChild(puzzleGame);


            ///Tutorial Text
            var tutorialDialogBox = new TextBox(new Vec2(300, 50), 500, 500);
            //tutorialDialogBox.y += 20;
            //tutorialDialogBox.x += 50;
            tutorialDialogBox.Configure(() =>
            {
                tutorialDialogBox.dialogBox.Message = "Click And Drag to solve the Puzzle!";
                tutorialDialogBox.dialogBox.fontSize = 15;
                tutorialDialogBox.dialogBox.color = new Color3(255, 255, 255);
            });
            tutorialDialogBox.EndConfigure();
            sceneRef.AddChild(tutorialDialogBox);



            ///
            ///Adding temporary dialog boxes
            ///
            //var TextMessageButton = new Button("art/Dialog Background.png", 1, 1);
            //TextMessageButton.SetScaleXY(0.7f, 0.7f);
            //TextMessageButton.SetXY(TextMessageButton.width / 2, game.height - TextMessageButton.height / 2);
            //sceneRef.AddChild(TextMessageButton);

            //var tempDialogBox = new TextBox(TextMessageButton, true);
            //tempDialogBox.y -= 28;
            //tempDialogBox.x += 20;
            //tempDialogBox.Configure(() =>
            //{
            //    tempDialogBox.dialogBox.Message = NaratorTextPath;
            //    tempDialogBox.dialogBox.fontSize = 15;
            //    tempDialogBox.dialogBox.color = new Color3(0, 0, 0);
            //});
            //tempDialogBox.EndConfigure();
            //sceneRef.AddTemporaryObject(TextMessageButton);
            //sceneRef.AddTemporaryObject(tempDialogBox);


            //UI Buttons
            AddBackButton(sceneRef);
            AddGallaryButtonPuzzle(sceneRef);
            scoreTextDialog = AddSunflowerTextBox(sceneRef);


            sceneRef.OnSceneOpen = () =>
            {
                //AudioManager.Instance.PlaySound("VoiceoverLines/scene 2");
                Controller.Instance.SetCursor(CursorType.BRUSH);
                AudioManager.Instance.ReduceVolume(0.1f);
            };
        });
    }
    private void CreatePuzzle7Level()
    {
        string NaratorTextPath = "This is a patient from the asylim \nI spent some time with.";
        string playerResponse = "Oh";

        string nextSceneVoiceOver = "VoiceoverLines/scene 16";
        PaintingsIdentifiers solvedPuzzle = PaintingsIdentifiers.Painting6;

        SceneType nextScene = SceneType.Scene12;

        AddLevel(SceneType.Puzzle7, (sceneRef) =>
        {
            TextBox scoreTextDialog = null;

            var puzzleGame = Puzzle.Create(sceneRef, "Art/Puzzles/puzzle7/", 4, 3, new Vec2(277, 73));
            puzzleGame.OnPuzzleSolved = () =>
            {
                AudioManager.Instance.PlayOnce("SoundEffect/positive feedback");
                Controller.Instance.stats.PuzzleSolved(solvedPuzzle);


                Controller.Instance.stats.AddScore(1);
                if (scoreTextDialog != null)
                {
                    scoreTextDialog.UpdateText(Controller.Instance.stats.Score.ToString());
                }

                var button = new Button("art/Dialog Background.png", 1, 1,
                   () => {
                       AudioManager.Instance.PlaySound(nextSceneVoiceOver);
                       Instance.OpenScene(nextScene);

                   });
                button.SetScaleXY(0.5f, 0.5f);
                button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                sceneRef.AddChild(button);

                var dialogBox = new TextBox(button, true);
                dialogBox.x += 20;
                dialogBox.y -= 20;
                dialogBox.Configure(() =>
                {
                    dialogBox.dialogBox.Message = playerResponse;
                    dialogBox.dialogBox.fontSize = 15;
                    dialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                dialogBox.EndConfigure();
                sceneRef.AddChild(dialogBox);

                sceneRef.RemoveTemporaryObjects();
            };
            sceneRef.AddChild(puzzleGame);


            ///Tutorial Text
            var tutorialDialogBox = new TextBox(new Vec2(300, 50), 500, 500);
            //tutorialDialogBox.y += 20;
            //tutorialDialogBox.x += 50;
            tutorialDialogBox.Configure(() =>
            {
                tutorialDialogBox.dialogBox.Message = "Click And Drag to solve the Puzzle!";
                tutorialDialogBox.dialogBox.fontSize = 15;
                tutorialDialogBox.dialogBox.color = new Color3(255, 255, 255);
            });
            tutorialDialogBox.EndConfigure();
            sceneRef.AddChild(tutorialDialogBox);



            ///
            ///Adding temporary dialog boxes
            ///
            var TextMessageButton = new Button("art/Dialog Background.png", 1, 1);
            TextMessageButton.SetScaleXY(0.7f, 0.7f);
            TextMessageButton.SetXY(TextMessageButton.width / 2, game.height - TextMessageButton.height / 2);
            sceneRef.AddChild(TextMessageButton);

            var tempDialogBox = new TextBox(TextMessageButton, true);
            tempDialogBox.y -= 28;
            tempDialogBox.x += 20;
            tempDialogBox.Configure(() =>
            {
                tempDialogBox.dialogBox.Message = NaratorTextPath;
                tempDialogBox.dialogBox.fontSize = 15;
                tempDialogBox.dialogBox.color = new Color3(0, 0, 0);
            });
            tempDialogBox.EndConfigure();
            sceneRef.AddTemporaryObject(TextMessageButton);
            sceneRef.AddTemporaryObject(tempDialogBox);


            //UI Buttons
            AddBackButton(sceneRef);
            AddGallaryButtonPuzzle(sceneRef);
            scoreTextDialog = AddSunflowerTextBox(sceneRef);


            sceneRef.OnSceneOpen = () =>
            {
                //AudioManager.Instance.PlaySound("VoiceoverLines/scene 2");
                Controller.Instance.SetCursor(CursorType.BRUSH);
                AudioManager.Instance.ReduceVolume(0.1f);
            };
        });
    }
    private void CreatePuzzle8Level()
    {
        string NaratorTextPath = "This is a patient from the asylim \nI spent some time with.";
        string playerResponse = "Return";

        string nextSceneVoiceOver = "VoiceoverLines/scene 17";
        PaintingsIdentifiers solvedPuzzle = PaintingsIdentifiers.Painting7;

        SceneType nextScene = SceneType.MainMenu;

        AddLevel(SceneType.Puzzle8, (sceneRef) =>
        {
            TextBox scoreTextDialog = null;

            var puzzleGame = Puzzle.Create(sceneRef, "Art/Puzzles/puzzle1_finish/", 3, 2, new Vec2(20, 153));
            puzzleGame.OnPuzzleSolved = () =>
            {
                AudioManager.Instance.PlayOnce("SoundEffect/positive feedback");
                Controller.Instance.stats.PuzzleSolved(solvedPuzzle);

                AudioManager.Instance.PlaySound(nextSceneVoiceOver);
                Controller.Instance.stats.AddScore(1);
                if (scoreTextDialog != null)
                {
                    scoreTextDialog.UpdateText(Controller.Instance.stats.Score.ToString());
                }

                var button = new Button("art/Dialog Background.png", 1, 1,
                   () => {
                       //AudioManager.Instance.PlaySound(nextSceneVoiceOver);
                       Instance.OpenScene(nextScene);

                   });
                button.SetScaleXY(0.5f, 0.5f);
                button.SetXY(game.width - button.width / 2 - 30, game.height - 70);
                sceneRef.AddChild(button);

                var dialogBox = new TextBox(button, true);
                dialogBox.x += 20;
                dialogBox.y -= 20;
                dialogBox.Configure(() =>
                {
                    dialogBox.dialogBox.Message = playerResponse;
                    dialogBox.dialogBox.fontSize = 15;
                    dialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                dialogBox.EndConfigure();
                sceneRef.AddChild(dialogBox);

                sceneRef.RemoveTemporaryObjects();
                ///
                ///Adding temporary dialog boxes
                ///
                button = new Button("art/Dialog Background.png", 1, 1);
                button.SetScaleXY(0.7f, 0.7f);
                button.SetXY(button.width / 2, game.height - button.height / 2);
                sceneRef.AddChild(button);

                dialogBox = new TextBox(button, true);
                dialogBox.y -= 30;
                dialogBox.x += 20;
                dialogBox.Configure(() =>
                {
                    dialogBox.dialogBox.Message = "Here it is! My last piece it's finished! \nNow people will start realizing why my art was \nso meaningful  Thank you for getting to know me! ";
                    dialogBox.dialogBox.fontSize = 15;
                    dialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                dialogBox.EndConfigure();
                sceneRef.AddTemporaryObject(button);
                sceneRef.AddTemporaryObject(dialogBox);
            };
            sceneRef.AddChild(puzzleGame);


            ///Tutorial Text
            var tutorialDialogBox = new TextBox(new Vec2(300, 50), 500, 500);
            //tutorialDialogBox.y += 20;
            //tutorialDialogBox.x += 50;
            tutorialDialogBox.Configure(() =>
            {
                tutorialDialogBox.dialogBox.Message = "Click And Drag to solve the Puzzle!";
                tutorialDialogBox.dialogBox.fontSize = 15;
                tutorialDialogBox.dialogBox.color = new Color3(255, 255, 255);
            });
            tutorialDialogBox.EndConfigure();
            sceneRef.AddChild(tutorialDialogBox);



            ///
            ///Adding temporary dialog boxes
            ///
            var TextMessageButton = new Button("art/Dialog Background.png", 1, 1);
            TextMessageButton.SetScaleXY(0.7f, 0.7f);
            TextMessageButton.SetXY(TextMessageButton.width / 2, game.height - TextMessageButton.height / 2);
            sceneRef.AddChild(TextMessageButton);

            var tempDialogBox = new TextBox(TextMessageButton, true);
            tempDialogBox.y -= 28;
            tempDialogBox.x += 20;
            tempDialogBox.Configure(() =>
            {
                tempDialogBox.dialogBox.Message = NaratorTextPath;
                tempDialogBox.dialogBox.fontSize = 15;
                tempDialogBox.dialogBox.color = new Color3(0, 0, 0);
            });
            tempDialogBox.EndConfigure();
            sceneRef.AddTemporaryObject(TextMessageButton);
            sceneRef.AddTemporaryObject(tempDialogBox);


            //UI Buttons
            AddBackButton(sceneRef);
            AddGallaryButtonPuzzle(sceneRef);
            scoreTextDialog = AddSunflowerTextBox(sceneRef);


            sceneRef.OnSceneOpen = () =>
            {
                //AudioManager.Instance.PlaySound("VoiceoverLines/scene 2");
                Controller.Instance.SetCursor(CursorType.BRUSH);
                AudioManager.Instance.ReduceVolume(0.1f);
            };
        });
    }
    void AddBackButton(Scene scene)
    {
        var button = new Button("art/BackButton.png", 1, 1,
                   () => {
                       //Instance.OpenLastScene();
                       Instance.OpenScene(SceneType.MainMenu);
                   });
        button.SetXY(button.width / 2 - 30, button.height / 2 - 30);
        scene.AddChild(button);

    }

    void PreviousSceneButton(Scene scene) {
        var button = new Button("art/BackButton.png", 1, 1,
            () => {
                Instance.OpenLastScene();
                //Instance.OpenScene(SceneType.MainMenu);
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
        flowerCount.y += 50;
        flowerCount.Configure(() =>
        {
            flowerCount.dialogBox.Message = Controller.Instance.stats.Score.ToString();
            flowerCount.dialogBox.fontSize = 15;
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

