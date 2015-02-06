ParsecSharp
===========
ParsecSharp is my take on Parsec, Haskell's monadic parser combinator framework.
It lets you to create complex parsers in C# by combining smaller parsers. This
allows you to write parsers in a declarative fashion, not dissimilar to BNF
(Backus-Naur Form) notation.



Usage
=====
The `Parser<T>` type is the type used for all parsers. Instances of this type 
are created by combining the basic building blocks such as character parsers.

The `Parse(string)` method attempts to parse the given input, and returns an 
`Either` instance. This type can have two states: either parsing was a Success, 
and it contains the parsed value, or there was an Error, in which case it will 
contain an error message.

```C#
Parser<char> parser = Chars.Any();
Either<char, ParseError> result = parser.Parse("x"); // Success 'x'
Either<char, ParseError> result = parser.Parse(""); // Error "Unexpected end of input"
```

Basic building blocks
---------------------
The `Chars` class defines many of the basic building blocks. Most of them parse
only a single character, for example:

```C#
Parser<char> parser = Chars.Char('a');    // Parses only 'a'.
Parser<char> parser = Chars.Digit();      // Parses a single digit character.
Parser<char> parser = Chars.OneOf("xyz"); // Parses either 'x', 'y', or 'z'.
```

Parsers for numeric types such as `int` or `double` are defined in the `Numeric`
class:

```C#
Parser<int> parser = Numeric.Int();
var result = parser.Parse("-42"); // Result: -42
```

Combining parsers
-----------------
These small building blocks can be combined into more complex parsers. A very
common way to combine parsers is by using the Or combinator. It attempts to run
the first parser, and if it fails, runs the second parser.

```C#
Parser<char> parser = Chars.Space() | Chars.Digit() | Chars.Char('x');
var result = parser.Parse("x"); // result: 'x'
```

Parsers can be repeated using methods such as `Many` and `SeparatedBy`. These
methods transform any `Parser<T>` into a `Parser<IEnumerable<T>>`. Or in the
somewhat special case of a `Parser<char>` into a `Parser<string>`:

```C#
Parser<string> parser = Chars.Letter().Many();
var result = parser.Parse("xyz123"); // result: "xyz"
```


Linq query comprehension
------------------------
Parsers can also be combined using linq query comprehension syntax. Even though
it may seem a little bit strange at first, it becomes a very powerful construct 
once you're used to it.

```C#
Parser<string> parser = from open in Chars.Char('{')
                        from value in Chars.Not('}').Many()
                        from close in Chars.Char('}')
                        select value;
var result = parser.Parse("{test}"); // result: "test"
```


Example projects
================
The source code contains a number of example parser projects:
* **BNF**: A Backus-Naur Form grammar parser.
* **JSON**: A JSON parser
* **Password Generator**: A simple Regular Expression parser that creates a type
  that generates a string matching the parsed Regex.
