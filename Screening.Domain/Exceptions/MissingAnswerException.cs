namespace Screening.Domain.Exceptions;

public class MissingAnswerException : Exception
{
    public MissingAnswerException(List<string> missing) : base ($"The answers for {string.Join(", ", missing)} are missed.")
    { }
    
    public MissingAnswerException(string missing) : base ($"The answers for {missing} is missed.")
    { }
}
