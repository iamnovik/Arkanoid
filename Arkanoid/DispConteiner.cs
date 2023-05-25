using System.Collections;
using System.Xml.Serialization;
using SFML.Graphics;

namespace Arkanoid;

public class DispConteiner
{
    
    [XmlIgnore]
    public List<DispObj> allObjects = new List<DispObj>();

    public int GameStatus;
    public Bricks bricks = new Bricks();
    public List<Ball> balls = new List<Ball>();
    public Platforms Platforms = new Platforms();
    public Settings Settings = new Settings();
    public DispConteiner(Settings sett)
    {
        Settings = sett;
        //allObjects = new DispObj[46];
        bricks = new Bricks(sett);
        addObjects(new List<DispObj>(bricks.bricks));
        Platforms = new Platforms(Settings);
        addObjects(new List<DispObj>(Platforms.platforms));
        balls= new Balls(sett).balls;
        
      
        addObjects(new List<DispObj>(balls));
        

    }

    public void Update()
    {
        addObjects(new List<DispObj>(balls));
        addObjects(new List<DispObj>(bricks.bricks));
        addObjects(new List<DispObj>(Platforms.platforms));
    }
    public DispConteiner()
    {
        

    }
    public void VolumeChange(float Volume)
    {

        foreach (var ball in balls)
        {
            ball.SetVolume(Volume);
        }
    }
    
        public void addObject(DispObj displayObject)
        {
            allObjects.Add(displayObject);
        }
    public void addObjects(List<DispObj> displayObject)
    {
        foreach (DispObj item in displayObject)
        {
            allObjects.Add(item);
        }
    }
    

    
}