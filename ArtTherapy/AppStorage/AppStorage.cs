using ArtTherapy.Models.PostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ArtTherapy.AppStorage
{
    public class AppStorage
    {
        private static void Remove(string key)
        {
            var local = ApplicationData.Current.LocalSettings;
            local.Values.Remove(key);
        }

        private static void SetValue(string key, object value)
        {
            var local = ApplicationData.Current.LocalSettings;
            local.Values[key] = value;
        }

        private static string GetValue(string key, string @default = null)
        {
            var local = ApplicationData.Current.LocalSettings;
            return local.Values[key] as string ?? @default;
        }

        private const string TokenKey = "AccessToken";

        public static string AccessToken
        {
            get { return GetValue(TokenKey); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Remove(TokenKey);
                    return;
                }

                SetValue(TokenKey, value);
            }
        }

        public static CurrentPostModel CurrentPostModel
        {
            get { return _CurrentPostModel; }
            set { _CurrentPostModel = value; }
        }
        private static CurrentPostModel _CurrentPostModel;

        #region Get Set Order
        public static async Task<CurrentPostModel> GetCurrentPost()
        {
            var reader = await GetAsync("path");
            return (reader == null) ? null : JsonConvert.DeserializeObject<CurrentPostModel>(reader);
        }

        public static async Task SetCurrentPost(CurrentPostModel value)
        {
            _CurrentPostModel = value;
            var file = await SetAsync("path", _CurrentPostModel);
        }

        public static async Task RemoveCurrentPost()
        {
            _CurrentPostModel = null;
            await RemoveAsync("path");
        }
        #endregion

        #region Get Set Values
        private static async Task<string> GetAsync(string fileName)
        {
            StorageFile file = null;

            try
            {
                file = await StorageFile.GetFileFromPathAsync(ApplicationData.Current.LocalFolder.Path + @"\" + fileName);
            }
            catch
            {
                file = await ApplicationData.Current.LocalFolder.CreateFileAsync(@"\" + fileName, CreationCollisionOption.ReplaceExisting);
            }

            string reader = FileIO.ReadTextAsync(file)
                .AsTask()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
            return reader;
        }

        private static async Task<StorageFile> SetAsync<T>(string fileName, T value) where T : class
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(@"\" + fileName, CreationCollisionOption.ReplaceExisting);
            string writer = JsonConvert.SerializeObject(value);
            if (value != null)
                FileIO.WriteTextAsync(file, writer)
                    .AsTask()
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
            return file;
        }

        private static async Task RemoveAsync(string fileName)
        {
            StorageFile file = null;

            try
            {
                file = await StorageFile.GetFileFromPathAsync(ApplicationData.Current.LocalFolder.Path + @"\" + fileName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (file != null)
                    await file.DeleteAsync(StorageDeleteOption.PermanentDelete);
            }
        }
        #endregion
    }
}
