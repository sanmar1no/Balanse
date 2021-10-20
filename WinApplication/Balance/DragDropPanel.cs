
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace Balance
{
    /// <summary>
    /// Панель для отрисовки блоков
    /// </summary>
    public partial class DragDropPanel : UserControl
    {
        IEnumerable<object> block;
        IDragable dragged;

        Point offset; // смещение фигуры
        Point mDown;  // выбрана точка

        public DragDropPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.Selectable | ControlStyles.StandardClick, true);
        }

        public void Build(IEnumerable<object> block)
        {
            this.block = block;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (block != null)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                e.Graphics.TranslateTransform(offset.X,offset.Y);
                foreach (IDrawable bl in block.OfType<IDrawable>())
                {
                    bl.Draw(e.Graphics);
                }
            }
            else return;            
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mDown = e.Location;
                //найти блок под курсором
                Point p = new Point(e.X - offset.X, e.Y - offset.Y);
                IDragable drag = block.OfType<IDragable>().FirstOrDefault();
                if (drag!=null)
                {
                    dragged = drag.StartDrag(p); //начало перетаскивания блока
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point newPos = new Point(e.Location.X - mDown.X, e.Location.Y - mDown.Y);
                mDown = e.Location;
                if (dragged != null)
                {
                    dragged.Drag(mDown);
                }
                else
                {
                    offset = new Point(offset.X + mDown.X, offset.Y + mDown.Y);
                }
                Invalidate();
            }
        }
    }
}
