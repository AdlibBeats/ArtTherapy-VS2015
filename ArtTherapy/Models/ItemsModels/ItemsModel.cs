using System.Collections.ObjectModel;
using Windows.UI.Xaml.Data;

namespace ArtTherapy.Models.ItemsModels
{
    public class ItemsModel : BaseModel
    {
        public ObservableCollection<CurrentItemModel> Items
        {
            get { return _Items; }
            set { _Items = GetValue(value, nameof(ItemsModel.Items)); }
        }
        private ObservableCollection<CurrentItemModel> _Items;

        public CollectionViewSource GroupItems
        {
            get { return _GroupItems; }
            set { _GroupItems = GetValue(value, nameof(ItemsModel.GroupItems)); }
        }
        private CollectionViewSource _GroupItems;
    }
}
