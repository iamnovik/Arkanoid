using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Serialization;
using SFML.Graphics;

namespace Arkanoid;

[Serializable]
public class DispObjProxy
{
    public ArrayList arr = new ArrayList();

    
    public DispObjProxy(Object obj)
    {


        if (obj is DispObj dispObj)
        {
            arr.Add(dispObj.GetType().Name);
            if (dispObj is Ball ball)
            {
                arr.Add(ball.refX);
                arr.Add(ball.refY);
                arr.Add(ball.radius);
                arr.Add(ball.speed);
                arr.Add(ball.direction);
            }
            else
            {
                arr.Add(dispObj.leftX);
                arr.Add(dispObj.leftY);
                arr.Add(dispObj.rightX);
                arr.Add(dispObj.rightY);
            }
        
            arr.Add(dispObj.color.ToInteger());
            arr.Add(dispObj.isVisible);
            arr.Add(dispObj.dynamic);
            if (dispObj is Label label)
            {
                arr.Add(label.text);
                arr.Add(label.fontsize);
                arr.Add(label.font);
            }
            if (dispObj is Platform plat)
            {
                arr.Add(plat.speed);
            }

            if (dispObj is Brick brick)
            {
                arr.Add(brick.hitPoints);
            }  
        }

        if (obj is Settings settings)
        {
            arr.Add(settings.volume);
            arr.Add(settings.level);
        }

        if (obj is Statistics stat)
        {
            arr.Add(stat.score);
        }

    }
    public DispObjProxy()
    {
        
    }
   public DispObj ToDispObj(String[] arr)
    {
        DispObj dispObj;

        switch (arr[0])
        {
            case "Ball":
                dispObj = new Ball(Int32.Parse(arr[1].ToString()),Int32.Parse(arr[2].ToString()),Int32.Parse(arr[3].ToString()),new Color(UInt32.Parse(arr[6].ToString())),Boolean.Parse(arr[7].ToString()),Boolean.Parse(arr[8].ToString()),Int32.Parse(arr[4].ToString()),float.Parse(arr[5].ToString()));
                break;
            case "Platform":
                dispObj = new Platform(Int32.Parse(arr[1].ToString()),Int32.Parse(arr[2].ToString()),Int32.Parse(arr[3].ToString()),Int32.Parse(arr[4].ToString()),new Color(UInt32.Parse(arr[5].ToString())),Boolean.Parse(arr[6].ToString()),Boolean.Parse(arr[7].ToString()),Int32.Parse(arr[8].ToString()));
                break;
            case "Brick":
               // dispObj = new Brick(Int32.Parse(arr[1].ToString()),Int32.Parse(arr[2].ToString()),Int32.Parse(arr[3].ToString()),Int32.Parse(arr[4].ToString()),new Color(UInt32.Parse(arr[5].ToString())),Boolean.Parse(arr[6].ToString()),Boolean.Parse(arr[7].ToString()),Int32.Parse(arr[8].ToString()));
                break;
            case "Menu":
                dispObj = new Menu(Int32.Parse(arr[1].ToString()),Int32.Parse(arr[2].ToString()),Int32.Parse(arr[3].ToString()),Int32.Parse(arr[4].ToString()),new Color(UInt32.Parse(arr[5].ToString())),Boolean.Parse(arr[6].ToString()),Boolean.Parse(arr[7].ToString()));
                break;
            case "StatusBar":
                dispObj = new StatusBar(Int32.Parse(arr[1].ToString()),Int32.Parse(arr[2].ToString()),Int32.Parse(arr[3].ToString()),Int32.Parse(arr[4].ToString()),new Color(UInt32.Parse(arr[5].ToString())),Boolean.Parse(arr[6].ToString()),Boolean.Parse(arr[7].ToString()));
                break;
            case "Label":
                dispObj = new Label(Int32.Parse(arr[1].ToString()),Int32.Parse(arr[2].ToString()),Int32.Parse(arr[3].ToString()),Int32.Parse(arr[4].ToString()),new Color(UInt32.Parse(arr[5].ToString())),Boolean.Parse(arr[6].ToString()),Boolean.Parse(arr[7].ToString()),arr[8],UInt32.Parse(arr[9]),arr[10]);
                break;
            default:
                throw new NotSupportedException($"Unsupported type '{arr[0]}'");
                
        }

        

        
        return null;
    }
}
