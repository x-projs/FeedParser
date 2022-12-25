using FeedParser.Rss;
using FeedParser.Test;

namespace FeedParser;

public class RssParserTest
{
    [Fact]
    public void ParseValidRss0_91Content1()
    {
        var content = TestUtils.LoadTestData("sample-rss-091.xml");
        var feedType = FeedTypeDetector.DetectFeedType(content);
        Assert.Equal(FeedType.Rss, feedType);

        var parser = new RssParser();

        // Parse without item.
        var feed = parser.Parse(content, noItem: true);
        Assert.Equal(RssVersion.Rss_0_91, feed.Version);
        Assert.Equal("WriteTheWeb", feed.Channel.Title);
        Assert.Equal("http://writetheweb.com", feed.Channel.Link);
        Assert.Equal("News for web users that write back", feed.Channel.Description);
        Assert.Equal("en-us", feed.Channel.Language);
        Assert.Equal("Copyright 2000, WriteTheWeb team.", feed.Channel.CopyRight);
        Assert.Equal("editor@writetheweb.com", feed.Channel.ManagingEditor);
        Assert.Equal("webmaster@writetheweb.com", feed.Channel.WebMaster);
        Assert.Equal("WriteTheWeb", feed.Channel.Image!.Title);
        Assert.Equal("http://writetheweb.com/images/mynetscape88.gif", feed.Channel.Image.Url);
        Assert.Equal("http://writetheweb.com", feed.Channel.Image.Link);
        Assert.Equal(88, feed.Channel.Image.Width);
        Assert.Equal(31, feed.Channel.Image.Height);
        Assert.Equal("News for web users that write back", feed.Channel.Image.Description);
        Assert.Null(feed.Channel.Docs);
        Assert.Null(feed.Channel.Items);
        Assert.Null(feed.Channel.LastBuildDate);
        Assert.Null(feed.Channel.PubDate);
        Assert.Null(feed.Channel.Rating);
        Assert.Null(feed.Channel.SkipDays);
        Assert.Null(feed.Channel.SkipHours);
        Assert.Null(feed.Channel.TextInput);

        // Parse with items.
        feed = parser.Parse(content, noItem: false);
        Assert.Equal(RssVersion.Rss_0_91, feed.Version);
        Assert.Equal("WriteTheWeb", feed.Channel.Title);
        Assert.Equal("http://writetheweb.com", feed.Channel.Link);
        Assert.Equal("News for web users that write back", feed.Channel.Description);
        Assert.Equal("en-us", feed.Channel.Language);
        Assert.Equal("Copyright 2000, WriteTheWeb team.", feed.Channel.CopyRight);
        Assert.Equal("editor@writetheweb.com", feed.Channel.ManagingEditor);
        Assert.Equal("webmaster@writetheweb.com", feed.Channel.WebMaster);
        Assert.Equal("WriteTheWeb", feed.Channel.Image!.Title);
        Assert.Equal("http://writetheweb.com/images/mynetscape88.gif", feed.Channel.Image.Url);
        Assert.Equal("http://writetheweb.com", feed.Channel.Image.Link);
        Assert.Equal(88, feed.Channel.Image.Width);
        Assert.Equal(31, feed.Channel.Image.Height);
        Assert.Equal("News for web users that write back", feed.Channel.Image.Description);
        Assert.Null(feed.Channel.Docs);
        Assert.Null(feed.Channel.LastBuildDate);
        Assert.Null(feed.Channel.PubDate);
        Assert.Null(feed.Channel.Rating);
        Assert.Null(feed.Channel.SkipDays);
        Assert.Null(feed.Channel.SkipHours);
        Assert.Null(feed.Channel.TextInput);
        Assert.Equal(6, feed.Channel.Items!.Count);

        var item = feed.Channel.Items[0];
        Assert.Equal("Giving the world a pluggable Gnutella", item.Title);
        Assert.Equal("http://writetheweb.com/read.php?item=24", item.Link);
        Assert.Equal("WorldOS is a framework on which to build programs that work like Freenet or Gnutella -allowing distributed applications using peer-to-peer routing.", item.Description);

        item = feed.Channel.Items[1];
        Assert.Equal("Syndication discussions hot up", item.Title);
        Assert.Equal("http://writetheweb.com/read.php?item=23", item.Link);
        Assert.Equal("After a period of dormancy, the Syndication mailing list has become active again, with contributions from leaders in traditional media and Web syndication.", item.Description);

        item = feed.Channel.Items[2];
        Assert.Equal("Personal web server integrates file sharing and messaging", item.Title);
        Assert.Equal("http://writetheweb.com/read.php?item=22", item.Link);
        Assert.Equal("The Magi Project is an innovative project to create a combined personal web server and messaging system that enables the sharing and synchronization of information across desktop, laptop and palmtop devices.", item.Description);

        item = feed.Channel.Items[3];
        Assert.Equal("Syndication and Metadata", item.Title);
        Assert.Equal("http://writetheweb.com/read.php?item=21", item.Link);
        Assert.Equal("RSS is probably the best known metadata format around. RDF is probably one of the least understood. In this essay, published on my O'Reilly Network weblog, I argue that the next generation of RSS should be based on RDF.", item.Description);

        item = feed.Channel.Items[4];
        Assert.Equal("UK bloggers get organised", item.Title);
        Assert.Equal("http://writetheweb.com/read.php?item=20", item.Link);
        Assert.Equal("Looks like the weblogs scene is gathering pace beyond the shores of the US. There's now a UK-specific page on weblogs.com, and a mailing list at egroups.", item.Description);

        item = feed.Channel.Items[5];
        Assert.Equal("Yournamehere.com more important than anything", item.Title);
        Assert.Equal("http://writetheweb.com/read.php?item=19", item.Link);
        Assert.Equal("Whatever you're publishing on the web, your site name is the most valuable asset you have, according to Carl Steadman.", item.Description);
    }

    [Fact]
    public void ParseValidRss0_92Content1()
    {
        var content = TestUtils.LoadTestData("sample-rss-092.xml");
        var feedType = FeedTypeDetector.DetectFeedType(content);
        Assert.Equal(FeedType.Rss, feedType);

        var parser = new RssParser();

        // Parse without item.
        var feed = parser.Parse(content, noItem: true);
        Assert.Equal(RssVersion.Rss_0_92, feed.Version);
        Assert.Equal("Dave Winer: Grateful Dead", feed.Channel.Title);
        Assert.Equal("http://www.scripting.com/blog/categories/gratefulDead.html", feed.Channel.Link);
        Assert.Equal("A high-fidelity Grateful Dead song every day. This is where we're experimenting with enclosures on RSS news items that download when you're not using your computer. If it works (it will) it will be the end of the Click-And-Wait multimedia experience on the Internet. ", feed.Channel.Description);
        Assert.Equal(new DateTime(2001, 4, 13, 19, 23, 02), feed.Channel.LastBuildDate);
        Assert.Equal("http://backend.userland.com/rss092", feed.Channel.Docs);
        Assert.Equal("dave@userland.com (Dave Winer)", feed.Channel.ManagingEditor);
        Assert.Equal("dave@userland.com (Dave Winer)", feed.Channel.WebMaster);
        Assert.Equal("data.ourfavoritesongs.com", feed.Channel.Cloud!.Domain);
        Assert.Equal(80, feed.Channel.Cloud!.Port);
        Assert.Equal("/RPC2", feed.Channel.Cloud!.Path);
        Assert.Equal("ourFavoriteSongs.rssPleaseNotify", feed.Channel.Cloud!.RegisterProcedure);
        Assert.Equal("xml-rpc", feed.Channel.Cloud!.Protocol);
        Assert.Null(feed.Channel.Language);
        Assert.Null(feed.Channel.CopyRight);
        Assert.Null(feed.Channel.Image);
        Assert.Null(feed.Channel.Items);
        Assert.Null(feed.Channel.PubDate);
        Assert.Null(feed.Channel.Rating);
        Assert.Null(feed.Channel.SkipDays);
        Assert.Null(feed.Channel.SkipHours);
        Assert.Null(feed.Channel.TextInput);

        // Parse with item.
        feed = parser.Parse(content, noItem: false);
        Assert.Equal(RssVersion.Rss_0_92, feed.Version);
        Assert.Equal("Dave Winer: Grateful Dead", feed.Channel.Title);
        Assert.Equal("http://www.scripting.com/blog/categories/gratefulDead.html", feed.Channel.Link);
        Assert.Equal("A high-fidelity Grateful Dead song every day. This is where we're experimenting with enclosures on RSS news items that download when you're not using your computer. If it works (it will) it will be the end of the Click-And-Wait multimedia experience on the Internet. ", feed.Channel.Description);
        Assert.Equal(new DateTime(2001, 4, 13, 19, 23, 02), feed.Channel.LastBuildDate);
        Assert.Equal("http://backend.userland.com/rss092", feed.Channel.Docs);
        Assert.Equal("dave@userland.com (Dave Winer)", feed.Channel.ManagingEditor);
        Assert.Equal("dave@userland.com (Dave Winer)", feed.Channel.WebMaster);
        Assert.Equal("data.ourfavoritesongs.com", feed.Channel.Cloud!.Domain);
        Assert.Equal(80, feed.Channel.Cloud!.Port);
        Assert.Equal("/RPC2", feed.Channel.Cloud!.Path);
        Assert.Equal("ourFavoriteSongs.rssPleaseNotify", feed.Channel.Cloud!.RegisterProcedure);
        Assert.Equal("xml-rpc", feed.Channel.Cloud!.Protocol);
        Assert.Null(feed.Channel.Language);
        Assert.Null(feed.Channel.CopyRight);
        Assert.Null(feed.Channel.Image);
        Assert.Null(feed.Channel.PubDate);
        Assert.Null(feed.Channel.Rating);
        Assert.Null(feed.Channel.SkipDays);
        Assert.Null(feed.Channel.SkipHours);
        Assert.Null(feed.Channel.TextInput);
        Assert.Equal(22, feed.Channel.Items!.Count);

        var item = feed.Channel.Items[0];
        Assert.Null(item.Title);
        Assert.Null(item.Link);
        Assert.Equal("\r\n\t\t\t\tIt's been a few days since I added a song to the Grateful Dead channel. Now that there are all these new Radio users, many of whom are tuned into this channel (it's #16 on the hotlist of upstreaming Radio users, there's no way of knowing how many non-upstreaming users are subscribing, have to do something about this..). Anyway, tonight's song is a live version of Weather Report Suite from Dick's Picks Volume 7. It's wistful music. Of course a beautiful song, oft-quoted here on Scripting News. <i>A little change, the wind and rain.</i>\r\n\t\t\t", item.Description);
        Assert.Equal("http://www.scripting.com/mp3s/weatherReportDicksPicsVol7.mp3", item.Enclosure!.Url);
        Assert.Equal(6182912, item.Enclosure!.Length);
        Assert.Equal("audio/mpeg", item.Enclosure!.Type);
        Assert.Null(item.Categories);
        Assert.Null(item.Source);

        item = feed.Channel.Items[1];
        Assert.Null(item.Title);
        Assert.Null(item.Link);
        Assert.Equal("Kevin Drennan started a <a href=\"http://deadend.editthispage.com/\">Grateful Dead Weblog</a>. Hey it's cool, he even has a <a href=\"http://deadend.editthispage.com/directory/61\">directory</a>. <i>A Frontier 7 feature.</i>", item.Description);
        Assert.Null(item.Enclosure);
        Assert.Null(item.Categories);
        Assert.Equal("http://scriptingnews.userland.com/xml/scriptingNews2.xml", item.Source!.Url);
        Assert.Equal("Scripting News", item.Source!.Value);

        item = feed.Channel.Items[3];
        Assert.Null(item.Title);
        Assert.Null(item.Link);
        Assert.Equal("This is a test of a change I just made. Still diggin..", item.Description);
        Assert.Null(item.Enclosure);
        Assert.Null(item.Categories);
        Assert.Null(item.Source);
    }

    [Fact]
    public void ParseValidRss2_0Content1()
    {
        var content = TestUtils.LoadTestData("sample-rss-2.xml");
        var feedType = FeedTypeDetector.DetectFeedType(content);
        Assert.Equal(FeedType.Rss, feedType);

        var parser = new RssParser();

        // Parse without item.
        var feed = parser.Parse(content, noItem: true);
        Assert.Equal(RssVersion.Rss_2_0, feed.Version);
        Assert.Equal("Liftoff News", feed.Channel.Title);
        Assert.Equal("http://liftoff.msfc.nasa.gov/", feed.Channel.Link);
        Assert.Equal("Liftoff to Space Exploration.", feed.Channel.Description);
        Assert.Equal("en-us", feed.Channel.Language);
        Assert.Equal(new DateTime(2003, 6, 10, 4, 0, 0), feed.Channel.PubDate);
        Assert.Equal(new DateTime(2003, 6, 10, 9, 41, 1), feed.Channel.LastBuildDate);
        Assert.Equal("http://blogs.law.harvard.edu/tech/rss", feed.Channel.Docs);
        Assert.Equal("Weblog Editor 2.0", feed.Channel.Generator);
        Assert.Equal("editor@example.com", feed.Channel.ManagingEditor);
        Assert.Equal("webmaster@example.com", feed.Channel.WebMaster);
        Assert.Null(feed.Channel.Cloud);
        Assert.Null(feed.Channel.CopyRight);
        Assert.Null(feed.Channel.Image);
        Assert.Null(feed.Channel.Items);
        Assert.Null(feed.Channel.Rating);
        Assert.Null(feed.Channel.SkipDays);
        Assert.Null(feed.Channel.SkipHours);
        Assert.Null(feed.Channel.TextInput);

        // Parse with items.
        feed = parser.Parse(content, noItem: false);
        Assert.Equal(RssVersion.Rss_2_0, feed.Version);
        Assert.Equal("Liftoff News", feed.Channel.Title);
        Assert.Equal("http://liftoff.msfc.nasa.gov/", feed.Channel.Link);
        Assert.Equal("Liftoff to Space Exploration.", feed.Channel.Description);
        Assert.Equal("en-us", feed.Channel.Language);
        Assert.Equal(new DateTime(2003, 6, 10, 4, 0, 0), feed.Channel.PubDate);
        Assert.Equal(new DateTime(2003, 6, 10, 9, 41, 1), feed.Channel.LastBuildDate);
        Assert.Equal("http://blogs.law.harvard.edu/tech/rss", feed.Channel.Docs);
        Assert.Equal("Weblog Editor 2.0", feed.Channel.Generator);
        Assert.Equal("editor@example.com", feed.Channel.ManagingEditor);
        Assert.Equal("webmaster@example.com", feed.Channel.WebMaster);
        Assert.Null(feed.Channel.Cloud);
        Assert.Null(feed.Channel.CopyRight);
        Assert.Null(feed.Channel.Image);
        Assert.Null(feed.Channel.Rating);
        Assert.Null(feed.Channel.SkipDays);
        Assert.Null(feed.Channel.SkipHours);
        Assert.Null(feed.Channel.TextInput);
        Assert.Equal(4, feed.Channel.Items!.Count);

        var item = feed.Channel.Items[0];
        Assert.Equal("Star City", item.Title);
        Assert.Equal("http://liftoff.msfc.nasa.gov/news/2003/news-starcity.asp", item.Link);
        Assert.Equal("How do Americans get ready to work with Russians aboard the International Space Station? They take a crash course in culture, language and protocol at Russia's <a href=\"http://howe.iki.rssi.ru/GCTC/gctc_e.htm\">Star City</a>.", item.Description);
        Assert.Equal(new DateTime(2003, 6, 3, 9, 39, 21), item.PubDate);
        Assert.Equal("http://liftoff.msfc.nasa.gov/2003/06/03.html#item573", item.Guid!.Value);
        Assert.True(item.Guid.IsPermaLink);
        Assert.Null(item.Enclosure);
        Assert.Null(item.Categories);
        Assert.Null(item.Source);

        item = feed.Channel.Items[1];
        Assert.Null(item.Title);
        Assert.Null(item.Link);
        Assert.Equal("Sky watchers in Europe, Asia, and parts of Alaska and Canada will experience a <a href=\"http://science.nasa.gov/headlines/y2003/30may_solareclipse.htm\">partial eclipse of the Sun</a> on Saturday, May 31st.", item.Description);
        Assert.Equal(new DateTime(2003, 5, 30, 11, 6, 42), item.PubDate);
        Assert.Equal("http://liftoff.msfc.nasa.gov/2003/05/30.html#item572", item.Guid!.Value);
        Assert.True(item.Guid.IsPermaLink);
        Assert.Null(item.Enclosure);
        Assert.Null(item.Categories);
        Assert.Null(item.Source);
    }
}