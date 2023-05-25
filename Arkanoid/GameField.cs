using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Xml.Serialization;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Arkanoid;
public class GameField
{
    Sound sound = new Sound(new SoundBuffer("bonus.wav"));
    public Game game;
    
    public void onClick()
    {
       
       foreach (var item in game.menu._menuItems._menuItems)
       {
           if (item.button.OnClick(game.window))
           {
               break;
           };
           
       }

       game.statusBar.menubtn.button.OnClick(game.window);
    }
    public void MoveAll()
    {
        bool inGame = true;
        foreach (var item in game.allobj.balls)
        {
            inGame &= item.inGame;
        }

        if (!inGame)
        {
            game.GameStatus = 2;
        }
        foreach (var brick in game.allobj.bricks.bricks)
        {
            if (brick.bonus is { isVisible: true } )
            {
                brick.bonus.Move();
            }
        }
        foreach (DispObj item in game.allobj.allObjects)
        {
            
            if (item.dynamic && item.getVisibility())
            {
                item.Move();

            }

            
        }
        
    }

    public void DrawAll()
    {
        if (game.allobj.bricks.Empty())
        {
            game.GameStatus = 3;
        }
        foreach(DispObj item in game.allobj.allObjects){
            if (item.getVisibility())
                item.Draw(game.window);

        }

        foreach (var brick in game.allobj.bricks.bricks)
        {
            if (brick.bonus is { isVisible: true })
            {
                brick.bonus.Draw(game.window);
            }
        }
        if (game.statusBar.getVisibility())
        {
            game.statusBar.Draw(game.window);
        }

        if (game.menu.getVisibility())
        {
            game.menu.Draw(game.window);
        }

        if (game.sett.isVisible)
        {
            game.sett.Draw(game.window);
        }
    }
    public void CollisionAll()
    {
        foreach (var obj in game.allobj.allObjects)
        {
               
            if(obj.dynamic && obj.getVisibility())
                foreach (var obj2 in game.allobj.allObjects)
                {
                    if(!(obj is Ball) || obj.Equals(obj2) || !obj2.getVisibility() || (obj2 is StatusBar)) continue;
                    if (obj.Collision(obj2))
                    {
                        break;
                    }
                }
        }

        foreach (var item in game.allobj.Platforms.platforms)
        {
            if (item.isVisible)
            {
                foreach (var obj in game.allobj.bricks.bricks)
                {
                    if (obj is {  bonus: { } } )
                    {
                        if (obj.bonus.isVisible)
                        {
                            if (obj.bonus.Collision(item))
                            {
                                break;
                            }
                        }
                        
                    }
                }
            }
        }
    }

   
    
    public GameField( Game game)
    {
        
        this.game = game;
        SetEventsBricks();
        //game.allobj.addObject(sett);
        //game.allobj.addObject(game.sett);
        this.game.sett.gamediff[0].button.ButtonClick += DiffKID;
        this.game.sett.gamediff[1].button.ButtonClick += DiffEZ;
        this.game.sett.gamediff[2].button.ButtonClick += DiffMID;
        this.game.sett.gamediff[3].button.ButtonClick += DiffHIGH;
        this.game.sett.gamediff[4].button.ButtonClick += DiffGG;
        this.game.sett.btnres[0].button.ButtonClick += Buttonressmallsmsm;
        this.game.sett.btnres[1].button.ButtonClick += Buttonressmallsm;
        this.game.sett.btnres[2].button.ButtonClick += Buttonressmall;
        this.game.sett.btnres[3].button.ButtonClick += Buttonresdef;
        this.game.sett.btnres[4].button.ButtonClick += Buttonresbig;
        this.game.sett.btnvolume[0].button.ButtonClick += SoundZero;
        this.game.sett.btnvolume[1].button.ButtonClick += Buttonlow;
        this.game.sett.btnvolume[2].button.ButtonClick += Buttonmid;
        this.game.sett.btnvolume[3].button.ButtonClick += Buttonhigh;
        this.game.sett.btnvolume[4].button.ButtonClick += SoundHigh;
        this.game.sett.exit.button.ButtonClick += Buttonexit;
        game.statusBar.menubtn.button.ButtonClick += Button0Click;
        game.menu._menuItems._menuItems[0].button.ButtonClick += Button1Click;
        game.menu._menuItems._menuItems[4].button.ButtonClick += Button4Click;
        game.menu._menuItems._menuItems[5].button.ButtonClick += Button5Click;
        game.menu._menuItems._menuItems[2].button.ButtonClick += Button6Click;
        game.menu._menuItems._menuItems[1].button.ButtonClick += Button2Click;
    }

    
    public GameField()
    {
        game.allobj.addObject(game.sett);
        SetEventsBricks();
        //game.allobj.addObject(sett);
        //game.allobj.addObject(game.sett);
        this.game.sett.gamediff[0].button.ButtonClick += DiffKID;
        this.game.sett.gamediff[1].button.ButtonClick += DiffEZ;
        this.game.sett.gamediff[2].button.ButtonClick += DiffMID;
        this.game.sett.gamediff[3].button.ButtonClick += DiffHIGH;
        this.game.sett.gamediff[4].button.ButtonClick += DiffGG;
        this.game.sett.btnres[0].button.ButtonClick += Buttonressmallsmsm;
        this.game.sett.btnres[1].button.ButtonClick += Buttonressmallsm;
        this.game.sett.btnres[2].button.ButtonClick += Buttonressmall;
        this.game.sett.btnres[3].button.ButtonClick += Buttonresdef;
        this.game.sett.btnres[4].button.ButtonClick += Buttonresbig;
        this.game.sett.btnvolume[0].button.ButtonClick += SoundZero;
        this.game.sett.btnvolume[1].button.ButtonClick += Buttonlow;
        this.game.sett.btnvolume[2].button.ButtonClick += Buttonmid;
        this.game.sett.btnvolume[3].button.ButtonClick += Buttonhigh;
        this.game.sett.btnvolume[4].button.ButtonClick += SoundHigh;
        this.game.sett.exit.button.ButtonClick += Buttonexit;
        game.statusBar.menubtn.button.ButtonClick += Button0Click;
        game.menu._menuItems._menuItems[0].button.ButtonClick += Button1Click;
        game.menu._menuItems._menuItems[4].button.ButtonClick += Button4Click;
        game.menu._menuItems._menuItems[5].button.ButtonClick += Button5Click;
        game.menu._menuItems._menuItems[2].button.ButtonClick += Button6Click;
        game.menu._menuItems._menuItems[1].button.ButtonClick += Button2Click;
    }
    
    private void SoundZero(object sender, EventArgs e)
    {
        game.settings.volume = 0f;
        ButtonsBlue(game.sett,1);
        game.sett.btnvolume[0].button.color = Color.Red;
        VolumeChange(game.settings.volume);
    }

    private void VolumeChange(float volume)
    {
        game.allobj.VolumeChange(volume);
        sound.Volume = volume;
        game.sound.Volume = volume;
        game.sound_lose.Volume = volume;
        game.sound_win.Volume = volume;
    }
    private void SoundHigh(object sender, EventArgs e)
    {
        game.settings.volume = 50f;
        ButtonsBlue(game.sett,1);
        game.sett.btnvolume[4].button.color = Color.Red;
        VolumeChange(game.settings.volume);
    }
    private void Button0Click(object sender, EventArgs e)
    {
        game.GameStatus = 1;
    }
    private void Button1Click(object sender, EventArgs e)
    {
        
        MessageBox ok = new MessageBox(500, 200, 750, 400, Color.Magenta, true, false, "Сохранено", 0, "Intro.otf", 15);
        ok.button.button.ButtonClick += CloseMenu;
        while (ok.button.button.isVisible)
        {
            game.window.DispatchEvents();
            game.window.Clear();
            DrawAll();
            ok.Draw(game.window);
            ok.button.button.OnClick(game.window);
            game.window.Display();
        }
    }
    private void Button2Click(object sender, EventArgs e)
    {
        Mouse.SetPosition(new Vector2i(game.sett.resolution.refX,game.sett.resolution.refY) + game.window.Position);
        game.menu.isVisible = false;
        game.sett.isVisible = true;
        while (game.sett.isVisible)
        {
            
            game.window.DispatchEvents();
            game.window.Clear(Color.White);
            
            foreach (var item in game.sett.btnres)
            {
                item.button.OnClick(game.window);
            }
            foreach (var item in game.sett.btnvolume)
            {
                item.button.OnClick(game.window);
            }
            foreach (var item in game.sett.gamediff)
            {
                item.button.OnClick(game.window);
            }
            
            game.sett.exit.button.OnClick(game.window);
            DrawAll();
            game.window.Display();
        }
        game.Update();
        game.menu.isVisible = true;
    }
   
    private void Button4Click(object sender, EventArgs e)
    {
        game.menu.Close();
        game.PauseGame();
    }
    private void Button5Click(object sender, EventArgs e)
    {
        
        game.ReWriteBestScore();
        Environment.Exit(0);
        
        
    }

    private void CloseMenu(object sender, EventArgs e)
    {
        Button b = (Button)sender;
        b.isVisible = false;
    }
    private void Button6Click(object sender, EventArgs e)
    {
        MessageBox ok = new MessageBox(500, 200, 750, 400, Color.Magenta, true, false, "Загружено", 0, "Intro.otf", 15);
        ok.button.button.ButtonClick += CloseMenu;
        while (ok.button.button.isVisible)
        {
            game.window.DispatchEvents();
            game.window.Clear();
            DrawAll();
            ok.Draw(game.window);
            ok.button.button.OnClick(game.window);
            game.window.Display();
        }
        game.allobj = (game.LoadGameXML(game.gamesave, ref game.sett));
        //game.window.Position = new Vector2i(0, 0);
        game.window.Size = game.settings.res;
        
        game.allobj.Update();
        SetEventsBricks();
        VolumeChange(game.settings.volume);
       
    }
    private void Buttonressmall(object sender, EventArgs e)
    {
        //game.window.Close();
        //game.window = new RenderWindow(new VideoMode(1280, 720), "Akr", Styles.None);
        game.window.Size = new Vector2u(1280, 720);
        game.settings.res = game.window.Size;
        //newWindow(game.settings.res);
        ButtonsBlue(game.sett,0);
        game.sett.btnres[2].button.color = Color.Red;
        
    }
    private void Buttonressmallsm(object sender, EventArgs e)
    {
        game.window.Size = new Vector2u(854, 480);
        game.settings.res = game.window.Size;
        ButtonsBlue(game.sett,0);
        game.sett.btnres[1].button.color = Color.Red;
    }
    private void Buttonressmallsmsm(object sender, EventArgs e)
    {
        /*View view = game.window.GetView();
        view.Size = new Vector2f(640, 360);
        view.Zoom(1920/640);
        
        game.window.SetView(view);*/
        game.window.Size = new Vector2u(640, 360);
        game.settings.res = game.window.Size;
        ButtonsBlue(game.sett,0);
        game.sett.btnres[0].button.color = Color.Red;
    }

    private void ButtonsBlue(SettingsField sf, int res )
    {
        switch (res)
        {
            case 0:
            {
                foreach (var item in sf.btnres)
                {
                    item.button.color = Color.Blue;
                }
                break;   
            }
            case 1:
            {
                foreach (var item in sf.btnvolume)
                {
                    item.button.color = Color.Blue;
                }
                break;
            }
            case 2:
            {
                foreach (var item in sf.gamediff)
                {
                    item.button.color = Color.Blue;
                }
                break;
            }
        }
       
    }
    private void Buttonlow(object sender, EventArgs e)
    {
        game.settings.volume = 10f;
        ButtonsBlue(game.sett,1);
        game.sett.btnvolume[1].button.color = Color.Red;
        VolumeChange(game.settings.volume);
       
    }
    private void Buttonmid(object sender, EventArgs e)
    {
        game.settings.volume = 20f;
        ButtonsBlue(game.sett,1);
        game.sett.btnvolume[2].button.color = Color.Red;
        VolumeChange(game.settings.volume);
  
    }
    private void Buttonhigh(object sender, EventArgs e)
    {
        game.settings.volume = 30f;
        ButtonsBlue(game.sett,1);
        game.sett.btnvolume[3].button.color = Color.Red;
        VolumeChange(game.settings.volume);
        
    }
    private void Buttonresdef(object sender, EventArgs e)
    {
     
        game.window.Size = new Vector2u(1440, 810);
        game.settings.res = game.window.Size;
        ButtonsBlue(game.sett,0);
        game.sett.btnres[3].button.color = Color.Red;
    }
    private void Buttonresbig(object sender, EventArgs e)
    {
        game.window.Size = new Vector2u(1920, 1080);
        game.settings.res = game.window.Size;
        ButtonsBlue(game.sett,0);
        game.sett.btnres[4].button.color = Color.Red;
    }

    public void SetEventsBricks()
    {
        foreach (var item in game.allobj.allObjects)
        {
            if (item is Brick brick)
            {
                brick.ScoreChange += DeleteBrickScore;
                if (brick.bonus != null)
                {
                    brick.BonusEvent += BrickBonusEventHandler;
                    brick.bonus.CollisionEvent += CollisionBonusEventHandler;
                }
            }
        }
    }

    private void CollisionBonusEventHandler(object sender, EventArgs e)
    {
        sound.Play();
        BonusItem temp = (BonusItem)sender;
        temp.isVisible = false;
        switch (temp.type)
        {
            case 0:
            {
                foreach (var ball in game.allobj.Platforms.platforms)
                {
                    ball.speed += 5;
                }
                break;
            }
            case 1:
            {
                game.stat.score += 100;
                break;
            }
            case 2:
            {
                game.GameStatus = 3;
                break;
            }
            case 3:
            {
                foreach (var item in game.allobj.balls)
                {
                    item.speed /= 2;
                }
                break;
            }
           
        }
    }
    private void BrickBonusEventHandler(object sender, EventArgs e)
    {
        
        sound.Volume = game.settings.volume;
        sound.Play();
        BonusItem temp = (BonusItem)sender;
        temp.isVisible = true;
        /*switch (temp.type)
        {
            case 0:
            {
                foreach (var ball in game.allobj.Platforms.platforms)
                {
                    ball.speed += 5;
                }
                break;
            }
            case 1:
            {
                game.stat.score += 100;
                break;
            }
            case 2:
            {
                game.GameStatus = 3;
                break;
            }
            case 3:
            {
                foreach (var item in game.allobj.balls)
                {
                    item.speed /= 2;
                }
                break;
            }
           
        }*/
    }
    private void DiffKID(object sender, EventArgs e)
    {
        game.stat.score = 0;
        game.settings.difficulty = 1;
        game.settings.level = 1;
        game.allobj = new DispConteiner(game.settings);
        ButtonsBlue(game.sett,2);
        game.sett.gamediff[0].button.color = Color.Red;
    }
    private void DiffEZ(object sender, EventArgs e)
    {
        game.stat.score = 0;
        game.settings.level = 1;
        game.settings.difficulty = 2;
        game.allobj = new DispConteiner(game.settings);
        ButtonsBlue(game.sett,2);
        game.sett.gamediff[1].button.color = Color.Red;
    }
    private void DiffMID(object sender, EventArgs e)
    {
        game.stat.score = 0;
        game.settings.level = 1;
        game.settings.difficulty = 3;
        game.allobj = new DispConteiner(game.settings);
        ButtonsBlue(game.sett,2);
        game.sett.gamediff[2].button.color = Color.Red;
    }
    private void DiffHIGH(object sender, EventArgs e)
    {
        game.stat.score = 0;
        game.settings.level = 1;
        game.settings.difficulty = 4;
        game.allobj = new DispConteiner(game.settings);
        ButtonsBlue(game.sett,2);
        game.sett.gamediff[3].button.color = Color.Red;
    }
    private void DiffGG(object sender, EventArgs e)
    {
        game.stat.score = 0;
        game.settings.level = 1;
        game.settings.difficulty = 5;
        game.allobj = new DispConteiner(game.settings);
        ButtonsBlue(game.sett,2);
        game.sett.gamediff[4].button.color = Color.Red;
    }
    private void DeleteBrickScore(object sender, EventArgs e)
    {
        game.stat.score += 10;
    }
    private void Buttonexit(object sender, EventArgs e)
    {
        game.sett.isVisible = false;
        Mouse.SetPosition(new Vector2i(game.window.Position.X + (int)game.window.Size.X/4,game.window.Position.Y + (int)game.window.Size.Y/2));

    }
    
    
    
}