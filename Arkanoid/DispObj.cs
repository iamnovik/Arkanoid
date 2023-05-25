using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using SFML.Graphics;

namespace Arkanoid;
[Serializable]
[XmlInclude(typeof(DispObj))]
public abstract class DispObj
{
    
    public int refX;
    public int refY;
    public int leftX;
    public int leftY;
    public int rightX;
    public int rightY;
    public Color color;
    public Color backcolor;
    public bool isVisible;
    public bool dynamic;
    public event EventHandler<EventArgs> CollisionEvent;
   
    public DispObj(int leftX, int leftY, int rightX, int rightY, Color color, bool isVisible, bool dynamic)
    {
        this.leftX = leftX;
        this.leftY = leftY;
        this.rightX = rightX;
        this.rightY = rightY;
        this.color = color;
        this.refX = (this.leftX + this.rightX)/2;
        this.refY = (this.leftY + this.rightY) / 2;
        this.isVisible = isVisible;
        backcolor = Color.Black;
        this.dynamic = dynamic;

    }
    

    protected DispObj()
    {
        
    }

    public int getLX() {
        return leftX;
    }
    public  void setLX(int coordinate) {
        this.leftX = coordinate;
    }
    public int getLY() {
        return leftY;
    }
    public  void setLY(int coordinate) {
        this.leftY=coordinate;
    }
    public int getRX() {
        return rightX;
    }
    public  void setRX(int coordinate) {
        this.rightX = coordinate;
    }
    public int getRY() {
        return rightY;
    }
    public  void setRY(int coordinate) {
        this.rightY=coordinate;
    }
    public int getrefX() {
        return refX;
    }
    public  void setrefX(int coordinate) {
        this.refX = coordinate;
    }
    public int getrefY() {
        return refY;
    }
    public  void setrefY(int coordinate) {
        this.refY=coordinate;
    }

    public Color getColor() {
        return color;
    }

    public void setColor(Color color) {
        this.color = color;
    }

    public bool getVisibility()
    {
        return isVisible;
    }

    public virtual void ChangeCoord(float ScaleX, float ScaleY)
    {
        refX = (int)(refX * ScaleX);
        refY = (int)(refY * ScaleY);
        leftX = (int)(leftX * ScaleX);
        leftY = (int)(leftY * ScaleY);
        rightX = (int)(rightX * ScaleX);
        rightY = (int)(rightY * ScaleY);

    }


    public virtual void Move()
    {
        throw new NotImplementedException();
    }

    public abstract String Serialize();
    public abstract DispObj Deserialize(String str);

    void HandleCollision(object sender, EventArgs e)
    {
    }



    public virtual bool Collision(DispObj obj1)
    {
        if(rightY >= obj1.leftY && leftY <= obj1.rightY)
            if (rightX >= obj1.leftX && leftX <= obj1.rightX)
            {
                this.CollisionEvent?.Invoke(obj1,EventArgs.Empty);
                obj1.CollisionEvent?.Invoke(this,EventArgs.Empty);
                return true;
            }
        return false;
    }

    public virtual void ChangeDirection(DispObj dispObj)
    {
        
    }
    

    public virtual void Draw(RenderWindow window)
    {
     
    }
    public void Update(){}
    public void Draw(RenderTarget target, RenderStates states)
    {
        throw new NotImplementedException();
    }
}