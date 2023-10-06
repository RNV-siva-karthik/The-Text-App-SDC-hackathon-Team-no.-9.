using IronOcr;
using Microsoft.Maui.Controls;
using SkiaSharp;
using System;
using Tesseract;

namespace The_Text_App;

public partial class imagetotext : ContentPage
{
    public static string imgtxt = "";
    int count = 0;
    bool isopen = false;
    List<string> names = new List<string>();
    List<ImageButton> images = new List<ImageButton>();
    TapGestureRecognizer taprecog = new TapGestureRecognizer();
    string localFilePath;
    public imagetotext()
	{
		InitializeComponent();
        openclose.TranslateTo(-150, 0, 150);
        mainvlay.TranslateTo(-60, 0, 0);
        taprecog.Tapped += imgtapped;
        taprecog.NumberOfTapsRequired = 2;
#if WINDOWS
      editorview.WidthRequest=700;
#endif
    }
    public async void onccamclicked(object sender, EventArgs e)
    {
#if WINDOWS
{
DisplayAlert("sorry :(","this feature is not available for windows please select an image","yes");
}
#endif
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.CapturePhotoAsync();

            if (photo != null)
            {
                // save the file into local storage
                localFilePath = Path.Combine(FileSystem.AppDataDirectory, "Thetextapp");
                Directory.CreateDirectory(localFilePath);
                localFilePath = Path.Combine(localFilePath, photo.FileName);
                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);
                localFileStream.Close();
                sourceStream.Close();
                names.Add(photo.FileName);
                rotateimg(localFilePath);
                updateimg();
            }
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {

    }

    public void ImageButton_Clicked(object sender, EventArgs e)
    {
        if (isopen)
        {
            openclose.TranslateTo(-150, 0, 150);
        }
        else
        {
            openclose.TranslateTo(-60, 0, 150);
        }
        isopen = !isopen;
    }
    public string takeimage()
    {
        return "";
    }
    public async void onocrclikced(object sender, EventArgs e)
    {
        bool navcheck = await DisplayAlert("one sec please!", "changes here may not be saved", "yes", "No");
        if (navcheck)
        {
            Navigation.PushAsync(new MainPage());
            //Navigation.PopAsync();
        }
    }
    public async void onplusclicked(object sender, EventArgs e)
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.PickPhotoAsync();

            if (photo != null)
            {
                // save the file into local storage
                localFilePath = Path.Combine(FileSystem.AppDataDirectory, "Thetextapp");
                Directory.CreateDirectory(localFilePath);
                localFilePath = Path.Combine(localFilePath, photo.FileName);
                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);
                localFileStream.Close();
                sourceStream.Close();
                names.Add(photo.FileName);
                updateimg();
            }
        }
    }
    public async void updateimg()
    {
        images.Clear();
        imggalary.Clear();
        int i = 0;
        foreach (string str in names)
        {
            ImageButton img = new ImageButton();
            images.Add(img);
            images[i].Source = Path.Combine(FileSystem.AppDataDirectory, "Thetextapp", str);
            images[i].HeightRequest = 100;
            images[i].GestureRecognizers.Add(taprecog);
            images[i].WidthRequest = 100;

            imggalary.Add(images[i]);
            i++;
        }
    }
    public void imgtapped(object sender, EventArgs e)
    {
        ImageButton i = (ImageButton)sender;
        int n = images.IndexOf(i);
        names.RemoveAt(n);
        updateimg();
    }
    public void rotateimg(string path)
    {

        using (var originalBitmap = SKBitmap.Decode(path))
        {
            using (var rotatedBitmap = new SKBitmap(originalBitmap.Height, originalBitmap.Width))
            {
                using (var canvas = new SKCanvas(rotatedBitmap))
                {
                    canvas.Clear(SKColors.Transparent);
                    canvas.RotateDegrees(90);

                    canvas.DrawBitmap(originalBitmap, new SKPoint(0, -originalBitmap.Height));

                    using (var image = SKImage.FromBitmap(rotatedBitmap))
                    using (var imageStream = new FileStream(path, FileMode.Create))
                    {
                        image.Encode(SKEncodedImageFormat.Png, 100).SaveTo(imageStream);
                    }
                }
            }
        }
    }

    private async void gobtn_Clicked(object sender, EventArgs e)
    {
        conversion();

    }
    private async void conversion()
    {
        if (names.Count != 0)
        {
            string result = string.Empty;
            await Task.Run(() =>
            {
                string path;

                IronTesseract itr = new IronTesseract();
                foreach (string str in names)
                {
                    path = Path.Combine(FileSystem.AppDataDirectory, "Thetextapp", str);
                    var res = itr.Read(path);
                     result += res.Text.ToString();
                     //result += "\n";

                    /*var recognitionEngine = new Aspose.OCR.AsposeOcr();
                    // Extract text from image
                    result+=recognitionEngine.RecognizeImage(path);
                    // Display the recognition result*/

                }

            });
            editorview.IsVisible = true;
            saveview.IsVisible = true;
            maineditor.Text = result;
            imgtxt = maineditor.Text;
        }
        else
        {
            DisplayAlert("hey!!", "seems like you didn't add any images.", "yes");
        }
    }

    public void Save_Clicked(object sender, EventArgs e)
    {
        filesaver();
    }

    private async void copy_Clicked(object sender, EventArgs e)
    {
        await Clipboard.Default.SetTextAsync(maineditor.Text);
    }
    public async void filesaver()
    {
        if (maineditor.Text != string.Empty)
        {
            string ans = await DisplayPromptAsync("one second !", "what would you like to name it?");
            if (ans != string.Empty)
            {
                // File.WriteAllText(Path.Combine("C:\\Users\\R.N.V Siva Karthik\\Desktop", ans+".docx"),maineditor.Text);
                using FileStream stream = File.OpenWrite(Path.Combine("C:\\Users\\R.N.V Siva Karthik\\Desktop", ans + ".docx"));
                using StreamWriter writer = new StreamWriter(stream);
                await writer.WriteLineAsync(maineditor.Text);
            }
            else
            {
                DisplayAlert("wrong!", "enter a valid name!", "yes");
            }
        }
    }
}