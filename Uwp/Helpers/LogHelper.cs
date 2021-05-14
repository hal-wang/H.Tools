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
        private static readonly string _fileName = SystemInformation.Instance.ApplicationName + ".log";
        private static readonly string _path = System.IO.Path.Combine(ApplicationData.Current.LocalFolder.Path, _fileName);
        /// <summary>
        /// 
        /// </summary>
        public static LogHelper Instance = new();

        private LogHelper() :
            base(_path,
                $"{SystemInformation.Instance.ApplicationVersion.Major}.{SystemInformation.Instance.ApplicationVersion.Minor}.{SystemInformation.Instance.ApplicationVersion.Build}",
                SystemInformation.Instance.OperatingSystemVersion.ToString(),
                SystemInformation.Instance.DeviceModel,
                SystemInformation.Instance.ApplicationName,
                "Uwp")
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
                SuggestedFileName = SystemInformation.Instance.ApplicationName
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