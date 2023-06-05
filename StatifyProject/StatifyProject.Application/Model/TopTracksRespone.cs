namespace StatifyProject.Application.Model
{


    namespace StatifyProject.Application.Model
    {
        public class TopTracksResponseWrapper
        {
            public string Href { get; set; } = string.Empty;
            public int Limit { get; set; }
            public string? Next { get; set; }
            public int Offset { get; set; }
            public string? Previous { get; set; }
            public int Total { get; set; }
            public TopTracksResponse[]? Items { get; set; }
        }

        public class TopTracksResponse
        {
            public TrackItem? Album { get; set; }
            public ArtistItem[]? Artists { get; set; }
            public string[]? AvailableMarkets { get; set; }
            public int? DiscNumber { get; set; }
            public int? DurationMs { get; set; }
            public bool Explicit { get; set; }
            public ExternalIds? ExternalIds { get; set; }
            public ExternalUrls? ExternalUrls { get; set; }
            public string Href { get; set; } = string.Empty;
            public string Id { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public int? Popularity { get; set; }
            public string PreviewUrl { get; set; } = string.Empty;
            public int? TrackNumber { get; set; }
            public string Type { get; set; } = string.Empty;
            public string Uri { get; set; } = string.Empty;
            public bool IsLocal { get; set; }
        }

        // Other classes remain the same...
    }



    public class TrackItem
    {
        public string AlbumType { get; set; } = string.Empty;
        public int? TotalTracks { get; set; }
        public string[]? AvailableMarkets { get; set; }
        public ExternalUrls? ExternalUrls { get; set; }
        public string Href { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public ImageItem[]? Images { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ReleaseDate { get; set; } = string.Empty;
        public string ReleaseDatePrecision { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Uri { get; set; } = string.Empty;
        public ArtistItem[]? Artists { get; set; }
    }


    public class ImageItem
    {
        public string? Url { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
    }


    public class ArtistItem
    {
        public ExternalUrls? ExternalUrls { get; set; }
        public string Href { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Uri { get; set; } = string.Empty;
    }


    public class ExternalUrls
    {
        public string? Spotify { get; set; }
    }

    public class ExternalIds
    {
        public string? Isrc { get; set; }
    }
}
