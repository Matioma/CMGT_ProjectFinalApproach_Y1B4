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

    public static readonly SceneManager SceneManger;
    public MyGame() : base(1024, 768, false)		// Create a window that's 800x600 and NOT fullscreen
    {
        AddChild(new SceneManager());
        //AddChild(new Scene());


        //var button = new MenuButton("art/Button.jpg", 2, 1);
        //button.SetXY(250, 120);



        //AddChild(button);
        //AddChild(new Controller());
    }

    void Update()
    {
    }
    static void Main()							// Main() is the first method that's called when the program is run
    {
        new MyGame().Start();					// Create a "MyGame" and start it
    }

}