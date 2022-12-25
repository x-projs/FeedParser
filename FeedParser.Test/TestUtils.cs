using System.Runtime.CompilerServices;
using System.Text;

namespace FeedParser.Test;

public static class TestUtils
{
    public static string LoadTestData(string testDataFileName, [CallerFilePath] string callerFilePath = null!)
    {
        var path = Path.GetDirectoryName(callerFilePath);
        path = Path.Combine(path!, "TestData", testDataFileName);
        return File.ReadAllText(path, Encoding.UTF8);
    }
}
