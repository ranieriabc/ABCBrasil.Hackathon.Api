using ABCBrasil.Hackathon.Api.Domain.Entities;
using ABCBrasil.Hackathon.Api.Domain.Interfaces;
using ABCBrasil.Hackathon.Api.Infra.Contexts;

namespace ABCBrasil.Hackathon.Api.Infra.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(HackathonContext db) : base(db)
        {
        }
    }
}
