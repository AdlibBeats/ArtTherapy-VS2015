using System;

using ArtTherapy.Models.ItemsModels;
using ArtTherapy.Pages.PostPages;
using ArtTherapy.Pages.SettingsPages;
using ArtTherapy.Pages.AboutAppPages;

using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Diagnostics;
using ArtTherapy.Models.ProfileModels;

namespace ArtTherapy.ViewModels
{
    public class CustomCommand : ICommand
    {
        #region Public Constructor

        public CustomCommand(MenuViewModel viewModel)
        {
            ViewModel = viewModel;
        }
        #endregion

        #region Private Members

        private MenuViewModel ViewModel;

        #endregion

        #region ICommand Members

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var currentItemModel = parameter as CurrentItemModel;
            if (currentItemModel != null)
            {
                Debug.WriteLine(currentItemModel.Name);
                ViewModel.ProfileModel.FirstName = currentItemModel.Name;
            }
        }
        #endregion
    }

    public class MenuViewModel : DependencyObject
    {
        public MenuViewModel()
        {
            MenuModel = new ItemsModel()
            {
                GroupItems = new CollectionViewSource(),
                Items = new ObservableCollection<CurrentItemModel>()
                {
                    new CurrentItemModel() { Icon = "\xE10F", Name = "Стихи", ItemsGroup=ItemsGroup.GroupOne, Type = typeof(PostPage) },
                    new CurrentItemModel() { Icon = "\xE1A5", Name = "Сказки", ItemsGroup=ItemsGroup.GroupOne, Type = typeof(PostPage) },
                    new CurrentItemModel() { Icon = "\xE7C3", Name = "Статьи", ItemsGroup=ItemsGroup.GroupOne, Type = typeof(PostPage) },
                    new CurrentItemModel() { Icon = "\xE77F", Name = "О приложении", ItemsGroup=ItemsGroup.GroupTwo, Type = typeof(AboutAppPage) },
                    new CurrentItemModel() { Icon = "\xE7F4", Name = "Настройки", ItemsGroup=ItemsGroup.GroupThree, Type = typeof(SettingsPage) }
                }
            };

            MenuModel.GroupItems.Source =
                MenuModel.Items.GroupBy(i => i.ItemsGroup);

            ProfileModel = new ProfileModel()
            {
                FirstName = "Андрей",
                LastName = "Васильев",
                MiddleName = "Сергеевич"
            };

            CustomCommand = new CustomCommand(this);
        }

        #region Public Dependency Properties

        public ICommand CustomCommand
        {
            get { return (ICommand)GetValue(CustomCommandProperty); }
            set { SetValue(CustomCommandProperty, value); }
        }

        public static readonly DependencyProperty CustomCommandProperty =
            DependencyProperty.Register("CustomCommand", typeof(ICommand), typeof(MenuViewModel), new PropertyMetadata(null));

        public ProfileModel ProfileModel
        {
            get { return (ProfileModel)GetValue(ProfileModelProperty); }
            set { SetValue(ProfileModelProperty, value); }
        }

        public static readonly DependencyProperty ProfileModelProperty =
            DependencyProperty.Register("ProfileModel", typeof(ProfileModel), typeof(MenuViewModel), new PropertyMetadata(null));

        public ItemsModel MenuModel
        {
            get { return (ItemsModel)GetValue(MenuModelProperty); }
            set { SetValue(MenuModelProperty, value); }
        }

        public static readonly DependencyProperty MenuModelProperty =
            DependencyProperty.Register("MenuModel", typeof(ItemsModel), typeof(MenuViewModel), new PropertyMetadata(null));

        #endregion
    }
}
