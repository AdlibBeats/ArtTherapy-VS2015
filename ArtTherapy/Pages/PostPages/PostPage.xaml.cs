﻿using System;
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

namespace ArtTherapy.Pages.PostPages
{
    public sealed partial class PostPage : Page, IPage
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

        public PostPage()
        {
            this.InitializeComponent();

            Id = 2;
            Title = "Стихи";
            NavigateEventType = NavigateEventTypes.ListBoxSelectionChanged;
            Initialized?.Invoke(this, new EventArgs());
        }

        public PostPage(uint id, string title)
        {
            this.InitializeComponent();

            Id = id;
            Title = title;
            NavigateEventType = NavigateEventTypes.ListBoxSelectionChanged;
            Initialized?.Invoke(this, new EventArgs());
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}
