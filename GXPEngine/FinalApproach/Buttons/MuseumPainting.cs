using GXPEngine;
using System;
using System.Linq;

public enum PaintingsIdentifiers { 
    Painting1,
    Painting2,
    Painting3,
    Painting4,
    Painting5,
    Painting6,
    Painting7
}
public class MuseumPainting:HUDElement
{
    bool _isAvailable=false;
    public bool IsAvailable {
        get { return _isAvailable; }
        set {
            if (value)
            {
                SetFrame(1);
            }
            else {
                SetFrame(0);
            }
            _isAvailable = value; 
        }
    }


    PaintingsIdentifiers _paintingIdentifier;


    public MuseumPainting(string path, int cols, int rows, PaintingsIdentifiers paintingIdentifier) : base(path, cols, rows) {
        IsAvailable = false;
        _paintingIdentifier = paintingIdentifier;
        Controller.Instance.stats.AddPainting(paintingIdentifier, this);
    }
    public override void OnClick()
    {
        if (IsAvailable) {
            Console.WriteLine("Clicked");

            foreach(var child in parent.GetChildren()) {
                if (child is MuseumUI) {
                    MuseumUI museumUI = child as MuseumUI;
                    museumUI.RemoveChildren();

                    AddText(child as MuseumUI);
                }
            }
        }
    }
    public override void OnHover()
    {
        Console.WriteLine("Hovered");
    }

    public override void OnHoverEnd()
    {
        Console.WriteLine("Hover end");
    }

    public override void OnClickRelease()
    {
        Console.WriteLine("On CLick release");
    }

    public override void OnClickPressed()
    {
        Console.WriteLine("On CLick pressed");
    }


    public void AddText(MuseumUI scene) {
        switch (_paintingIdentifier) {
            case PaintingsIdentifiers.Painting1:
                AnimationSprite paintingSprite = new AnimationSprite("art/GallaryPaintings/painting2.png", 1, 1);
                paintingSprite.SetOrigin(paintingSprite.width / 2, paintingSprite.height / 2);
                paintingSprite.SetScaleXY(0.8f, 0.8f);
                paintingSprite.SetXY(game.width / 2 + paintingSprite.width / 2 + 30, paintingSprite.height / 2 - 20);
                scene.AddChild(paintingSprite);
                break;
            case PaintingsIdentifiers.Painting2:
                paintingSprite = new AnimationSprite("art/GallaryPaintings/painting3.png", 1, 1);
                paintingSprite.SetOrigin(paintingSprite.width / 2, paintingSprite.height / 2);
                paintingSprite.SetScaleXY(0.8f, 0.8f);
                paintingSprite.SetXY(game.width / 2 + paintingSprite.width / 2 + 30, paintingSprite.height / 2 - 20);
                scene.AddChild(paintingSprite);
                break;
            case PaintingsIdentifiers.Painting3:
                paintingSprite = new AnimationSprite("art/GallaryPaintings/painting5.png", 1, 1);
                paintingSprite.SetOrigin(paintingSprite.width / 2, paintingSprite.height / 2);
                paintingSprite.SetScaleXY(0.8f, 0.8f);
                paintingSprite.SetXY(game.width / 2 + paintingSprite.width / 2 + 30, paintingSprite.height / 2 - 20);
                scene.AddChild(paintingSprite);
                break;
            case PaintingsIdentifiers.Painting4:
                paintingSprite = new AnimationSprite("art/GallaryPaintings/painting4.png", 1, 1);
                paintingSprite.SetOrigin(paintingSprite.width / 2, paintingSprite.height / 2);
                paintingSprite.SetScaleXY(0.8f, 0.8f);
                paintingSprite.SetXY(game.width / 2 + paintingSprite.width / 2 + 30, paintingSprite.height / 2 - 20);
                scene.AddChild(paintingSprite);
                break;
            case PaintingsIdentifiers.Painting5:
                paintingSprite = new AnimationSprite("art/GallaryPaintings/painting6.png", 1, 1);
                paintingSprite.SetOrigin(paintingSprite.width / 2, paintingSprite.height / 2);
                paintingSprite.SetScaleXY(0.8f, 0.8f);
                paintingSprite.SetXY(game.width / 2 + paintingSprite.width / 2 + 30, paintingSprite.height / 2 - 20);
                scene.AddChild(paintingSprite);
                break;
            case PaintingsIdentifiers.Painting6:
                paintingSprite = new AnimationSprite("art/GallaryPaintings/painting7.png", 1, 1);
                paintingSprite.SetOrigin(paintingSprite.width / 2, paintingSprite.height / 2);
                paintingSprite.SetScaleXY(0.8f, 0.8f);
                paintingSprite.SetXY(game.width / 2 + paintingSprite.width / 2 + 30, paintingSprite.height / 2 - 20);
                scene.AddChild(paintingSprite);
                break;
            case PaintingsIdentifiers.Painting7:
                paintingSprite = new AnimationSprite("art/GallaryPaintings/painting3.png", 1, 1);
                paintingSprite.SetOrigin(paintingSprite.width / 2, paintingSprite.height / 2);
                paintingSprite.SetScaleXY(0.8f, 0.8f);
                paintingSprite.SetXY(game.width / 2 + paintingSprite.width / 2 + 30, paintingSprite.height / 2 - 20);
                scene.AddChild(paintingSprite);
                break;
        }

        var animationSprite = new AnimationSprite("art/frame gallery.png", 1,1);
        animationSprite.SetOrigin(animationSprite.width / 2, animationSprite.height / 2);
        
        animationSprite.width = game.width / 2 +30;
        animationSprite.height = game.height / 2+30;
        animationSprite.SetXY(game.width / 2 + animationSprite.width / 2 -15, animationSprite.height / 2 - 20);
        scene.AddChild(animationSprite);


        var TextMessageButton = new Button("art/BigTextBoxBG.png", 1, 1);
        TextMessageButton.width = game.width / 2 + 10;
        TextMessageButton.height = game.height / 2 + 10;
        TextMessageButton.SetXY(game.width / 2 + TextMessageButton.width / 2, game.height / 2 + TextMessageButton.height / 2 - 20);
        scene.AddChild(TextMessageButton);

        //Adding text
        switch (_paintingIdentifier)
        {
            case PaintingsIdentifiers.Painting1:
                var tempDialogBox = new TextBox(TextMessageButton, true);
                tempDialogBox.y -= 50;
                tempDialogBox.Configure(() =>
                {
                    tempDialogBox.dialogBox.Message = "Women on the Peat Moor\n" +
                                    "Vincent van Gogh (1853 - 1890),\nAuvers-sur-Oise, July 1890 oil on canvas,\n50.5 cm x 103 cm\n\n" +
                                    "Van Gogh was fascinated with the simple country\n life. He had already spent more than two\n" +
                                    " years trying to 'examine and draw everything\n that's part of a peasant's life', he wrote.";
                    tempDialogBox.dialogBox.fontSize = 15;
                    tempDialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                tempDialogBox.EndConfigure();
                scene.AddChild(tempDialogBox);
                break;

            case PaintingsIdentifiers.Painting2:
                tempDialogBox = new TextBox(TextMessageButton, true);
                tempDialogBox.y -= 50;
                tempDialogBox.Configure(() =>
                {
                    tempDialogBox.dialogBox.Message = "Still Life with Bible\n"+
                                        "Vincent van Gogh(1853 - 1890),\nNuenen, October 1885" +
                                        "oil on canvas,\n65.7 cm x 78.5 cm" +
                                        "\n\nThat book was a kind of 'bible' for modern life.\nThe books symbolize the different worldviews\nof Van Gogh and his father.";

                                        //"Wheatfield with Crows\n" +
                                        //"Vincentvan Gogh(1853 - 1890), \nNieuw Amsterdam, October 1883 oil on canvas,\n27.8 cm x 36.5 cm\n\n" +
                                        //"Wheatfield with Crows is one of Van Gogh's\n most famous paintings. It is often claimed\n that this was his very last work.";

                    tempDialogBox.dialogBox.fontSize = 15;
                    tempDialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                tempDialogBox.EndConfigure();
                scene.AddChild(tempDialogBox);
                break;
            case PaintingsIdentifiers.Painting3:
                tempDialogBox = new TextBox(TextMessageButton, true);
                tempDialogBox.y -= 50;
                tempDialogBox.Configure(() =>
                {
                    tempDialogBox.dialogBox.Message = "Houses Seen from the Back\n" +
                                    "Vincent van Gogh(1853 - 1890), \nntwerp, December 1885 - February 1886"+
                                    "oil on\n canvas,43.7 cm x 33.7 cm\n\n" +
                                    "Van Gogh himself lived in such an area: \nhe rented a small room at Lange\nBeeldekensstraat 194. This is the rear view \nof that house.";

                    tempDialogBox.dialogBox.fontSize = 15;
                    tempDialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                tempDialogBox.EndConfigure();
                scene.AddChild(tempDialogBox);
                break;
            case PaintingsIdentifiers.Painting4:
                tempDialogBox = new TextBox(TextMessageButton, true);
                tempDialogBox.y -= 50;
                tempDialogBox.Configure(() =>
                {
                    tempDialogBox.dialogBox.Message = "Portrait of Theo van Gogh\n" +
                                    "Vincentvan Gogh(1853 - 1890), \nParis, Summer 1887 oil on cardboard,\n19.0 cm x 14.1 cm\n\n" +
                                    "This painting was long thought to be a self-\nportrait of Vincent; however, another view is \nthat it shows his brother Theo. The portrait is \nunusually small and painted in considerable \ndetail. ";


                    tempDialogBox.dialogBox.fontSize = 15;
                    tempDialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                tempDialogBox.EndConfigure();
                scene.AddChild(tempDialogBox);
                break;
            case PaintingsIdentifiers.Painting5:
                tempDialogBox = new TextBox(TextMessageButton, true);
                tempDialogBox.y -= 100;
                tempDialogBox.Configure(() =>
                {
                    tempDialogBox.dialogBox.Message = " Sunflowers\n" +
                                    "Vincentvan Gogh(1853 - 1890), \nArles, January 1889 oil on canvas,\n 95 cm x 73 cm\n\n" +
                                    "Van Gogh’s paintings of Sunflowers are\namong his most famous.Vincent painted a \ntotal of five'+" +
                                    "large canvases with sunflowers \nin a vase, with three shades of yellow \n‘and nothing else’. In this way, " +
                                    " he \ndemonstrated that it was possible to create an \nimage with numerous variations of a single color.";

                   tempDialogBox.dialogBox.fontSize = 15;
                    tempDialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                tempDialogBox.EndConfigure();
                scene.AddChild(tempDialogBox);
                break;
            case PaintingsIdentifiers.Painting6:
                tempDialogBox = new TextBox(TextMessageButton, true);
                tempDialogBox.y -= 70;
                tempDialogBox.Configure(() =>
                {
                    tempDialogBox.dialogBox.Message = " Portrait of a Man\n" +
                                    "Vincentvan Gogh(1853 - 1890), \nSaint - Rémy - de - Provence, October 1889 \noil on canvas,32.2 cm x 23.3 cm\n\n" +
                                    "This man was a fellow-patient at the \npsychiatric clinic in Saint-Rémy-de-Provence.\nVan Gogh wrote to his mother, " +
                                    "‘It is strange \nthat when one is with them for some time and \nis used to them, one no longer thinks \nabout their being mad.’";
                  
                    tempDialogBox.dialogBox.fontSize = 15;
                    tempDialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                tempDialogBox.EndConfigure();
                scene.AddChild(tempDialogBox);
                break;
            case PaintingsIdentifiers.Painting7:
                tempDialogBox = new TextBox(TextMessageButton, true);
                tempDialogBox.y -= 50;
                tempDialogBox.Configure(() =>
                {
                    tempDialogBox.dialogBox.Message = "Wheatfield with Crows\n" +
                                    "Vincentvan Gogh(1853 - 1890), \nNieuw Amsterdam, October 1883 oil on canvas,\n27.8 cm x 36.5 cm\n\n" +
                                    "Wheatfield with Crows is one of Van Gogh's\n most famous paintings. It is often claimed\n that this was his very last work.";



                    tempDialogBox.dialogBox.fontSize = 15;
                    tempDialogBox.dialogBox.color = new Color3(0, 0, 0);
                });
                tempDialogBox.EndConfigure();
                scene.AddChild(tempDialogBox);
                break;
        }


        var textBox = TutorialText();
        var closeButton = new Button("art/CloseButton.png", 1, 1, 
            ()=> {
                scene.RemoveChildren();
                scene.parent.GetChildren().Last().LateRemove();

               
                if (textBox != null) {
                    textBox.visible = true;
                }

            });
        closeButton.SetXY(game.width / 2 + closeButton.width / 2 - closeButton.width, closeButton.height / 2 );
        scene.parent.AddChild(closeButton);

        if (textBox != null)
        {
            textBox.visible = false;
        }
    }


    TextBox TutorialText() {
        foreach (var child in parent.GetChildren())
        {
            if (child is TextBox)
            {
                return child as TextBox;
            }
        }
        
        return null;

    }
}
