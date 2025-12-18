using GymManagementBL.Service.ViewModels.MemberViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBL.Service.Interfaces
{
    internal interface IMemberService
    {
        IEnumerable<MemberViewModels> GetAllMember();
        bool CreateMember(CreateMemberViewModel createMember);
        MemberViewModels? GetMemberDetails(int id);
        HealthRecordViewModel? GetMemberHealthRecordDetails(int id);
        public MemberToUpdateViewModel? GetMemberToUpdate(int id);
        bool UpdateMember(int id, MemberToUpdateViewModel memberToUpdate);
        bool RemoveMember(int id);
    }
}
