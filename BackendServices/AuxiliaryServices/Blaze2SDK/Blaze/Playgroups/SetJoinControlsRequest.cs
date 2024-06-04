using Tdf;

namespace Blaze2SDK.Blaze.Playgroups
{
    [TdfStruct]
    public struct SetJoinControlsRequest
    {
        
        [TdfMember("OPEN")]
        public PlaygroupJoinability mPlaygroupJoinability;
        
        [TdfMember("PGID")]
        public uint mPlaygroupId;
        
    }
}
