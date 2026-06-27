using Screening.Domain.Questions;

namespace Screening.Presentation;

public class QuestionTemplateSelector : DataTemplateSelector
{
    public DataTemplate? TextInputTemplate { get; set; }
    public DataTemplate? SingleChoiceTemplate { get; set; }
    public DataTemplate? MultiChoiceTemplate { get; set; }

    protected override DataTemplate? SelectTemplateCore(object item) =>
        SelectQuestionTemplate(item);

    protected override DataTemplate? SelectTemplateCore(object item, DependencyObject container) =>
        SelectQuestionTemplate(item);

    private DataTemplate? SelectQuestionTemplate(object? item) =>
        item switch
        {
            TextInputDisplayableQuestion    => TextInputTemplate,
            MultiChoiceDisplayableQuestion  => MultiChoiceTemplate,
            SingleChoiceDisplayableQuestion => SingleChoiceTemplate,

            _ => null
        };
}
