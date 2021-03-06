using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace DirTree_File_Finder
{
    class DFS : Base_Class_Searcher
    {
        //Attributes
        public event FilePathFound FileLocation;
        private bool findAllOccurrences;
        private bool fileIsFound;
        private Queue<string> filePaths;


        //CTOR
        public DFS(string filename, string current_path) : base(filename, current_path)
        {
            //default
            this.findAllOccurrences = false;
            this.fileIsFound = false;
            this.filePaths = new Queue<string>();
        }

        //Methods
        public void findFileDFS(string current_path, bool findAllOccurrences, bool findAll)
        {
            this.findAllOccurrences = findAllOccurrences;
            List<string> contents = findContents(current_path);  //all content (files and dirs) in a form of abs. path
            foreach (string c in contents)                        //for every content (path) in contents
            {
                if (this.fileIsFound && !this.findAllOccurrences)
                {
                    this.filePaths.Enqueue(c);
                }
                else
                {
                    //Add to search_log
                    this.search_log.Add(c);

                    //Search directory first, findContents already sorted by dir first then file
                    if (Directory.Exists(c) && (c != "." || c != ".."))
                    {
                        this.findFileDFS(c, this.findAllOccurrences, findAll);    //if path c is a directory, go to inside c
                        continue;
                    }
                    else if ((Path.GetFileName(c)).Equals(this.filename))
                    {
                        this.FileLocation(c); //path c is a file found

                        this.foundFilePath.Add(c); //Add found path

                        this.fileIsFound = true;

                        if (findAll == false)
                        {
                            this.findAllOccurrences = false;
                        }
                    }
                }
            }
        }

        // Getter & Setter
        public bool FileIsFound
        {
            set { this.fileIsFound = value; }
            get { return this.fileIsFound; }
        }

        public Queue<string> FilePaths
        {
            set { this.filePaths = value; }
            get { return this.filePaths; }
        }
    }
}