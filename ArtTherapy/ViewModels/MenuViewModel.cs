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

namespace ArtTherapy.ViewModels
{
    public class FrameNavigatedCommand : ICommand
    {
        #region Public Constructor

        public FrameNavigatedCommand(MenuViewModel viewModel)
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
                Debug.WriteLine(currentItemModel.Name);
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
                ViewModel.Frame.Navigate(currentItemModel.Type);
                ViewModel.ClearStackFrame();
            }
        }
        #endregion
    }

    public class MenuViewModel : DependencyObject
    {
        public Frame Frame { get; private set; }
        public MenuViewModel(Frame frame)
        {
            Frame = frame;
            Frame.Navigate(typeof(PostPage));
            ClearStackFrame();

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
                FirstName = "Юлия",
                LastName = "Свиридова",
                MiddleName = "Психология",
                Avatar = new BitmapImage()
                {
                    CreateOptions = BitmapCreateOptions.IgnoreImageCache,
                    UriSource = new Uri("ms-appx:///Avatar/avatar.jpg", UriKind.RelativeOrAbsolute)
                }
            };

            SelectionChangedCommand = new SelectionChangedCommand(this);
        }

        public void ClearStackFrame()
        {
            this.Frame.BackStack.Clear();
            this.Frame.ForwardStack.Clear();
        }

        #region Public Dependency Properties

        public ICommand SelectionChangedCommand
        {
            get { return (ICommand)GetValue(SelectionChangedCommandProperty); }
            set { SetValue(SelectionChangedCommandProperty, value); }
        }

        public static readonly DependencyProperty SelectionChangedCommandProperty =
            DependencyProperty.Register("SelectionChangedCommand", typeof(ICommand), typeof(MenuViewModel), new PropertyMetadata(null));

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
