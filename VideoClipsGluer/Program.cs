using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace VideoClipsGluer
{
    class Program
    {
        static void Main(string[] args)
        {
            const bool stopAtFirstDir = false;

            string workingDir;
            if (args.Length < 1 || args[0] == "")
            {
                workingDir = "/Users/dstrube/Downloads/TeslaCam/SavedClips";
            }
            else
            {
                workingDir = args[0];
            }

            var dirs = new ArrayList(Directory.GetDirectories(workingDir));
            dirs.Sort();
            dirs.TrimToSize();
            foreach (var dir in dirs)
            {
                Console.WriteLine($"dir: {dir}");
                var mp4files = new ArrayList(Directory.GetFiles(dir.ToString(), "*.mp4"));
                //processFileType("all", mp4files, dir.ToString());

                var mp4filesFront = new ArrayList(Directory.GetFiles(dir.ToString(), "*-front.mp4"));
                processFileType("front", mp4filesFront, dir.ToString());

                var mp4filesLeft = new ArrayList(Directory.GetFiles(dir.ToString(), "*-left_repeater.mp4"));
                processFileType("left", mp4filesLeft, dir.ToString());

                var mp4filesRight = new ArrayList(Directory.GetFiles(dir.ToString(), "*-right_repeater.mp4"));
                processFileType("right", mp4filesRight, dir.ToString());

                if (mp4files.Count == 1)
                {
                    //there is only 1 so skip this folder
                    continue;
                }
                if (stopAtFirstDir)
                {
                    break;
                }
            }

            /*
            Console.WriteLine($"Done gluing. Now deleting originals...");
            foreach (var dir in dirs)
            {
                var mp4files = new ArrayList(Directory.GetFiles(dir.ToString(), "*.mp4"));
                mp4files.TrimToSize();
                mp4files.Sort();
                if (mp4files.Count == 1)
                {
                    //there is only 1 so skip this folder
                    continue;
                }
                foreach (var file in mp4files)
                {
                    if (file.ToString().EndsWith("output.mp4", StringComparison.CurrentCulture))
                    {
                        continue;
                    }
                    //File.Delete(file.ToString());
                }
                if (stopAtFirstDir)
                {
                    break;
                }
            }*/
            Console.WriteLine($"Done");
        }

        static void processFileType(string fileType, ArrayList files, string dir)
        {
            files.TrimToSize();
            files.Sort();
            Console.WriteLine($"{fileType} file count: {files.Count}");
            //TODO Only combine files from the same day
            //If multiple days exist in 1 folder, make a separate file for each day
            //Console.WriteLine("List of files:");
            var stringBuilder = new StringBuilder();
            foreach (var file in files)
            {
                FileInfo fileInfo = new FileInfo(file.ToString());
                if (fileInfo.Length == 0)
                {
                    Console.WriteLine($"Empty file; deleting: {file}");
                    File.Delete(file.ToString());
                    continue;
                }
                var content = $"file {file.ToString().Substring(1 + file.ToString().LastIndexOf(Path.DirectorySeparatorChar))}";
                //Console.WriteLine(content);
                stringBuilder.AppendLine(content);

            }

            File.WriteAllText(dir + Path.DirectorySeparatorChar + fileType + "_input.txt", stringBuilder.ToString());

            ProcessWindowStyle windowStyle = ProcessWindowStyle.Hidden;//Normal;              
            Process p1a = new Process();
            try
            {
                Console.WriteLine($"ffmpeg -f concat -i {dir + Path.DirectorySeparatorChar + fileType}_input.txt -c copy {dir + Path.DirectorySeparatorChar + fileType}_output.mp4");
                p1a.StartInfo.FileName = "ffmpeg";
                p1a.StartInfo.WindowStyle = windowStyle;
                p1a.StartInfo.Arguments = $"-f concat -i {dir + Path.DirectorySeparatorChar + fileType}_input.txt -c copy {dir + Path.DirectorySeparatorChar + fileType}_output.mp4";
                p1a.Start();
                Console.WriteLine($"Hit enter after the file stuff is done...");
                Console.ReadLine();
                /*
                */
            }
            finally
            {
                p1a.Dispose();
            }

        }
    }
}
