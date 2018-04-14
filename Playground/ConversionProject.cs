using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground
{
    public class ConversionProject : INotifyPropertyChanged
    {
        public string ApexDirectoryName { get; set; }

        public string CSharpDirectoryName { get; set; }

        public BindingList<ConversionFileItem> ApexFiles { get; } = new BindingList<ConversionFileItem>();

        public BindingList<ConversionFileItem> CSharpFiles { get; } = new BindingList<ConversionFileItem>();

        public ConversionFileItem CurrentApexFile { get; set; }

        public ConversionFileItem CurrentCSharpFile { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(params string[] properties)
        {
            foreach (var prop in properties)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }

        public void SetCurrentFileItem(ConversionFileItem item)
        {
            if (item.IsApex)
            {
                CurrentApexFile = item;
                CurrentCSharpFile = FindOrCreateMatchingItem(item);
            }
            else
            {
                CurrentCSharpFile = item;
                CurrentApexFile = FindOrCreateMatchingItem(item);
            }

            NotifyPropertyChanged(nameof(CurrentApexFile), nameof(CurrentCSharpFile));
        }

        private ConversionFileItem FindOrCreateMatchingItem(ConversionFileItem item)
        {
            var otherList = item.IsApex ? CSharpFiles : ApexFiles;
            var otherPath = item.IsApex ? CSharpDirectoryName : ApexDirectoryName;
            var otherExtension = item.IsApex ? "cs" : "cls";

            var matchingItem = otherList.FirstOrDefault(i => i.ClassName == item.ClassName);
            if (matchingItem == null)
            {
                matchingItem = new ConversionFileItem
                {
                    Directory = otherPath,
                    FileName = Path.ChangeExtension(item.FileName, otherExtension),
                    ClassName = item.ClassName,
                    IsApex = !item.IsApex,
                    IsNew = true,
                };

                otherList.Add(matchingItem);
            }
            else if (!matchingItem.IsLoaded && !matchingItem.IsNew)
            {
                matchingItem.Load();
            }

            return matchingItem;
        }

        public void LoadFiles()
        {
            LoadFiles(ApexFiles, ApexDirectoryName, "*.cls");
            LoadFiles(CSharpFiles, CSharpDirectoryName, "*.cs");
        }

        private void LoadFiles(IList<ConversionFileItem> fileList, string path, string mask)
        {
            fileList.Clear();

            try
            {
                var fileItems =
                    from file in Directory.GetFiles(path, mask)
                    let fileName = Path.GetFileName(file)
                    let className = Path.GetFileNameWithoutExtension(file)
                    let isApex = fileList == ApexFiles
                    select new ConversionFileItem
                    {
                        Directory = path,
                        FileName = fileName,
                        ClassName = className,
                        IsApex = isApex,
                    };

                foreach (var item in fileItems)
                {
                    fileList.Add(item);
                }
            }
            catch (ArgumentException)
            {
                // empty path?
            }
            catch (DirectoryNotFoundException)
            {
                // bad path?
            }
        }
    }
}
