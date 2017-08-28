﻿using System.Collections.ObjectModel;

namespace ArtTherapy.Models.PostModels
{
    public class PostModel : BaseModel
    {
        public ObservableCollection<CurrentPostModel> Items
        {
            get { return _Items; }
            set { _Items = GetValue(value, nameof(Items)); }
        }
        private ObservableCollection<CurrentPostModel> _Items;
    }
}
