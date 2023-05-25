using System.Collections;
using SFML.Graphics;

namespace Arkanoid;

public class Bricks
{
    public List<Brick>bricks = new List<Brick>();
    int pos =0;
   
    public Bricks(Settings sett)
    {
        int x1 = 150, x2 = 230;
        int y1 = 70, y2 = 130;
        int y3 = 130, y4 = 190;
        int y5 = 190, y6 = 250;
        int y7 = 250, y8 = 310;
    
        for (int i = 0; i < 11; i++) {
           
            addBlock(new Brick(x1,y1,x2,y2,Color.Red,sett.level >5 ,false,true,sett.difficulty));
            addBlock(new Brick(x1,y3,x2,y4,Color.Yellow,sett.level > 1,false,true,sett.difficulty));
            addBlock(new Brick(x1,y5,x2,y6,Color.Green,true,false,true,sett.difficulty));
            addBlock(new Brick(x1,y7,x2,y8,Color.Magenta,true,false,true,sett.difficulty));
            
            x1+=80;
            x2+=80;
        }

        /*try
        {
            int x = new Random().Next(100);
            bricks[x].isDestroyable = false;
            Console.WriteLine("Есть");
        }
        catch
        {
            // ignored
        }*/

        bool fact = true;
        foreach (var variableBrick in bricks)
        {
            variableBrick.bonus = new BonusItem(variableBrick.leftX, variableBrick.leftY, variableBrick.rightX, variableBrick.rightY,
                Color.Red, false, true);
            variableBrick.isDestroyable = fact;
            fact = true ^ fact;
            if (!variableBrick.isDestroyable)
            {
                variableBrick.color = Color.Cyan;
            }

        }
        /*for (int i = 0; i < 10; i++)
        {
            int x = new Random().Next(0, 43);
            bricks[x].bonus = new BonusItem(bricks[x].leftX, bricks[x].leftY, bricks[x].rightX, bricks[x].rightY,
                Color.Red, false, true);
        }*/
    }

    public Bricks()
    {
        
    }
    public void addBlock(Brick block)
    {
        bricks.Add(block);
        
    }
    public void removeblock(Brick block)
    {

    }
    public Brick getBlock(int index) {
        return null;
    }
    private void ScoreChange(object sender, EventArgs e)
    {
       
    }
    public bool Empty()
    {
        foreach (var brick in bricks)
        {
            if (brick.isVisible)
            {
                return false;
            }
        }

        return true;
    }
}