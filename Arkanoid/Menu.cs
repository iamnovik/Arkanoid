using System.Xml.Serialization;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Arkanoid;
[XmlInclude(typeof(Menu))]
public class Menu : DispObj
{
    public MenuItems _menuItems;
    
    private bool isOpen; // флаг, указывающий, открыто ли меню

    public Menu() 
    {
       
        
       
    }

    public override void ChangeCoord(float ScaleX, float ScaleY)
    {
        base.ChangeCoord(ScaleX, ScaleY);
        foreach (var item in _menuItems._menuItems)
        {
            item.ChangeCoord(ScaleX,ScaleY);
        }
    }

    public Menu(int leftX, int leftY, int rightX, int rightY, Color color, bool isVisible, bool dynamic) : base(leftX, leftY, rightX, rightY, color, isVisible, dynamic)
    {
        _menuItems = new MenuItems();
    }

    public override String Serialize()
    {
        String temp;
        temp = GetType().Name +'\n'+ leftX +" "+ leftY +" "+ rightX +" "+ rightY +" "+ color.ToInteger() +" "+ isVisible +" "+ dynamic;
        

        return temp;
    }

    public void Deactive()
    {
        foreach (var item in _menuItems._menuItems)
        {
            item.button.active = false;
        }
    }
    public void Activate()
    {
        foreach (var item in _menuItems._menuItems)
        {
            item.button.active = true;
        }
    }
    public override DispObj Deserialize(string str)
    {
        String[] fields = str.Split(" ");
        Menu brick = new Menu(Int32.Parse(fields[0]),Int32.Parse(fields[1]),Int32.Parse(fields[2]),Int32.Parse(fields[3]),new Color(uint.Parse(fields[4])),Boolean.Parse(fields[5]),Boolean.Parse(fields[6]) );
        
        return brick;
    }

    public override void Draw(RenderWindow window)
    {
        RectangleShape rect = new RectangleShape(new Vector2f(rightX - leftX, rightY - leftY));
        rect.Position = new Vector2f(leftX, leftY);
        rect.FillColor = Color.Transparent;
        rect.OutlineThickness = 1;
        rect.OutlineColor = Color.Transparent;
        
        window.Draw(rect);
        foreach (DispObj item in _menuItems._menuItems)
        {
            item.Draw(window);
        }
    }

   
    public void Open() // метод для открытия меню
    {
        isVisible = true;
        // отображение меню на экране
    }

    public void Close() // метод для закрытия меню
    {

        isVisible = false;
        // скрытие меню на экране
    }

    
}