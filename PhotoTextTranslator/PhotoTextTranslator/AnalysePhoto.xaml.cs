using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;


namespace PhotoTextTranslator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnalysePhoto : ContentPage
    {
        public AnalysePhoto()
        {
            InitializeComponent();
        }

        private void translateButton_Clicked(object sender, EventArgs e)
        {
            picSentence.Text = TranslateManager.createInstance.getSentence();
        }

        private async void languages_Clicked(object sender, EventArgs e)
        {
            string token = await getTokenAsync();
            string[] lang = { "1", "2", "3" };
            getLanguagesAsync(token);
           /* var action = await DisplayActionSheet("Select language", "Cancel", "", lang);
            if (action != "Cancel") { selected.Text = "Language Selected: " + action; }
            else { selected.Text = ""; }*/
        }

        private void getLanguagesAsync(string token)
        {
            string uri = "https://api.microsofttranslator.com/V2/Http.svc/GetLanguagesForTranslate";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Headers["Authorization"] = token;

            httpWebRequest.BeginGetResponse(new AsyncCallback(FinishRequest), httpWebRequest);
        }

        private async void FinishRequest(IAsyncResult ar)
        {
            HttpWebResponse response = (ar.AsyncState as HttpWebRequest).EndGetResponse(ar) as HttpWebResponse;

            using (Stream receiveStream = response.GetResponseStream())
            {
                DataContractSerializer dcs = new DataContractSerializer(typeof(string[]));
                string[] languages = (string[])dcs.ReadObject(receiveStream);
                await DisplayActionSheet("Select language", "Cancel", "", languages);
            }
        }

        public async Task<string> getTokenAsync()
        {
            string token;
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri("https://api.cognitive.microsoft.com/sts/v1.0/issueToken");
                request.Content = new StringContent(String.Empty);
                request.Headers.TryAddWithoutValidation("Ocp-Apim-Subscription-Key", "e891c180ea62477ab3d6136b73e94d24");
                var response = await client.SendAsync(request);
                token = await response.Content.ReadAsStringAsync();
            }
            return token;
        }
    }
}