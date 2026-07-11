namespace Screening.Presentation.Views;

public sealed partial class DisorderCardView : UserControl
{
    public DisorderCardView()
    {
        InitializeComponent();
    }

    // --- Bottom Label ---

    public string BottomLabelText
    {
        get => (string)GetValue(BottomLabelTextProperty);
        set => SetValue(BottomLabelTextProperty, value);
    }
    public static readonly DependencyProperty BottomLabelTextProperty =
        DependencyProperty.Register(nameof(BottomLabelText), typeof(string),
            typeof(DisorderCardView), new PropertyMetadata(string.Empty));

    public double BottomLabelFontSize
    {
        get => (double)GetValue(BottomLabelFontSizeProperty);
        set => SetValue(BottomLabelFontSizeProperty, value);
    }
    public static readonly DependencyProperty BottomLabelFontSizeProperty =
        DependencyProperty.Register(nameof(BottomLabelFontSize), typeof(double),
            typeof(DisorderCardView), new PropertyMetadata(14.0));

    public FontFamily BottomLabelFontFamily
    {
        get => (FontFamily)GetValue(BottomLabelFontFamilyProperty);
        set => SetValue(BottomLabelFontFamilyProperty, value);
    }
    public static readonly DependencyProperty BottomLabelFontFamilyProperty =
        DependencyProperty.Register(nameof(BottomLabelFontFamily), typeof(FontFamily),
            typeof(DisorderCardView), new PropertyMetadata(null));

    public Brush BottomLabelForeground
    {
        get => (Brush)GetValue(BottomLabelForegroundProperty);
        set => SetValue(BottomLabelForegroundProperty, value);
    }
    public static readonly DependencyProperty BottomLabelForegroundProperty =
        DependencyProperty.Register(nameof(BottomLabelForeground), typeof(Brush),
            typeof(DisorderCardView), new PropertyMetadata(null));

    // --- Main Label ---

    public string MainLabelText
    {
        get => (string)GetValue(MainLabelTextProperty);
        set => SetValue(MainLabelTextProperty, value);
    }
    public static readonly DependencyProperty MainLabelTextProperty =
        DependencyProperty.Register(nameof(MainLabelText), typeof(string),
            typeof(DisorderCardView), new PropertyMetadata(string.Empty));

    public double MainLabelFontSize
    {
        get => (double)GetValue(MainLabelFontSizeProperty);
        set => SetValue(MainLabelFontSizeProperty, value);
    }
    public static readonly DependencyProperty MainLabelFontSizeProperty =
        DependencyProperty.Register(nameof(MainLabelFontSize), typeof(double),
            typeof(DisorderCardView), new PropertyMetadata(14.0));

    public FontFamily MainLabelFontFamily
    {
        get => (FontFamily)GetValue(MainLabelFontFamilyProperty);
        set => SetValue(MainLabelFontFamilyProperty, value);
    }
    public static readonly DependencyProperty MainLabelFontFamilyProperty =
        DependencyProperty.Register(nameof(MainLabelFontFamily), typeof(FontFamily),
            typeof(DisorderCardView), new PropertyMetadata(null));

    public Brush MainLabelForeground
    {
        get => (Brush)GetValue(MainLabelForegroundProperty);
        set => SetValue(MainLabelForegroundProperty, value);
    }
    public static readonly DependencyProperty MainLabelForegroundProperty =
        DependencyProperty.Register(nameof(MainLabelForeground), typeof(Brush),
            typeof(DisorderCardView), new PropertyMetadata(null));

    // --- Image ---

    public ImageSource ImageSource
    {
        get => (ImageSource)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
    public static readonly DependencyProperty ImageSourceProperty =
        DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource),
            typeof(DisorderCardView), new PropertyMetadata(null));

    public double ImageWidth
    {
        get => (double)GetValue(ImageWidthProperty);
        set => SetValue(ImageWidthProperty, value);
    }
    public static readonly DependencyProperty ImageWidthProperty =
        DependencyProperty.Register(nameof(ImageWidth), typeof(double),
            typeof(DisorderCardView), new PropertyMetadata(double.NaN));

    public double ImageHeight
    {
        get => (double)GetValue(ImageHeightProperty);
        set => SetValue(ImageHeightProperty, value);
    }
    public static readonly DependencyProperty ImageHeightProperty =
        DependencyProperty.Register(nameof(ImageHeight), typeof(double),
            typeof(DisorderCardView), new PropertyMetadata(double.NaN));

    public Stretch ImageStretch
    {
        get => (Stretch)GetValue(ImageStretchProperty);
        set => SetValue(ImageStretchProperty, value);
    }
    public static readonly DependencyProperty ImageStretchProperty =
        DependencyProperty.Register(nameof(ImageStretch), typeof(Stretch),
            typeof(DisorderCardView), new PropertyMetadata(Stretch.Uniform));
    
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand),
            typeof(DisorderCardView), new PropertyMetadata(null));

    public object QuestionnaireName
    {
        get => (object)GetValue(QuestionnaireNameProperty);
        set => SetValue(QuestionnaireNameProperty, value);
    }
    public static readonly DependencyProperty QuestionnaireNameProperty =
        DependencyProperty.Register(nameof(QuestionnaireName), typeof(object),
            typeof(DisorderCardView), new PropertyMetadata(null));
}
