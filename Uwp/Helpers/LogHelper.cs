using HTools.HLog;
using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

namespace HTools.Uwp.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class LogHelper : HLogBase
    {
        private static readonly string _fileName = SystemInformation.ApplicationName + ".log";
        private static readonly string _path = System.IO.Path.Combine(ApplicationData.Current.LocalFolder.Path, _fileName);
        /// <summary>
        /// 
        /// </summary>
        public static LogHelper Instance = new LogHelper();

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

            StorageFile storageFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(_fileName, CreationCollisionOption.OpenIfExists);
            await storageFile.CopyAndReplaceAsync(targetFile);
        }
    }
}