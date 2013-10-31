using System.Runtime.Serialization;

namespace MoviesService.Contracts
{
    [DataContract]
    public class Movie
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string[] Cast { get; set; }

        [DataMember]
        public string Classification { get; set; }

        [DataMember]
        public string Genre { get; set; }

        [DataMember]
        public int ReleaseDate { get; set; }

        [DataMember]
        public int Rating { get; set; }

        [DataMember]
        public int MovieId { get; set; }
    }
}