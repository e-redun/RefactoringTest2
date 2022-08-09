using System;

namespace LegacyApp.Factories
{
    public interface IUserFactory
    {
        IUser CreateUser(string firstname, string surname, string email, DateTime dateOfBirth, int clientId);
    }
}