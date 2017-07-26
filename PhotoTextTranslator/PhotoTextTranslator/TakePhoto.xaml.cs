using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhotoTextTranslator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TakePhoto : ContentPage
    {
        public MediaFile text = null;
        public TakePhoto()
        {            
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            this.text = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Small,
                Directory = "sample",
                Name = $"{DateTime.UtcNow}.jpg"
            });

            if (this.text != null)
            {
                textPic.Source = ImageSource.FromStream(() =>
                {
                    return this.text.GetStream();
                });
            }
        }

        async Task GetText(MediaFile text)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "{subscription key}");
        }
    }
}