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
        public string Title
        {
            get { return _Title; }
            set { _Title = GetValue(value, nameof(MenuPage.Title)); }
        }
        private string _Title;

        public NavigateEventTypes NavigateEventType
        {
            get { return _NavigateEventType; }
            set { _NavigateEventType = GetValue(value, nameof(MenuPage.NavigateEventType)); }
        }
        private NavigateEventTypes _NavigateEventType;

        public event EventHandler<EventArgs> Initialized;

        public MenuPage()
        {
            this.InitializeComponent();

            Title = "Меню";
            NavigateEventType = NavigateEventTypes.ListBoxSelectionChanged;
            Initialized?.Invoke(this, new EventArgs());
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
                MainFrame.GoBack();
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            //TODO:
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private T GetValue<T>(T value, string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return value;
        }

        #endregion
    }
}
