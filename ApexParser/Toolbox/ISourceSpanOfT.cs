using System;
using Sprache;

namespace ApexParser.Toolbox
{
    public interface ISourceSpan<T>
    {
        Position Start { get; }

        Position End { get; }

        int Length { get; }

        T Value { get; }
    }
}