﻿using GXPEngine;
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
               var draggableElement = new DraggableElement("art/MuseumMap.jpg", 1, 1);
               draggableElement.width *= 2;
               draggableElement.height *= 2;
               sceneRef.AddChild(draggableElement);

               var button = new Button("art/Button.jpg", 2, 1,
                   () => {
                       Instance.OpenScene("MainMenu");
                       Controller.Instance.SetCursor(CursorType.BRUSH);
                       //Controller.Instance.SwitchCursor();
                   });
               button.CreateText("Random");
               button.SetXY(250, 120);
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
                    });
                button.width = 120;
                button.height = 200;
                button.SetXY(350, game.height - 250);
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



                var button = new Button("art/Dialog Background.png", 1, 1,
                    () => {
                        Instance.OpenScene("Puzzle");
                        Controller.Instance.SetCursor(CursorType.BRUSH);
                    });
                button.SetScaleXY(0.5f, 0.5f);
                button.SetXY(250, game.height - 120);
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

            });
    }
    private void CreatePuzzle1Level()
    {
        AddLevel("Puzzle", (sceneRef) =>
        {
            var puzzleGame = Puzzle.Create(sceneRef, "Art/Puzzles/puzzle1_start/", 3, 2, new Vec2(20, 153));
            puzzleGame.OnPuzzleSolved = () =>
            {
                var button = new Button("art/Button.jpg", 2, 1,
                 () =>
                 {
                     Instance.OpenScene("Gallery");
                     Controller.Instance.SetCursor(CursorType.HAND);
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
    void AddLevel(string Name , ActionBuilder sceneDataActions) {
        Scene scene = new Scene();
        sceneDataActions.Invoke(scene);

        levels.Add(Name, scene);
        scenes.Add(scene);
    }



}

