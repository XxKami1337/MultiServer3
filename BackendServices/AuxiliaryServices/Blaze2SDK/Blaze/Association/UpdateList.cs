using System.ComponentModel.DataAnnotations;
using Tdf;

namespace Blaze2SDK.Blaze.Association
{
    [TdfStruct]
    public struct UpdateList
    {
        
        /// <summary>
        /// Max String Length: 32
        /// </summary>
        [TdfMember("ALNM")]
        [StringLength(32)]
        public string mListName;
        
        [TdfMember("BIDL")]
        public List<ListMemberId> mUserListMemberIdList;
        
    }
}
