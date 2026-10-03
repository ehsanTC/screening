namespace Screening.Presentation;
using System;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

public sealed partial class QuestionPage : Page
{
    // No-op edit: touch file to ensure it is reloaded for edit.
    public QuestionPage()
    {
        this.InitializeComponent();
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private InputPane? _inputPane;

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        // Subscribe to the soft keyboard (InputPane) events to move the page up when it appears
        try
        {
            _inputPane = InputPane.GetForCurrentView();
            if (_inputPane != null)
            {
                _inputPane.Showing += InputPane_Showing;
                _inputPane.Hiding += InputPane_Hiding;
            }
        }
        catch (Exception)
        {
            // InputPane may not be available on some platforms; ignore failures
        }
    }

    private void OnUnloaded(object? sender, RoutedEventArgs e)
    {
        if (_inputPane != null)
        {
            _inputPane.Showing -= InputPane_Showing;
            _inputPane.Hiding -= InputPane_Hiding;
            _inputPane = null;
        }
    }

    private void InputPane_Showing(InputPane sender, InputPaneVisibilityEventArgs args)
    {
        // Move the root grid up by the height of the occluded rect so bottom controls stay visible
        var occludedHeight = args.OccludedRect.Height;
        // Convert pixels to effective pixels if necessary - on Uno, OccludedRect is in view pixels already
        RootTranslate.Y = -occludedHeight;
    }

    private void InputPane_Hiding(InputPane sender, InputPaneVisibilityEventArgs args)
    {
        RootTranslate.Y = 0;
    }
}
