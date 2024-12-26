namespace QRCodeScanner
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            barcodeReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions
            {
                Formats = ZXing.Net.Maui.BarcodeFormat.QrCode,
                AutoRotate = true
                //TryHarder = true,
                //Multiple  = true
                
            };

        }

        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}

        private void barcodeReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
        {
            var first = e.Results.FirstOrDefault();

            if (first is null)
                return;

            Dispatcher.DispatchAsync(async () =>
            {
                //await DisplayAlert("Barcode Detected", first.Value, "OK");

                lblQRResult.Text = first.Value;
            });



        }

        private async void btnOpenLink_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblQRResult.Text))
            {
                return;
            }
            else
            {
                Uri uri = new Uri(lblQRResult.Text);
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
        }

        private void btnReset_Clicked(object sender, EventArgs e)
        {
            lblQRResult.Text = "";
        }
    }

}
