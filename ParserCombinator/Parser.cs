namespace ParserCombinator;

public delegate ParseResult<T> Parser<T>(ParserInput input);


public static class ParserExtensions
{
    public static Parser<T> Commit<T>(this Parser<T> parser)
    {
        return input =>
        {
            var result = parser(input);

            return result with { Committed = true };
        };
    }   
    
    public static ParseResult<T> Parse<T>(this Parser<T> parser, ParserInput input)
    {
        var result = 
            parser(input);

        return result;
    }

    public static Parser<T> Or<T>(this Parser<T> first, Parser<T> second)
    {
        return input =>
        {
            // Save current index in the token list
            var checkpoint = input.Position;

            var result = first(input);

            if (result.Success || result.Committed)
                return result;

            // Restore the token index 
            input.Position = checkpoint;

            return second(input);
        };
    }

    public static Parser<Optional<T>> Maybe<T>(this Parser<T> parser)
    {
        return input =>
        {
            // Save current index in the token list
            var checkpoint = input.Position;

            var result = parser(input);

            if (result.Success)
                return ParseResult<Optional<T>>.Ok(Optional<T>.Of(result.Result!), result.Committed);

            if (result.Committed)
                return ParseResult<Optional<T>>.Fail(result.Error!, committed: true);
            
            // Restore the token index 
            input.Position = checkpoint;

            return ParseResult<Optional<T>>.Miss();
        };
    }
    
    
    
}
