using Newtonsoft.Json;

namespace The_Text_App;

public partial class textsummarizer : ContentPage
{
	 bool takeocr=false;
	public static string res = "";
	string textfromimg = "";
	public textsummarizer()
	{
		InitializeComponent();
		textfromimg = imagetotext.imgtxt;
		
	}
	public void takefromocr(object sender , EventArgs e)
	{
        if(switcho.IsToggled)
		{
			takeocr = true;
		}
		rawtxt.Text = textfromimg;
    }

    private async void go_Clicked(object sender, EventArgs e)
    {
		if (switcho.IsToggled || rawtxt.Text != string.Empty)
		{
			Rootobject resu = new Rootobject();
			await Task.Run(async () =>
			{
				HttpClient client = new HttpClient();
				//string inp = "i am fine here and going to college regularly";
				string requestAddress = "http://127.0.0.1:8000";
				HttpResponseMessage respo = client.GetAsync(requestAddress + "/" + rawtxt.Text).Result;
				var ans = await respo.Content.ReadAsStringAsync();
				resu = JsonConvert.DeserializeObject<Rootobject>(ans);
			});
            result.Text = resu.summary;
        }
        res = result.Text;
    }
}
	public class Rootobject
	{
        public string summary { get; set; }
    }