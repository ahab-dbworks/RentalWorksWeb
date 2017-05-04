using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReplaceInFile
{
    class Program
    {
        //---------------------------------------------------------------------------------------------
        static void Main(string[] args)
        {
            try
            {
                string path = string.Empty;
                string pattern = string.Empty;
                string replacement = string.Empty;
                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        case "-path":
                            i++;
                            path = args[i];
                            break;
                        case "-pattern":
                            i++;
                            pattern = args[i];
                            break;
                        case "-replacement":
                            i++;
                            replacement = args[i];
                            break;
                    }
                }
                string text = File.ReadAllText(path);
                text = Regex.Replace(text, pattern, replacement, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                File.WriteAllText(path, text);
            }
            catch(Exception ex)
            {
                Console.Error.Write(ex.Message + ex.StackTrace);
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
