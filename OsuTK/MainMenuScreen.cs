using System.IO;
using OpenTK;
using OpenTK.Input;

namespace OsuTK
{
    public class MainMenuScreen : GameStateObject
    {
        public MainMenuScreen()
        { }

        private Sprite Background { get; set; }
        private MainOsuButton MainButton { get; set; } 
        public override void Load(IStateBasedGame game)
        {
            Background = new Sprite(Path.Combine(OsuTK.SkinDirectory, "menu-background.jpg"), Vector2.Zero, new Vector2(4096, 2160));
            MainButton = new MainOsuButton(new Vector2(1328, (2160-1440)/2), new Vector2(1440, 1440));
            game.SetMouseBasedViewport(true);
        }

        public override void Unload()
        {
            Background.Dispose();
            Background = null;
        }

        public override void Render()
        {
            Background.Render();
            MainButton.Render();
        }

        public override void Update(double dT)
        {
        }

        public override void MouseMoved(float x, float y)
        {
            MainButton.MouseMoved(x, y);
        }
    }
}
