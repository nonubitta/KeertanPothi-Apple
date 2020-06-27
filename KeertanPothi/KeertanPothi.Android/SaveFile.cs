
using Android;
using Android.Content;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Webkit;
using Java.IO;
using System;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Xamarin.Forms;
using KeertanPothi.Droid;
using System.Collections.Generic;

[assembly: Xamarin.Forms.Dependency(typeof(SaveFile))]
namespace KeertanPothi.Droid
{
    public class SaveFile : ISaveFile
    {
        void ISaveFile.SaveFile(string fileName, string json)
        {
            string folderName = "KeertanPothi";
            var externalPath = Android.OS.Environment.ExternalStorageDirectory;
            string folderPath = System.IO.Path.Combine(externalPath.ToString(), folderName);
            DirectoryInfo dir = new DirectoryInfo(folderPath);
            if (!dir.Exists)
                dir.Create();
               
            var filePath = System.IO.Path.Combine(folderPath, fileName);
            System.IO.File.WriteAllText(filePath, json);
        }

        List<string> ISaveFile.ReadFile()
        {
            string folderName = "KeertanPothi";
            var externalPath = Android.OS.Environment.ExternalStorageDirectory;
            string folderPath = System.IO.Path.Combine(externalPath.ToString(), folderName);
            DirectoryInfo dir = new DirectoryInfo(folderPath);
            if (!dir.Exists)
            {
                return null;
            }
            else
            {
                FileInfo[] files = dir.GetFiles("*.json"); ;
                if (files.Length > 0)
                {
                    List<string> jsons = new List<string>();
                    foreach (FileInfo file in files)
                    {
                        FileStream stream = file.OpenRead();
                        string js = string.Empty;
                        using (StreamReader read = new StreamReader(stream))
                        {
                            js = read.ReadToEnd();
                        }
                        jsons.Add(js);
                    }
                    return jsons;
                }
                else
                    return null;
            }
        }
    }
}