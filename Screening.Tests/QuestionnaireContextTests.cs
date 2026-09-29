//using Screening.Domain.Answers;
//using Screening.Domain.Exceptions;
//using Screening.Domain.Parser.Models;
//using Screening.Domain.Questions;

//namespace Screening.Tests;

//public class QuestionnaireContextTests
//{
//    [Fact]
//    public void ShowQuestionById_ShouldReturnTrue_WhenNoDisplayConditionsExist()
//    {
//        // Arrange
//        var questionViewModel = Substitute.For<IDisplayableQuestion>();
//        questionViewModel.Question.Returns(new YamlQuestion { Id = 1, DisplayConditions = [] });

//        var viewModel = new QuestionnaireContext
//        {
//            Questions = new List<IDisplayableQuestion> { questionViewModel }
//        };

//        // Act
//        var result = viewModel.ShowQuestionById(1);

//        // Assert
//        result.Should().BeTrue();
//    }

//    [Fact]
//    public void ShowQuestionById_ShouldReturnTrue_WhenDisplayConditionsIsNull()
//    {
//        // Arrange
//        var questionViewModel = Substitute.For<IDisplayableQuestion>();
//        questionViewModel.Question.Returns(new YamlQuestion { Id = 1, DisplayConditions = null });

//        var viewModel = new QuestionnaireContext
//        {
//            Questions = new List<IDisplayableQuestion> { questionViewModel }
//        };

//        // Act
//        var result = viewModel.ShowQuestionById(1);

//        // Assert
//        result.Should().BeTrue();
//    }

//    [Fact]
//    public void ShowQuestionById_ShouldThrowArgumentOutOfRangeException_WhenIdIsNegative()
//    {
//        // Arrange
//        var viewModel = new QuestionnaireContext
//        {
//            Questions = new List<IDisplayableQuestion>()
//        };

//        // Act
//        Action act = () => viewModel.ShowQuestionById(-1);

//        // Assert
//        act.Should().Throw<ArgumentOutOfRangeException>();
//    }

//    [Fact]
//    public void ShowQuestionById_ShouldThrowArgumentOutOfRangeException_WhenIdIsGreaterThanOrEqualToQuestionsCount()
//    {
//        // Arrange
//        var questionViewModel = Substitute.For<IDisplayableQuestion>();
//        var viewModel = new QuestionnaireContext
//        {
//            Questions = new List<IDisplayableQuestion> { questionViewModel }
//        };

//        // Act
//        Action act = () => viewModel.ShowQuestionById(1);

//        // Assert
//        act.Should().Throw<QuestionNotFoundException>();
//    }

//    [Fact]
//    public void ShowQuestionById_ShouldReturnFalse_WhenTargetQuestionIsNotAnswered()
//    {
//        // Arrange
//        var targetQuestion = Substitute.For<IDisplayableQuestion>();
//        targetQuestion.Question.Returns(new YamlQuestion { Id = 1 });
//        targetQuestion.IsAnswered.Returns(false);

//        var currentQuestion = Substitute.For<IDisplayableQuestion>();
//        var condition = new YamlCondition
//            { QuestionId = 1, Operator = ConditionOperator.Equals, ExpectedValue = "Yes" };
//        currentQuestion.Question.Returns(new YamlQuestion { Id = 2, DisplayConditions = [condition] });

//        var viewModel = new QuestionnaireContext
//        {
//            Questions = new List<IDisplayableQuestion> { targetQuestion, currentQuestion }
//        };

//        // Act
//        var result = viewModel.ShowQuestionById(2);

//        // Assert
//        result.Should().BeFalse();
//    }

//    [Fact]
//    public void ShowQuestionById_ShouldReturnTrue_WhenConditionIsMet()
//    {
//        // Arrange
//        var targetQuestion = Substitute.For<IDisplayableQuestion>();
//        targetQuestion.Question.Returns(new YamlQuestion { Id = 1 });
//        targetQuestion.IsAnswered.Returns(true);

//        var answer = Substitute.For<IAnswer>();
//        answer.GetAnswers().Returns(new List<string> { "Yes" });
//        targetQuestion.Answer.Returns(answer);

//        var currentQuestion = Substitute.For<IDisplayableQuestion>();
//        var condition = new YamlCondition
//            { QuestionId = 1, Operator = ConditionOperator.Equals, ExpectedValue = "Yes" };
//        currentQuestion.Question.Returns(new YamlQuestion
//            { Id = 2, DisplayConditions = new List<YamlCondition> { condition } });

//        var viewModel = new QuestionnaireContext
//        {
//            Questions = new List<IDisplayableQuestion> { targetQuestion, currentQuestion }
//        };

//        // Act
//        var result = viewModel.ShowQuestionById(1);

//        // Assert
//        result.Should().BeTrue();
//    }

//    [Fact]
//    public void ShowQuestionById_ShouldReturnFalse_WhenConditionIsNotMet()
//    {
//        // Arrange
//        var targetQuestion = Substitute.For<IDisplayableQuestion>();
//        targetQuestion.Question.Returns(new YamlQuestion { Id = 1 });
//        targetQuestion.IsAnswered.Returns(true);

//        var answer = Substitute.For<IAnswer>();
//        answer.GetAnswers().Returns(["No"]);
//        targetQuestion.Answer.Returns(answer);

//        var currentQuestion = Substitute.For<IDisplayableQuestion>();
//        var condition = new YamlCondition
//            { QuestionId = 1, Operator = ConditionOperator.Equals, ExpectedValue = "Yes" };
//        currentQuestion.Question.Returns(new YamlQuestion { Id = 2, DisplayConditions = [condition] });

//        var viewModel = new QuestionnaireContext
//        {
//            Questions = new List<IDisplayableQuestion> { targetQuestion, currentQuestion }
//        };

//        // Act
//        var result = viewModel.ShowQuestionById(2);

//        // Assert
//        result.Should().BeFalse();
//    }

//    [Fact]
//    public void ShowQuestionById_ShouldReturnTrue_WhenAllConditionsAreMet()
//    {
//        // Arrange
//        var targetQuestion1 = Substitute.For<IDisplayableQuestion>();
//        targetQuestion1.Question.Returns(new YamlQuestion { Id = 1 });
//        targetQuestion1.IsAnswered.Returns(true);

//        var answer1 = Substitute.For<IAnswer>();
//        answer1.GetAnswers().Returns(["Yes"]);
//        targetQuestion1.Answer.Returns(answer1);

//        var targetQuestion2 = Substitute.For<IDisplayableQuestion>();
//        targetQuestion2.Question.Returns(new YamlQuestion { Id = 2 });
//        targetQuestion2.IsAnswered.Returns(true);

//        var answer2 = Substitute.For<IAnswer>();
//        answer2.GetAnswers().Returns(["10"]);
//        targetQuestion2.Answer.Returns(answer2);

//        var currentQuestion = Substitute.For<IDisplayableQuestion>();
//        var conditions = new List<YamlCondition>
//        {
//            new YamlCondition { QuestionId = 1, Operator = ConditionOperator.Equals, ExpectedValue = "Yes" },
//            new YamlCondition { QuestionId = 2, Operator = ConditionOperator.GreaterThan, ExpectedValue = "5" }
//        };
//        currentQuestion.Question.Returns(new YamlQuestion { Id = 3, DisplayConditions = conditions });

//        var viewModel = new QuestionnaireContext
//        {
//            Questions = new List<IDisplayableQuestion> { targetQuestion1, targetQuestion2, currentQuestion }
//        };

//        // Act
//        var result = viewModel.ShowQuestionById(3);

//        // Assert
//        result.Should().BeTrue();
//    }

//    [Fact]
//    public void ShowQuestionByIndex_ShouldReturnFalse_WhenAnyConditionIsNotMet()
//    {
//        // Arrange
//        var targetQuestion1 = Substitute.For<IDisplayableQuestion>();
//        targetQuestion1.Question.Returns(new YamlQuestion { Id = 1 });
//        targetQuestion1.IsAnswered.Returns(true);

//        var answer1 = Substitute.For<IAnswer>();
//        answer1.GetAnswers().Returns(["Yes"]);
//        targetQuestion1.Answer.Returns(answer1);

//        var answer2 = Substitute.For<IAnswer>();
//        answer2.GetAnswers().Returns(["3"]);

//        var targetQuestion2 = Substitute.For<IDisplayableQuestion>();
//        targetQuestion2.Question.Returns(new YamlQuestion { Id = 2 });
//        targetQuestion2.IsAnswered.Returns(true);
//        targetQuestion2.Answer.Returns(answer2);

//        var currentQuestion = Substitute.For<IDisplayableQuestion>();
//        var conditions = new List<YamlCondition>
//        {
//            new() { QuestionId = 1, Operator = ConditionOperator.Equals, ExpectedValue = "Yes" },
//            new() { QuestionId = 2, Operator = ConditionOperator.GreaterThan, ExpectedValue = "5" }
//        };
//        currentQuestion.Question.Returns(new YamlQuestion { Id = 3, DisplayConditions = conditions });

//        var viewModel = new QuestionnaireContext
//        {
//            Questions = new List<IDisplayableQuestion> { targetQuestion1, targetQuestion2, currentQuestion }
//        };

//        // Act
//        var result = viewModel.ShowQuestionAtIndex(2);

//        // Assert
//        result.Should().BeFalse();
//    }

//    [Fact]
//    public void ShowQuestionById_ShouldThrowInvalidYamlDataException_WhenConditionReferencesNonExistentQuestion()
//    {
//        // Arrange
//        var currentQuestion = Substitute.For<IDisplayableQuestion>();
//        var condition = new YamlCondition
//        {
//            QuestionId = 999,
//            Operator = ConditionOperator.Equals,
//            ExpectedValue = "Yes"
//        };
//        currentQuestion.Question.Returns(new YamlQuestion
//        {
//            Id = 1,
//            DisplayConditions = [condition]
//        });

//        var viewModel = new QuestionnaireContext
//        {
//            Questions = new List<IDisplayableQuestion> { currentQuestion }
//        };

//        // Act
//        Action act = () => viewModel.ShowQuestionAtIndex(0);

//        // Assert
//        act.Should().Throw<InvalidYamlDataException>();
//    }

//    [Theory]
//    [InlineData(ConditionOperator.Equals, "Yes", "Yes", true)]
//    [InlineData(ConditionOperator.Equals, "Yes", "No", false)]
//    [InlineData(ConditionOperator.NotEquals, "Yes", "No", true)]
//    [InlineData(ConditionOperator.NotEquals, "Yes", "Yes", false)]
//    [InlineData(ConditionOperator.GreaterThan, "10", "5", false)]
//    [InlineData(ConditionOperator.GreaterThan, "5", "10", true)]
//    [InlineData(ConditionOperator.LessThan, "5", "10", false)]
//    [InlineData(ConditionOperator.LessThan, "10", "5", true)]
//    [InlineData(ConditionOperator.GreaterThanOrEquals, "10", "10", true)]
//    [InlineData(ConditionOperator.GreaterThanOrEquals, "5", "10", true)]
//    [InlineData(ConditionOperator.GreaterThanOrEquals, "10", "5", false)]
//    [InlineData(ConditionOperator.LessThanOrEquals, "10", "10", true)]
//    [InlineData(ConditionOperator.LessThanOrEquals, "5", "10", false)]
//    [InlineData(ConditionOperator.LessThanOrEquals, "10", "5", true)]
//    public void ShowQuestionById_ShouldEvaluateCorrectly_WhenUsingDifferentOperators(
//        ConditionOperator op, string conditionValue, string answerValue, bool expected)
//    {
//        // Arrange
//        var targetQuestion = Substitute.For<IDisplayableQuestion>();
//        targetQuestion.Question.Returns(new YamlQuestion { Id = 1 });
//        targetQuestion.IsAnswered.Returns(true);

//        var answer = Substitute.For<IAnswer>();
//        answer.GetAnswers().Returns([answerValue]);
//        targetQuestion.Answer.Returns(answer);

//        var currentQuestion = Substitute.For<IDisplayableQuestion>();
//        var condition = new YamlCondition
//        {
//            QuestionId = 1,
//            Operator = op,
//            ExpectedValue = conditionValue
//        };
        
//        var question2Id = 2;
//        currentQuestion.Question.Returns(
//            new YamlQuestion
//            {
//                Id = question2Id, DisplayConditions = [condition]
//            });

//        var viewModel = new QuestionnaireContext
//        {
//            Questions = new List<IDisplayableQuestion> { targetQuestion, currentQuestion }
//        };

//        // Act
//        var result = viewModel.ShowQuestionById(question2Id);

//        // Assert
//        result.Should().Be(expected);
//    }

//    [Theory]
//    [InlineData(-1)]
//    [InlineData(2)]
//    public void ShowQuestionByIndex_ShouldThrowException_WhenIndexIsOutOfRange(int index)
//    {
//        // Arrange
//        var viewModel = new QuestionnaireContext()
//        {
//            Questions = [Substitute.For<IDisplayableQuestion>()]
//        };

//        // Act
//        var act = () => viewModel.ShowQuestionAtIndex(index);

//        // Assert
//        act.Should().Throw<ArgumentOutOfRangeException>();
//    }

//    [Fact]
//    public void CompareAnswerWithCondition_ShouldThrowNotSupportedException_WhenMultipleAnswersProvided()
//    {
//        // Arrange
//        var condition = new YamlCondition
//        {
//            QuestionId = 1,
//            Operator = ConditionOperator.Equals,
//            ExpectedValue = "Yes"
//        };
//        var answer = Substitute.For<IAnswer>();
//        answer.GetAnswers().Returns(["Yes", "No"]);

//        var question = Substitute.For<IDisplayableQuestion>();
//        question.Question.Returns(new YamlQuestion { Id = 1, DisplayConditions = [condition] });
//        question.IsAnswered.Returns(true);
//        question.Answer.Returns(answer);

//        var viewModel = new QuestionnaireContext()
//        {
//            Questions = [question]
//        };

//        // Act
//        Action act = () => viewModel.ShowQuestionAtIndex(0);

//        // Assert
//        act.Should().Throw<NotSupportedException>()
//            .WithMessage("Multiple answers are not supported.");
//    }

//    [Fact]
//    public void ShowQuestionById_ShouldThrowException_WhenQuestionPropertyIsNull()
//    {
//        // Arrange
//        var questionViewModel = Substitute.For<IDisplayableQuestion>();
//        questionViewModel.Question.Returns((YamlQuestion) null);

//        var viewModel = new QuestionnaireContext
//        {
//            Questions = new List<IDisplayableQuestion> { questionViewModel }
//        };

//        // Act
//        var act = () => viewModel.ShowQuestionById(0);

//        // Assert
//        act.Should().Throw<QuestionNotFoundException>();
//    }

//    [Fact]
//    public void ShowQuestionByIndex_ShouldHandleNullAnswerProperty_WhenGettingAnswers()
//    {
//        // Arrange
//        var targetQuestion = Substitute.For<IDisplayableQuestion>();
//        targetQuestion.Question.Returns(new YamlQuestion { Id = 1 });
//        targetQuestion.IsAnswered.Returns(true);
//        targetQuestion.Answer.Returns((IAnswer)null);

//        var currentQuestion = Substitute.For<IDisplayableQuestion>();
//        var condition = new YamlCondition
//        {
//            QuestionId = 1,
//            Operator = ConditionOperator.Equals,
//            ExpectedValue = "Yes"
//        };
//        currentQuestion.Question.Returns(
//            new YamlQuestion
//            {
//                Id = 2,
//                DisplayConditions = [condition]
//            });

//        var viewModel = new QuestionnaireContext
//        {
//            Questions = new List<IDisplayableQuestion> { targetQuestion, currentQuestion }
//        };

//        // Act
//        Action act = () => viewModel.ShowQuestionAtIndex(1);

//        // Assert
//        act.Should().Throw<ArgumentNullException>();
//    }

//    [Theory]
//    [InlineData(ConditionOperator.Equals, "10", "10", true)]
//    [InlineData(ConditionOperator.Equals, "10", "5", false)]
//    [InlineData(ConditionOperator.NotEquals, "10", "5", true)]
//    [InlineData(ConditionOperator.NotEquals, "10", "10", false)]
//    [InlineData(ConditionOperator.GreaterThan, "10", "5", true)]
//    [InlineData(ConditionOperator.GreaterThan, "10", "10", false)]
//    [InlineData(ConditionOperator.GreaterThan, "5", "10", false)]
//    [InlineData(ConditionOperator.LessThan, "5", "10", true)]
//    [InlineData(ConditionOperator.LessThan, "10", "10", false)]
//    [InlineData(ConditionOperator.LessThan, "10", "5", false)]
//    [InlineData(ConditionOperator.GreaterThanOrEquals, "10", "5", true)]
//    [InlineData(ConditionOperator.GreaterThanOrEquals, "10", "10", true)]
//    [InlineData(ConditionOperator.GreaterThanOrEquals, "5", "10", false)]
//    [InlineData(ConditionOperator.LessThanOrEquals, "5", "10", true)]
//    [InlineData(ConditionOperator.LessThanOrEquals, "10", "10", true)]
//    [InlineData(ConditionOperator.LessThanOrEquals, "10", "5", false)]
//    public void ShowQuestionById_ShouldEvaluateCorrectly_WhenUsingNumericComparisons(
//        ConditionOperator op, string answerValue, string conditionValue, bool expected)
//    {
//        // Arrange
//        var targetQuestion = Substitute.For<IDisplayableQuestion>();
//        targetQuestion.Question.Returns(new YamlQuestion { Id = 1 });
//        targetQuestion.IsAnswered.Returns(true);
//        var answer = Substitute.For<IAnswer>();
//        answer.GetAnswers().Returns(new List<string> { answerValue });
//        targetQuestion.Answer.Returns(answer);

//        var currentQuestion = Substitute.For<IDisplayableQuestion>();
//        var condition = new YamlCondition { QuestionId = 1, Operator = op, ExpectedValue = conditionValue };
//        currentQuestion.Question.Returns(new YamlQuestion
//            { Id = 2, DisplayConditions = new List<YamlCondition> { condition } });

//        var viewModel = new QuestionnaireContext
//        {
//            Questions = new List<IDisplayableQuestion> { targetQuestion, currentQuestion }
//        };

//        // Act
//        var result = viewModel.ShowQuestionById(2);

//        // Assert
//        result.Should().Be(expected);
//    }

//    [Theory]
//    [InlineData(ConditionOperator.GreaterThan, "apple", "banana", false)]
//    [InlineData(ConditionOperator.LessThan, "apple", "banana", true)]
//    [InlineData(ConditionOperator.GreaterThanOrEquals, "apple", "apple", true)]
//    [InlineData(ConditionOperator.LessThanOrEquals, "zebra", "apple", false)]
//    public void ShowQuestionById_ShouldEvaluateCorrectly_WhenUsingStringComparisons(
//        ConditionOperator op, string answerValue, string conditionValue, bool expected)
//    {
//        // Arrange
//        var targetQuestion = Substitute.For<IDisplayableQuestion>();
//        targetQuestion.Question.Returns(new YamlQuestion { Id = 1 });
//        targetQuestion.IsAnswered.Returns(true);
//        var answer = Substitute.For<IAnswer>();
//        answer.GetAnswers().Returns([answerValue]);
//        targetQuestion.Answer.Returns(answer);

//        var currentQuestion = Substitute.For<IDisplayableQuestion>();
//        var condition = new YamlCondition { QuestionId = 1, Operator = op, ExpectedValue = conditionValue };
//        currentQuestion.Question.Returns(new YamlQuestion { Id = 2, DisplayConditions = [condition] });

//        var viewModel = new QuestionnaireContext
//        {
//            Questions = new List<IDisplayableQuestion> { targetQuestion, currentQuestion }
//        };

//        // Act
//        var result = viewModel.ShowQuestionById(2);

//        // Assert
//        result.Should().Be(expected);
//    }

//    [Fact]
//    public void ShowQuestionById_ShouldThrowArgumentOutOfRangeException_WhenOperatorIsInvalid()
//    {
//        // Arrange
//        var targetQuestion = Substitute.For<IDisplayableQuestion>();
//        targetQuestion.Question.Returns(new YamlQuestion { Id = 1 });
//        targetQuestion.IsAnswered.Returns(true);
//        var answer = Substitute.For<IAnswer>();
//        answer.GetAnswers().Returns(new List<string> { "test" });
//        targetQuestion.Answer.Returns(answer);

//        var currentQuestion = Substitute.For<IDisplayableQuestion>();
//        var condition = new YamlCondition { QuestionId = 1, Operator = (ConditionOperator)999, ExpectedValue = "test" };
//        currentQuestion.Question.Returns(new YamlQuestion
//            { Id = 2, DisplayConditions = new List<YamlCondition> { condition } });

//        var viewModel = new QuestionnaireContext
//        {
//            Questions = new List<IDisplayableQuestion> { targetQuestion, currentQuestion }
//        };

//        // Act
//        Action act = () => viewModel.ShowQuestionById(2);

//        // Assert
//        act.Should().Throw<ArgumentOutOfRangeException>();
//    }

//    [Fact]
//    public void ShowQuestionById_ShouldReturnTrue_WhenConditionValueIsNumericAndAnswerMatches()
//    {
//        // Arrange
//        var targetQuestion = Substitute.For<IDisplayableQuestion>();
//        targetQuestion.Question.Returns(new YamlQuestion { Id = 1 });
//        targetQuestion.IsAnswered.Returns(true);
//        var answer = Substitute.For<IAnswer>();
//        answer.GetAnswers().Returns(new List<string> { "42" });
//        targetQuestion.Answer.Returns(answer);

//        var currentQuestion = Substitute.For<IDisplayableQuestion>();
//        var condition = new YamlCondition { QuestionId = 1, Operator = ConditionOperator.Equals, ExpectedValue = 42 };
//        currentQuestion.Question.Returns(new YamlQuestion
//            { Id = 2, DisplayConditions = new List<YamlCondition> { condition } });

//        var viewModel = new QuestionnaireContext
//        {
//            Questions = new List<IDisplayableQuestion> { targetQuestion, currentQuestion }
//        };

//        // Act
//        var result = viewModel.ShowQuestionById(1);

//        // Assert
//        result.Should().BeTrue();
//    }

//    [Fact]
//    public void ShowQuestionById_ShouldHandleCaseSensitiveStringComparison()
//    {
//        // Arrange
//        var targetQuestion = Substitute.For<IDisplayableQuestion>();
//        targetQuestion.Question.Returns(new YamlQuestion { Id = 1 });
//        targetQuestion.IsAnswered.Returns(true);
//        var answer = Substitute.For<IAnswer>();
//        answer.GetAnswers().Returns(new List<string> { "Yes" });
//        targetQuestion.Answer.Returns(answer);

//        var currentQuestion = Substitute.For<IDisplayableQuestion>();
//        var condition = new YamlCondition
//            { QuestionId = 1, Operator = ConditionOperator.Equals, ExpectedValue = "yes" };
//        currentQuestion.Question.Returns(new YamlQuestion
//            { Id = 2, DisplayConditions = new List<YamlCondition> { condition } });

//        var viewModel = new QuestionnaireContext
//        {
//            Questions = new List<IDisplayableQuestion> { targetQuestion, currentQuestion }
//        };

//        // Act
//        var result = viewModel.ShowQuestionById(2);

//        // Assert
//        result.Should().BeFalse();
//    }
//}
