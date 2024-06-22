using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using System.Buffers;

namespace SearchValuesExample;

[Config(typeof(StyleConfig))]
[HideColumns(Column.RatioSD)]
public class SearchValuesBenchMarks
{
    private static readonly char[] vowelsCharArr = ['a', 'e', 'i', 'o', 'u'];
    private static readonly SearchValues<char> vowelsSearchValues = SearchValues.Create("aeiou");

    [Params("aeiouqazwsxedcrfv")]
    public string SourecText { get; set; }

    [Benchmark(Baseline = true)]
    public bool IsAllVowels_CharArray()
    {
        return SourecText.AsSpan().IndexOfAnyExcept(vowelsCharArr) == -1;
    }

    [Benchmark]
    public bool IsAllVowels_SearchValues()
    {
        return SourecText.AsSpan().IndexOfAnyExcept(vowelsSearchValues) == -1;
    }  
}

public class StyleConfig : ManualConfig
{
    public StyleConfig()
    {
        SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
    }
}
