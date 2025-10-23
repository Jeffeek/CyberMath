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
    public void IsPalindrome_ThrowsOnNull()
    {
        string? nullString = null;
        Assert.Throws<ArgumentNullException>(() => nullString.IsPalindrome());
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

    #region WordsFrequency Tests

    [Fact]
    public void WordsFrequency_ReturnsCorrectFrequencies()
    {
        const string input = "hello world hello test world world";
        var result = input.WordsFrequency();

        Assert.Equal(2, result['h']);  // 'hello' appears twice
        Assert.Equal(3, result['w']);  // 'world' appears three times  
        Assert.Equal(1, result['t']);  // 'test' appears once
    }

    [Fact]
    public void WordsFrequency_HandlesEmptyString()
    {
        var result = "".WordsFrequency();
        Assert.Empty(result);
    }

    [Fact]
    public void WordsFrequency_ThrowsOnNull()
    {
        string? nullString = null;
        Assert.Throws<ArgumentNullException>(() => nullString.WordsFrequency());
    }

    #endregion
}