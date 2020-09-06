using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    public abstract class MyBrush
    {
        public Color color { get; protected set; } // color for brushes
        public string imagePath { get; protected set; } // image path 

        public MyBrush(Color color, string imagePath)
        {
            this.color = color;
            this.imagePath = imagePath;
        }
        public abstract Brush GetBrush();
        public abstract void ChangeColor(Color color);
        public virtual void ChangeImage(string imagePath) => this.imagePath = imagePath;
    } // MyBrush

    public class MySolidBrush : MyBrush
    {
        SolidBrush brush;
        public MySolidBrush(Color color, string imagePath = "picture.jpg") : base(color, imagePath)
        {
            brush = new SolidBrush(color);
        }
        public override void ChangeColor(Color color)
        {
            this.color = color;
            brush.Color = color;
        }
        public override Brush GetBrush() => brush;
    } // MySolidBrush

    public class MyHatchBrush : MyBrush
    {
        HatchBrush brush;
        public MyHatchBrush(Color color, string imagePath = "picture.jpg") : base(color, imagePath)
        {
            brush = new HatchBrush(HatchStyle.Cross, color, Color.Red);
        }
        public override void ChangeColor(Color color)
        {
            this.color = color;
            brush = new HatchBrush(HatchStyle.Cross, color, Color.Red);
        }

        public override Brush GetBrush() => brush;
    } // MyHatchBrush

    public class MyGradientBrush : MyBrush
    {
        PathGradientBrush brush;
        public MyGradientBrush(Color color, string imagePath = "picture.jpg") : base(color, imagePath)
        {
            Point[] points = { new Point(20, 20), new Point(40, 140), new Point(100, 40) };
            brush = new PathGradientBrush(points, WrapMode.TileFlipXY);
            brush.SurroundColors = new Color[] { color, Color.Red, Color.Cyan };
            brush.CenterColor = Color.White;
            brush.CenterPoint = new PointF(50, 40);
            brush.RotateTransform(30);
        }
        public override void ChangeColor(Color color)
        {
            this.color = color;
            brush.SurroundColors = new Color[] { color, Color.Red, Color.Cyan };
        }
        public override Brush GetBrush() => brush;
    } // MyGradientBrush

    public class MyTextureBrush : MyBrush
    {
        TextureBrush brush;
        public MyTextureBrush(Color color, string imagePath = "picture.jpg") :base(color, imagePath)
        {
            CreateNewTextureBrush();
        }
        public override void ChangeColor(Color color)=> this.color = color;

        public override Brush GetBrush() => brush;
        public override void ChangeImage(string imagePath)
        {
            this.imagePath = imagePath;
            CreateNewTextureBrush();
        }

        void CreateNewTextureBrush()
        {
            brush = new TextureBrush(new Bitmap(imagePath));
            brush.RotateTransform(45);
            brush.ScaleTransform(1.5f, 1.25f);
            brush.TranslateTransform(150, 150);
        }
    } // MyTextureBrush
}
