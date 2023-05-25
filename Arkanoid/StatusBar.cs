using System.Numerics;
using System.Xml.Serialization;
using SFML.Graphics;
using SFML.System;

namespace Arkanoid;
[XmlInclude(typeof(StatusBar))]
public class StatusBar : DispObj
{
    public Statistics stat;
    public MenuItem menubtn;
    public Label left;
    public Label midLeft;
    public Label mid;
    public Label midright;
    public Label right;

    void Show()
    {
    }

    public override void ChangeCoord(float ScaleX, float ScaleY)
    {
        base.ChangeCoord(ScaleX, ScaleY);
        menubtn.ChangeCoord(ScaleX,ScaleY);
        left.ChangeCoord(ScaleX,ScaleY);
        midLeft.ChangeCoord(ScaleX,ScaleY);
        mid.ChangeCoord(ScaleX,ScaleY);
        midright.ChangeCoord(ScaleX,ScaleY);
        right.ChangeCoord(ScaleX,ScaleY);
    }

    void Hide(){}

    public override String Serialize()
    {
        String temp = GetType().Name +'\n'+ leftX +" "+ leftY +" "+ rightX +" "+ rightY +" "+ color.ToInteger() +" "+ isVisible +" "+ dynamic;
        return temp;
    }

    public override DispObj Deserialize(string str)
    {
        String[] fields = str.Split(" ");
        StatusBar brick = new StatusBar(Int32.Parse(fields[0]),Int32.Parse(fields[1]),Int32.Parse(fields[2]),Int32.Parse(fields[3]),new Color(uint.Parse(fields[4])),Boolean.Parse(fields[5]),Boolean.Parse(fields[6]) );
        
        return brick;
    }

    public override void Draw(RenderWindow window)
    {
        
            RectangleShape rect = new RectangleShape(new Vector2f(rightX - leftX, rightY - leftY));
            rect.Position = new Vector2f(leftX, leftY);
            rect.FillColor = color;
            rect.OutlineThickness = 1;
            rect.OutlineColor = Color.Black;
            window.Draw(rect);
            menubtn.Draw(window);
            left.Draw(window);
            midLeft.Draw(window);
            mid.Draw(window);
            midright.Draw(window);
            right.Draw(window);
    }

    public StatusBar() : base()
    {
        
    }
    public StatusBar(int leftX, int leftY, int rightX, int rightY, Color color, bool isVisible, bool dynamic) : base(leftX, leftY, rightX, rightY, color, isVisible, dynamic)
    {
        
        menubtn = new MenuItem(rightX - (base.rightX / 7), base.leftY + (rightY - leftY) / 10,
            base.rightX - rightX / 15, base.rightY - (rightY - leftY) / 10,
            Color.Red, true, false, "Menu", 0, "Intro.otf",25);
        left = new Label(leftX, leftY, leftX + 150, rightY + 50, Color.Red, true, false, "Score:", 25, "Intro.otf");
        midLeft = new Label(base.leftX + 200, base.leftY, base.leftX + 350, base.leftY + 50, Color.Red, true, false,
            "Level:", 25, "Intro.otf");
        mid = new Label(base.leftX + 400, base.leftY, base.leftX + 550, base.leftY + 50, Color.Red, true, false,
            "Player1", 25, "Intro.otf");
        midright = new Label(base.leftX + 600, base.leftY, base.leftX + 750, base.leftY + 50, Color.Red, true, false,
            "PLAYER1", 25, "Intro.otf");
        right = new Label(base.leftX + 800, base.leftY, base.leftX + 950, base.leftY + 50, Color.Red, true, false,
            "Best Score:", 25, "Intro.otf");
    }


   
}