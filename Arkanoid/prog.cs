using Arkanoid;
using SFML.Graphics;
using SFML.Window;

internal class prog
{
    [STAThread] 
    public static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
    }
}