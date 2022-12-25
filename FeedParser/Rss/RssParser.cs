using System.Xml;

namespace FeedParser.Rss;

public class RssParser
{
    /// <summary>
    /// Parse rss feed content.
    /// </summary>
    /// <exception cref="BadFormatException">If hit any error, will throw this exception.</exception>
    public RssFeed Parse(string content, bool noItem = false)
    {
        try
        {
            var xml = new XmlDocument();
            xml.LoadXml(content);
            if (xml.DocumentElement?.Name != "rss")
            {
                throw new FormatException("Not valid rss feed.");
            }

            var feed = new RssFeed();

            // Parse version.
            feed.Version = ParseVersion(xml.DocumentElement);

            // Parse channel.
            var channelNodes = xml.DocumentElement.SelectNodes("channel");
            if (channelNodes == null || channelNodes.Count == 0)
            {
                throw new FormatException("No channel element.");
            }
            else if (channelNodes.Count > 1)
            {
                throw new FormatException("Too many channel elements.");
            }
            feed.Channel = ParseChannel(channelNodes[0]!, noItem, feed.Version);

            // Done.
            return feed;
        }
        catch (Exception ex) when (!(ex is FormatException))
        {
            throw new FormatException("Parse failed.", ex);
        }
    }

    private RssVersion ParseVersion(XmlNode node)
    {
        var version = node.Attributes?["version"]?.InnerText;
        if (string.IsNullOrEmpty(version))
        {
            throw new FormatException("Rss version is missing.");
        }

        switch (version)
        {
            case "0.91":
                return RssVersion.Rss_0_91;

            case "0.92":
                return RssVersion.Rss_0_92;

            case "2.0":
                return RssVersion.Rss_2_0;

            default:
                throw new FormatException("Unknown rss version.");
        }
    }

    private RssChannel ParseChannel(XmlNode node, bool noItem, RssVersion version)
    {
        var channel = new RssChannel();

        var title = node["title"]?.InnerText;
        if (title == null)
        {
            throw new FormatException("No title element of channel.");
        }
        channel.Title = title;

        var link = node["link"]?.InnerText;
        if (link == null)
        {
            throw new FormatException("No link element of channel.");
        }
        channel.Link = link;

        var description = node["description"]?.InnerText;
        if (description == null)
        {
            throw new FormatException("No description of channel.");
        }
        channel.Description = description;

        var language = node["language"]?.InnerText;
        if (language == null && version == RssVersion.Rss_0_91)
        {
            throw new FormatException("No language of channel.");
        }
        channel.Language = language;

        var image = node["image"];
        if (image == null)
        {
            // In https://www.rssboard.org/rss-0-9-1, image is required item,
            // in https://www.rssboard.org/rss-0-9-2, it doesn't mention image is
            // required or optional. But in its example file, there is no image,
            // so we assuem it is optional from 0.92.
            if (version == RssVersion.Rss_0_91)
            {
                throw new FormatException("No image of channel.");
            }
        }
        else
        {
            channel.Image = ParseImage(image);
        }

        channel.CopyRight = node["copyright"]?.InnerText;

        channel.ManagingEditor = node["managingEditor"]?.InnerText;

        channel.WebMaster = node["webMaster"]?.InnerText;

        channel.Rating = node["rating"]?.InnerText;

        var pubDate = node["pubDate"];
        if (pubDate != null)
        {
            channel.PubDate = ParsePubDate(pubDate);
        }

        var lastBuildDate = node["lastBuildDate"];
        if (lastBuildDate != null)
        {
            channel.LastBuildDate = ParseLastBuildDate(lastBuildDate);
        }

        channel.Docs = node["docs"]?.InnerText;

        var textInput = node["textInput"];
        if (textInput != null)
        {
            channel.TextInput = ParseTextInput(textInput);
        }

        var skipDays = node["skipDays"];
        if (skipDays != null)
        {
            channel.SkipDays = ParseSkipDays(skipDays);
        }

        var skipHours = node["skipHours"];
        if (skipHours != null)
        {
            channel.SkipHours = ParseSkipHours(skipHours);
        }

        if (version != RssVersion.Rss_0_91)
        {
            var cloud = node["cloud"];
            if (cloud != null)
            {
                channel.Cloud = ParseCloud(cloud);
            }
        }

        if (version == RssVersion.Rss_2_0)
        {
            var categories = node.SelectNodes("category");
            if (categories?.Count > 0)
            {
                channel.Categories = new List<RssCategory>();
                foreach (XmlNode category in categories)
                {
                    channel.Categories.Add(ParseCategory(category));
                }
            }

            channel.Generator = node["generator"]?.InnerText;

            var ttl = node["ttl"];
            if (ttl != null)
            {
                int value;
                if (!int.TryParse(ttl.InnerText, out value))
                {
                    throw new FormatException("Invalid ttl of channel.");
                }
                channel.Ttl = value;
            }
        }

        if (!noItem)
        {
            var items = node.SelectNodes("item");
            if (items != null)
            {
                channel.Items = new List<RssItem>();
                foreach (XmlNode item in items)
                {
                    channel.Items.Add(ParseItem(item, version));
                }
            }
        }

        

        return channel;
    }

    private RssImage ParseImage(XmlNode node)
    {
        var img = new RssImage();

        var url = node["url"]?.InnerText;
        if (url == null)
        {
            throw new FormatException("No url element of image.");
        }
        img.Url = url;

        var title = node["title"]?.InnerText;
        if (title == null)
        {
            throw new FormatException("No title element of image.");
        }
        img.Title = title;

        var link = node["link"]?.InnerText;
        if (link == null)
        {
            throw new FormatException("No link element of image.");
        }
        img.Link = link;

        var width = node["width"];
        if (width != null)
        {
            int value;
            if (!int.TryParse(width.InnerText, out value))
            {
                throw new FormatException("Invalid width of image.");
            }
            img.Width = value;
        }

        var height = node["height"];
        if (height != null)
        {
            int value;
            if (!int.TryParse(height.InnerText, out value))
            {
                throw new FormatException("Invalid height of image.");
            }
            img.Height = value;
        }

        img.Description = node["description"]?.InnerText;

        return img;
    }

    private DateTime ParsePubDate(XmlNode node)
    {
        var dateTime = Utils.TryParseToUtcDateTime(node.InnerText);
        if (dateTime == null)
        {
            throw new FormatException("Invalid value of pubDate.");
        }
        return dateTime.Value;
    }

    private DateTime ParseLastBuildDate(XmlNode node)
    {
        var lastBuildDate = Utils.TryParseToUtcDateTime(node.InnerText);
        if (lastBuildDate == null)
        {
            throw new FormatException("Invalid value of lastBuildDate.");
        }
        return lastBuildDate.Value;
    }

    private RssTextInput ParseTextInput(XmlNode node)
    {
        var textInput = new RssTextInput();

        var title = node["title"]?.InnerText;
        if (title == null)
        {
            throw new FormatException("No title of textInput.");
        }
        textInput.Title = title;

        var description = node["description"]?.InnerText;
        if (description == null)
        {
            throw new FormatException("No description of textInput.");
        }
        textInput.Description = description;

        var name = node["name"]?.InnerText;
        if (name == null)
        {
            throw new FormatException("No name of textInput.");
        }
        textInput.Name = name;

        var link = node["link"]?.InnerText;
        if (link == null)
        {
            throw new FormatException("No link of textInput.");
        }
        textInput.Link = link;

        return textInput;
    }

    private List<DayOfWeek> ParseSkipDays(XmlNode node)
    {
        var skipDays = new List<DayOfWeek>();
        var days = node.SelectNodes("day");
        if (days != null)
        {
            foreach (XmlNode day in days)
            {
                DayOfWeek dayOfWeek;
                if (Enum.TryParse<DayOfWeek>(day.InnerText, out dayOfWeek))
                {
                    skipDays.Add(dayOfWeek);
                }
                else
                {
                    throw new FormatException("Invalid value of skipDays.");
                }
            }
        }
        return skipDays;
    }

    private List<int> ParseSkipHours(XmlNode node)
    {
        var skipHours = new List<int>();
        var hours = node.SelectNodes("hour");
        if (hours != null)
        {
            foreach (XmlNode hour in hours)
            {
                int value;
                if (int.TryParse(hour.InnerText, out value))
                {
                    skipHours.Add(value);
                }
                else
                {
                    throw new FormatException("Invalid value of skipHours.");
                }
            }
        }
        return skipHours;
    }

    private RssCloud ParseCloud(XmlNode node)
    {
        var cloud = new RssCloud();

        var domain = node.Attributes?["domain"]?.InnerText;
        if (domain == null)
        {
            throw new FormatException("No domain of cloud.");
        }
        cloud.Domain = domain;

        var port = node.Attributes?["port"]?.InnerText;
        if (port == null)
        {
            throw new FormatException("No port of cloud.");
        }
        else
        {
            int value;
            if (!int.TryParse(port, out value))
            {
                throw new FormatException("Invalid port of cloud.");
            }
            else
            {
                cloud.Port = value;
            }
        }

        var path = node.Attributes?["path"]?.InnerText;
        if (path == null)
        {
            throw new FormatException("No path of cloud.");
        }
        cloud.Path = path;

        var registerProcedure = node.Attributes?["registerProcedure"]?.InnerText;
        if (registerProcedure == null)
        {
            throw new FormatException("No registerProcedure of cloud.");
        }
        cloud.RegisterProcedure = registerProcedure;

        var protocol = node.Attributes?["protocol"]?.InnerText;
        if (protocol == null)
        {
            throw new FormatException("No protocol of cloud.");
        }
        cloud.Protocol = protocol;

        return cloud;
    }

    private RssItem ParseItem(XmlNode node, RssVersion version)
    {
        var item = new RssItem();

        var title = node["title"]?.InnerText;
        if (title == null && version == RssVersion.Rss_0_91)
        {
            throw new FormatException("No title of item.");
        }
        item.Title = title;

        var link = node["link"]?.InnerText;
        if (link == null && version == RssVersion.Rss_0_91)
        {
            throw new FormatException("No link of item.");
        }
        item.Link = link;

        item.Description = node["description"]?.InnerText;

        if (version == RssVersion.Rss_2_0 && item.Title == null && item.Description == null)
        {
            throw new FormatException("At least one of title of item or description of item must be present.");
        }

        if (version != RssVersion.Rss_0_91)
        {
            var source = node["source"];
            if (source != null)
            {
                item.Source = ParseSource(source);
            }

            var enclosure = node["enclosure"];
            if (enclosure != null)
            {
                item.Enclosure = ParseEnclosure(enclosure);
            }

            var categories = node.SelectNodes("category");
            if (categories?.Count > 0)
            {
                item.Categories = new List<RssCategory>();
                foreach (XmlNode category in categories)
                {
                    item.Categories.Add(ParseCategory(category));
                }
            }
        }

        if (version == RssVersion.Rss_2_0)
        {
            item.Author = node["author"]?.InnerText;

            item.Comments = node["comments"]?.InnerText;

            var guid = node["guid"];
            if (guid != null)
            {
                item.Guid = ParseGuid(guid);
            }

            var pubDate = node["pubDate"];
            if (pubDate != null)
            {
                item.PubDate = ParsePubDate(pubDate);
            }
        }
        return item;
    }

    private RssSource ParseSource(XmlNode node)
    {
        var source = new RssSource();

        source.Value = node.InnerText;

        var url = node.Attributes?["url"]?.InnerText;
        if (url == null)
        {
            throw new FormatException("No url of source.");
        }
        source.Url = url;

        return source;
    }

    private RssEnclosure ParseEnclosure(XmlNode node)
    {
        var enclosure = new RssEnclosure();

        var url = node.Attributes?["url"]?.InnerText;
        if (url == null)
        {
            throw new FormatException("No url of enclosure.");
        }
        enclosure.Url = url;

        var length = node.Attributes?["length"]?.InnerText;
        if (length == null)
        {
            throw new FormatException("No length of enclosure.");
        }
        else
        {
            int value;
            if (!int.TryParse(length, out value))
            {
                throw new FormatException("Invalid length of enclosure.");
            }
            enclosure.Length = value;
        }

        var type = node.Attributes?["type"]?.InnerText;
        if (type == null)
        {
            throw new FormatException("No type of enclosure.");
        }
        enclosure.Type = type;

        return enclosure;
    }

    private RssCategory ParseCategory(XmlNode node)
    {
        var category = new RssCategory();

        category.Value = node.InnerText;

        var domain = node.Attributes?["domain"]?.InnerText;
        if (domain != null)
        {
            category.Domain = domain;
        }

        return category;
    }

    private RssGuid ParseGuid(XmlNode node)
    {
        var guid = new RssGuid();

        guid.Value = node.InnerText;

        var isPermaLink = node.Attributes?["isPermaLink"];
        if (isPermaLink != null)
        {
            bool value;
            if (!bool.TryParse(isPermaLink.InnerText, out value))
            {
                throw new FormatException("Invalid isPermaLink of guid.");
            }
            else
            {
                guid.IsPermaLink = value;
            }
        }

        return guid;
    }
}
