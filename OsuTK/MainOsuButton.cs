using System.Drawing;
using System.IO;
using OpenTK;

namespace OsuTK
{
    public class MainOsuButton : Sprite
    {
        protected bool MouseOver;

        public MainOsuButton(Vector2 position, Vector2 size)
            : base(Path.Combine(OsuTK.SkinDirectory, "OsuTKIcon.png"), position, size)
        {
        }

        public MainOsuButton(Vector2 position) : base(Path.Combine(OsuTK.SkinDirectory, "OsuTKIcon.png"), position)
        {
        }

        public void MouseMoved(float x, float y)
        {
            var rect = new Rectangle((int) Position.X, (int) Position.Y, (int) Size.X, (int) Size.Y);
            if (MouseOver)
            {
                if (rect.Contains((int) x, (int) y)) return;

                MouseOver = false;
                var xDiff = Size.X - Size.X/1.2f;
                var yDiff = Size.Y - Size.Y/1.2f;
                Position = new Vector2(Position.X + (xDiff/2), Position.Y + (yDiff/2));
                Size /= 1.2f;
            }
            else if (rect.Contains((int) x, (int) y))
            {
                MouseOver = true;
                var xDiff = (Size.X*1.2f) - Size.X;
                var yDiff = (Size.Y*1.2f) - Size.Y;
                Position = new Vector2(Position.X - (xDiff/2), Position.Y - (yDiff/2));
                Size *= 1.2f;
            }
        }
    }
}