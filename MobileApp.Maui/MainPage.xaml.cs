using Newtonsoft.Json;

namespace MobileApp.Maui;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);

		ResponseButton.Text = "Connecting";

		string baseAddress = "http://192.168.139.10:5108";
		HttpClient client = new HttpClient()
		{
			BaseAddress = new Uri(baseAddress)
		};

		try
		{
            var response = client.GetStringAsync("/weatherforecast");
			var forecastList = JsonConvert.DeserializeObject<List<WeatherForecast>>(response.Result);
			
			ResponseButton.Text = response.Result;
        }
		catch (Exception exception)
		{
			ResponseButton.Text = exception.Message;
		}
    }
}

