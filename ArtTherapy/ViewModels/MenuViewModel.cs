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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls;
using ArtTherapy.Pages.ProfilePages;
using ArtTherapy.Models.FrameModels;

namespace ArtTherapy.ViewModels
{
    public class ProfileClickCommand : ICommand
    {
        #region Public Constructor

        public ProfileClickCommand(MenuViewModel viewModel)
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
            ViewModel.FrameModel.Content = new ProfilePage();
            ViewModel.ProfileModel.IsChecked = true;
            ViewModel.MenuModel.IsMenuPaneOpen = false;
            ViewModel.MenuModel.SelectedIndex = -1;
        }
        #endregion
    }

    public class SelectionChangedCommand : ICommand
    {
        #region Public Constructor

        public SelectionChangedCommand(MenuViewModel viewModel)
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
                ViewModel.FrameModel.Content = currentItemModel.Content;
                ViewModel.ProfileModel.IsChecked = false;
                ViewModel.MenuModel.IsMenuPaneOpen = false;
            }
        }
        #endregion
    }

    public class MenuViewModel : DependencyObject
    {
        public MenuViewModel()
        {
            // fix
            MenuModel = new ItemsModel()
            {
                GroupItems = new CollectionViewSource(),
                Items = new ObservableCollection<CurrentItemModel>()
                {
                    new CurrentItemModel() { Icon = "\xE10F", Name = "Стихи", ItemsGroup=ItemsGroup.GroupOne, Content = new PostPage() },
                    new CurrentItemModel() { Icon = "\xE1A5", Name = "Сказки", ItemsGroup=ItemsGroup.GroupOne, Content = new PostPage() },
                    new CurrentItemModel() { Icon = "\xE7C3", Name = "Статьи", ItemsGroup=ItemsGroup.GroupOne, Content = new PostPage() },
                    new CurrentItemModel() { Icon = "\xE77F", Name = "О приложении", ItemsGroup=ItemsGroup.GroupTwo, Content = new AboutAppPage() },
                    new CurrentItemModel() { Icon = "\xE7F4", Name = "Настройки", ItemsGroup=ItemsGroup.GroupThree, Content = new SettingsPage() }
                },
                SelectedIndex = 0,
                IsMenuPaneOpen = false
            };

            MenuModel.GroupItems.Source =
                MenuModel.Items.GroupBy(i => i.ItemsGroup);

            ProfileModel = new ProfileModel()
            {
                FirstName = "Юлия",
                LastName = "Свиридова",
                MiddleName = "Психология",
                Avatar = new BitmapImage()
                {
                    CreateOptions = BitmapCreateOptions.IgnoreImageCache,
                    UriSource = new Uri("ms-appx:///Avatar/avatar.jpg", UriKind.RelativeOrAbsolute)
                },
                IsChecked = false
            };

            FrameModel = new FrameModel()
            {
                Content = new PostPage()
            };

            ProfileClickCommand = new ProfileClickCommand(this);
            SelectionChangedCommand = new SelectionChangedCommand(this);
        }

        #region Public Dependency Properties

        public ICommand ProfileClickCommand
        {
            get { return (ICommand)GetValue(ProfileClickCommandProperty); }
            set { SetValue(ProfileClickCommandProperty, value); }
        }
        
        public static readonly DependencyProperty ProfileClickCommandProperty =
            DependencyProperty.Register("ProfileClickCommand", typeof(ICommand), typeof(MenuViewModel), new PropertyMetadata(null));

        public ICommand SelectionChangedCommand
        {
            get { return (ICommand)GetValue(SelectionChangedCommandProperty); }
            set { SetValue(SelectionChangedCommandProperty, value); }
        }

        public static readonly DependencyProperty SelectionChangedCommandProperty =
            DependencyProperty.Register("SelectionChangedCommand", typeof(ICommand), typeof(MenuViewModel), new PropertyMetadata(null));

        public FrameModel FrameModel
        {
            get { return (FrameModel)GetValue(FrameModelProperty); }
            set { SetValue(FrameModelProperty, value); }
        }
        
        public static readonly DependencyProperty FrameModelProperty =
            DependencyProperty.Register("FrameModel", typeof(FrameModel), typeof(MenuViewModel), new PropertyMetadata(null));

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
