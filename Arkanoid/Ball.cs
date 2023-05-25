
using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Xml.Serialization;
using Microsoft.VisualBasic.CompilerServices;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using Color = SFML.Graphics.Color;

namespace Arkanoid;
[XmlInclude(typeof(Ball))]
public class Ball:DispObj
{
    public float soundlevel;
    public event EventHandler<EventArgs> SoundChange;
    private Sound sound;
    public bool inGame = true;
    public double radius;
    public float direction;
    public float speed;
    public float getSpeed() {
        return speed;
    }
    public  void setSpeed(int coordinate) {
        this.speed = coordinate;
    }
    public float getDirection() {
        return direction;
    }
    public void setDirection(float coordinate) {
        this.direction=coordinate;
    }

    public override void ChangeDirection(DispObj obj)
    {
       
        if (leftY > obj.leftY && rightY < obj.rightY )
        {
          
            setDirection((float)(Math.PI) - getDirection());
        }
        else
        {
           
            setDirection(2 * (float)Math.PI-getDirection());
        }
       
       
    }

    public override void ChangeCoord(float ScaleX, float ScaleY)
    {
        base.ChangeCoord(ScaleX, ScaleY);
        radius = this.radius * ScaleX;
    }

    private void BallCollisionEventHandler(object sender, EventArgs e)
    {
        if (sound.Volume == 0f)
        {
            
            sound.Volume = soundlevel;
          
        }
        sound.Play();
        if (sender is DispObj obj)
        {
            if (leftY >= obj.leftY && rightY <= obj.rightY )
            {
                if (leftX <= obj.leftX)
                {
                    rightX = obj.leftX;
                    leftX = rightX - 2 * (int)radius;
                    refX = rightX - (int)radius;
                }
                else
                {
                    leftX = obj.rightX;
                    rightX = leftX + 2 * (int)radius;
                    refX = rightX - (int)radius;
                }
                setDirection((float)(Math.PI) - getDirection());
            }
            else
            {
                if (leftY <= obj.leftY)
                {
                    rightY = obj.leftY;
                    leftY = rightY - 2 * (int)radius;
                    refY = leftY + (int)radius;
                }
                else
                {
                    leftY = obj.rightY;
                    rightY = leftY + 2 * (int)radius;
                    refY = leftY + (int)radius;
                }
                setDirection(2 * (float)Math.PI-getDirection());
            } 
        }
       
        
    }

    public void SetVolume(float volume)
    {
        soundlevel = volume;
        sound.Volume = soundlevel;

    }






    public override void Move() {
        if (leftY >= 720)
        {
            inGame = false;
        }
        if (leftX<=0)
        {
            sound.Play();
            setDirection((float)(Math.PI) - getDirection());
            leftX = 0;
            refX = leftX + (int)radius;
            rightX = (int)(2 * radius);
        }else{
            if (rightX>=1280)
            {
                leftX = 1280 - (int)(2 * radius);
                refX = leftX + (int)radius;
                rightX = 1280;
               sound.Play();
                setDirection((float)(Math.PI) - getDirection());
                
            }
            else {
                if (leftY<=70)
                {
                    leftY = 70;
                    refY = leftY + (int)radius;
                    rightY = leftY + (int)(2 * radius);
                    sound.Play();   
                    setDirection( - getDirection());
                }
            }
        }
        float dx = (float) Math.Cos(direction) * speed;
        float dy = (float) Math.Sin(direction) * speed;
        setLX((int)(getLX() + dx));
    setRX((int)(getRX() + dx));
    setLY((int)(getLY() + dy));
    setRY((int)(getRY() + dy));
    setrefX((int)(getrefX() + dx));
    setrefY((int)(getrefY() + dy));
    }

    public override String Serialize()
    {
        return GetType().Name +'\n' + refX +" "+ refY +" "+ radius +" "+ color.ToInteger() +" "+ isVisible +" "+ dynamic +" "+ speed +" "+ direction;
       
    }

    public override Ball Deserialize(String str)
    {
        
        String[] fields = str.Split(" ");
        Ball ball = new Ball(Int32.Parse(fields[0]),Int32.Parse(fields[1]), Int32.Parse(fields[2]),new Color(uint.Parse(fields[3])),Boolean.Parse(fields[4]),Boolean.Parse(fields[5]),Int32.Parse(fields[6]),float.Parse(fields[7]));
        
        return ball;
    }

    public override void Draw(RenderWindow window)
    {
        if (isVisible)
        {
            CircleShape circle = new CircleShape((int)radius);
            circle.Position = new Vector2f(leftX, leftY);
            circle.FillColor = color;
            circle.OutlineThickness = 1;
            circle.OutlineColor = Color.Black;
            window.Draw(circle);
        }
    }
    public Ball() 
    {
        sound = new Sound(new SoundBuffer("hit.wav"));
        
       
        CollisionEvent += BallCollisionEventHandler;
        // This constructor is required for serialization.
    }
    public Ball(int refX, int refY, int radius, Color color, bool isVisible, bool dynamic, int speed, float direction)
    {
        sound = new Sound(new SoundBuffer("hit.wav"));
        soundlevel = 0.7f;
        CollisionEvent += BallCollisionEventHandler;
        this.refX = refX;
        this.refY = refY;
        this.radius = radius;
        this.color = color;
        this.isVisible = isVisible;
        this.dynamic = dynamic;
        this.speed = speed;
        this.direction = direction;
        setLX(refX - radius);
        setRX(refX + radius);
        setLY(refY - radius);
        setRY(refY + radius);
    }
}