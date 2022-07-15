using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace LTB_Verwaltung
{
    class LTB
    {
        private static LTB instance = null;
        private static readonly object padlock = new object();
        private static readonly ComponentResourceManager resources = new ComponentResourceManager(typeof(GUI));

        public List<string[]> library0 = new List<string[]>(), 
            library1 = new List<string[]>(), 
            library2 = new List<string[]>(), 
            library3 = new List<string[]>(), 
            library4 = new List<string[]>(), 
            library5 = new List<string[]>(), 
            library6 = new List<string[]>();
        public readonly string[] categories = new string[7]
        {
            resources.GetString("ddEditionSelector.Items1"),
            resources.GetString("ddEditionSelector.Items2"),
            resources.GetString("ddEditionSelector.Items3"),
            resources.GetString("ddEditionSelector.Items4"),
            resources.GetString("ddEditionSelector.Items5"),
            resources.GetString("ddEditionSelector.Items6"),
            resources.GetString("ddEditionSelector.Items7")

        };
        public bool isDirty = false;

        private LTB() { }

        public static LTB Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new LTB();
                    }
                    return instance;
                }
            }
        }

        private List<string[]> GetAllCategories()
        {
            return (List<string[]>)library0.Concat(library1).Concat(library2).Concat(library3)
                .Concat(library4).Concat(library5).Concat(library6).ToList();
        }

        public List<string[]> GetCategory(int categroyId)
        {
            switch (categroyId)
            {
                case 0:
                    return library0;
                case 1:
                    return library1;
                case 2:
                    return library2;
                case 3:
                    return library3;
                case 4:
                    return library4;
                case 5:
                    return library5;
                case 6:
                    return library6;
                default:
                    return GetAllCategories();
            }
        }

        public List<string[]> GetSpecificItems(List<string[]> baseLibrary, bool showOwned)
        {
            List<string[]> newLibrary = new List<string[]>();

            foreach (string[] item in baseLibrary)
            {
                if (showOwned == bool.Parse(item[0]))
                {
                    newLibrary.Add(item);
                }
            }

            return newLibrary;
        }

        public void ChangeLTB(string itemTag, bool checkState, int categoryId)
        {
            isDirty = true;

            switch (categoryId)
            {
                case 0:
                    foreach (var item in library0)
                    {
                        if (item[2].Equals(itemTag))
                        {
                            item[0] = checkState.ToString().ToLower();
                            break;
                        }
                    }
                    break;
                case 1:
                    foreach (var item in library1)
                    {
                        if (item[2].Equals(itemTag))
                        {
                            item[0] = checkState.ToString().ToLower();
                            break;
                        }
                    }
                    break;
                case 2:
                    foreach (var item in library2)
                    {
                        if (item[2].Equals(itemTag))
                        {
                            item[0] = checkState.ToString().ToLower();
                            break;
                        }
                    }
                    break;
                case 3:
                    foreach (var item in library3)
                    {
                        if (item[2].Equals(itemTag))
                        {
                            item[0] = checkState.ToString().ToLower();
                            break;
                        }
                    }
                    break;
                case 4:
                    foreach (var item in library4)
                    {
                        if (item[2].Equals(itemTag))
                        {
                            item[0] = checkState.ToString().ToLower();
                            break;
                        }
                    }
                    break;
                case 5:
                    foreach (var item in library5)
                    {
                        if (item[2].Equals(itemTag))
                        {
                            item[0] = checkState.ToString().ToLower();
                            break;
                        }
                    }
                    break;
                case 6:
                    foreach (var item in library6)
                    {
                        if (item[2].Equals(itemTag))
                        {
                            item[0] = checkState.ToString().ToLower();
                            break;
                        }
                    }
                    break;
                default:
                    ChangeLTB(itemTag, checkState, 0);
                    ChangeLTB(itemTag, checkState, 1);
                    ChangeLTB(itemTag, checkState, 2);
                    ChangeLTB(itemTag, checkState, 3);
                    ChangeLTB(itemTag, checkState, 4);
                    ChangeLTB(itemTag, checkState, 5);
                    ChangeLTB(itemTag, checkState, 6);
                    break;
            }
        }

        public void LoadLTB()
        {
            FTP.Instance.Download();
            CSV.Instance.ReadCSV();
        }

        public void SaveLTB(bool printAll, bool upload, bool copy)
        {
            CSV.Instance.WriteCSV(printAll);

            if (upload)
            {
                FTP.Instance.Upload();
            }

            if (copy)
            {
                CSV.Instance.CopyCSV();
            }
        }
    }
}
