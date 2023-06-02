using CoreRepository;
using CoreRepository.Models;

namespace HappyCompany_API.Helper
{
    public class DataSeeder
    {
        private readonly IUnitOfWork _unitOfWork;
        public DataSeeder(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Seed()
        {
            if (_unitOfWork.Roles.Count() == 0)
            {
                List<Role> roles = new List<Role>()
                {
                    new Role(){ Name = "Admin" },
                    new Role(){ Name = "Management"},
                    new Role(){ Name = "Auditor"}
                };
                _unitOfWork.Roles.SaveRange(roles);
                _unitOfWork.Complete();
            }

            if (_unitOfWork.Users.Count() == 0)
            {
                User admin = new User()
                {
                    Id = Guid.NewGuid(),
                    Email = "admin@happywarehouse.com",
                    Password = "P@ssw0rd",
                    FullName = "Admin",
                    Role = "Admin",
                    Active = true,
                };
                _unitOfWork.Users.Add(admin);
                _unitOfWork.Complete();
            }
        }
    }
}
