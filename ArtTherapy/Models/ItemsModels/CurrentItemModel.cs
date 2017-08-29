using ArtTherapy.Pages;
using System;
using Windows.UI.Xaml;

namespace ArtTherapy.Models.ItemsModels
{
    public enum ItemsGroup
    {
        GroupOne,
        GroupTwo,
        GroupThree,
        GroupFour,
        GroupFive,
    }
    public class CurrentItemModel : BaseModel
    {
        public uint Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        private uint _Id;

        public string Icon
        {
            get { return _Icon; }
            set
            {
                _Icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }
        private string _Icon;

        public ItemsGroup ItemsGroup
        {
            get { return _ItemsGroup; }
            set
            {
                _ItemsGroup = value;
                OnPropertyChanged(nameof(ItemsGroup));
            }
        }
        private ItemsGroup _ItemsGroup;

        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string _Name;

        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        private string _Description;

        public IPage Content
        {
            get { return _Content; }
            set
            {
                _Content = value;
                OnPropertyChanged(nameof(Content));
            }
        }
        private IPage _Content;
    }
}
