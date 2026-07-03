namespace ParserCombinator;

public delegate ParseResult<T> Parser<T>(ParserInput input);
