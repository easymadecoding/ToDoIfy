using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using ToDoIfy.ViewModels;
using Plugin.LocalNotification;

#if ANDROID
using Android.Views;
#endif

#if IOS
using UIKit;
#endif

namespace ToDoIfy;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseLocalNotification()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
        .ConfigureLifecycleEvents(events =>
        {

#if ANDROID
		events.AddAndroid(android => android.OnPause((activity) =>
			activity.Window?.SetFlags(WindowManagerFlags.Secure, WindowManagerFlags.Secure)));
        events.AddAndroid(android => android.OnResume ((activity) =>
			activity.Window?.ClearFlags(WindowManagerFlags.Secure)));
#endif


#if IOS
				UIVisualEffectView _blurWindow = null;

				events.AddiOS(ios => ios.OnResignActivation((application) =>
				{
					using (var blurEffect = UIBlurEffect.FromStyle(UIBlurEffectStyle.Dark))
					{
						_blurWindow = new UIVisualEffectView(blurEffect)
						{
							Frame = UIApplication.SharedApplication.KeyWindow.RootViewController.View.Bounds
						};

						UIApplication.SharedApplication.KeyWindow.RootViewController.View.AddSubview(_blurWindow);
					}
				}));

				events.AddiOS(ios => ios.OnActivated((application) =>
				{
					_blurWindow?.RemoveFromSuperview();
					_blurWindow?.Dispose();
					_blurWindow = null;
				}));
#endif
	});

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<ToDoViewModel>();

        builder.Services.AddTransient<DetailsPage>();
        builder.Services.AddTransient<DetailsViewModel>();

        builder.Services.AddSingleton<TodoItemDatabase>();

        return builder.Build();
	}
}

