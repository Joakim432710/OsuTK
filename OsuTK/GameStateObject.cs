namespace OsuTK
{
    public abstract class GameStateObject
    {
        public abstract void Load(IStateBasedGame game);
        public abstract void Unload();
        public abstract void Render();
        public abstract void Update(double dT);
        public abstract void MouseMoved(float x, float y);
    }
}