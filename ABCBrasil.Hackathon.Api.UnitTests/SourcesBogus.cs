using ABCBrasil.Hackathon.Api.Domain.DataContracts.Requests;
using ABCBrasil.Hackathon.Api.Domain.Entities;
using AutoBogus;
using AutoBogus.Conventions;

namespace ABCBrasil.Hackathon.Api.UnitTests
{
    public static class SourcesBogus
    {
        internal static UserRequest GenerateCreateUserRequest()
        {
            var autoFaker = new AutoFaker<UserRequest>().Configure(builder =>
            {
                builder.WithConventions();
            });

            UserRequest fakeObject = autoFaker.Generate();

            return fakeObject;
        }

        internal static User GenerateCreateUser(string name, string email, string password)
        {
            User fakeObject = GenerateCreateUser();

            fakeObject.Name = name;
            fakeObject.Email = email;
            fakeObject.Password = password;

            return fakeObject;
        }

        internal static User GenerateCreateUser()
        {
            var autoFaker = new AutoFaker<User>().Configure(builder =>
            {
                builder.WithConventions();
            });

            User fakeObject = autoFaker.Generate();

            return fakeObject;
        }
    }
}
