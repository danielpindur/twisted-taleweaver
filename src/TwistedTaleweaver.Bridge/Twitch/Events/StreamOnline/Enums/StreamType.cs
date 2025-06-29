using System.Runtime.Serialization;

namespace TwistedTaleweaver.Bridge.Twitch.Events.StreamOnline.Enums;

internal enum StreamType
{
    Unknown = 0,
    
    [EnumMember(Value = "live")]
    Live, 
    
    [EnumMember(Value = "playlist")]
    Playlist,

    [EnumMember(Value = "watch_party")]
    WatchParty,
    
    [EnumMember(Value = "premiere")]
    Premiere,

    [EnumMember(Value = "rerun")]
    Rerun
}