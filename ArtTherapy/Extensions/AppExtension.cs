using Windows.Foundation.Metadata;

namespace ArtTherapy.Extensions
{
    public static class AppExtension
    {
        public static bool IsMobile =>
            ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0);
    }
}
