using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Storage;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // Change this to Windows.Storage.StorageFolder roamingFolder = Windows.Storage.ApplicationData.Current.RoamingFolder;
        // to use the RoamingFolder instead, for example.
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        private const string dataFileName = "dataFile.txt";

        public Form1()
        {
            InitializeComponent();
        }

        private async void label1_Click(object sender, EventArgs e)
        {
            Windows.Globalization.DateTimeFormatting.DateTimeFormatter formatter = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("longtime");
            StorageFile sampleFile = await localFolder.CreateFileAsync(dataFileName, CreationCollisionOption.OpenIfExists);
            await FileIO.AppendLinesAsync(sampleFile, new string[] { formatter.Format(DateTime.Now) });
        }

        private async void label2_Click(object sender, EventArgs e)
        {
            try
            {
                StorageFile sampleFile = await localFolder.GetFileAsync(dataFileName);
                String timestamps = await FileIO.ReadTextAsync(sampleFile);
                textBox1.Text = timestamps;
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
