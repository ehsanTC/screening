using Screening.Domain.Questions;

namespace Screening.Presentation;

public class QuestionTemplateSelector : DataTemplateSelector
{
    public DataTemplate TextInputTemplate { get; set; }
    public DataTemplate SingleChoiceTemplate { get; set; }
    public DataTemplate MultiChoiceTemplate { get; set; }

    protected override DataTemplate SelectTemplateCore(object item) =>
        (item switch
        {
            TextInputDisplayableQuestion => TextInputTemplate,
            SingleChoiceDisplayableQuestion => SingleChoiceTemplate,
            MultiChoiceDisplayableQuestion => MultiChoiceTemplate,
            
            _ => null
        })!;
}
