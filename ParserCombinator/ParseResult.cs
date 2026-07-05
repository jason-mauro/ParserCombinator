using System.Security;

namespace ParserCombinator;

public struct ParseResult<T>
{
    public bool Success { get; set; }
    public T? Result { get; }
    public ParseError? Error { get; }
    public bool Committed { get; set; }

    public ParseResult(bool Success, T? Result, ParseError? Error, bool Committed)
    {
        this.Committed = Committed;
        this.Result = Result;
        this.Error = Error;
        this.Success = Success;
    }
        
    
    public static ParseResult<T> Ok(T? value, bool committed = false)
    {
        return new ParseResult<T>(
            Success: true,
            Result: value,
            Error: null,
            Committed: committed
        );
    }

    public static ParseResult<T> Miss()
    {
        return new ParseResult<T>(
            Success: false,
            Result: default,
            Error: null,
            Committed: false
        );
    }

    public static ParseResult<T> Fail(ParseError error, bool committed)
    {
        return new ParseResult<T>(
            Success: false,
            Result: default,
            Error: error,
            Committed: committed
        );
    }
}