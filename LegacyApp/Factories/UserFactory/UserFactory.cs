using LegacyApp.Services;
using System;

namespace LegacyApp.Factories
{
    public class UserFactory : IUserFactory
    {
        private readonly IClientRepository _clientRepository;

        private readonly ICreditLimitProviderFactory _creditLimitProviderFactory;

        public UserFactory(IClientRepository clientRepository, ICreditLimitProviderFactory creditLimitProviderFactory)
        {
            _clientRepository = clientRepository;

            _creditLimitProviderFactory = creditLimitProviderFactory;
        }

        /// <summary>
        /// Создает пользователя
        /// </summary>
        /// <param name="firstname">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="email">Эл. почта</param>
        /// <param name="dateOfBirth">Дата рождения</param>
        /// <param name="clientId">ID клиента</param>
        /// <returns></returns>
        public IUser CreateUser(string firstname,
                               string surname,
                               string email,
                               DateTime dateOfBirth,
                               int clientId)
        {
            IUser user = new User
            {
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstname,
                Surname = surname
            };


            IUserValidator userValidator = new UserValidator();
            
            // валидация пользователя
            if (!userValidator.ValidateUser(user))
            {
                return null;
            }

            // валидация кредитоспособности пользователя
            user.Client = _clientRepository.GetById(clientId);

            if (!userValidator.ValidateCreditworthiness(user))
            {
                return null;
            }


            var creditLimitProvider = _creditLimitProviderFactory.GetCreditLimitProvider(user.Client.Name);

            user.HasCreditLimit = creditLimitProvider.HasCreditLimit();

            // получение кредитного лимита
            var creditLimit = creditLimitProvider.GetCreditLimit(user);

            user.CreditLimit = creditLimit ?? 0;

            return user;
        }
    }
}