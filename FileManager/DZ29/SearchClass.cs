using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DZ29
{
    public class SearchFile
    {
        public string Name { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string Size { get; private set; }
        public string Path { get; private set; }
        public bool IsFolder { get; private set; }

        public SearchFile(string name, DateTime creationDate, string size, string path, bool isFolder = false)
        {
            Name = name;
            CreationDate = creationDate;
            Size = size;
            Path = path;
            IsFolder = isFolder;
        }

        public string[] SplitPath() 
        {
            var path = Path.Split('\\');
            path[0] += '\\';
            return path;
        }
    } // class SearchFile

    public class SearchList
    {
        string SearchName;
        string SearchPath;
        public SearchList(string searchPath)
        {
            SearchPath = searchPath;
        }
        public List<SearchFile> Files { get; private set; } = new List<SearchFile>();

        public void Add(string name, DateTime creationDate, string size, string path) => Files.Add(new SearchFile(name, creationDate, size, path));
        public int Lenght => Files.Count;

        public void Search(string searchName)
        {
            Files.Clear();
            SearchName = searchName;
            Scan(SearchPath);
        } // void Search

        void Scan(string path)
        {
            DirectoryInfo dinfo = new DirectoryInfo(path);
            FileInfo[] files = dinfo.GetFiles();

            foreach (var file in files.Where(item => item.Name.ToLower().Contains(SearchName.ToLower())))
                Files.Add(new SearchFile(file.Name, file.CreationTime, file.Length.ToString(), path));

            DirectoryInfo[] dirs = dinfo.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                if(dir.Name.ToLower().Contains(SearchName.ToLower())) 
                    Files.Add(new SearchFile(dir.Name, dir.CreationTime,"", path, true));
                Scan(dir.FullName);
            } // foreach
        } // Scan

        public SearchFile this[int index]
        {
            get
            {
                return Files[index];
            } // get
        }

        public IEnumerator<SearchFile> GetEnumerator()
        {
            foreach (var file in Files)
                yield return file;
        } // GetEnumerator

    } // class SearchList
}
