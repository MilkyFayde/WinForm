using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public class Painter
    {
        public Action<Graphics, Pen, Point, Point, MyFigures> action; // delegate for painting
        public bool BrushOn = false; // if fill is On
        public MyBrush brush { get; private set; } 
        public Painter()
        {
            action = PenMode;
            brush = new MySolidBrush(Color.Black);
        }

        public void ChangeToPenMode() => action = PenMode;
        public void ChangeToLineMode() => action = LineMode;
        public void ChangeToRectangleMode() => action = RectangleMode;
        public void ChangeToCircleMode() => action = CircleMode;
        public void ChangeToTriangleMode() => action = TriangleMode;
        public bool IsPenMode() => action == PenMode;

        void PenMode(Graphics graphics, Pen pen, Point start, Point end, MyFigures figures)
        {
            graphics.DrawLine(pen, start, end);
            figures.Add(new MyPencil(start, end, pen.Color, pen.Width));
        } // PenMode
        void LineMode(Graphics graphics, Pen pen, Point start, Point end, MyFigures figures)
        {
            graphics.DrawLine(pen, start, end);
            figures.Add(new MyPencil(start, end, pen.Color, pen.Width));
        } // LineMode
        void RectangleMode(Graphics graphics, Pen pen, Point start, Point end, MyFigures figures)
        {
            if (start.X > end.X) // if start point > end point swap
            {
                int temp = start.X;
                start.X = end.X;
                end.X = temp;
            }

            if (start.Y > end.Y) // if start point > end point swap
            {
                int temp = start.Y;
                start.Y = end.Y;
                end.Y = temp;
            }
            int width = end.X - start.X;
            int height = end.Y - start.Y;
            Rectangle rect = new Rectangle(start.X, start.Y, width, height);
            graphics.DrawRectangle(pen, rect);
            if (BrushOn)
            {
                graphics.FillRectangle(brush.GetBrush(), rect);
                figures.Add(new MyRectangle(start, width, height, pen.Color, pen.Width, brush));
            }
            else figures.Add(new MyRectangle(start, width, height, pen.Color, pen.Width));
        } // RectangleMode
        void CircleMode(Graphics graphics, Pen pen, Point start, Point end, MyFigures figures)
        {
            if (start.X > end.X) // if start point > end point swap
            {
                int temp = start.X;
                start.X = end.X;
                end.X = temp;
            }

            if (start.Y > end.Y) // if start point > end point swap
            {
                int temp = start.Y;
                start.Y = end.Y;
                end.Y = temp;
            }

            int width = end.X - start.X;
            int height = end.Y - start.Y;
            Rectangle rect = new Rectangle(start.X, start.Y, width, height);
            graphics.DrawEllipse(pen, rect);

            if (BrushOn)
            {
                graphics.FillEllipse(brush.GetBrush(), rect);
                figures.Add(new MyCircle(start, width, height, pen.Color, pen.Width, brush));
            }
            else figures.Add(new MyCircle(start, width, height, pen.Color, pen.Width));
        } // CircleMode
        void TriangleMode(Graphics graphics, Pen pen, Point start, Point end, MyFigures figures)
        {
            if (start.X > end.X) // if start point > end point swap
            {
                int temp = start.X;
                start.X = end.X;
                end.X = temp;
            }

            if (start.Y > end.Y) // if start point > end point swap
            {
                int temp = start.Y;
                start.Y = end.Y;
                end.Y = temp;
            }

            Point point1 = new Point(end.X/2 , start.Y);
            Point point2 = new Point(end.X, end.Y);
            Point point3 = new Point(start.X, end.Y);

            graphics.DrawPolygon(pen, new Point[] { point1, point2, point3 });

            if (BrushOn)
            {
                graphics.FillPolygon(brush.GetBrush(), new Point[] { point1, point2, point3 });
                figures.Add(new MyTriangle(point1, point2, point3, pen.Color, pen.Width, brush));
            }
            else figures.Add(new MyTriangle(point1, point2, point3, pen.Color, pen.Width));
        } // RectangleMode
        public void ChangeBrushToSolid() => brush = new MySolidBrush(brush.color, brush.imagePath);
        public void ChangeBrushToHatch() => brush = new MyHatchBrush(brush.color, brush.imagePath);
        public void ChangeBrushToGradient() => brush = new MyGradientBrush(brush.color, brush.imagePath);
        public void ChangeBrushToTexture() => brush = new MyTextureBrush(brush.color, brush.imagePath);
        public void ChangeImage(string imagePath) => brush.ChangeImage(imagePath);
    }
}
