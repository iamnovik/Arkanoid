using System;
using System.Numerics;
using System.Xml.Serialization;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Arkanoid;
[XmlInclude(typeof(Button))]
public class Button : DispObj
{
    
    public bool active;
    public event EventHandler<EventArgs> ButtonClick;
    public override String Serialize()
    {
        return GetType().Name+'\n'+leftX +" "+ leftY +" "+ rightX +" "+ rightY +" "+ color.ToInteger() +" "+ isVisible +" "+ dynamic;
    }

    public override DispObj Deserialize(string str)
    {
        String[] fields = str.Split(" ");
        Button brick = new Button(Int32.Parse(fields[0]),Int32.Parse(fields[1]),Int32.Parse(fields[2]),Int32.Parse(fields[3]),new Color(uint.Parse(fields[4])),Boolean.Parse(fields[5]),Boolean.Parse(fields[6]) );
        
        return brick;
    }

    public override void Draw(RenderWindow window)
    {
        
        RectangleShape rect = new RectangleShape(new Vector2f(rightX - leftX, rightY - leftY));
        rect.Position = new Vector2f(leftX, leftY);
        rect.FillColor = color;
        rect.OutlineThickness = 1;
        //rect.OutlineColor = Color.Black;
        window.Draw(rect);
        

    }

    public bool OnClick(RenderWindow window)
    {
        if (Mouse.IsButtonPressed(Mouse.Button.Left) && active)
        {
            
            var position = Mouse.GetPosition(window);
            Vector2f scalingFactors = new Vector2f(
                (float)1280/window.Size.X,
                (float)720/window.Size.Y
            );
            Vector2f newMousePosition = new Vector2f(
                position.X * scalingFactors.X,
                position.Y * scalingFactors.Y
            );
          
            if (newMousePosition.X >= leftX && newMousePosition.X <= rightX && newMousePosition.Y >= leftY && newMousePosition.Y <= rightY)
            {
                
                ButtonClick?.Invoke(this, EventArgs.Empty);
                return true;
                // выполнение нужных действий при нажатии на кнопку
            }
        
        }
        
        return false;
    }

    public Button()
    {
        
    }

   
    public Button(int leftX, int leftY, int rightX, int rightY, Color color, bool isVisible, bool dynamic) : base(leftX, leftY, rightX, rightY, color, isVisible, dynamic)
    {
        active = true;

    }


 
}