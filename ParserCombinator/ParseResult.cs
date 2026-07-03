namespace ParserCombinator;

public class ParseResult<T>
{
    bool Success { get;  }
    T? Result { get;  }
    ParseError? Error { get;  }
    bool Committed { get;  }
}