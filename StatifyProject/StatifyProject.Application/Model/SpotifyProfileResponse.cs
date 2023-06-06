using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatifyProject.Application.Model
{


        public class SpotifyProfileResponse
        {
            public string? Country { get; set; }
            public string? Display_Name { get; set; }
            //public string? Email { get; set; }
            public ExplicitContent? Explicit_Content { get; set; }
            public ExternalUrls? External_Urls { get; set; }
            public Followers? Followers { get; set; }
            public string? Href { get; set; }
            public string? Id { get; set; }
            public ImageItem[]? Images { get; set; }
            public string? Product { get; set; }
            public string? Type { get; set; }
            public string? Uri { get; set; }
        }

        public class ExplicitContent
        {
            public bool? Filter_Enabled { get; set; }
            public bool? Filter_Locked { get; set; }
        }

      

    
}
