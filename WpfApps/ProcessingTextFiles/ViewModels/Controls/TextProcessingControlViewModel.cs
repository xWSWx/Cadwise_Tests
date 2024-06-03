using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
namespace ProcessingTextFiles.ViewModels.Controls
{
    public class TextProcessingControlViewModel : ViewModelBase
    {
        private string _inputPath;
        public string InputPath 
        {
            get => _inputPath;
            set => this.RaiseAndSetIfChanged(ref _inputPath, value);
        } 
        private string _outPath;
        public string OutPath
        {
            get => _outPath;
            set => this.RaiseAndSetIfChanged(ref _outPath, value);
        }
        public ICommand Play { get; }
        public ICommand Stop { get; }
        private int _completePercent;
        public int CompletePercents 
        {
            get => _completePercent;
            set => this.RaiseAndSetIfChanged(ref _completePercent, value);
        }

        public TextProcessingControlViewModel() 
        {
            InputPath = "Dummy path 1";
            OutPath = "Dummy path 2";
            CompletePercents = 40;
            Play = ReactiveCommand.Create(() => InputPath = "PLAY RESULT");
            Stop = ReactiveCommand.Create(() => OutPath = "STOP RESULT");
        }

    }
}
