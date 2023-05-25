using SFML.Graphics;

namespace Arkanoid;

public class MenuItems
{
    public MenuItem[] _menuItems;
    public int pos = 0;

    public MenuItems()
    {
        _menuItems = new MenuItem[6];
        addMenuItem(new MenuItem(480, 200, 780, 250, Color.Blue, true, false, " Save game", 1, "Intro.otf",25));
        addMenuItem(new MenuItem(480, 300, 780, 350, Color.Blue, true, false, " Settings", 2, "Intro.otf",25));
        addMenuItem(new MenuItem(480, 100, 780, 150, Color.Blue, true, false, " Load game", 6, "Intro.otf",25));
        addMenuItem(new MenuItem(480, 400, 780, 450, Color.Blue, true, false, " Pause game", 3, "Intro.otf",25));
        addMenuItem(new MenuItem(480, 500, 780, 550, Color.Blue, true, false, " Resume game", 4, "Intro.otf",25));
        addMenuItem(new MenuItem(480, 600, 780, 650, Color.Blue, true, false, " Exit game", 5, "Intro.otf",25));
    }

    void addMenuItem(MenuItem item)
    {
        _menuItems[pos] = item;
        pos++;
    }

    void delMenuItem()
    {
        
    }
}