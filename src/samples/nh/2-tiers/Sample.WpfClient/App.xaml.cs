using Sample.WpfClient.Presentation;
using System.Net;
using System.Windows;
using Topics.Radical.Windows.Presentation.Boot;

namespace Sample.WpfClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
	{
		public App()
		{
			ServicePointManager.DefaultConnectionLimit = 10;

			var bootstrapper = new WindsorApplicationBootstrapper<MainView>();
		}
	}
}
