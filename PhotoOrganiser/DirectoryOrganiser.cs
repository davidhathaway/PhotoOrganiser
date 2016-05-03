using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoOrganiser
{
    public class DirectoryOrganiser
    {
        public string RootDirectory { get; set; }

        public DirectoryInfo RootDirectoryInfo { get; set; }

        public DirectoryInfo OrganisedDirectoryInfo { get; set; }

        public const string OrganisedPathName = "Organised";

        public IAttributeProvidor AttributeHandler { get; set; }

        public System.Collections.Concurrent.ConcurrentQueue<string> _fileQ;

        private IAttributeOrganiser organiser;


        public string TargetAttributeKey { get; set; }
        public DirectoryOrganiser(string root, IAttributeProvidor attributeProvidor, IAttributeOrganiser organiser)
        {
            RootDirectory = root;
            AttributeHandler = attributeProvidor;
            this.organiser = organiser;
           
            _fileQ = new System.Collections.Concurrent.ConcurrentQueue<string>();
            RootDirectoryInfo = new DirectoryInfo(RootDirectory);
            var organisedFullPath = Path.Combine(RootDirectory, OrganisedPathName);
            this.OrganisedDirectoryInfo = new DirectoryInfo(organisedFullPath);
        }

        public void Organise()
        {
            this.CreateOrganisedFolder();

            ThreadPool.QueueUserWorkItem((c) => {


                this.EnqueueAllFiles();


            });

            Thread.Sleep(1000);


            var tasks = new List<Task>();

            var maxThreads = 8;

            while(!_fileQ.IsEmpty)
            {
               

                if(tasks.Count < maxThreads)
                {

                    var task = Task.Run(() => { DequeueFile(); });
                    tasks.Add(task);

                }
                else
                {
                    Thread.Sleep(1000);
                }

                tasks = tasks.Where(x => !x.IsCompleted).ToList();

                Console.Clear();
                Console.WriteLine("Current Threads: " + tasks.Count);
                Console.WriteLine("Current Queue:" + _fileQ.Count);
            }
          


        }

        private void CreateOrganisedFolder()
        {
            if (!this.OrganisedDirectoryInfo.Exists)
            {
                OrganisedDirectoryInfo.Create();
            }
        }

        private void EnqueueAllFiles()
        {
            //get files at dir
            var filesAtDir = this.RootDirectoryInfo.EnumerateFiles();
            foreach (var f in filesAtDir)
            {
                _fileQ.Enqueue(f.FullName);
            }

            var subFolders = RootDirectoryInfo.GetDirectories("*", SearchOption.TopDirectoryOnly);
            foreach (var subDir in subFolders)
            {
                var subFolderFiles = subDir.EnumerateFiles("*", SearchOption.AllDirectories);

                foreach (var f in subFolderFiles)
                {
                    _fileQ.Enqueue(f.FullName);
                }
            }
        }

        private void DequeueFile()
        {
            var sourceFileName = string.Empty;
            if (_fileQ.TryDequeue(out sourceFileName))
            {
                var attributes = this.AttributeHandler.GetAttributes(sourceFileName);

                //convert to frendly folder name
                var destDir = this.organiser.GetDirectory(attributes);
                EnsureDir(destDir);

                var fileName = this.organiser.GetFileName(attributes, sourceFileName);
                var destFileName = Path.Combine(destDir, sourceFileName);

                File.Move(sourceFileName, destFileName);
            }
        }



        private void EnsureDir(string path)
        {
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
