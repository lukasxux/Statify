using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatifyProject.Application.Model
{
    public class TopArtistsResponseWrapper
    {
        public string Href { get; set; } = string.Empty;
        public int Limit { get; set; }
        public string? Next { get; set; }
        public int? Offset { get; set; }
        public string? Previous { get; set; }
        public int? Total { get; set; }
        public TopArtistsResponse[]? Items { get; set; }
    }

    public class TopArtistsResponse
    {
        public ExternalUrls? ExternalUrls { get; set; }
        public Followers? Followers { get; set; }
        public string[]? Genres { get; set; }
        public string Href { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public ImageItem[]? Images { get; set; }
        public string? Name { get; set; }
    }

    public class Followers
    {
        public string? Href { get; set; }
        public int? Total { get; set; }
    }

}

