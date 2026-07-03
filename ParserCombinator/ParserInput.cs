namespace ParserCombinator;

public class SyntaxToken
{
    
}
public class ParserInput
{
    private readonly IReadOnlyList<SyntaxToken> _tokens;
    
    public int Position { get; set; }

    public ParserInput(IReadOnlyList<SyntaxToken> tokens)
    {
        _tokens = tokens;
    }
    
    public SyntaxToken Current => 
        Position >= _tokens.Count ?
            _tokens[^1] :
            _tokens[Position];

    public SyntaxToken Peek(int offset)
    {
        var index = Position + offset;
        return index >= _tokens.Count ?
            _tokens[^1] :
            _tokens[index];
    }
    
    public SyntaxToken NextToken()
    {
        var current = Current;
        Position++;
        return current;
    }
    
    
    
}