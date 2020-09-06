using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Xml;

namespace Paint
{
    public abstract class PaintCoords
    {
        public Point start { get; set; } = new Point();
        public Color color { get; set; }
        public float penWidth { get; set; }
        public MyBrush brush;

        public PaintCoords(Point start, Color color, float penWidth, MyBrush brush = null)
        {
            this.start = start;
            this.color = color;
            this.penWidth = penWidth;
            this.brush = brush;
        }
        public abstract void Draw(Graphics graphics);
        public abstract void WriteData(XmlWriter writer);

        protected void WritePaintCoords(XmlWriter writer)
        {
            if (brush == null) writer.WriteAttributeString("brush", "None");
            else
            {
                writer.WriteAttributeString("brush", brush.GetType().ToString());
                writer.WriteAttributeString("brushColor", brush.color.ToArgb().ToString());
                if (brush is MyTextureBrush) writer.WriteAttributeString("brushImage", brush.imagePath);
            }

            writer.WriteAttributeString("startX", start.X.ToString());
            writer.WriteAttributeString("startY", start.Y.ToString());
            writer.WriteAttributeString("color", color.ToArgb().ToString());
            writer.WriteAttributeString("penWitdh", penWidth.ToString());
        }

        protected static void GetPaintCoords(XmlReader reader, out Point start, out Color color, out float penWidth, out MyBrush brush)
        {
            reader.MoveToFirstAttribute();
            LoadBrush(reader, out brush);

            reader.MoveToNextAttribute();
            int startX = int.Parse(reader.Value);

            reader.MoveToNextAttribute();
            int startY = int.Parse(reader.Value);
            start = new Point(startX, startY);

            reader.MoveToNextAttribute();
            int colorInt = int.Parse(reader.Value);

            color = Color.FromArgb(colorInt);

            reader.MoveToNextAttribute();
            penWidth = float.Parse(reader.Value);
        } // GetPaintCoords
        static void LoadBrush(XmlReader reader, out MyBrush brush)
        {
            string myBrushIs = reader.Value;
            Color brushColor;
            int colorInt;
            switch (myBrushIs)
            {
                case "Paint.MySolidBrush":
                    reader.MoveToNextAttribute();
                    colorInt = int.Parse(reader.Value);
                    brushColor = Color.FromArgb(colorInt);
                    brush = new MySolidBrush(brushColor);
                    break;
                case "Paint.MyHatchBrush":
                    reader.MoveToNextAttribute();
                    colorInt = int.Parse(reader.Value);
                    brushColor = Color.FromArgb(colorInt);
                    brush = new MyHatchBrush(brushColor);
                    break;
                case "Paint.MyGradientBrush":
                    reader.MoveToNextAttribute();
                    colorInt = int.Parse(reader.Value);
                    brushColor = Color.FromArgb(colorInt);
                    brush = new MyGradientBrush(brushColor);
                    break;
                case "Paint.MyTextureBrush":
                    reader.MoveToNextAttribute();
                    colorInt = int.Parse(reader.Value);
                    brushColor = Color.FromArgb(colorInt);

                    reader.MoveToNextAttribute();
                    string imagePath = reader.Value;
                    brush = new MyTextureBrush(brushColor, imagePath);
                    //brush = new MyTextureBrush(brushColor);
                    break;
                default:
                    brush = null;
                    break;
            } // switch
        } // LoadBrush

    } // class PaintCoords

    public class MyPencil : PaintCoords
    {
        public Point end { get; set; } = new Point();
        public MyPencil(Point start, Point end, Color color, float penWidth, MyBrush brush = null) : base(start, color, penWidth, brush)
        {
            this.end = end;
        }

        public override void Draw(Graphics graphics)
        {
            Pen pen = new Pen(color, penWidth);
            pen.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
            graphics.DrawLine(pen, start, end);
        }

        public override void WriteData(XmlWriter writer)
        {
            writer.WriteStartElement("MyPencil");
            WritePaintCoords(writer);
            writer.WriteAttributeString("endX", end.X.ToString());
            writer.WriteAttributeString("endY", end.Y.ToString());
            writer.WriteEndElement();
        }

        public static PaintCoords ReadData(XmlReader reader)
        {
            GetPaintCoords(reader, out Point start, out Color color, out float penWidth, out MyBrush brush);

            reader.MoveToNextAttribute();
            int endtX = int.Parse(reader.Value);

            reader.MoveToNextAttribute();
            int endtY = int.Parse(reader.Value);

            return new MyPencil(start, new Point(endtX, endtY), color, penWidth, brush);
        }

    } // class MyPencil

    public class MyRectangle : PaintCoords
    {
        public int width { get; set; }
        public int height { get; set; }
        public MyRectangle(Point start, int width, int height, Color color, float penWidth, MyBrush brush = null) : base(start, color, penWidth, brush)
        {
            this.width = width;
            this.height = height;
        }

        public override void Draw(Graphics graphics)
        {
            Pen pen = new Pen(color, penWidth);
            pen.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
            Rectangle rect = new Rectangle(start.X, start.Y, width, height);
            graphics.DrawRectangle(pen, rect);
            if (brush != null)
                graphics.FillRectangle(brush.GetBrush(), rect);
        }

        public override void WriteData(XmlWriter writer)
        {
            writer.WriteStartElement("MyRectangle");
            WritePaintCoords(writer);
            writer.WriteAttributeString("width", width.ToString());
            writer.WriteAttributeString("height", height.ToString());
            writer.WriteEndElement();
        }

        public static PaintCoords ReadData(XmlReader reader)
        {
            GetPaintCoords(reader, out Point start, out Color color, out float penWidth, out MyBrush brush);
            reader.MoveToNextAttribute();
            int width = int.Parse(reader.Value);

            reader.MoveToNextAttribute();
            int height = int.Parse(reader.Value);

            return new MyRectangle(start, width, height, color, penWidth, brush);
        }
    } // class MyRectangle

    public class MyCircle : MyRectangle
    {
        public MyCircle(Point start, int width, int height, Color color, float penWidth, MyBrush brush = null) : base(start, width, height, color, penWidth, brush)
        {
        }

        public override void Draw(Graphics graphics)
        {
            Pen pen = new Pen(color, penWidth);
            pen.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
            Rectangle rect = new Rectangle(start.X, start.Y, width, height);
            graphics.DrawEllipse(pen, rect);
            if (brush != null)
                graphics.FillEllipse(brush.GetBrush(), rect);
        }

        public override void WriteData(XmlWriter writer)
        {
            writer.WriteStartElement("MyCircle");
            WritePaintCoords(writer);
            writer.WriteAttributeString("width", width.ToString());
            writer.WriteAttributeString("height", height.ToString());
            writer.WriteEndElement();
        }

        public static new PaintCoords ReadData(XmlReader reader)
        {
            GetPaintCoords(reader, out Point start, out Color color, out float penWidth, out MyBrush brush);

            reader.MoveToNextAttribute();
            int width = int.Parse(reader.Value);

            reader.MoveToNextAttribute();
            int height = int.Parse(reader.Value);

            return new MyCircle(start, width, height, color, penWidth, brush);
        }
    } // class MyCircle

    public class MyTriangle : PaintCoords
    {
        Point point2;
        Point point3;
        public MyTriangle(Point point1, Point point2, Point point3, Color color, float penWidth, MyBrush brush = null) : base(point1, color, penWidth, brush)
        {
            this.point2 = point2;
            this.point3 = point3;
        }
        public override void Draw(Graphics graphics)
        {
            Pen pen = new Pen(color, penWidth);
            pen.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
            graphics.DrawPolygon(pen, new Point[] { start, point2, point3 });

            if (brush != null)
                graphics.FillPolygon(brush.GetBrush(), new Point[] { start, point2, point3 });
        }

        public override void WriteData(XmlWriter writer)
        {
            writer.WriteStartElement("MyTriangle");
            WritePaintCoords(writer);
            writer.WriteAttributeString("point2X", point2.X.ToString());
            writer.WriteAttributeString("point2Y", point2.Y.ToString());
            writer.WriteAttributeString("point3X", point3.X.ToString());
            writer.WriteAttributeString("point3Y", point3.Y.ToString());
            writer.WriteEndElement();
        }
        public static PaintCoords ReadData(XmlReader reader)
        {
            GetPaintCoords(reader, out Point start, out Color color, out float penWidth, out MyBrush brush);

            reader.MoveToNextAttribute();
            int pointX = int.Parse(reader.Value);

            reader.MoveToNextAttribute();
            int pointY = int.Parse(reader.Value);
            Point point2 = new Point(pointX, pointY);

            reader.MoveToNextAttribute();
            pointX = int.Parse(reader.Value);

            reader.MoveToNextAttribute();
            pointY = int.Parse(reader.Value);
            Point point3 = new Point(pointX, pointY);

            return new MyTriangle(start, point2, point3, color, penWidth, brush);
        }
    }
}
