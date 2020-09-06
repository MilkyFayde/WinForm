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

        public PaintCoords(Point start, Color color, float penWidth)
        {
            this.start = start;
            this.color = color;
            this.penWidth = penWidth;
        }
        public abstract void Draw(Graphics graphics);
        public abstract void WriteData(XmlWriter writer);

        protected void WritePaintCoords(XmlWriter writer)
        {
            writer.WriteAttributeString("startX", start.X.ToString());
            writer.WriteAttributeString("startY", start.Y.ToString());
            writer.WriteAttributeString("color", color.ToArgb().ToString());
            writer.WriteAttributeString("penWitdh", penWidth.ToString());
        }

        protected static void GetPaintCoords(XmlReader reader, out Point start, out Color color, out float penWidth)
        {
            reader.MoveToFirstAttribute();
            int startX = int.Parse(reader.Value);

            reader.MoveToNextAttribute();
            int startY = int.Parse(reader.Value);
            start = new Point(startX, startY);

            reader.MoveToNextAttribute();
            int a = int.Parse(reader.Value);

            color = Color.FromArgb(a);

            reader.MoveToNextAttribute();
            penWidth = float.Parse(reader.Value);
        }
    } // class PaintCoords

    public class MyPencil : PaintCoords
    {
        public Point end { get; set; } = new Point();
        public MyPencil(Point start, Point end, Color color, float penWidth) : base(start, color, penWidth)
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
            GetPaintCoords(reader, out Point start, out Color color, out float penWidth);

            reader.MoveToNextAttribute();
            int endtX = int.Parse(reader.Value);

            reader.MoveToNextAttribute();
            int endtY = int.Parse(reader.Value);

            return new MyPencil(start, new Point(endtX, endtY), color, penWidth); ;
        }

    } // class MyPencil

    public class MyRectangle : PaintCoords
    {
        public int width { get; set; }
        public int height { get; set; }
        public MyRectangle(Point start, int width, int height, Color color, float penWidth) : base(start, color, penWidth)
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
            GetPaintCoords(reader, out Point start, out Color color, out float penWidth);
            //start.Y -= 20;
            reader.MoveToNextAttribute();
            int width = int.Parse(reader.Value);

            reader.MoveToNextAttribute();
            int height = int.Parse(reader.Value);

            return new MyRectangle(start, width, height, color, penWidth);
        }
    } // class MyRectangle

    public class MyCircle : MyRectangle
    {
        public MyCircle(Point start, int width, int height, Color color, float penWidth) : base(start, width, height, color, penWidth)
        {
        }

        public override void Draw(Graphics graphics)
        {
            Pen pen = new Pen(color, penWidth);
            pen.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
            Rectangle rect = new Rectangle(start.X, start.Y, width, height);
            graphics.DrawEllipse(pen, rect);
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
            GetPaintCoords(reader, out Point start, out Color color, out float penWidth);

            reader.MoveToNextAttribute();
            int width = int.Parse(reader.Value);

            reader.MoveToNextAttribute();
            int height = int.Parse(reader.Value);

            return new MyCircle(start, width, height, color, penWidth);
        }
    } // class MyCircle
}
