using System.ComponentModel;

namespace ArtTherapy.Models
{
    public abstract class BaseModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual T GetValue<T>(T value, string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return value;
        }

        #endregion
    }
}
