using System.Drawing;

namespace Balance
{
    interface IDragable
    {
        void Drag(Point offset);
        IDragable StartDrag(Point p);
        void StopDrag(Point p);
    }

    interface IDrawable
    {
        void Draw(Graphics graph);    
    }
}
