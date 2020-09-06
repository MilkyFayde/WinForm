using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Paint
{
    public class MyFigures
    {
        public List<PaintCoords> figures { get; set; } = new List<PaintCoords>();
        public int borderWidth { get; set; }
        public int borderHeight { get; set; }
        XmlSerializer formatter = new XmlSerializer(typeof(List<PaintCoords>));
        public MyFigures(int borderWidth, int borderHeight)
        {
            this.borderWidth = borderWidth;
            this.borderHeight = borderHeight;
        }

        public void Add(PaintCoords coords) => figures.Add(coords);
        public void Clear() => figures.Clear();
        public void ChangeBorderSize(int borderWidth, int borderHeight)
        {
            this.borderWidth = borderWidth;
            this.borderHeight = borderHeight;
        } // ChangeBorderSize

        public void Save(string fileName)
        {
            XmlWriter writer;
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineChars = "\r\n";
            settings.Encoding = Encoding.ASCII;
            settings.NewLineOnAttributes = false;

            writer = XmlWriter.Create(fileName, settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("Figures");
            writer.WriteAttributeString("borderWidth", borderWidth.ToString());
            writer.WriteAttributeString("borderHeight", borderHeight.ToString());

            foreach (var figure in figures)
                figure.WriteData(writer);

            writer.WriteEndElement();
            writer.WriteEndDocument();

            writer.Flush();
            writer.Close();
        } // Save

        public void Load(string fileName)
        {
            Clear();
            XmlReader reader;
            reader = XmlReader.Create(fileName);

            while (reader.Read())
            {
                if (reader.HasAttributes)
                {
                    if (reader.Name == "Figures")
                    {
                        reader.MoveToFirstAttribute();
                        borderWidth = int.Parse(reader.Value);

                        reader.MoveToNextAttribute();
                        borderHeight = int.Parse(reader.Value);
                    } // if
                    if (reader.Name == "MyPencil")
                    {
                        Add(MyPencil.ReadData(reader));
                    } // if
                    if (reader.Name == "MyRectangle")
                    {
                        Add(MyRectangle.ReadData(reader));
                    } // if
                    if (reader.Name == "MyCircle")
                    {
                        Add(MyCircle.ReadData(reader));
                    } // if
                }
            } // while
            reader.Close();
        } // Load
    } // class MyFigures
}
