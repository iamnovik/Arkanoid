using System.Diagnostics;
using System.Numerics;
using System.Xml.Serialization;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Arkanoid;
[XmlInclude(typeof(Platform))]
public class Platform : DispObj
{
    public int speed;
    

    public override void Move()
    {
        int direction = 0;
        if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
        {
            direction = -1;
        }else if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
        {
            direction = 1;
        }
        // Вычисляем новую позицию платформы
        float newPositionLX = leftX + direction * speed;
        float newPositionRX = rightX + direction * speed;
        float newrefX = base.refX + direction * speed;
        // Ограничиваем движение платформы в пределах допустимого диапазона
        if (newPositionLX < 0)
        {
            refX = (rightX + leftX) / 2;
            newPositionLX = 0;
            newPositionRX = rightX - leftX;
        }
        else if (newPositionRX > 1280)
        {
            refX = leftX + (rightX + leftX) / 2;
            newPositionRX = 1280;
            newPositionLX = 1280 - (rightX - leftX);
        }
        // Обновляем позицию платформы
        refX = (int)newrefX;   
        leftX = (int)newPositionLX;
        rightX = (int)newPositionRX;
    }
    private void PlatformCollisionEventHandler(object sender, EventArgs e)
    {
       
        
    }
    public override String Serialize()
    {
        return GetType().Name +'\n'+ leftX +" "+ leftY +" "+ rightX +" "+ rightY +" "+ color.ToInteger() +" "+ isVisible +" "+ dynamic +" "+ speed;
    }

    public override DispObj Deserialize(string str)
    {
        String[] fields = str.Split(" ");
        Platform brick = new Platform(Int32.Parse(fields[0]),Int32.Parse(fields[1]),Int32.Parse(fields[2]),Int32.Parse(fields[3]),new Color(uint.Parse(fields[4])),Boolean.Parse(fields[5]),Boolean.Parse(fields[6]), Int32.Parse(fields[7]) );
        
        return brick;
    }


    public override void Draw(RenderWindow window)
    {
        if (isVisible)
        {
            RectangleShape rect = new RectangleShape(new Vector2f(rightX - leftX, rightY - leftY));
            rect.Position = new Vector2f(leftX, leftY);
            rect.FillColor = color;
            rect.OutlineThickness = 1;
            rect.OutlineColor = Color.Black;
            window.Draw(rect);
        }
    }

    public Platform() 
    {
        // This constructor is required for serialization.
    }
    public Platform(int leftX, int leftY, int rightX, int rightY, Color color, bool isVisible, bool dynamic, int speed) : base(leftX, leftY, rightX, rightY, color, isVisible, dynamic)
    {
        this.speed = speed;
        CollisionEvent += PlatformCollisionEventHandler;
    }
}