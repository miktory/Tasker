using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Application.Interfaces
{
	public interface IIdentityApiClient
	{
		/// <summary>
		/// Аутентификация пользователя и получение токена доступа.
		/// </summary>
		/// <returns>Токен доступа для авторизации последующих запросов.</returns>
		Task<string> AuthenticateAsync(string username, string password, string clientId, CancellationToken cancellationToken);

		// Можете добавить дополнительные методы, если необходимо, например:
		// Task<bool> ChangePasswordAsync(string oldPassword, string newPassword);
	}
}
