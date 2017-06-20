namespace FwCore.Mustache
{
    public abstract class Part
    {
        public abstract void Render(RenderContext context);

        public abstract string Source();
    }
}