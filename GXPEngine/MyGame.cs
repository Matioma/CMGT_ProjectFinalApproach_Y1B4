using System;                                   // System contains a lot of default C# libraries 
using System.Configuration;
using System.IO;
using System.Runtime.Remoting.Activation;
using System.Xml.Serialization;
using GXPEngine;								// GXPEngine contains the engine
using GXPEngine.Core;
using TiledMapParser;
public class MyGame : Game
{

    public static SceneManager sceneManger { get;private set; }
    public MyGame() : base(1024, 768, false)		// Create a window that's 800x600 and NOT fullscreen
    {
        sceneManger = new SceneManager();

    
        AddChild(sceneManger);
    }

    void Update()
    {
    }
    static void Main()							// Main() is the first method that's called when the program is run
    {
        new MyGame().Start();					// Create a "MyGame" and start it
    }

}