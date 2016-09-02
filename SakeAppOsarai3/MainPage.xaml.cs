using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using System.Collections.ObjectModel;
using Windows.Storage;
using System.Runtime.Serialization;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SakeAppOsarai3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<string> SakeCollection = new ObservableCollection<string>();

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;

        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.listView.ItemsSource = SakeCollection; 
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            if ( this.textBox.Text == "お酒の名前を入力してください" )
            {
                await new MessageDialog("ちゃんと入力してね！").ShowAsync();
            }
            else
            {
                //                this.listView.Items.Add(this.textBox.Text);
                this.SakeCollection.Add(this.textBox.Text);
                // ファイルの定義
                StorageFile file = await KnownFolders.PicturesLibrary.CreateFileAsync("sakedata.txt", CreationCollisionOption.ReplaceExisting);
                // Stream処理
                using (Stream ws = await file.OpenStreamForWriteAsync())
                {
                    DataContractSerializer sl = new DataContractSerializer(typeof(ObservableCollection<string>));
// Write
                    sl.WriteObject(ws, this.SakeCollection);
                    // Flush
                    await ws.FlushAsync();
                }
                
            }

            //            this.listView.Items.Add(this.textBox.Text);
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //  this.listView.Items.Remove(this.listView.SelectedValue);
            //  this.listView.Items.Remove(this.listView.SelectedValue);
             if (listView.SelectedIndex != -1 )
            {
                this.SakeCollection.RemoveAt(listView.SelectedIndex);
            }
           
            
        }
    }
}
