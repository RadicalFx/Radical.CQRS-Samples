using Castle;
using Castle.Facilities.TypedFactory;
using Jason.Configuration;
using Jason.Windsor;
using Sample.WpfClient.Presentation;
using System.Linq;
using System.Net;
using System.Windows;
using Topics.Radical.Windows.Presentation.Boot;
using Topics.Radical.Windows.Presentation.Helpers;

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

            new WindsorApplicationBootstrapper<MainView>()
                .OnServiceProviderCreated( container => 
                {
                    ( ( ServiceProviderWrapper )container ).Container.AddFacility<TypedFactoryFacility>();
                } )
                .OnBeforeInstall( bootConventions =>
                {
                    bootConventions.AssemblyFileScanPatterns = entryAssembly =>
                    {
                        return bootConventions
                            .DefaultAssemblyFileScanPatterns( entryAssembly )
                            .Union( new[] { "Sample.*.dll" } );
                    };
                } )
                .OnBoot( container =>
                {
                    var probeDirectory = EnvironmentHelper.GetCurrentDirectory();
                    var wrapper = ( ServiceProviderWrapper )container;

                    new DefaultJasonServerConfiguration( probeDirectory )
                    {
                        Container = new WindsorJasonContainerProxy( wrapper.Container ),
                        //TypeFilter = t => !t.Is<ShopperFallbackCommandHandler>()
                    }
                    .AddEndpoint( new Jason.Client.JasonInProcessEndpoint() )
                    .Initialize();

                    //.UsingAsFallbackCommandValidator<ObjectDataAnnotationValidator>();
                } );
        }
    }
}
