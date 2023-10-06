
namespace The_Text_App;

public partial class MainPage : ContentPage
{
	int count = 0;
	//TapGestureRecognizer imgtotxttap = new TapGestureRecognizer();
	//TapGestureRecognizer txtsummary = new TapGestureRecognizer();
	//TapGestureRecognizer txttospeech = new TapGestureRecognizer();
	public MainPage()
	{
		InitializeComponent();
        //imgtotxttap.Tapped += imgtotxtevent;
        //txtsummary.Tapped += txtsummarytap;
    }

    private void txtsummarytap(object sender, TappedEventArgs e)
    {
		Navigation.PushAsync(new textsummarizer());
    }

    private void imgtotxtevent(object sender, TappedEventArgs e)
    {
		Navigation.PushAsync(new imagetotext());
    }
    private void txttospeechevent(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new txttospeechpage());
    }
}

