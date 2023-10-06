
using System.Speech.Synthesis;

namespace The_Text_App;

public partial class txttospeechpage : ContentPage
{
    bool takeocr = false;
    List<string> voicenames = new List<string>();
    List<Button> btn = new List<Button>();
    List<HorizontalStackLayout> hly = new List<HorizontalStackLayout>();
    string sumtext = "";

	public txttospeechpage()
	{
		InitializeComponent();
        textsummarizer tx=new textsummarizer();
        sumtext=textsummarizer.res;
	}
    public void takefromocr(object sender, EventArgs e)
    {
        if (switcho.IsToggled)
        {
            takeocr = true;
            rawtxt.Text= sumtext;
        }

    }
    private async void go_Clicked(object sender, EventArgs e)
    {
        if (switcho.IsToggled || rawtxt.Text != string.Empty)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            //synth.SelectVoice("Microsoft Irina Desktop");
            //string ans = await DisplayPromptAsync("one second !", "what would you like to name it?");
            synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
            //synth.SetOutputToWaveFile(FileSystem.AppDataDirectory+ans + ".wav");
            //voicenames.Add(ans + ".wav");
            synth.Speak(rawtxt.Text);
            //updatemenus();
        }
    }
    /*public void updatemenus()
    {
        for(int i=0;i<btn.Count;i++)
        {
            hly[i] = new HorizontalStackLayout();
            Label lbl = new Label();
            lbl.Text= voicenames[i];
            btn[i].Text = "play";
            btn[i].Clicked += Byn_Clicked;
            hly[i].Add(lbl);
            hly[i].Add(btn[i]);
            mainvlay.Add(hly[i]);
        }
    }

    private void Byn_Clicked(object sender, EventArgs e)
    {
        Button b = sender as Button;
        int n = btn.IndexOf(b);
        mediaElement.Source = FileSystem.AppDataDirectory+"/thetextapp1/" +voicenames[n];
        mediaElement.IsVisible = true;  
    }*/
}