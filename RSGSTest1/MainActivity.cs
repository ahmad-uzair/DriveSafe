using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Plugin.CurrentActivity;
using Plugin.Media;
using Android.Graphics;
using Plugin.Media.Abstractions;
using System.Threading.Tasks;
using Plugin.TextToSpeech;

namespace RSGSTest1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        readonly ImageClassifier imageClassifier = new ImageClassifier();

        int speed = 0;
        int speedLimit = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
           
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Button buttonThirty = FindViewById<Button>(Resource.Id.button30);
            Button buttonEighty = FindViewById<Button>(Resource.Id.button80);
            buttonThirty.Click += ButtonThirty_Click;
            buttonEighty.Click += ButtonEighty_Click;
        }

        async private void ButtonEighty_Click(object sender, System.EventArgs e)
        {
            try
            {
                speed = 80;
                //var vid = await CrossMedia.Current.TakeVideoAsync(new StoreVideoOptions { PhotoSize = PhotoSize.Medium });
                var image = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { PhotoSize = PhotoSize.Medium });
                var bitmap = await BitmapFactory.DecodeStreamAsync(image.GetStreamWithImageRotatedForExternalStorage());

                //PhotoView.SetImageBitmap(bitmap);
                var result = await Task.Run(() => imageClassifier.RecognizeImage(bitmap));
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

                if (result.Equals("60km"))
                {
                    speedLimit = 60;
                    if (speed > speedLimit)
                    {
                        CrossTextToSpeech.Current.Speak($"The speed limit is {result}. Please Slow Down");
                    }
                    else
                        CrossTextToSpeech.Current.Speak($"The speed limit is {result}");
                }
                else
                {
                    speedLimit = 40;
                    if (speed > speedLimit)
                    {
                        CrossTextToSpeech.Current.Speak($"The speed limit is {result}. Please Slow Down");
                    }
                    else
                        CrossTextToSpeech.Current.Speak($"The speed limit is {result}");

                }
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                //ResultLabel.Text = result;
            }
            finally
            {

            }
        }

        async private void ButtonThirty_Click(object sender, System.EventArgs e)
        {
            try
            {
                speed = 30;
                //var vid = await CrossMedia.Current.TakeVideoAsync(new StoreVideoOptions { PhotoSize = PhotoSize.Medium });
                var image = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { PhotoSize = PhotoSize.Medium });
                var bitmap = await BitmapFactory.DecodeStreamAsync(image.GetStreamWithImageRotatedForExternalStorage());

                //PhotoView.SetImageBitmap(bitmap);
                var result = await Task.Run(() => imageClassifier.RecognizeImage(bitmap));

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                if (result.Equals("40km"))
                {
                    speedLimit = 40;
                    if (speed > speedLimit)
                    {
                        CrossTextToSpeech.Current.Speak($"The speed limit is {result}. Please Slow Down");
                    }
                    else
                        CrossTextToSpeech.Current.Speak($"The speed limit is {result}");
                }
                else
                {
                    speedLimit = 60;
                    if (speed > speedLimit)
                    {
                        CrossTextToSpeech.Current.Speak($"The speed limit is {result}. Please Slow Down");
                    }
                    else
                        CrossTextToSpeech.Current.Speak($"The speed limit is {result}");

                }

#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                //ResultLabel.Text = result;
            }
            finally {
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}

