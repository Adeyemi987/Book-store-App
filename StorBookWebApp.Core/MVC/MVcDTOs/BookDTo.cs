using System;

namespace StorBookWebApp.Core.MVC
{
    public class BookDTo
    {
        public string Id { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string PublishedOn { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public string AuthorsOrdered { get; set; }
        public int ReviewsCount { get; set; }
        public double? ReviesAverageCount { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }


    }
}
