using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ContactBook
{
    public class Person
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string PicPath { get; set; }
        public string Category { get; set; }

        public Person(string fname, string lname, string address, string phoneNumber, string category, string pic = null)
        {
            FName = fname;
            LName = lname;
            Address = address;
            PhoneNumber = phoneNumber;
            Category = category;
            PicPath = pic;
        }
    } // class Person

    public class Category
    {
        public List<string> Categories { get; private set; } = new List<string>();
        public string FileName { get; private set; } = "Categories.xml";
        public int Count => Categories.Count;
        public void Add(string name) => Categories.Add(name);

        public bool Exists(string name) => Categories.Exists(x => x == name);
        public int IndexOf(string name) => Categories.IndexOf(name);
        public void RenameCategory(string oldCategory, string newCategory) => Categories = Categories.Select(x => x.Replace(oldCategory, newCategory)).ToList();


        public void Save()
        {
            XmlWriter writer;
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineChars = "\r\n";
            settings.Encoding = Encoding.ASCII;
            settings.NewLineOnAttributes = false;

            writer = XmlWriter.Create(FileName, settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("Categories");

            foreach (var category in Categories)
            {
                writer.WriteStartElement("Category");
                writer.WriteAttributeString("Category", category);
                writer.WriteEndElement();
            } // foreach

            writer.WriteEndElement();
            writer.WriteEndDocument();

            writer.Flush();
            writer.Close();
        } // Save

        public void Load()
        {
            Categories.Clear();

            XmlReader reader;
            reader = XmlReader.Create(FileName);

            while (reader.Read())
            {
                if (reader.HasAttributes)
                {
                    if (reader.Name == "Category")
                    {
                        reader.MoveToFirstAttribute();
                        Categories.Add(reader.Value);
                    } // if
                } // if
            } // while
            reader.Close();
        } // Load
    } // class Category

    public class Group
    {
        public List<Person> Persons { get; private set; } = new List<Person>();
        string FileName = "Group.xml";

        public void Add(Person person) => Persons.Add(person);
        public bool PhoneExists(string phone) => Persons.Exists(x => x.PhoneNumber == phone);

        public void RenameCategory(string oldCategory, string newCategory)
        {
            for (int i = 0; i < Persons.Count; i++)
            {
                if (Persons[i].Category == oldCategory)
                    Persons[i].Category = newCategory;
            }
        } // RenameCategory
        public void Save()
        {
            XmlWriter writer;
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineChars = "\r\n";
            settings.Encoding = Encoding.ASCII;
            settings.NewLineOnAttributes = false;

            writer = XmlWriter.Create(FileName, settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("Group");

            foreach (var person in Persons)
            {
                writer.WriteStartElement("Person");
                writer.WriteAttributeString("FName", person.FName);
                writer.WriteAttributeString("LName", person.LName);
                writer.WriteAttributeString("Address", person.Address);
                writer.WriteAttributeString("PhoneNumber", person.PhoneNumber);
                writer.WriteAttributeString("Category", person.Category);
                writer.WriteAttributeString("PicPath", person.PicPath);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();

            writer.Flush();
            writer.Close();

        } // Save

        public void Load()
        {
            Persons.Clear();

            XmlReader reader;
            reader = XmlReader.Create(FileName);

            while (reader.Read())
            {
                if (reader.HasAttributes)
                {
                    if (reader.Name == "Person")
                    {
                        reader.MoveToFirstAttribute();
                        string fname = reader.Value;

                        reader.MoveToNextAttribute();
                        string lname = reader.Value;

                        reader.MoveToNextAttribute();
                        string address = reader.Value;

                        reader.MoveToNextAttribute();
                        string phoneNumber = reader.Value;

                        reader.MoveToNextAttribute();
                        string category = reader.Value;

                        reader.MoveToNextAttribute();
                        string pic = reader.Value;

                        Persons.Add(new Person(fname, lname, address, phoneNumber, category, pic));
                    } // if
                } // if
            } // while
            reader.Close();
        } // Load

        public List<Person> FindByPhone(string phone) => Persons.Where(x => x.PhoneNumber.Contains(phone)).ToList();

        public List<Person> FindByLName(string lName) => Persons.Where(x => x.LName.Contains(lName)).ToList();
        public List<Person> FindPersons(string lName, string phone) => Persons.Where(x => x.PhoneNumber.Contains(phone)).Where(x => x.LName.ToLower().Contains(lName.ToLower())).ToList();

        public void DeleteUnusedPhotos()
        {
            DirectoryInfo dinfo = new DirectoryInfo("Photos");
            FileInfo[] files = dinfo.GetFiles();
            foreach (FileInfo current in files)
                if (!Persons.Exists(x => x.PicPath == $"Photos\\{current.Name}")) File.Delete(current.FullName);
        } // DeleteUnusedPhotos

    } // class Group
}
