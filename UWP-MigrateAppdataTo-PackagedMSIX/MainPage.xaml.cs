using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace UWP_MigrateAppdataTo_PackagedMSIX
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // Change this to Windows.Storage.StorageFolder roamingFolder = Windows.Storage.ApplicationData.Current.RoamingFolder;
        // to use the RoamingFolder instead, for example.
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        private const string dataFileName = "dataFile.txt";

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void WriteAppdata_Click(object sender, RoutedEventArgs e)
        {
            Windows.Globalization.DateTimeFormatting.DateTimeFormatter formatter = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("longtime");
            StorageFile sampleFile = await localFolder.CreateFileAsync(dataFileName, CreationCollisionOption.OpenIfExists);
            await FileIO.AppendLinesAsync(sampleFile, new string[] { formatter.Format(DateTime.Now) });
        }

        private async void ReadAppdata_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StorageFile sampleFile = await localFolder.GetFileAsync(dataFileName);
                String timestamp = await FileIO.ReadTextAsync(sampleFile);
                SettingsTextBlock.Text = timestamp;
                // Data is contained in timestamp
            }
            catch (FileNotFoundException ex)
            {
                // Cannot find file
                Debug.WriteLine("Cannot find file: {0}", ex.FileName);
            }
            catch (IOException ex)
            {
                // Get information from the exception, then throw
                // the info to the parent method.
                if (ex.Source != null)
                {
                    Debug.WriteLine("IOException source: {0}", ex.Source);
                }
                throw;
            }
        }
    }
}
