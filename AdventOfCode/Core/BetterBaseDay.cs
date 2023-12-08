using System.IO;
using AoCHelper;

namespace AdventOfCode.Core;

public abstract class BetterBaseDay : BaseDay
{
    public bool IsTest { get; set; } = false;
    public string TestInput { get; set; }
    public string InputData => GetData();

    private string GetData()
    {
        return IsTest ? TestInput : File.ReadAllText(InputFilePath);
    }
}