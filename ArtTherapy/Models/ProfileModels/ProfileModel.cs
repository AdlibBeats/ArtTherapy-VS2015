using Windows.UI.Xaml.Media;

namespace ArtTherapy.Models.ProfileModels
{
    public class ProfileModel : BaseModel
    {
        public ImageSource Avatar
        {
            get { return _Avatar; }
            set { _Avatar = GetValue(value, nameof(Avatar)); }
        }
        private ImageSource _Avatar;
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = GetValue(value, nameof(FirstName)); }
        }
        private string _FirstName;

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = GetValue(value, nameof(LastName)); }
        }
        private string _LastName;

        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = GetValue(value, nameof(MiddleName)); }
        }
        private string _MiddleName;

        public string Email
        {
            get { return _Email; }
            set { _Email = GetValue(value, nameof(Email)); }
        }
        private string _Email;

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = GetValue(value, nameof(Phone)); }
        }
        private string _Phone;

        public string Company
        {
            get { return _Company; }
            set { _Company = GetValue(value, nameof(Company)); }
        }
        private string _Company;

        public string Job
        {
            get { return _Job; }
            set { _Job = GetValue(value, nameof(Job)); }
        }
        private string _Job;
    }
}
