using Hubery.Tools;
using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;

namespace Hubery.Tools.Uwp.Helpers
{
    public class LogHelper : BaseLogHelper
    {
        private static readonly string _path = Path.Combine(ApplicationData.Current.LocalFolder.Path, SystemInformation.ApplicationName + ExtendName);
        public static LogHelper Instance = new LogHelper();

        public static string ExtendName { get; set; } = ".log";

        private LogHelper() :
            base(_path)
        { }

        public void Log(Exception ex, [CallerMemberName] string source = null)
        {
            Log(ex?.ToString(), source);
        }

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