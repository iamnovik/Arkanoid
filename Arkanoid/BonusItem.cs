using System;
using System.Numerics;
using System.Xml.Serialization;
using SFML.Audio;
using SFML.Graphics;
using System.Timers;
using SFML.System;
using Timer = System.Timers.Timer;


namespace Arkanoid;
[XmlInclude(typeof(BonusItem))]
public class BonusItem : DispObj
{
    public Label Points;
    public Label label;
    public int type;
    public override String Serialize()
    {
        return GetType().Name +'\n'+ leftX +" "+ leftY +" "+ rightX +" "+ rightY +" "+ color.ToInteger() +" "+ isVisible +" "+ dynamic;
    }
    public override DispObj Deserialize(string str)
    {
        String[] fields = str.Split(" ");
        BonusItem bonusitem = new BonusItem(Int32.Parse(fields[0]),Int32.Parse(fields[1]),Int32.Parse(fields[2]),Int32.Parse(fields[3]),new Color(uint.Parse(fields[4])),Boolean.Parse(fields[5]),Boolean.Parse(fields[6]) );
        return bonusitem;
    }
    public override void Draw(RenderWindow window)
    {
        if (Points == null)
        {
            Points = new Label(leftX, (refY + leftY) / 2 + 50, base.rightX, base.rightY + 50, Color.Black, base.isVisible,
                base.dynamic,
                "10", 25, "Intro.otf");
        }
        CircleShape circle = new CircleShape((float)(refX - leftX)*3/4);
        circle.Position = new Vector2f(leftX, leftY);
        circle.FillColor = color;
        circle.OutlineThickness = 1;
        circle.OutlineColor = Color.Black;
        window.Draw(circle);
        label.Draw(window);
        
            Points.Draw(window);

        }

    public override bool Collision(DispObj obj1)
    {
        
        if(rightY >= obj1.leftY && leftY <= obj1.rightY)
            if (rightX >= obj1.leftX && leftX <= obj1.rightX)
            {
                return base.Collision(this);
            }
        return false;
    }

    public BonusItem() 
    {
        
      
    }
    public override void Move()
    {
        if (Points == null)
        {
            Points = new Label(leftX, (refY + leftY) / 2 + 50, base.rightX, base.rightY + 50, Color.Black, base.isVisible,
                base.dynamic,
                "10", 25, "Intro.otf");
        }
        refY += 7;
        leftY += 7;
        rightY += 7;
        label.refY += 7;
        label.leftY += 7;
        label.rightY += 7;
        Points.refY += 7;
        Points.rightY += 7;
        Points.leftY += 7;
        if (leftY >= 720)
        {
            isVisible = false;
        }
    }

    public static Timer timer;
 
 
    public BonusItem(int leftX, int leftY, int rightX, int rightY, Color color, bool isVisible, bool dynamic) : base(leftX, leftY, rightX, rightY, color, isVisible, dynamic)
    {
        Points = new Label(leftX, (refY + leftY) / 2 + 50, base.rightX, base.rightY + 50, Color.Black, base.isVisible,
            base.dynamic,
            "10", 25, "Intro.otf");
        type = new Random().Next(0, 3);
        switch (type)
        {
                case 0:
                {
                    this.color = Color.Yellow;
                    label = new Label(leftX, (refY + leftY)/2, base.rightX, base.rightY, Color.Black, base.isVisible, base.dynamic,
                        "Speed\n    10", 10,"Intro.otf");
                    break;
                }
                case 1:
                {
                    this.color = Color.Red;
                    label = new Label(leftX, (refY + leftY)/2, base.rightX, base.rightY, Color.Black, base.isVisible, base.dynamic,
                        "100\n      10", 10,"Intro.otf");
                    break;
                }
                case 2:
                {
                    this.color = Color.Green;
                    label = new Label(leftX, (refY + leftY)/2, base.rightX, base.rightY, Color.Black, base.isVisible, base.dynamic,
                        "LEVEL\n    10", 10,"Intro.otf");
                    break;
                }
                case 3:
                {
                    this.color = Color.Black;
                    label = new Label(leftX, (refY + leftY)/2, base.rightX, base.rightY, Color.White, base.isVisible, base.dynamic,
                        "Speed\n    10", 10,"Intro.otf");
                    break;
                }
        }
       
    }
}