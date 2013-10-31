using System.Runtime.Serialization;

namespace MoviesService.Contracts
{
    [DataContract]
    public enum MovieSortFields
    {
        [EnumMember]
        Title,

        [EnumMember]
        Classification,

        [EnumMember]
        Genre,

        [EnumMember]
        ReleaseDate,

        [EnumMember]
        Rating,

        [EnumMember]
        MovieId
    }
}