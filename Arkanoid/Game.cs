

using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Color = SFML.Graphics.Color;

namespace Arkanoid
{
    public class Game
    {
        public Thread? threadReceive = null;
        public UdpClient? udp = null;
        public Sound sound;
        public Sound sound_lose;
        public Sound sound_win;
        public event EventHandler<EventArgs> GameOver;
        public event EventHandler<EventArgs> NextLevel;
        public int GameStatus = 1;
        public SettingsField sett = new SettingsField(300,100,900,600,Color.Black,false,false);
        public String gamesave = "game_obj_save.txt";
        public String menusave = "menu_obj_save.txt";
        public Settings settings = new Settings();
        public Statistics stat = new Statistics();
        public DispConteiner allobj;
        public StatusBar statusBar = new StatusBar(0, 0, 1280, 70, Color.Black, true, false);
        public RenderWindow window;
        public Menu menu = new Menu(300,100,900,600,Color.Black,false,false);
       
        private Players _players;
        private GameField gf;
        private bool isPaused = false;
        public void Start()
        {
            
            sound_lose = new Sound(new SoundBuffer("lose.wav"));
            sound_win = new Sound(new SoundBuffer("win.wav"));
            sound = new Sound(new SoundBuffer("soundback.wav"));
            allobj = new DispConteiner(settings);
            GameOver += GameOverHadler;
            NextLevel += NextLevelHandler;
            var serialist = new XmlSerializer(typeof(Statistics));
            Statistics temp;
            using (FileStream stream = new FileStream("Stat.txt",FileMode.Open))
            {
                temp = (Statistics)serialist.Deserialize(stream);
                stat.bestScore = temp.bestScore;
            }
            statusBar.midLeft.text = "Level:" + settings.level;
            statusBar.midright.text = "Volume:" + 100;
            //allobj.addObject(statusBar);
            window = new RenderWindow(new VideoMode(1280, 720), "Akr", Styles.None);
           //window.Resized += Window_Resized;
            //window.Size = new Vector2u(500, 500);
            window.SetTitle("");
            window.SetFramerateLimit(50);
            window.SetVisible(true);
            window.Closed += (sender, args) => window.Close();
            GameField gf = new GameField(this);
            window.Resized += WindowResize;
            window.Size = new Vector2u(1920, 1080);
            settings.res = window.Size;
            sound.Volume = settings.volume;
            sound_lose.Volume = settings.volume;
            sound_win.Volume = settings.volume;
            sound.Play();
            sound.Loop = true;
            
            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(Color.White);
                gf.onClick();
                switch (GameStatus)
                {
                    case 0:
                    {
                        gf.MoveAll();
                        gf.CollisionAll();
                        
                        break;
                    }
                    case 1:
                    {
                        PauseGame();
                        menu.Open();
                        while (menu.isVisible)
                        {
                            window.DispatchEvents();
                            window.Clear(Color.White);
                            gf.onClick();
                            window.Clear(Color.White);
                            gf.DrawAll();
                            window.Display();
                        }
                        
                        GameStatus = 0;
                        break;
                        

                    }
                    case 2:
                    {
                       
                        
                        settings.level = 1;
                        stat.score = 0;
                        GameOver?.Invoke(gf,EventArgs.Empty);
                        gf.game.allobj = new DispConteiner(settings);
                        gf.SetEventsBricks();
                        break;
                    }
                    case 3:
                    {
                        settings.level++;
                        NextLevel?.Invoke(gf,EventArgs.Empty);
                        gf.game.allobj = new DispConteiner(settings);
                        gf.SetEventsBricks();
                        break;
                    }
                        
                }
                Update();
                gf.DrawAll();
                window.Display();
            }
            ReWriteBestScore();
            sound.Stop();
            
        }
        
        public void Update()
        {
            //score.text = "Score:" + stat.score;
            stat.UpdateBestScore();
            statusBar.left.text = "Score:\n" + stat.score;
            statusBar.midLeft.text = "Level:\n" + settings.level;
            statusBar.mid.text = "Skill:\n" + settings.GetDiff();
            statusBar.midright.text = "PLAYER";
            statusBar.right.text = "BestScore:\n" + stat.bestScore;
            //allobj.allObjects[47] = statusBar;

        }
        private void GameOverHandler(object sender, EventArgs e)
        {
            
        }

        public void ReWriteBestScore()
        {
            var serialist = new XmlSerializer(typeof(Statistics));
            Statistics temp;
            using (FileStream stream = new FileStream("Stat.txt",FileMode.Open))
            {
                temp = (Statistics)serialist.Deserialize(stream);
                temp.bestScore = stat.bestScore;
                
            }
            using (FileStream stream = new FileStream("Stat.txt",FileMode.Create))
            {
                serialist.Serialize(stream,temp);
                
            }
        }
        private void WindowResize(Object sender, EventArgs e)
        {
            window.Position = new Vector2i(0 + (int)(1920 - window.Size.X) / 2,
                0 + (int)(1080 - window.Size.Y) / 2);
            
            
        }
        public void SaveGame(String filesave,DispConteiner arr, SettingsField sett)
        {
            /*StreamWriter streamWriter = new StreamWriter("out.txt");
            //streamWriter.WriteLine(settings.volume +" " + settings.level);
            //streamWriter.WriteLine(stat.score);
            String str = "";
            DispObjProxy obj = new DispObjProxy(settings);
            for (int i = 0; i < obj.arr.Count; i++)
            {
                if (i != obj.arr.Count - 1)
                {
                    str += obj.arr[i] + " ";
                }
                else
                {
                    str += obj.arr[i];
                }
            }
            streamWriter.WriteLine(str);
            obj = new DispObjProxy(stat);
            str = "";
            for (int i = 0; i < obj.arr.Count; i++)
            {
                if (i != obj.arr.Count - 1)
                {
                    str += obj.arr[i] + " ";
                }
                else
                {
                    str += obj.arr[i];
                }
            }
            streamWriter.WriteLine(str);
            foreach (var item in arr)
            {
                obj = new DispObjProxy(item);
                str = "";
                
                for (int i = 0; i < obj.arr.Count; i++)
                {
                    if (i != obj.arr.Count - 1)
                    {
                        str += obj.arr[i] + " ";
                    }
                    else
                    {
                        str += obj.arr[i];
                    }
                }
                streamWriter.WriteLine(str);
            }
            streamWriter.Close();
            */
            var serializer = new XmlSerializer(typeof(DispConteiner), new Type[] { typeof(SettingsField),typeof(Brick), typeof(Platform), typeof(Ball), typeof(BonusItem),typeof(StatusBar),typeof(MenuItem),typeof(Label),typeof(Button), typeof(Menu) });
            var serializs = new XmlSerializer(typeof(Settings));
            var serialist = new XmlSerializer(typeof(Statistics));
            var serialobj = new XmlSerializer(typeof(DispObj),extraTypes:new Type[]{typeof(Menu),typeof(StatusBar), typeof(SettingsField)});
            //using (FileStream stream = new FileStream(filesave, FileMode.Create))
            //{
            FileStream stream = new FileStream(filesave, FileMode.Create);
            serializer.Serialize(stream, arr);
            stream.Close();
            stream = new FileStream("settings.txt", FileMode.Create);
            serializs.Serialize(stream,settings);
            stream.Close();
            stream = new FileStream("Stat.txt", FileMode.Create);
            serialist.Serialize(stream,stat);
            stream.Close();
            stream = new FileStream("Menu.txt", FileMode.Create);
            serialobj.Serialize(stream,menu);
            stream.Close();
            stream = new FileStream("StatusBar.txt", FileMode.Create);
            serialobj.Serialize(stream,statusBar);
            stream.Close();
            stream = new FileStream("SettingsField.txt", FileMode.Create);
            serialobj.Serialize(stream,sett);
            stream.Close();
            


        }

        public List<DispObj> LoadGame(String filesave, Settings settings, Statistics stat)
        {
            StreamReader streamReader = new StreamReader("out.txt");
            List<DispObj> arrtemp = new List<DispObj>();
            int i = 0;
            DispObjProxy proxy = new DispObjProxy();
            String[] fields = streamReader.ReadLine().Split(" ");
            settings.volume = float.Parse(fields[0]);
            settings.level = Int32.Parse(fields[1]);
            fields = streamReader.ReadLine().Split(" ");
            stat.score = Int32.Parse(fields[0]);
            while (!streamReader.EndOfStream)
            {
                fields = streamReader.ReadLine().Split(" ");
                arrtemp[i] = proxy.ToDispObj(fields);
                i++;
                

            }

            
            streamReader.Close();
            return arrtemp;
            
            
            
            
        }
        public DispConteiner LoadGameXML(String filesave,  ref SettingsField sett)
        {
            
           
            //return arrtemp;
            var serializer = new XmlSerializer(typeof(DispConteiner), new Type[] {typeof(SettingsField), typeof(Brick), typeof(Platform), typeof(Ball), typeof(BonusItem),typeof(StatusBar),typeof(MenuItem),typeof(Label),typeof(Button), typeof(Menu) });
            var serializs = new XmlSerializer(typeof(Settings));
            var serialist = new XmlSerializer(typeof(Statistics));
            var serialobj = new XmlSerializer(typeof(DispObj),extraTypes:new Type[]{typeof(Menu),typeof(StatusBar), typeof(SettingsField)});
            
            using (FileStream stream = new FileStream("settings.txt",FileMode.Open))
            {
                settings = (Settings)serializs.Deserialize(stream);
            }
            
            using (FileStream stream = new FileStream("Stat.txt",FileMode.Open))
            {
                stat = (Statistics)serialist.Deserialize(stream);
            }
            using (FileStream stream = new FileStream("Menu.txt",FileMode.Open))
            { 
                menu = (Menu)serialobj.Deserialize(stream);
            }
            using (FileStream stream = new FileStream("StatusBar.txt",FileMode.Open))
            {
                statusBar = (StatusBar)serialobj.Deserialize(stream);
            }
            using (FileStream stream = new FileStream("SettingsField.txt",FileMode.Open))
            {
                sett = (SettingsField)serialobj.Deserialize(stream);
            }
            gf = new GameField(this);
            using (FileStream stream = new FileStream(filesave, FileMode.Open))
            {

                return    (DispConteiner)serializer.Deserialize(stream); 
                // 
            }
            
            
        }

        public void PauseGame()
        { 
            this.isPaused = !isPaused;

        }
        private void GameOverHadler(object sender, EventArgs e)
        {
            sound_lose.Play();
            GameField temp = (GameField)sender;
            
            Label labelgameover = new Label(480, 330, 780, 660,
                Color.Red, true, false, "GameOver", 50, "Intro.otf");
            MenuItem menuItemgameover = new MenuItem(480, 500,
                780, 600,
                Color.Red, true, false, "    Restart", 15, "Intro.otf",25);
            menuItemgameover.button.ButtonClick += ButtonGameOverOnClick;
            while (GameStatus == 2)
            {
                window.DispatchEvents();
                window.Clear();
                temp.DrawAll();
                labelgameover.Draw(window);
                menuItemgameover.Draw(window);
                menuItemgameover.button.OnClick(window);
                window.Display();
            }
            
        }
        private void ButtonGameOverOnClick(object sender, EventArgs e)
        {
            GameStatus = 0;

        }
        private void NextLevelHandler(object sender, EventArgs e)
        {
            sound_win.Play();
            GameField temp = (GameField)sender;
            
            Label labelgameover = new Label(480, 330, 780, 660,
                Color.Red, true, false, "Level Complete", 50, "Intro.otf");
            MenuItem menuItemgameover = new MenuItem(480, 500,
                780, 600,
                Color.Red, true, false, "    Go Next", 15, "Intro.otf",25);
            menuItemgameover.button.ButtonClick += ButtonNextLevelClick;
            while (GameStatus == 3)
            {
                window.DispatchEvents();
                window.Clear();
                temp.DrawAll();
                labelgameover.Draw(window);
                menuItemgameover.Draw(window);
                menuItemgameover.button.OnClick(window);
                window.Display();
            }
            
        }
        private void ButtonNextLevelClick(object sender, EventArgs e)
        {
            GameStatus = 0;

        }
       
        
    }
    

}
