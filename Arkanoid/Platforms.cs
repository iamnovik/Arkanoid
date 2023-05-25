using SFML.Graphics;

namespace Arkanoid;

public class Platforms
{
    public List<Platform> platforms = new List<Platform>();
    int pos =0;
    
    public Platforms(Settings sett){
      
       // platforms.Add();
        addPlatform(new Platform(400,670,550,690, Color.Yellow, true,true,10*sett.difficulty*(3/2)));
    }
    public Platforms(){
      
        
    }
    public void addPlatform(Platform platform)
    {
        platforms.Add(platform);
        pos++;
    }
    public void removePlatform(Platform platform)
    {

    }
    public Platform getPlatform(int index) {
        return null;
    }
}