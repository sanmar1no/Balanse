using System.Drawing;

namespace Balance
{
    interface IDragable
    {
        void Drag(Point offset);
    }

    interface IDrawable
    {
        void Draw(Graphics graph);    
    }
}
