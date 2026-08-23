using Screening.Domain.Models;
using Screening.Domain.RiskCalculator.Abstraction;
using Screening.Presentation.Dialogs;

namespace Screening.Presentation;

public partial record MainModel
{
    private async Task ShowDialog(RiskResult? riskResult)
    {
        if (riskResult is null)
            return;

        var dialogResponse = await
            _navigator.NavigateViewModelForResultAsync<QuestionnaireFinishedDialogModel, bool>(
                this,
                qualifier: Qualifiers.Dialog,
                data: riskResult.Summary);

        if (dialogResponse == null)
            return;
            
        var okOption = await dialogResponse.AsResultResponse<bool>()!.Result ;
        var okClicked = okOption.SomeOrDefault();

        if (okClicked)
        {
            _ = await _navigator.NavigateViewModelAsync<ResultDialogModel>(
                this,
                qualifier: Qualifiers.Dialog,
                data: riskResult);
        }
    }

    private static async Task<RiskResult?> ExtractResult(NavigationResultResponse<RiskResult>? result)
    {
        var option = await result?.AsResultResponse<RiskResult>()?.Result!;
        var riskResult = option.SomeOrDefault();
        return riskResult;
    }

    private async Task ShowQuestionnaire(QuestionnaireType type)
    {
        var result = await _navigator.NavigateViewModelForResultAsync<QuestionModel, RiskResult>(this,
            data: new QuestionnairePageRequest(type,
                $"{_localizer["Next"]}",
                $"{_localizer["Calculate"]}"));

        var risk = await ExtractResult(result);
        await ShowDialog(risk);
    }
}
