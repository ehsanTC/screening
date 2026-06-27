namespace Screening.Domain.Answers;

public record MultipleChoiceAnswer(List<string> SelectedOptions) : Answer;
