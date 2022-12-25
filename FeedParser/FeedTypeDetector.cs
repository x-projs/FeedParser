using System.Xml;

namespace FeedParser;

public sealed class FeedTypeDetector
{
    /// <summary>
    /// Detect the feed type.
    /// </summary>
    public static FeedType DetectFeedType(string content)
    {
        try
        {
            var xml = new XmlDocument();
            xml.LoadXml(content);
            if (xml.DocumentElement?.Name == "rss")
            {
                return FeedType.Rss;
            }
        }
        catch
        {
        }
        return FeedType.Unknown;
    }
}
