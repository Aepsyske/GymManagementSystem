using GymManagementBL.Service.Interfaces;
using GymManagementBL.Service.ViewModels.MemberViewModel;
using GymManagementDL.Entities;
using GymManagementDL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBL.Service.Classes
{
    internal class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MemberService(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork; 
        }

        public IEnumerable<MemberViewModels> GetAllMember()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll();
            if (members is null || members.Any()) return [];
            #region Way1 
            //var memberViewModel = new List<MemberViewModels>();
            //foreach(var member in members)
            //{
            //    var memberviewmodel = new MemberViewModels()
            //    {
            //        ID = member.ID,
            //        Email = member.Email,
            //        Phone = member.Phone,
            //        Photo = member.photo,
            //        Gender = member.Gender.ToString(),
            //        Name = member.Name,
            //    };
            //    memberViewModel.Add(memberviewmodel);
            //}
            #endregion
            var memberViewModel = members.Select(member => new MemberViewModels
            {
                ID = member.ID,
                Email = member.Email,
                Phone = member.Phone,
                Photo = member.photo,
                Gender = member.Gender.ToString(),
                Name = member.Name,
            });
            return memberViewModel;
        }
        public bool CreateMember(CreateMemberViewModel createMember)
        {
            try
            {
                if (IsEmailExist(createMember.Email) || IsPhoneExist(createMember.Phone)) return false;
                var member = new Member()
                {
                    Name = createMember.Name,
                    Email = createMember.Email,
                    Phone = createMember.Phone,
                    Gender = createMember.Gender,
                    DateOfBirth = createMember.DateOfBirth,
                    Address = new Address()
                    {
                        City = createMember.City,
                        Street = createMember.Street,
                        BuildNo = createMember.BuildingNumber.ToString(),
                    },
                    HealthRecord = new HealthRecord()
                    {
                        Height = createMember.HealthRecord.Height,
                        Weight = createMember.HealthRecord.Weight,
                        BloodType = createMember.HealthRecord.BloodType,
                        Note = createMember.HealthRecord.Note
                    }
                };
                _unitOfWork.GetRepository<Member>().Add(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public MemberViewModels? GetMemberDetails(int id)
        {
            var member = _unitOfWork.GetRepository<Member>().GetID(id);
            if (member is null) return null;
            var viewModel = new MemberViewModels()
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBirth.ToString("dd/MM/yyyy"),
                Address = $"{member.Address.BuildNo}, {member.Address.Street}, {member.Address.City}",
                Photo = member.photo
            };
            var ActiveMembership = _unitOfWork.GetRepository<Membership>().GetAll(x => x.ID == id && x.Status == "Active").FirstOrDefault();
            if(ActiveMembership is not null)
            {
                viewModel.MemberShipStartDate = ActiveMembership.MadeAt.ToShortDateString();
                viewModel.MemberShipEndDate = ActiveMembership.ExpiredAt.ToShortDateString();
                var plan = _unitOfWork.GetRepository<Plan>().GetID(ActiveMembership.PlanID);
                viewModel.PlanName = plan?.Name;
            }
            return viewModel;
        }
        public HealthRecordViewModel? GetMemberHealthRecordDetails(int id)
        {
            var memberHealthRecord = _unitOfWork.GetRepository<HealthRecord>().GetID(id);
            if (memberHealthRecord is null) return null;
            return new HealthRecordViewModel()
            {
                Height = memberHealthRecord.Height,
                Weight = memberHealthRecord.Weight,
                BloodType = memberHealthRecord.BloodType,
                Note = memberHealthRecord.Note
            };
        }
        public MemberToUpdateViewModel? GetMemberToUpdate(int id)
        {
            var member = _unitOfWork.GetRepository<Member>().GetID(id);
            if(member is null) return null;
            return new MemberToUpdateViewModel()
            {
                Email = member.Email,
                Name = member.Name,
                Phone = member.Phone,
                Photo = member.photo,
                BuildingNumber = member.Address.BuildNo,
                Street = member.Address.Street,
                City = member.Address.City
            };
        } // CTRL + M + M TO OPEN
        public bool UpdateMember(int id, MemberToUpdateViewModel memberToUpdate)
        {
            try
            {
                /* var emailExist = _memberRepository.GetAll(x => x.Email == memberToUpdate.Email).Any();
                 var phoneExist = _memberRepository.GetAll(x => x.Phone == memberToUpdate.Phone).Any();
                 if(emailExist || phoneExist) return false;*/

                if (IsEmailExist(memberToUpdate.Email) || IsPhoneExist(memberToUpdate.Phone)) return false;

                var MemberRepo = _unitOfWork.GetRepository<Member>();
                var member = MemberRepo.GetID(id);
                if (member is null) return false;
                member.Email = memberToUpdate.Email;
                member.Phone = memberToUpdate.Phone;
                member.Address.BuildNo = memberToUpdate.BuildingNumber;
                member.Address.City = memberToUpdate.City;
                member.Address.Street = memberToUpdate.Street;
                member.UpdatedAt = DateTime.Now;
                MemberRepo.Update(member);
                return _unitOfWork.SaveChanges() > 0;

            }catch(Exception)
            {
                return false;
            }
        }
        public bool RemoveMember(int id)
        {
            var member = _unitOfWork.GetRepository<Member>().GetID(id);
            if (member is null) return false;
            var Active = _unitOfWork.GetRepository<MemberSessions>().GetAll(x => x.ID == id && x.Session.StartDate > DateTime.Now).Any();
            if (Active) return false;
            var membership = _unitOfWork.GetRepository<Membership>().GetAll(x => x.ID == id);
            try
            {
                if(membership.Any())
                {
                    foreach(var membeership in membership)
                    {
                        _unitOfWork.GetRepository<Membership>().Delete(membeership);
                    }
                }
                _unitOfWork.GetRepository<Member>().Delete(member);
                return _unitOfWork.SaveChanges() > 0;
            }catch(Exception)
            {
                return false;
            }
        }

        #region HelperMethod
        private bool IsEmailExist(string email)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(x => x.Email == email).Any();
        }
        private bool IsPhoneExist(string phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(x => x.Phone == phone).Any();
        }
        #endregion
    }
}
