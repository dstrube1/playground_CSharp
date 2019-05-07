using Xamarin.Forms;

namespace SmartHome
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new SmartHome.MainPage());

            var simulator = new SmartHomeSimulator();
            simulator.Run();
        }
	}
}