using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OsuTK
{
    public class Sprite : IDisposable, IRenderable
    {
        protected Sprite(Texture texture, Vector2 position, Vector2 size)
        {
            Texture = texture;
            Position = position;
            Size = size;
        }

        public Sprite(string sprite, Vector2 position, Vector2 size) : this(new Texture(sprite), position, size)
        {
        }

        public Sprite(string sprite, Vector2 position)
        {
            Texture = new Texture(sprite);
            Position = position;
            Size = Texture.Size;
        }

        protected Texture Texture { get; }
        protected Vector2 Position { get; set; }
        protected Vector2 Size { get; set; }

        public void Dispose()
        {
            Texture.Dispose();
        }

        public void Render()
        {
            Texture.Bind();
            GL.Translate(Position.X, Position.Y, 0);

            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 0);
            GL.Vertex2(0, 0);

            GL.TexCoord2(1, 0);
            GL.Vertex2(Size.X, 0);

            GL.TexCoord2(1, 1);
            GL.Vertex2(Size.X, Size.Y);

            GL.TexCoord2(0, 1);
            GL.Vertex2(0, Size.Y);

            GL.End();

            Texture.Unbind();
        }

        public void Resize(int width, int height)
        {
        }
    }
}