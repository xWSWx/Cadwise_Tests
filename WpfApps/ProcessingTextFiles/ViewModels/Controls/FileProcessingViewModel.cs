using Microsoft.Win32;
using ProcessingTextFiles.FileProcessing;
using ProcessingTextFiles.Wrappers;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProcessingTextFiles.ViewModels.Controls
{
    public enum FileActions
    {
        RemoveWordsLessThan,
        removePunctuation
    }
    public class FileProcessingViewModel : ReactiveObject
    {
        public event EventHandler OnDelete;
        FileActions FileAction = FileActions.RemoveWordsLessThan;

        private int _charactersCount = 0;
        public int CharactersCount
        {
            get => _charactersCount;
            set => this.RaiseAndSetIfChanged(ref _charactersCount, value);
        }

        private string _filePrefix;
        public string FilePrefix
        {
            get => _filePrefix; 
            set => this.RaiseAndSetIfChanged(ref _filePrefix, value);
        }
        private int _selectedActionIndex = 0;
        public int SelectedActionIndex
        {
            get => _selectedActionIndex;
            set
            {
                //дешёвый кастыль
                try
                {
                    FileAction = (FileActions)value;
                    if (FileAction == FileActions.RemoveWordsLessThan)
                    {
                        IsSizeBoxVisible = Visibility.Visible;
                    }
                    else 
                    {
                        IsSizeBoxVisible = Visibility.Collapsed;
                    }
                }
                catch { }
                this.RaiseAndSetIfChanged(ref _selectedActionIndex, value);
            }
        }

        private Visibility _isSizeBoxVisible;
        public Visibility IsSizeBoxVisible
        {
            get => _isSizeBoxVisible;
            set => this.RaiseAndSetIfChanged(ref _isSizeBoxVisible, value);
        }

        public ObservableCollection<FileViewModel> Files { get; } = new ObservableCollection<FileViewModel> { };

        private string _curentProcessingText;
        public string CurentProcessingText
        {
            get => _curentProcessingText;
            set => this.RaiseAndSetIfChanged(ref _curentProcessingText, value);
        }

        private int _completePercent;
        public int CompletePercents
        {
            get => _completePercent;
            set => this.RaiseAndSetIfChanged(ref _completePercent, value);
        }
        private bool _isPlayEnabled = false;
        public bool IsPlayEnabled 
        {
            get => _isPlayEnabled;
            set => this.RaiseAndSetIfChanged(ref _isPlayEnabled, value);
        }
        private bool _isPauseEnabled = false;
        public bool IsPauseEnabled
        {
            get => _isPauseEnabled;
            set => this.RaiseAndSetIfChanged(ref _isPauseEnabled, value);
        }
        private bool _isStopEnabled = false;
        public bool IsCancelEnabled
        {
            get => _isStopEnabled;
            set => this.RaiseAndSetIfChanged(ref _isStopEnabled, value);
        }
        private bool _isSelectEnabled = false;
        public bool IsSelectEnabled
        {
            get => _isSelectEnabled;
            set => this.RaiseAndSetIfChanged(ref _isSelectEnabled, value);
        }
        private bool _isDeleteEnabled = false;
        public bool IsDeleteEnabled
        {
            get => _isDeleteEnabled;
            set => this.RaiseAndSetIfChanged(ref _isDeleteEnabled, value);
        }
        public ICommand Delete { get; }
        public ICommand Play { get; }
        public ICommand Cancel { get; }
        public ICommand Pause { get; }
        public ICommand Select { get; }

        CustomCancellationToken cancelToken;
        //TODO: закрыть от редактирования. Отправлять копию. Публичный доступ вообще для тестов только
        public CustomCancellationToken CancelToken => cancelToken;
        private Guid id = Guid.NewGuid();

        public FileProcessingViewModel() 
        {
            FilePrefix = "Prefix-" + id.ToString()+".";
            cancelToken = new CustomCancellationToken(id);
            SelectedActionIndex = 0;
            FileProcessor.OnProgress += (x, y) =>
            {
                if (y.tokenId != cancelToken.Id)
                    return;
                CompletePercents = y.CompletedPercent;
            };
            FileProcessor.OnFileDone += (x, y) => 
            {
                if (y.id != cancelToken.Id)
                    return;
                Files.First(x => x.Path == y.str).IsSelected = true;
                CurentProcessingText = string.Format(ResourcesNameSpace.Resources.NAMEDFILECOMPLETE, y.str);

            };
            FileProcessor.OnCancelled += (x, y) =>
            {
                if (y.id != cancelToken.Id)
                    return;
                IsCancelEnabled = false;
                IsPauseEnabled = false;
                IsPlayEnabled = true;
                IsSelectEnabled = true;
                CurentProcessingText = ResourcesNameSpace.Resources.CANCELLED;
            };
            FileProcessor.OnDone += (x, y) =>
            {
                if (y.id != cancelToken.Id)
                    return;
                IsCancelEnabled = false;
                IsPauseEnabled = false;
                IsPlayEnabled = true;
                IsSelectEnabled = true;
                CurentProcessingText = ResourcesNameSpace.Resources.ALLFILESDONE;
            };
            FileProcessor.OnPaused += (x, y) =>
            {
                if (y.id != cancelToken.Id)
                    return;
                CurentProcessingText = ResourcesNameSpace.Resources.PAUSED;
            };
            FileProcessor.OnStarted += (x, y) =>
            {
                if (y.id != cancelToken.Id)
                    return;
                CurentProcessingText = "Started!";
            };
            IsSelectEnabled = true;
            IsDeleteEnabled = true;
            Select = ReactiveCommand.Create(SelectFiles);
            Play = ReactiveCommand.Create(() =>
            {
                if (CharactersCount == 0 && FileAction == FileActions.RemoveWordsLessThan)
                {
                    MessageBox.Show("Please, choose characters count");
                    return;
                }
                if (FilePrefix == String.Empty)
                {
                    MessageBox.Show("Prefix shuld be not empty string");
                    return;
                }
                if (FileProcessor.Start(Files.Select(x => x.Path), FilePrefix, cancelToken, FileAction, CharactersCount))
                {
                    IsPlayEnabled = false;
                    IsPauseEnabled = true;
                    IsCancelEnabled = true;
                    IsSelectEnabled = false;
                };
            });
            Pause = ReactiveCommand.Create(() => 
            { 
                if (cancelToken.Paused) 
                    cancelToken.Resume(); 
                else 
                    cancelToken.Pause();                
            });
            Cancel = ReactiveCommand.Create(() => 
            {
                cancelToken.Stop();
            });
            Delete = ReactiveCommand.Create(() => OnDelete?.Invoke(this, EventArgs.Empty));

            CurentProcessingText = "Dummy string";
            CompletePercents = 30;

            Files = new ObservableCollection<FileViewModel>
            {
                new(@"C:\Path\To\File1.txt"),
                new(@"C:\Path\To\File2.txt"),
                new(@"C:\Path\To\File3.txt")
            };
        }
        public void Clear() 
        {
            Files.Clear();
            CompletePercents = 0;
            
            CurentProcessingText = ResourcesNameSpace.Resources.LETUCHOOSEFILES;
        }
        void SelectFiles()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true, 
                Filter = "All files (*.*)|*.*", 
                Title = "Select Files"
            };           

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {                
                Files.Clear();                
                foreach (string fileName in openFileDialog.FileNames)
                {
                    Files.Add(new FileViewModel(fileName));
                }

                IsPlayEnabled = true;
                
            }
        }

    }
}
