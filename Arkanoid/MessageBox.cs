using System.Numerics;
using System.Xml.Serialization;
using SFML.Graphics;
using SFML.System;

namespace Arkanoid;

public class MessageBox : DispObj
{
    public int id;
    public Label text;
    public MenuItem button;
    public String font;
    public void show()
    {
        
    }

    public override void Draw(RenderWindow window)
    {
        RectangleShape rect = new RectangleShape(new Vector2f(rightX - leftX, rightY - leftY));
        rect.Position = new Vector2f(leftX, leftY);
        rect.FillColor = color;
        rect.OutlineThickness = 1;
        rect.OutlineColor = Color.Black;
        window.Draw(rect);
        button.Draw(window);
        text.Draw(window);
            
    }

    public override void ChangeCoord(float ScaleX, float ScaleY)
    {
        base.ChangeCoord(ScaleX, ScaleY);
        text.ChangeCoord(ScaleX,ScaleY);
        button.ChangeCoord(ScaleX,ScaleY);
    }

    public override string Serialize()
    {
        throw new NotImplementedException();
    }

    public override DispObj Deserialize(string str)
    {
        throw new NotImplementedException();
    }

    void setinfo()
    {
        
    }
    public MessageBox() : base()
    {
        
    }
    public MessageBox(int leftX, int leftY, int rightX, int rightY, Color color, bool isVisible, bool dynamic,String text, int id, String font, uint Size) : base(leftX, leftY, rightX, rightY, color, isVisible, dynamic)
    {
        
        this.font = font;
        this.id = id;
        this.button = new MenuItem(575, 350, 675, 400, Color.Black, base.isVisible, base.dynamic,"OK",0,"Intro.otf",10);
        this.text = new Label(530, 200, 800, 400, Color.White, base.isVisible, base.dynamic,
            text, 30,font);
        

    }
}