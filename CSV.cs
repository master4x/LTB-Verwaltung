using System;
using System.Collections.Generic;
using System.IO;

namespace LTB_Verwaltung
{
    class CSV
    {
        private static CSV instance = null;
        private static readonly object padlock = new object();

        private readonly string WIN_TMP = FTP.Instance.WIN_TMP;
        private readonly string WIN_FILE = FTP.Instance.CSV_FILE;
        private readonly string WIN_HOME = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";

        private CSV() { }

        public static CSV Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new CSV();
                    }
                    return instance;
                }
            }
        }

        public void ReadCSV()
        {
            using (var reader = new StreamReader(WIN_TMP + WIN_FILE))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(';');

                    for (int i = 0; i < 7; i++)
                    {
                        if (values[4].Equals(LTB.Instance.categories[i]))
                        {
                            values[4] = LTB.Instance.categories[i];

                            switch (i)
                            {
                                case 0:
                                    LTB.Instance.library0.Add(values);
                                    break;
                                case 1:
                                    LTB.Instance.library1.Add(values);
                                    break;
                                case 2:
                                    LTB.Instance.library2.Add(values);
                                    break;
                                case 3:
                                    LTB.Instance.library3.Add(values);
                                    break;
                                case 4:
                                    LTB.Instance.library4.Add(values);
                                    break;
                                case 5:
                                    LTB.Instance.library5.Add(values);
                                    break;
                                case 6:
                                    LTB.Instance.library6.Add(values);
                                    break;
                            }
                        }
                    }
                }

                reader.Close();
            }
        }

        public void WriteCSV(bool printAll)
        {
            using (var writer = new StreamWriter(WIN_TMP + WIN_FILE))
            {
                List<string[]> library;

                if (GUI.Instance.getCbShowOwnedState() && !printAll)
                {
                    library = LTB.Instance.GetSpecificItems(LTB.Instance.GetCategory(GUI.Instance.getDdEditionSelectorIndex()), true);
                }
                else if (GUI.Instance.getCbShowNotOwnedState() && !printAll)
                {
                    library = LTB.Instance.GetSpecificItems(LTB.Instance.GetCategory(GUI.Instance.getDdEditionSelectorIndex()), false);
                }
                else
                {
                    library = LTB.Instance.GetCategory(GUI.Instance.getDdEditionSelectorIndex());
                }

                foreach (var arr in library)
                {
                    string line = string.Format("{0};{1};{2};{3};{4};{5}",
                        arr[0], arr[1], arr[2], arr[3], arr[4], arr[5]);

                    writer.WriteLine(line);
                }

                writer.Flush();
                writer.Close();
            }
        }

        public void CopyCSV()
        {
            File.Copy(WIN_TMP + WIN_FILE, WIN_HOME + WIN_FILE, true);
        }

        public void DeleteCSV()
        {
            File.Delete(WIN_TMP + WIN_FILE);
        }
    }
}
