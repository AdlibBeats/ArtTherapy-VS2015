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
using ArtTherapy.Pages;

namespace ArtTherapy.ViewModels
{
    public class MenuPaneButtonClickCommand : ICommand
    {
        #region Public Constructor

        public MenuPaneButtonClickCommand(MenuViewModel viewModel)
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
            ViewModel.SetMenuPaneOpen(true);
        }
        #endregion
    }

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
            ViewModel.SetMenuSelectedIndex(-1);
            ViewModel.SetProfileChecked(true);

            IPage currentPage = ViewModel.FrameModel.Content;
            if (currentPage.Id != 1)
            {
                ViewModel.SetMenuPaneOpen(false);
                ViewModel.SetFrameContent(new ProfilePage());
                Debug.WriteLine(ViewModel.FrameModel.Content.Title);
            }
        }
        #endregion
    }

    public class MenuListSelectionChangedCommand : ICommand
    {
        #region Public Constructor

        public MenuListSelectionChangedCommand(MenuViewModel viewModel)
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
            var newItemModel = parameter as CurrentItemModel;
            IPage currentPage = ViewModel.FrameModel.Content;
            if (newItemModel != null && currentPage.Id != newItemModel.Id)
            {
                ViewModel.SetProfileChecked(false);
                ViewModel.SetMenuPaneOpen(false);
                ViewModel.SetFrameContent(newItemModel.Content);
                Debug.WriteLine(ViewModel.FrameModel.Content.Title);
            }
        }
        #endregion
    }

    public class MenuViewModel : DependencyObject
    {
        public MenuViewModel()
        {
            // fix E80F <- домик, сказки E11D
            MenuModel = new ItemsModel()
            {
                GroupItems = new CollectionViewSource(),
                Items = new ObservableCollection<CurrentItemModel>()
                {
                    new CurrentItemModel() { Id = 2, Icon = "\xE15C", Name = "Стихи", ItemsGroup=ItemsGroup.GroupOne, Content = new PostPage(2, "Стихи") },
                    new CurrentItemModel() { Id = 3, Icon = "\xE12F", Name = "Сказки", ItemsGroup=ItemsGroup.GroupOne, Content = new PostPage(3, "Сказки") },
                    new CurrentItemModel() { Id = 4, Icon = "\xE12A", Name = "Статьи", ItemsGroup=ItemsGroup.GroupOne, Content = new PostPage(4, "Статьи") },
                    new CurrentItemModel() { Id = 5, Icon = "\xE11B", Name = "О приложении", ItemsGroup=ItemsGroup.GroupTwo, Content = new AboutAppPage() },
                    new CurrentItemModel() { Id = 6, Icon = "\xE115", Name = "Настройки", ItemsGroup=ItemsGroup.GroupThree, Content = new SettingsPage() }
                },
                SelectedIndex = 0,
                IsMenuPaneOpen = false
            };

            MenuModel.GroupItems.Source =
                MenuModel.Items.GroupBy(i => i.ItemsGroup);

            ProfileModel = new ProfileModel()
            {
                Id = 1,
                Icon = "\xE13D",
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
                Content = new PostPage(2, "Стихи")
            };

            MenuPaneButtonClickCommand = new MenuPaneButtonClickCommand(this);
            ProfileButtonClickCommand = new ProfileClickCommand(this);
            MenuListSelectionChangedCommand = new MenuListSelectionChangedCommand(this);
        }

        public void SetProfileChecked(bool value)
        {
            this.ProfileModel.IsChecked = value;
        }

        public void SetMenuPaneOpen(bool value)
        {
            this.MenuModel.IsMenuPaneOpen = value;
        }

        public void SetFrameContent(IPage value)
        {
            this.FrameModel.Content = value;
        }

        public void SetMenuSelectedIndex(int value)
        {
            this.MenuModel.SelectedIndex = value;
        }

        #region Public Dependency Properties

        public ICommand MenuPaneButtonClickCommand
        {
            get { return (ICommand)GetValue(MenuPaneButtonClickCommandProperty); }
            set { SetValue(MenuPaneButtonClickCommandProperty, value); }
        }

        public static readonly DependencyProperty MenuPaneButtonClickCommandProperty =
            DependencyProperty.Register("MenuPaneButtonClickCommand", typeof(ICommand), typeof(MenuViewModel), new PropertyMetadata(null));

        public ICommand ProfileButtonClickCommand
        {
            get { return (ICommand)GetValue(ProfileButtonClickCommandProperty); }
            set { SetValue(ProfileButtonClickCommandProperty, value); }
        }
        
        public static readonly DependencyProperty ProfileButtonClickCommandProperty =
            DependencyProperty.Register("ProfileButtonClickCommand", typeof(ICommand), typeof(MenuViewModel), new PropertyMetadata(null));

        public ICommand MenuListSelectionChangedCommand
        {
            get { return (ICommand)GetValue(MenuListSelectionChangedCommandProperty); }
            set { SetValue(MenuListSelectionChangedCommandProperty, value); }
        }

        public static readonly DependencyProperty MenuListSelectionChangedCommandProperty =
            DependencyProperty.Register("MenuListSelectionChangedCommand", typeof(ICommand), typeof(MenuViewModel), new PropertyMetadata(null));

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
