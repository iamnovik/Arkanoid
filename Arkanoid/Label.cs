using System.Numerics;
using System.Resources;
using System.Runtime.InteropServices.JavaScript;
using System.Xml.Serialization;
using SFML.Graphics;
using SFML.System;

namespace Arkanoid;
[XmlInclude(typeof(Label))]

public class Label : DispObj
{
    public uint fontsize;
    public String text;
    public String font;
    [XmlIgnore]
    private Text temp;

    private Font font_sfml;
    


    void show(){}
    void hide(){}

    public override String Serialize()
    {
        return GetType().Name +'\n'+ leftX +" "+ leftY +" "+ rightX +" "+ rightY +" "+ color.ToInteger() +" "+ isVisible +" "+ dynamic + text +" "+ fontsize +" "+ font;
    }

    public override DispObj Deserialize(string str)
    {
        String[] fields = str.Split(" ");
        Label brick = new Label(Int32.Parse(fields[0]),Int32.Parse(fields[1]),Int32.Parse(fields[2]),Int32.Parse(fields[3]),new Color(uint.Parse(fields[4])),Boolean.Parse(fields[5]),Boolean.Parse(fields[6]),fields[7],uint.Parse(fields[8]),fields[9]);
        
        return brick;
    }

    public override void Draw(RenderWindow window)
    {
        if (temp.Font == null)
            temp.Font = new Font(font);
        temp.DisplayedString = text;
        temp.Position = new Vector2f(leftX, leftY);
        temp.CharacterSize = fontsize;
        temp.Color = base.color;
       
       window.Draw(temp);
    }
    public Label() : base()
    {
        temp = new Text();
        
       
       
        
    }
    public Label(int leftX, int leftY, int rightX, int rightY, Color color, bool isVisible, bool dynamic, String text, uint fontsize,String font) : base(leftX, leftY, rightX, rightY, color, isVisible, dynamic)
    {
        
        this.font = font;
        this.fontsize = fontsize;
        this.text = text;
        temp = new Text();




    }


   
}