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
                var backgroundsample = new AnimationSprite("art/Background1.png", 1, 1);
                backgroundsample.SetOrigin(backgroundsample.width / 2, backgroundsample.height / 2);
                backgroundsample.SetXY(game.width / 2, game.height / 2);
                sceneRef.AddChild(backgroundsample);

                var imageLayer = new AnimationSprite("art/paintStand.png", 1, 1);
                imageLayer.SetOrigin(imageLayer.width / 2, imageLayer.height / 2);
                imageLayer.SetXY(300, 450);
                sceneRef.AddChild(imageLayer);

                var character = new Character("art/vangoghpainting.png", 6, 1);
                character.SetXY(300, 450);
                sceneRef.AddChild(character);

              

                var foreGround = new AnimationSprite("art/ForeGround1.png", 1, 1);
                foreGround.SetOrigin(backgroundsample.width / 2, backgroundsample.height / 2);
                foreGround.SetXY(game.width / 2, game.height / 2);
                sceneRef.AddChild(foreGround);

                var dialogBox = new TextDialogBox("Example", 500,300,120,200,0);
                sceneRef.AddChild(dialogBox);

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

            var background = new Sprite("Art/Puzzles/Puzzle1/puzzle1_background.png");
            background.SetOrigin(background.width / 2, background.height / 2);
            background.SetXY(game.width / 2, game.height / 2);
            sceneRef.AddChild(background);



            var dictionary = new Dictionary<DraggableElement, Vec2>();


            List<PuzzleElement> puzzleElements = new List<PuzzleElement>();
            Vec2[] coordinates ={
                new Vec2(218.50f,166.00f),
                new Vec2(361.50f,166.50f),
                new Vec2(166.00f,166.00f),
                new Vec2(665.00f,153.00f),
                new Vec2(808.50f,165.50f),

                new Vec2(136,218),
                new Vec2(276,218),
                new Vec2(412,215),
                new Vec2(572,217),
                new Vec2(711,216),

                new Vec2(135,366),
                new Vec2(252,365),
                new Vec2(423,360),
                new Vec2(559,360),
                new Vec2(728,360),

                new Vec2(135,508),
                new Vec2(273,504),
                new Vec2(409,502),
                new Vec2(575,500),
                new Vec2(707,503)
            };


            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 5; j++) {

                    string fileName = "Art/Puzzles/Puzzle1/" + (i * 5 + j + 1) + ".png";
                    var test = new PuzzleElement(fileName, 1, 1);
                    puzzleElements.Add(test);

                    Vec2 position = coordinates[i * 5 + j];
                    test.SetXY(position.x, position.y);

                    //test.SetXY(155 + (game.width-155*2)/ 5 * j +test.width/2, 132+(game.height-132*2)/ 4 * i+ test.height / 2);
                    //test.SetXY(random.Next(155, game.width - 155), random.Next(132, game.height - 132));

                    sceneRef.AddChild(test);
                    //Vec2 slotPosition = new Vec2();
                    //slotPosition.x =155 + (game.width - 155 * 2) / 5 * j + test.width / 2;
                    //slotPosition.y = 132 + (game.height - 132 * 2) / 4 * i + test.height / 2;

                    //dictionary.Add(test, slotPosition);
                }
            }
            //dictionary.Add();



            //var puzzleElement = new PuzzleElement("art/Button.jpg", 2, 1);
            //puzzleElement.SetXY(180, 180);
            //sceneRef.AddChild(puzzleElement);






            var puzzleGame = new Puzzle(dictionary);
            puzzleGame.OnPuzzleSolved = () =>
            {
                Instance.OpenScene("Gallery");
            };
            AddChild(puzzleGame);
        });
    }
    void CreateMainMenu() {
        AddLevel("MainMenu",
           (sceneRef) => {
               var button = new Button("art/Button.jpg", 2, 1,
                  () =>
                  {
                      Instance.OpenScene("Scene1");
                  });
               button.CreateText("Visit");
               button.SetupText(() => {
                   button.textobject.fontSize = 15;
                   button.textobject.color = new Color3(0, 255, 0);
                   button.textobject.textRotation = 0;
               });
               button.SetXY(250, 120);
               sceneRef.AddChild(button);



               button = new Button("art/Button.jpg", 2, 1,
                    () =>
                    {
                        Instance.OpenScene("Gallery");
                    });
               button.CreateText("Galary");
               button.SetupText(() => {
                   button.textobject.fontSize = 15;
                   button.textobject.color = new Color3(0, 255, 0);
                   button.textobject.textRotation = 0;
               });
               button.SetXY(250, 320);
               sceneRef.AddChild(button);



               button = new Button("art/Button.jpg", 2, 1,
                   () =>
                   {
                       System.Diagnostics.Process.Start("https://www.vangoghmuseum.nl/"); //Opens the Link
                });
               button.CreateText("Museum");
               button.SetupText(() => {
                   button.textobject.fontSize = 15;
                   button.textobject.color = new Color3(0, 255, 0);
                   button.textobject.textRotation = 0;
               });
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


                var textBox = new Button("art/transparent.png", 1, 1,
                () =>
                {
                });
                textBox.CreateText("I have asdae \n test asdfasr sda \n sdf aer sdf ae \n test srseffa fase \n asdfaesf\n asdf sdfasdf\n");
                textBox.SetupText(() => {
                    textBox.textobject.fontSize = 10;
                    textBox.textobject.color = new Color3(0, 255, 0);
                    textBox.textobject.textRotation = 0;
                });

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

