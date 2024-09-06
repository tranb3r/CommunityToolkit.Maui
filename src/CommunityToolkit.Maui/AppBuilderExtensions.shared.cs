﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Handlers;
using CommunityToolkit.Maui.PlatformConfiguration.AndroidSpecific;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.LifecycleEvents;

namespace CommunityToolkit.Maui;

/// <summary>
/// Extensions for MauiAppBuilder
/// </summary>
public static class AppBuilderExtensions
{
	/// <summary>
	/// Initializes the .NET MAUI Community Toolkit Library
	/// </summary>
	/// <param name="builder"><see cref="MauiAppBuilder"/> generated by <see cref="MauiApp"/> </param>
	/// <param name="options"><see cref="Options"/></param>
	/// <returns><see cref="MauiAppBuilder"/> initialized for <see cref="CommunityToolkit.Maui"/></returns>
	public static MauiAppBuilder UseMauiCommunityToolkit(this MauiAppBuilder builder, Action<Options>? options = default)
	{
		// Pass `null` because `options?.Invoke()` will set options on both `CommunityToolkit.Maui` and `CommunityToolkit.Maui.Core`
		builder.UseMauiCommunityToolkitCore(null);

		builder.Services.AddSingleton<IPopupService, PopupService>();

		// Invokes options for both `CommunityToolkit.Maui` and `CommunityToolkit.Maui.Core`
		options?.Invoke(new Options(builder));

		builder.ConfigureMauiHandlers(h =>
		{
			h.AddHandler<DrawingView, DrawingViewHandler>();
			h.AddHandler<Popup, PopupHandler>();
			h.AddHandler<SemanticOrderView, SemanticOrderViewHandler>();
		});

		Popup.RemapForControls();

		builder.UseMauiCommunityToolkitNavigationBar();

		return builder;
	}

	#region Use specific features

	/// <summary>
	/// Initializes only the NavigationBar feature from .NET MAUI Community Toolkit
	/// </summary>
	/// <param name="builder"><see cref="MauiAppBuilder"/> generated by <see cref="MauiApp"/> </param>
	/// <returns><see cref="MauiAppBuilder"/> initialized for <see cref="CommunityToolkit.Maui"/></returns>
	public static MauiAppBuilder UseMauiCommunityToolkitNavigationBar(this MauiAppBuilder builder)
	{
		NavigationBar.RemapForControls();
		return builder;
	}

	#endregion Use specific features
}