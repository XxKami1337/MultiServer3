using Tdf;

namespace Blaze2SDK.Blaze.GameManager
{
    [TdfStruct]
    public struct ReturnDedicatedServerToPoolRequest
    {
        
        [TdfMember("GID")]
        public uint mGameId;
        
    }
}
