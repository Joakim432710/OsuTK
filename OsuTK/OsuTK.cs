using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OsuTK
{
    public class OsuTK : GameWindow, IStateBasedGame
    {
        public static string SkinDirectory = Path.Combine(Path.Combine(Environment.CurrentDirectory, "Skins"), "Default");
        public static GameState MainMenu = new GameState(0, "MainMenu");
        public Dictionary<GameState, GameStateObject> GameStateObjects { get; } = new Dictionary<GameState, GameStateObject>
        {
            [MainMenu] = new MainMenuScreen()
        };
        protected Vector2 IntendedSize = new Vector2(4096, 2160);

        public GameState CurrentGameState { get; private set; }
        public GameStateObject CurrentGameStateObject { get; private set; }


        public GameStateObject GetGameStateObject(GameState state)
        {
            return GameStateObjects[state];
        }

        public void SetGameState(GameState state)
        {
            CurrentGameStateObject.Unload();
            CurrentGameStateObject = GameStateObjects[state]; //TODO: Ensure game is always in valid state
            CurrentGameState = state;
            CurrentGameStateObject.Load(this);
        }

        public void SetMouseBasedViewport(bool follow)
        {
            MouseBasedViewport = follow;
            UpdateViewport();
        }

        public OsuTK()
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            VSync = VSyncMode.On; //TODO: Load preferred from settings file
            GL.ClearColor(Color.CornflowerBlue);
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0.0f, IntendedSize.X, IntendedSize.Y, 0.0f, 0.0f, 1.0f); //Map (0, 0) -> (0, 0) and (Width, Height) -> (1, 1)
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.AlphaTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            Title = "osutk!cuttingedge b20160118";

            CurrentGameState = MainMenu;
            CurrentGameStateObject = GameStateObjects[MainMenu];
            CurrentGameStateObject.Load(this);
        }

        protected bool MouseBasedViewport { get; set; }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //Assume proportions are too much to keep up with
            //Note 1: Favour good looks! Constrain proportions
            //Note 2: On top of constraining proportions make sure that 
            //an image which filled the screen before still fills it 
            //but instead of leaving whitespace has content outside view
            //Note 3: On top of the above we need to center the view on the scaled dimension
            //Note 4: Move the screen based on cursor position to make it feel more alive

            //GL.Viewport(0, 0, (int)(Height * (IntendedSize.X / IntendedSize.Y)), Height); //Derived from Note 1

            //Derived from Note 2
            //if (targetAspectRatio > aspectRatio) //This means our target width is wider, hide width of image
            //    GL.Viewport(0, 0, (int)(Height * (IntendedSize.X / IntendedSize.Y)), Height);
            //else //This means our current width is wider, hide height of image
            //    GL.Viewport(0, 0, Width, (int)(Width * (IntendedSize.Y / IntendedSize.X)));

            //Derived from Note 3
            //if (targetAspectRatio > aspectRatio) //This means our target width is wider, hide width of image
            //{
            //    var newWidth = (int)(Height * targetAspectRatio);
            //    var errorAddition = (Width - newWidth) / 2;
            //    GL.Viewport(errorAddition, 0, newWidth, Height);
            //}
            //else //This means our current width is wider, hide height of image
            //{
            //    var newHeight = (int)(Width / targetAspectRatio);
            //    var errorAddition = (Height - newHeight) / 2;
            //    GL.Viewport(0, errorAddition, Width, newHeight);
            //}
            UpdateViewport();
        }

        private const float ShowPercent = 0.9f;
        private const float HidePercent = 0.1f;
        private MouseState _lastKnownMouseState;
        protected void UpdateViewport()
        {
            //Derived from Note 4 in OnResize
            var aspectRatio = ((float)Width / Height);
            var targetAspectRatio = (IntendedSize.X / IntendedSize.Y);
            if (targetAspectRatio > aspectRatio) //This means our target width is wider, hide width of image
            {
                var newWidth = (int)(Height * targetAspectRatio);
                var errorAddition = (float)(Width - newWidth) / 2;
                if (MouseBasedViewport)
                    GL.Viewport((int)(errorAddition - (int)(0.02 * _lastKnownMouseState.X)), (int)(0.02 * _lastKnownMouseState.Y) - (int)(0.02 * Height), (int)(1.02 * newWidth), (int)(1.02 * Height));
                else
                    GL.Viewport((int)errorAddition, 0, newWidth, Height);
            }
            else //This means our current width is wider, hide height of image
            {
                var newHeight = (int)(Width / targetAspectRatio);
                var errorAddition = (float)(Height - newHeight) / 2;
                if (MouseBasedViewport)
                    GL.Viewport(-(int)(0.02 * _lastKnownMouseState.X), (int)(errorAddition + (int)(0.02 * _lastKnownMouseState.Y) - (int)(0.02 * Height)), (int)(1.02 * Width), (int)(1.02 * newHeight));
                else
                    GL.Viewport(0, (int)errorAddition, Width, newHeight);
            }
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            _lastKnownMouseState = e.Mouse;
            UpdateViewport();
            CurrentGameStateObject.MouseMoved(IntendedSize.X * e.X / Width, IntendedSize.Y * e.Y / Height);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            CurrentGameStateObject.Update(e.Time);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            CurrentGameStateObject.Render();
            SwapBuffers();
        }
    }
}
