﻿using ArtTherapy.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ArtTherapy.Pages.MenuPages
{
    public sealed partial class MenuPage : Page, IPage
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

        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        private string _Title;

        public NavigateEventTypes NavigateEventType
        {
            get { return _NavigateEventType; }
            set
            {
                _NavigateEventType = value;
                OnPropertyChanged(nameof(NavigateEventType));
            }
        }
        private NavigateEventTypes _NavigateEventType;

        public event EventHandler<EventArgs> Initialized;

        public MenuPage()
        {
            this.InitializeComponent();

            Title = "Меню";
            NavigateEventType = NavigateEventTypes.ListBoxSelectionChanged;
            this.DataContext = new MenuViewModel();
            Initialized?.Invoke(this, new EventArgs());
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            //if (MainFrame.CanGoBack)
            //    MainFrame.GoBack();
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}
