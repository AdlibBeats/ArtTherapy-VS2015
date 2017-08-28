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
        public string Icon
        {
            get { return _Icon; }
            set { _Icon = GetValue(value, nameof(Icon)); }
        }
        private string _Icon;

        public ItemsGroup ItemsGroup
        {
            get { return _ItemsGroup; }
            set { _ItemsGroup = GetValue(value, nameof(ItemsGroup)); }
        }
        private ItemsGroup _ItemsGroup;

        public string Name
        {
            get { return _Name; }
            set { _Name = GetValue(value, nameof(Name)); }
        }
        private string _Name;

        public string Description
        {
            get { return _Description; }
            set { _Description = GetValue(value, nameof(Description)); }
        }
        private string _Description;

        public Type Type
        {
            get { return _Type; }
            set { _Type = GetValue(value, nameof(Type)); }
        }
        private Type _Type;
    }
}
