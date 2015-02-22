ParsecSharp
===========
ParsecSharp is my take on Parsec, Haskell's monadic parser combinator framework.
It lets you to create complex parsers in C# by combining smaller parsers. This
allows you to write parsers in a declarative fashion, not dissimilar to BNF
(Backus-Naur Form) notation.



Usage
=====
All parsers are of the type `IParser<T>`, where `T` is the type of the resulting 
value. Instances of this type are created by combining the basic building blocks 
such as character parsers.

The `Parse(..)` method attempts to parse the given input, and returns an 
`Either` instance. This type can have two states: either parsing was a Success, 
and it contains the parsed value, or there was an Error, in which case it will 
contain an error message.

```C#
IParser<char> parser = Chars.Any();
Either<char, ParseError> result = parser.Parse("x"); // Success 'x'
Either<char, ParseError> result = parser.Parse(""); // Error "Unexpected end of input"
```

Basic building blocks
---------------------
The `Chars` class defines many of the basic building blocks. Most of them parse
only a single character, for example:

```C#
IParser<char> parser = Chars.Char('a');    // Parses only 'a'.
IParser<char> parser = Chars.Digit();      // Parses a single digit character.
IParser<char> parser = Chars.OneOf("xyz"); // Parses either 'x', 'y', or 'z'.
```

Parsers for numeric types such as `int` or `double` are defined in the `Numeric`
class:

```C#
IParser<int> parser = Numeric.Int();
var result = parser.Parse("-42"); // Result: -42
```

Combining parsers
-----------------
These small building blocks can be combined into more complex parsers. A very
common way to combine parsers is by using the Or combinator. It attempts to run
the first parser, and if it fails, runs the second parser.

```C#
IParser<char> parser = Chars.Space() | Chars.Digit() | Chars.Char('x');
var result = parser.Parse("x"); // result: 'x'
```

Parsers can be repeated using methods such as `Many` and `SeparatedBy`. These
methods transform any `IParser<T>` into a `IParser<IEnumerable<T>>`. Or in the
somewhat special case of a `IParser<char>` into a `IParser<string>`:

```C#
IParser<string> parser = Chars.Letter().Many();
var result = parser.Parse("xyz123"); // result: "xyz"
```


Linq query comprehension
------------------------
Parsers can also be combined using linq query comprehension syntax. Even though
it may seem a little bit strange at first, it becomes a very powerful construct 
once you're used to it.

```C#
IParser<string> parser = from open in Chars.Char('{')
                         from value in Chars.Not('}').Many()
                         from close in Chars.Char('}')
                         select value;
var result = parser.Parse("{test}"); // result: "test"
```

CSV example
===========
Creating a parser for a simple grammar like CSV using ParsecSharp is very 
straightforward. You can start by making a parser for the smallest entity to be 
parsed: a single value. Using the value parser, you can create a parser that 
parses values separated by comma's. And finally, using the line parser, you can 
create a parser that parses multiple lines:

```C#
// A parser for a single value: read all chars up to the next comma or line break.
var csvValue = Chars.NoneOf(',', '\r', 'n').Many();

// A parser for a single line: repeatedly read values separated by a comma.
var csvLine = csvValue.SeparatedBy(Chars.Char(','));

// The complete csv parser: repeatedly read lines separated by a line break.
var csvParser = csvLine.SeparatedBy(Chars.EndOfLine());
```

You could even write the parser as a single statement, although I personally 
think that doing so makes it a little less readable.

```C#
var csvParser = Chars.NoneOf(',', '\r', '\n')
                     .Many()
                     .SeparatedBy(Chars.Char(','))
                     .SeparatedBy(Chars.EndOfLine());
```

This parser doesn't include support for quoted values, which would make it a
little bit more complicated. For a complete CSV parser, see the CSV example 
project.

Example projects
================
The source code contains a number of example parser projects:
* **BNF**: A Backus-Naur Form grammar parser.
* **CSV**: A simple CSV parser
* **JSON**: A JSON parser
* **Password Generator**: A simple Regular Expression parser that creates a type
  that generates a string matching the parsed Regex.