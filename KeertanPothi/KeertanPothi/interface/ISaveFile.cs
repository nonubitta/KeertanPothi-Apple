using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KeertanPothi
{
    public interface ISaveFile
    {
        void SaveFile(string fileName, string json);
        List<string> ReadFile();
    }
}
