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

        public List<string[]> GetSpecificLTB(List<string[]> baseLibrary, bool showOwned)
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

        public void AddLTB(string[] book, int library)
        {
            switch (library)
            {
                case 0:
                    library0.Add(book);
                    break;
                case 1:
                    library1.Add(book);
                    break;
                case 2:
                    library2.Add(book);
                    break;
                case 3:
                    library3.Add(book);
                    break;
                case 4:
                    library4.Add(book);
                    break;
                case 5:
                    library5.Add(book);
                    break;
                case 6:
                    library6.Add(book);
                    break;
            }
        }

        public void ChangeLTB(string itemTag, bool checkState, int categoryId)
        {
            List<string[]> library = null;

            isDirty = true;

            switch (categoryId)
            {
                case 0:
                    library = library0;
                    break;
                case 1:
                    library = library1;
                    break;
                case 2:
                    library = library2;
                    break;
                case 3:
                    library = library3;
                    break;
                case 4:
                    library = library4;
                    break;
                case 5:
                    library = library5;
                    break;
                case 6:
                    library = library6;
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

            if (library != null)
            {
                foreach (var item in library)
                {
                    if (item[2].Equals(itemTag))
                    {
                        item[0] = checkState.ToString().ToLower();
                        break;
                    }
                }
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
                isDirty = false;

                FTP.Instance.Upload();
            }

            if (copy)
            {
                CSV.Instance.CopyCSV();
            }
        }
    }
}
