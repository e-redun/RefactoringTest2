using System;

namespace LegacyApp.Services
{
    /// <summary>
    /// Валидатор пользователя
    /// </summary>
    public class UserValidator : IUserValidator
    {
        public bool ValidateUser(IUser user)
        {
            bool output = true;

            output &= ValidateNamePart(user.FirstName);
            output &= ValidateNamePart(user.Surname);
            output &= ValidateEmail(user.EmailAddress);
            output &= ValidateAge(user.DateOfBirth);

            return output;
        }


        /// <summary>
        /// Валидирует часть имени
        /// </summary>
        /// <param name="namePart">Часть имени</param>
        /// <returns>True, если валидация успешна</returns>
        public bool ValidateNamePart(string namePart)
        {
            return !string.IsNullOrEmpty(namePart);
        }


        /// <summary>
        /// Валидирует email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>True, если валидация успешна</returns>
        public bool ValidateEmail(string email)
        {
            bool output = true;

            output &= email.Contains("@");               // содержит собачку
            output &= email.Contains(".");               // содержит точку
            output &= !string.IsNullOrWhiteSpace(email);  // не пустая строка
            
            return output;
        }

        /// <summary>
        /// Валидирует возраст
        /// </summary>
        /// <param name="dateOfBirth">DateTime Возраст</param>
        /// <returns>True, если валидация успешна</returns>
        public bool ValidateAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;

            // вычисление полных лет
            if (now < dateOfBirth.AddYears(age))
            {
                age--;
            }

            if (age < GlobalConfig.minAge)
            {
                return false;
            }

            return true;
        }

        public bool ValidateCreditworthiness(IUser user)
        {
            if (user.HasCreditLimit &&
                user.CreditLimit < GlobalConfig.minGreditLimit)
            {
                return false;
            }

            return true;
        }
    }
}