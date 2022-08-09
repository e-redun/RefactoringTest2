using System;

namespace LegacyApp.Services
{
    public interface IUserValidator
    {
        /// <summary>
        /// Возвращает результат валидации user-a
        /// </summary>
        /// <returns>Результат валидации</returns>
        bool ValidateUser(IUser user);

        /// <summary>
        /// Валидирует часть имени
        /// </summary>
        /// <param name="namePart">Часть имени</param>
        bool ValidateNamePart(string namePart);


        /// <summary>
        /// Валидирует email
        /// </summary>
        /// <param name="email"></param>
        bool ValidateEmail(string email);

        /// <summary>
        /// Валидирует возраст
        /// </summary>
        /// <param name="dateOfBirth">DateTime Возраст</param>
        bool ValidateAge(DateTime dateOfBirth);

        /// <summary>
        /// Выполняет валидацию кредитоспособности пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        bool ValidateCreditworthiness(IUser user);
    }
}