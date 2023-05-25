using System;
using SFML.Graphics;

namespace Arkanoid;

public class Balls
{
    public List<Ball> balls = new List<Ball>();
    private Settings _settings;

    public Balls (Settings settings)
    {
        _settings = settings;
        addBall(new Ball(315,515,15,Color.Black, true,true,10*settings.difficulty*(6/5),(float)(Math.PI/2 * 0.5)));
        
    }

    public Balls()
    {
        addBall(new Ball(315,515,15,Color.Black, true,true,10*_settings.difficulty*(3/2),(float)(Math.PI/2 * 0.5)));

    }
  
    public Ball getBall(int index) {
        return null;
    }
    
    public void addBall(Ball ball){
        ball.SetVolume(_settings.volume);
        balls.Add(ball);
        
     
    }
    
    public void removeBall(Ball ball){

    }
}