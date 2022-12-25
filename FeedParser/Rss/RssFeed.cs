namespace FeedParser.Rss;

public class RssFeed
{
    public RssVersion Version { get; set; }
    public RssChannel Channel { get; set; } = null!;
}

public class RssChannel
{
    public string Title { get; set; } = null!;
    public string Link { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? Language { get; set; }
    public RssImage? Image { get; set; }
    public string? CopyRight { get; set; }
    public string? ManagingEditor { get; set; }
    public string? WebMaster { get; set; }
    public string? Rating { get; set; }
    public DateTime? PubDate { get; set; }
    public DateTime? LastBuildDate { get; set; }
    public string? Docs { get; set; }
    public List<DayOfWeek>? SkipDays { get; set; }
    public List<int>? SkipHours { get; set; }
    public List<RssItem>? Items { get; set; }
    public RssTextInput? TextInput { get; set; }
    public RssCloud? Cloud { get; set; }
    public List<RssCategory>? Categories { get; set; }
    public string? Generator { get; set; }
    public int? Ttl { get; set; }
}

public class RssImage
{
    public string Url { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Link { get; set; } = null!;
    public int Width { get; set; } = 88;
    public int Height { get; set; } = 31;
    public string? Description { get; set; }
}

public class RssItem
{
    public string? Title { get; set; }
    public string? Link { get; set; }
    public string? Description { get; set; }
    public RssSource? Source { get; set; }
    public RssEnclosure? Enclosure { get; set; }
    public List<RssCategory>? Categories { get; set; }
    public string? Author { get; set; }
    public string? Comments { get; set; }
    public RssGuid? Guid { get; set; }
    public DateTime? PubDate { get; set; }
}

public class RssTextInput
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Link { get; set; } = null!;
}

public class RssSource
{
    public string Value { get; set; } = null!;
    public string Url { get; set; } = null!;
}

public class RssEnclosure
{
    public string Url { get; set; } = null!;
    public int Length { get; set; }
    public string Type { get; set; } = null!;
}

public class RssCategory
{
    public string Value { get; set; } = null!;
    public string? Domain { get; set; }
}

public class RssCloud
{
    public string Domain { get; set; } = null!;
    public int Port { get; set; }
    public string Path { get; set; } = null!;
    public string RegisterProcedure { get; set; } = null!;
    public string Protocol { get; set; } = null!;
}

public class RssGuid
{
    public string Value { get; set; } = null!;

    public bool IsPermaLink { get; set; } = true;
}