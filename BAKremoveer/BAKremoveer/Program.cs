// See https://aka.ms/new-console-template for more information

//This removes the ".bak" extension from all files in a given directory.
//Not as slick as the previous version of this program (with namespace, class, and main), but we're dusting off cobwebs here.

string dir = @"/Users/dstrube/Downloads/temp/";
string ext = ".bak";

string file1, file2;
while (isFileWithBak(dir, ext)){
    file1 = getFileWithBak(dir, ext);
    file2 = file1.Substring(0,file1.IndexOf(ext));
    File.Move(file1, file2);
}

static bool isFileWithBak(string dir, string ext) {
    string [] files = Directory.GetFiles(dir, "*"+ext);
    bool answer=false;
    if (files.Length > 0)
        answer=true;
    return answer;
}

static string getFileWithBak(string dir, string ext){
    string [] files = Directory.GetFiles(dir, "*"+ext);
    return files[0];
}

