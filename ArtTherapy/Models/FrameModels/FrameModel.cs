using ArtTherapy.Pages;

namespace ArtTherapy.Models.FrameModels
{
    public class FrameModel : BaseModel
    {
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
