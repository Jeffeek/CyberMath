#region Using namespaces

using System;
using CyberMath.Extensions;
using Xunit;

#endregion

namespace CyberMath.xUnit.Tests.Extensions;

public class StringExtensionTests
{
    #region ToAlternatingCase Tests

    [Theory]
    [InlineData("", "")]
    [InlineData("HELLO", "hello")]
    [InlineData("hello", "HELLO")]
    [InlineData("HeLLo WoRLD", "hEllO wOrld")]
    [InlineData("12345", "12345")]
    [InlineData("Hello123World", "hELLO123wORLD")]
    [InlineData("ABC!@#def", "abc!@#DEF")]
    [InlineData("Test", "tEST")]
    [InlineData("ABC", "abc")]
    public void ToAlternatingCase_ReturnsExpectedResult(string input, string expected)
    {
        Assert.Equal(expected, input.ToAlternatingCase());
    }

    [Fact]
    public void ToAlternatingCase_ThrowsOnNull()
    {
        string? nullString = null;
        Assert.Throws<ArgumentNullException>(() => nullString.ToAlternatingCase());
    }

    #endregion

    #region IsPalindrome Tests

    [Theory]
    [InlineData("", true)]
    [InlineData("a", true)]
    [InlineData("aa", true)]
    [InlineData("aba", true)]
    [InlineData("abba", true)]
    [InlineData("racecar", true)]
    [InlineData("A man a plan a canal Panama", false)]
    [InlineData("race a car", false)]
    [InlineData("hello", false)]
    [InlineData("Aa", false)] 
    public void IsPalindrome_ReturnsExpectedResult(string input, bool expected)
    {
        Assert.Equal(expected, input.IsPalindrome());
    }

    [Fact]
    public void IsPalindrome_ReturnsFalseForNull()
    {
        string? nullString = null;
        Assert.False(nullString.IsPalindrome());
    }

    #endregion

    #region IsAnagramOf Tests

    [Theory]
    [InlineData("listen", "silent", true)]
    [InlineData("evil", "vile", true)]
    [InlineData("a gentleman", "elegant man", true)]
    [InlineData("abc", "bca", true)]
    [InlineData("abc", "def", false)]
    [InlineData("abc", "abcd", false)]
    [InlineData("", "", true)]
    [InlineData("aaa", "aab", false)]
    public void IsAnagramOf_ReturnsExpectedResult(string first, string second, bool expected)
    {
        Assert.Equal(expected, first.IsAnagramOf(second));
    }

    [Fact]
    public void IsAnagramOf_HandlesNulls()
    {
        string? nullString = null;
        Assert.False(nullString.IsAnagramOf("test"));
        Assert.False("test".IsAnagramOf(null));
        Assert.False(nullString.IsAnagramOf(null));
    }

    #endregion

    #region CharacterFrequency Tests

    [Fact]
    public void CharacterFrequency_ReturnsCorrectFrequencies()
    {
        const string input = "hello world hello test world world";
        var result = input.CharacterFrequency();

        // CharacterFrequency counts character frequency in the string
        // String: "hello world hello test world world"
        Assert.Equal(2, result['h']);  // 'h' appears 2 times (hello x2)
        Assert.Equal(3, result['e']);  // 'e' appears 3 times (hello x2 + test)
        Assert.Equal(7, result['l']);  // 'l' appears 7 times (hello x2 has 4, world x3 has 3)
        Assert.Equal(5, result['o']);  // 'o' appears 5 times (hello x2 + world x3)
        Assert.Equal(3, result['w']);  // 'w' appears 3 times (world x3)
        Assert.Equal(2, result['t']);  // 't' appears 2 times (test)
        Assert.Equal(5, result[' ']);  // 5 spaces
    }

    [Fact]
    public void CharacterFrequency_HandlesEmptyString()
    {
        var result = "".CharacterFrequency();
        Assert.Empty(result);
    }

    [Fact]
    public void CharacterFrequency_ThrowsOnNull()
    {
        string? nullString = null;
        Assert.Throws<ArgumentNullException>(() => nullString.CharacterFrequency());
    }

    [Fact]
    public void WordsFrequency_IsObsoleteAlias()
    {
        // Test that the obsolete WordsFrequency still works as an alias
        const string input = "test";
        #pragma warning disable CS0618 // Type or member is obsolete
        var result = input.WordsFrequency();
        #pragma warning restore CS0618
        Assert.Equal(3, result.Count); // t, e, s (3 unique chars - 't' appears twice but counted once)
    }

    #endregion
}