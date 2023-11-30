
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

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
}