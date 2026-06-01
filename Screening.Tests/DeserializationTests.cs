using Screening.Domain.Parser.Models;
using Screening.Domain.Services;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Unit = Screening.Domain.Parser.Models.Unit;

namespace Screening.Tests;

public class DeserializationTests
{
    [Fact]
    public void Deserialization_ShouldDeserializeYamlContentCorrectly()
    {
        // Arrange
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .IgnoreUnmatchedProperties()
            .Build();
        
        // Act
        var act = () => deserializer.Deserialize<YamlQuestionnaire>(YamlContent);
        
        // Assert
        act.Should().NotThrow();
        var questionnaire = act();
        questionnaire.Title.Should().Be("Diabetes Risk Assessment");
        questionnaire.Questions.Should().HaveCount(10);
    }

    [Theory]
    [InlineData("cm", Unit.Centimeter)]
    [InlineData("Cm", Unit.Centimeter)]
    [InlineData("centimeter", Unit.Centimeter)]
    [InlineData("Centimeter", Unit.Centimeter)]
    [InlineData("m", Unit.Meter)]
    [InlineData("meter", Unit.Meter)]
    [InlineData("kg", Unit.KiloGrams)]
    [InlineData("kilogram", Unit.KiloGrams)]
    [InlineData("kilograms", Unit.KiloGrams)]
    public void Deserialization_ShouldDeserializeUnitsCorrectly_UsingYamlDeserializer(
        string unit, Unit expectedUnit)
    {
        // Arrange
        var content = $"unit: {unit}";
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .WithTypeConverter(new UnitYamlTypeConverter())
            .IgnoreUnmatchedProperties()
            .Build();
        
        // Act
        var actual = deserializer.Deserialize<YamlSaveConfig>(content);
        
        // Assert
        actual.Should().NotBeNull();
        actual.Unit.Should().Be(expectedUnit);
    }

    private const string YamlContent =
        """
        title: "Diabetes Risk Assessment"
        
        questions:
          - id: 1
            text: "What is your age?"
            type: Number
        
          - id: 2
            text: "What is your gender?"
            type: SingleChoice
            choices:
              - Female
              - Male
        
          - id: 3
            text: "Have you ever had gestational diabetes OR given birth to a baby weighing 9 pounds or more?"
            type: SingleChoice
            showIf:
              - questionId: 2
                operator: equals
                value: Female
            choices:
              - "Yes"
              - "No"
        
          - id: 4
            text: "Do you have a parent, brother, or sister with diabetes?"
            type: SingleChoice
            choices:
              - "Yes"
              - "No"
        
          - id: 5
            text: "Have you ever been diagnosed with high blood pressure or are you on medication for high blood pressure?"
            type: SingleChoice
            choices:
              - "Yes"
              - "No"
        
          - id: 6
            text: "Are you physically active?"
            type: SingleChoice
            choices:
              - "Yes"
              - "No"
        
          - id: 7
            text: "What is your height (cm)?"
            type: Number
        
          - id: 8
            text: "What is your weight (kg)?"
            type: Number
        
          - id: 9
            text: "Which of the following do you use?"
            type: MultiChoice
            choices:
              - Smoking
              - Alcohol
              - Aspirin
        
          - id: 10
            text: "How many cigarettes per day?"
            type: Number
            showIf:
              - questionId: 9
                operator: equals
                value: Smoking
        
        """;
}
