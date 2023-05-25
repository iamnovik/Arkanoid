using System.Xml.Serialization;
using SFML.Graphics;

namespace Arkanoid;
[XmlInclude(typeof(MenuItem))]
public class MenuItem : DispObj
{
    public int id;
    public Label text;
    public Button button;
    public String font;

    public override String Serialize()
    {
        String temp =GetType().Name +'\n'+ leftX +" "+ leftY +" "+ rightX +" "+ rightY +" "+ color.ToInteger() +" "+ isVisible +" "+ dynamic +" "+ text.text +" "+ id +" "+ font;
        temp += button.Serialize();
        temp += text.Serialize();
        return temp;
    }

    public override DispObj Deserialize(string str)
    {/*
        String[] fields = str.Split(" ");
        //MenuItem brick = new MenuItem(Int32.Parse(fields[0]),Int32.Parse(fields[1]),Int32.Parse(fields[2]),Int32.Parse(fields[3]),new Color(uint.Parse(fields[4])),Boolean.Parse(fields[5]),Boolean.Parse(fields[6]), fields[7],Int32.Parse(fields[8]),fields[9] );
        
        return brick;
        */
        return new MenuItem();
    }

    public override void Draw(RenderWindow window)
    {
        button.Draw(window);
        text.Draw(window);
    }

    public override void ChangeCoord(float ScaleX, float ScaleY)
    {
        base.ChangeCoord(ScaleX, ScaleY);
        text.ChangeCoord(ScaleX,ScaleY);
        button.ChangeCoord(ScaleX,ScaleY);
    }

    void setinfo()
    {
        
    }
    public MenuItem() : base()
    {
        
    }
    public MenuItem(int leftX, int leftY, int rightX, int rightY, Color color, bool isVisible, bool dynamic,String text, int id, String font, uint Size) : base(leftX, leftY, rightX, rightY, color, isVisible, dynamic)
    {
        /*int pointX1 = refX - (base.rightX - base.leftX) / 4;
        if (text.Length > 9)
        {
            pointX1 = refX - (base.rightX - base.leftX)*2 /3 ;
        }
        int pointX2 = refX + (base.rightX - base.leftX) / 4;
        if (text.Length > 9)
        {
            pointX2 = refX + (base.rightX - base.leftX)*2 /3 ;
        }*/
        this.font = font;
        this.id = id;
        this.button = new Button(base.leftX, base.leftY, base.rightX, base.rightY, base.color, base.isVisible, base.dynamic);
        this.text = new Label( refX - (rightX - base.leftX)/4, base.refY - (base.rightY - base.leftY)/3, refX + (rightX - leftX)/4, refY + (base.rightY - base.leftY)/3, Color.White, base.isVisible, base.dynamic,
            text, Size,font);
        

    }
}