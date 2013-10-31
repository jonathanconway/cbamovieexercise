using System.Runtime.Serialization;

namespace MoviesService.Contracts
{
    [DataContract]
    public enum MovieFilterFields
    {
        [EnumMember]
        Title,

        [EnumMember]
        Cast,

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