using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace BPConsole
{
    public class ConfigReader
    {
        public static List<string> GetTaskList(string inputFilename)
        {
            List<string> result = new List<string>();
            const string STR_Comment_Designator = "//";
            string temp, temp_internal;

            try
            {
                using (StreamReader reader = new StreamReader(inputFilename))
                {
                    while (!reader.EndOfStream)
                    {
                        temp = reader.ReadLine();
                        // only add to list if it isn't a comment

                        if (!temp.TrimStart().StartsWith(STR_Comment_Designator))
                        {
                            temp_internal = (temp.Split(STR_Comment_Designator.ToCharArray()).First());
                            // only add if it is a non-empty string
                            if (! String.IsNullOrEmpty(temp_internal))
                            {
                                result.Add(temp_internal);
                            }
                        }
                    }
                    reader.Close();
                }

            }
            catch (FileNotFoundException)
            {
                // do nothing, let it return empty list
                Console.WriteLine("Configuration file not found: {0}", inputFilename);
            }

            return result;
        }
    }
}
