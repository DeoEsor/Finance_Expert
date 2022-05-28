using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ExpertDesktop.Annotations;

namespace ExpertDesktop.MVVM.ViewModel;

public class MainViewModel : INotifyPropertyChanged
{
    public MainViewModel()
    {
        Line1.Add(new Data() { Id = 1, Value = 200 });
        Line1.Add(new Data() { Id = 2, Value = 150 });
        Line1.Add(new Data() { Id = 3, Value = 0 });
        Line1.Add(new Data() { Id = 4, Value = 200 });
        Line1.Add(new Data() { Id = 5, Value = 150 });
        Line1.Add(new Data() { Id = 6, Value = 0});
             
    }
    public List<Data> Line1 { get; set; } = new List<Data>();
    public class Data
    {
        public int Value { get; set; }
        public int Id { get; set; }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}