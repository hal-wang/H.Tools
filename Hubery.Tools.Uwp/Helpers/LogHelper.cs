using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;

namespace Hubery.Tools.Uwp.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class LogHelper : BaseLogHelper
    {
        private static readonly string _path = Path.Combine(ApplicationData.Current.LocalFolder.Path, SystemInformation.ApplicationName + ExtendName);
        /// <summary>
        /// 
        /// </summary>
        public static LogHelper Instance = new LogHelper();

        /// <summary>
        /// 
        /// </summary>
        public static string ExtendName { get; set; } = ".log";

        private LogHelper() :
            base(_path)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="source"></param>
        public void Log(Exception ex, [CallerMemberName] string source = null)
        {
            Log(ex?.ToString(), source);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="source"></param>
        public void Log(string content, [CallerMemberName] string source = null)
        {
            Log(new Log()
            {
                Source = source,
                Content = content,
                Time = DateTime.Now,
                Version = $"{SystemInformation.ApplicationVersion.Major}.{SystemInformation.ApplicationVersion.Minor}.{SystemInformation.ApplicationVersion.Build}",
                OperatingSystemVersion = SystemInformation.OperatingSystemVersion.ToString(),
                DeviceModel = SystemInformation.DeviceModel,
                AppName = SystemInformation.ApplicationName,
                Platform = "UWP",
            });
        }

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