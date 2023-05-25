using System;
using System.Numerics;
using System.Timers;
using System.Xml.Serialization;
using SFML.Graphics;
using SFML.System;
using Timer = System.Threading.Timer;

namespace Arkanoid;
[Serializable]
[XmlInclude(typeof(Brick))]
public class Brick : DispObj
{
    public bool isDestroyable;
    public event EventHandler<EventArgs> BonusEvent;
    public event EventHandler<EventArgs> ScoreChange;
    public int hitPoints;
    public BonusItem? bonus;
    public void TakeHit()
    {
        hitPoints--;
        if (hitPoints == 0)
        {
            ScoreChange?.Invoke(this,EventArgs.Empty);
            BonusEvent?.Invoke(bonus,EventArgs.Empty);
            isVisible = false;
        }
    }
    

    public int getHP() {
        return hitPoints;
    }

    public void setHP(int healthlevel) {
        this.hitPoints = healthlevel;
    }


    public override void Move()
    {
        throw new NotImplementedException();
    }

    public override String Serialize()
    {
        return GetType().Name+'\n' +leftX +" "+ leftY +" "+ rightX +" "+ rightY +" "+ color.ToInteger() +" "+ isVisible +" "+ dynamic +" "+ hitPoints;
    }

    public override DispObj Deserialize(string str)
    {
        String[] fields = str.Split(" ");
       // Brick brick = new Brick(Int32.Parse(fields[0]),Int32.Parse(fields[1]),Int32.Parse(fields[2]),Int32.Parse(fields[3]),new Color(uint.Parse(fields[4])),Boolean.Parse(fields[5]),Boolean.Parse(fields[6]), Int32.Parse(fields[7]) );
        
        return null;
    }
    
    
    private void BrickCollisionEventHandler(object sender, EventArgs e)
    {
        if (isDestroyable)
        {
            TakeHit();  
        }
        
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
    
    public Brick():base()
    {
        this.CollisionEvent += BrickCollisionEventHandler;
      
        // This constructor is required for serialization.
    }
    public Brick(int leftX, int leftY, int rightX, int rightY, Color color, bool isVisible, bool dynamic,bool isDestroyable, int hitPoints) : base(leftX, leftY, rightX, rightY, color, isVisible, dynamic)
    {
        this.isDestroyable = isDestroyable;
        this.hitPoints = hitPoints;
        this.CollisionEvent += BrickCollisionEventHandler;
       
    }
}