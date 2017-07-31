using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            var action = await DisplayActionSheet("Select language", "Cancel", "", "Text1", "Text2", "Text3", "Text4", "Text5", "Text6", "Text7", "Text8", "Text9", "Text10", "Text1", "Text2", "Text3", "Text4", "Text5", "Text6", "Text7", "Text8", "Text9", "Text10");
            if (action != "Cancel") { selected.Text = "Language Selected: " + action; }
            else { selected.Text = ""; }
        }
    }
}