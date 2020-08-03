using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

namespace Hubery.Tools.Uwp.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class LogHelper : LogBase
    {
        private static readonly string _path = System.IO.Path.Combine(ApplicationData.Current.LocalFolder.Path, SystemInformation.ApplicationName + ExtendName);
        /// <summary>
        /// 
        /// </summary>
        public static LogHelper Instance = new LogHelper();

        /// <summary>
        /// 
        /// </summary>
        public static string ExtendName { get; set; } = ".log";

        private LogHelper() :
            base(_path,
                $"{SystemInformation.ApplicationVersion.Major}.{SystemInformation.ApplicationVersion.Minor}.{SystemInformation.ApplicationVersion.Build}",
                SystemInformation.OperatingSystemVersion.ToString(),
                SystemInformation.DeviceModel,
                SystemInformation.ApplicationName,
                "UWP")
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task Export()
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker
            {
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop,
                SuggestedFileName = SystemInformation.ApplicationName
            };
            savePicker.FileTypeChoices.Add("log", new List<string>() { ".log" });
            StorageFile targetFile = await savePicker.PickSaveFileAsync();
            if (targetFile == null)
            {
                return;
            }

            StorageFile storageFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(SystemInformation.ApplicationName + ExtendName, CreationCollisionOption.OpenIfExists);
            await storageFile.CopyAndReplaceAsync(targetFile);
        }
    }
}