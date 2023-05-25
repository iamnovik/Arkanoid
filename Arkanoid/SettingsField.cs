using Microsoft.VisualBasic;
using SFML.Graphics;
using SFML.System;

namespace Arkanoid;

public class SettingsField:DispObj
{
    public Label resolution;
    public Label volume;
    public Label diff;
    public MenuItem[] btnres = new MenuItem[5];
    public MenuItem[] btnvolume = new MenuItem[5];
    public MenuItem[] gamediff = new MenuItem[5];
    public MenuItem exit;
    public override string Serialize()
    {
        throw new NotImplementedException();
    }

    public override DispObj Deserialize(string str)
    {
        throw new NotImplementedException();
    }

    public SettingsField()
    {
        
    }

    public override void ChangeCoord(float ScaleX, float ScaleY)
    {
        base.ChangeCoord(ScaleX, ScaleY);
        volume.ChangeCoord(ScaleX,ScaleY);
        resolution.ChangeCoord(ScaleX,ScaleY);
        diff.ChangeCoord(ScaleX,ScaleY);
        foreach (var item in btnvolume)
        {
            item.ChangeCoord(ScaleX,ScaleY);
        }
        foreach (var item in btnres)
        {
            item.ChangeCoord(ScaleX,ScaleY);
        }
        foreach (var item in gamediff)
        {
            item.ChangeCoord(ScaleX,ScaleY);
        }
        exit.ChangeCoord(ScaleX,ScaleY);
    }

    public SettingsField(int leftX, int leftY, int rightX, int rightY, Color color, bool isVisible, bool dynamic) : base(leftX, leftY, rightX, rightY, color, isVisible, dynamic)
    {
        //Сделай размеры относительно координат поля!!!
        resolution = new Label(refX - (base.rightX - leftX)/5 , leftY, refX + (base.rightX - leftX)/2, leftY + (rightY -leftY)/10, Color.Red, base.isVisible, dynamic, "Resolution",
            30, "Intro.otf");
        
        volume = new Label(refX - (base.rightX - leftX)/7 , leftY + (rightY -leftY)/4, refX + (base.rightX - leftX)/2, leftY + (rightY -leftY)/3, Color.Red, base.isVisible, dynamic, "Volume",
            30, "Intro.otf");
        diff = new Label(refX - (base.rightX - leftX)/5 , leftY + (rightY - leftY) / 2, refX + (base.rightX - leftX)/2, leftY + (rightY - leftY) * 3/5, Color.Red, base.isVisible, dynamic, "Difficulty",
            30, "Intro.otf");

        
        for (int i = 0; i < btnres.Length; i++)
        {
            /*btnres[i] = new MenuItem(rightX - (rightX - base.leftX)*(5 - i) / 5, leftY + (rightY -leftY)/10,
                rightX - (rightX - base.leftX)*(5 - i) / 5, leftY + (rightY -leftY)/4, Color.Black, true, dynamic, "300x300",
                i, "Intro.otf");*/
            btnres[i] = new MenuItem(leftX + (rightX - leftX)*(i)/5,leftY + (rightY -leftY)/10 ,
                leftX + (rightX - leftX)*(i + 1)/5,  leftY + (rightY -leftY)/4, Color.Blue, true, dynamic, "640x360",
                50, "Intro.otf",25);
        }

        for (int i = 0; i < gamediff.Length; i++)
        {
            gamediff[i] = new MenuItem(rightX - (rightX - base.leftX) * (5 - i) / 5, leftY + (rightY - leftY) * 3/5 ,
                rightX - (rightX - base.leftX) * (4 - i) / 5, leftY + (rightY - leftY) * 4 / 5, Color.Blue, true, dynamic,
                "KID",
                50, "Intro.otf", 25);
        }

        gamediff[0].button.color = Color.Red;
        gamediff[1].text.text = "EZ";
        gamediff[4].text.text = "GG";
        gamediff[2].text.text = "MID";
        gamediff[3].text.text = "HARD";
        btnres[1].text.text = "854x480";
        btnres[2].text.text = "1280x720";
        btnres[3].text.text = "1440x810";
        btnres[4].text.text = "FullScreen";
        btnres[4].button.color = Color.Red;
        for (int i = 0; i < btnvolume.Length; i++)
        {
            btnvolume[i] = new MenuItem(rightX - (rightX - base.leftX)*(5 - i) / 5, leftY + (rightY - leftY) / 3,
                rightX - (rightX - base.leftX)*(4 - i) / 5, leftY + (rightY - leftY) / 2, Color.Blue, true, dynamic, "0",
                50, "Intro.otf",25);
        }
        btnvolume[1].text.text = "LOW";
        btnvolume[0].button.color = Color.Red;
        btnvolume[2].text.text = "MID";
        btnvolume[3].text.text = "HIGH";
        btnvolume[4].text.text = "100";
        
        exit = new MenuItem(base.leftX + (base.rightX - base.leftX) / 3, base.rightY - (base.rightY - base.leftY) / 7,
            base.leftX + (base.rightX - base.leftX) * 2 / 3, base.rightY, Color.Red, true, false, "Exit", 50,
            "Intro.otf",25);
    }
    public override void Draw(RenderWindow window)
    {
        RectangleShape rect = new RectangleShape(new Vector2f(rightX - leftX, rightY - leftY));
        rect.Position = new Vector2f(leftX, leftY);
        rect.FillColor = color;
        rect.OutlineThickness = 1;
        rect.OutlineColor = Color.Transparent;
        
        window.Draw(rect);
        resolution.Draw(window);
        foreach (var item in btnres)
        {
            item.Draw(window);
        }
        volume.Draw(window);
        foreach (var item in btnvolume)
        {
            item.Draw(window);
        }
        diff.Draw(window);
        foreach (var item in gamediff)
        {
            item.Draw(window);
        }
        exit.Draw(window);

    }
    
}