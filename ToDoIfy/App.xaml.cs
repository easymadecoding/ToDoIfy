
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Plugin.LocalNotification;
using Plugin.LocalNotification.iOSOption;

namespace ToDoIfy;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        MainPage = new AppShell();
	}

    protected override async void OnStart()
	{
        await CheckFaceIdAuth();
        await CheckNotificationPermissions();
    }

    protected override async void OnResume()
	{
        await CheckFaceIdAuth();
    }

	private async Task CheckFaceIdAuth()
	{
        var authenticationRequest = new
        AuthenticationRequestConfiguration("Are you authorized?",
        "We need to ensure it is you before proceeding");

        var authenticationResult =
            await CrossFingerprint.Current.AuthenticateAsync(authenticationRequest);

        if (authenticationResult.Authenticated)
        {
            MainPage = new AppShell();
        }
        else
        {
            System.Environment.Exit(0);
        }
    }

    private async Task CheckNotificationPermissions()
    {
        if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
        {
            await LocalNotificationCenter.Current.RequestNotificationPermission(new NotificationPermission
            {
                IOS =
                {
                    LocationAuthorization = iOSLocationAuthorization.WhenInUse
                }
            });
        }
    }
}