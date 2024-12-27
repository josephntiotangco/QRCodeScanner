namespace QRCodeScanner
{
    public partial class MainPage : ContentPage
    {
        //int count = 0;
        string qrCodeValue;
        Color defaultBtnColor;
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
            qrCodeValue = "";
            defaultBtnColor = btnOpenLink.BackgroundColor;
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

            if (string.IsNullOrEmpty(qrCodeValue))
            {
                qrCodeValue = first.Value;
                Dispatcher.DispatchAsync(async () =>
                {
                    lblQRResult.Text = first.Value;
                    await DisplayAlert("QR Code Detected", first.Value, "OK");

                    btnOpenLink.BackgroundColor = Color.FromRgb(150, 0, 0);
                });
            }



        }

        private async void btnOpenLink_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblQRResult.Text))
            {
                return;
            }
            else
            {
                try
                {
                    Uri uri = new Uri(lblQRResult.Text);
                    await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Invalid QR Code Value", lblQRResult.Text, "OK");
                }
            }
        }

        private void btnReset_Clicked(object sender, EventArgs e)
        {
            lblQRResult.Text = "";
            qrCodeValue = "";

            Dispatcher.DispatchAsync(async () =>
            {
                btnOpenLink.BackgroundColor = defaultBtnColor;
            });
        }
    }

}
