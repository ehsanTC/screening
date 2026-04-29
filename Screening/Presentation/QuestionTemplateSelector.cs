using Screening.Domain.Questions;

namespace Screening.Presentation;

public class QuestionTemplateSelector : DataTemplateSelector
{
    public DataTemplate TextInputTemplate { get; set; }

    protected override DataTemplate SelectTemplateCore(object item) =>
        (item switch
        {
            QuestionState { CurrentQuestion: TextInputQuestion } => TextInputTemplate,
            // other questions here
            _ => null
        })!;
}

