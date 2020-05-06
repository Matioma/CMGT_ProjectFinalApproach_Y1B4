using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using GXPEngine;

public class SceneManager : GameObject
{
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
        CurrentLevel = levels[key];
        AddChildAt(CurrentLevel, 0);
    }
    
    void LoadLevel() {
        scenes.Add(new Scene(0));
        scenes.Add(new Scene(1));
        scenes.Add(new Scene(2));

        levels.Add("MainMenu",scenes[0]);
        levels.Add("Level1", scenes[1]);
        levels.Add("Level2", scenes[2]);
    }



}

