using Screening.Presentation.Utility;

namespace Screening.Presentation.Dialogs;

public sealed partial class ResultDialog : ContentDialog
{
    public ResultDialog()
    {
        this.InitializeComponent();
        FlowDirection = DirectionHelper.Direction;
    }
}

