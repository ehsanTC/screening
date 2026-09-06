using Screening.Domain.Models;
using Screening.Domain.Questions;
using Screening.Domain.RiskCalculator.Abstraction;
using Screening.Presentation.Dialogs;
using Screening.Presentation.Model;

namespace Screening.Presentation;

public partial record MainModel
{
    private async Task ShowDialog(QuestionnaireOutcome? outcome)
    {
        if (outcome is null)
            return;

        var dialogResponse = await
            _navigator.NavigateViewModelForResultAsync<QuestionnaireFinishedDialogModel, bool>(
                this,
                qualifier: Qualifiers.Dialog,
                data: outcome.RiskResult.Summary);

        if (dialogResponse == null)
            return;
            
        var okOption = await dialogResponse.AsResultResponse<bool>()!.Result ;
        var okClicked = okOption.SomeOrDefault();

        if (okClicked)
        {
            _ = await _navigator.NavigateViewModelAsync<ResultDialogModel>(
                this,
                qualifier: Qualifiers.Dialog,
                data: outcome);
        }
    }

    private static async Task<QuestionnaireOutcome?> ExtractResult(NavigationResultResponse<QuestionnaireOutcome>? result)
    {
        var option = await result?.AsResultResponse<QuestionnaireOutcome>()?.Result!;
        var riskResult = option.SomeOrDefault();
        return riskResult;
    }

    private async Task ShowQuestionnaire(QuestionnaireType type)
    {
        var result = await _navigator.NavigateViewModelForResultAsync<QuestionModel, QuestionnaireOutcome>(this,
            data: new QuestionnairePageRequest(type,
                $"{_localizer["Next"]}",
                $"{_localizer["Calculate"]}"));

        var outcome = await ExtractResult(result);
        if (outcome == null)
            return;
        
        await ShowDialog(outcome);
    }
}
