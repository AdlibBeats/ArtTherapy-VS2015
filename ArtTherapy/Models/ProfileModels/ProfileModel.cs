namespace ArtTherapy.Models.ProfileModels
{
    public class ProfileModel : BaseModel
    {
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = GetValue(value, nameof(FirstName)); }
        }
        string _FirstName;

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = GetValue(value, nameof(LastName)); }
        }
        string _LastName;

        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = GetValue(value, nameof(MiddleName)); }
        }
        string _MiddleName;

        public string Email
        {
            get { return _Email; }
            set { _Email = GetValue(value, nameof(Email)); }
        }
        string _Email;

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = GetValue(value, nameof(Phone)); }
        }
        string _Phone;

        public string Company
        {
            get { return _Company; }
            set { _Company = GetValue(value, nameof(Company)); }
        }
        string _Company;

        public string Job
        {
            get { return _Job; }
            set { _Job = GetValue(value, nameof(Job)); }
        }
        string _Job;
    }
}
