namespace Tasker.Identity
{
	using Duende.IdentityServer.Extensions;
	using Duende.IdentityServer.Models;
	using Duende.IdentityServer.Services;
	using Microsoft.AspNetCore.Identity;
	using System.Linq;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using Tasker.Identity.Models;

	public class CustomProfileService : IProfileService
	{
		private readonly UserManager<AppUser> _userManager;

		public CustomProfileService(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task GetProfileDataAsync(ProfileDataRequestContext context)
		{
			// Получаем пользователя
			var user = await _userManager.FindByIdAsync(context.Subject.GetSubjectId());

			// Добавляем ролей пользователя в Claims
			var roles = await _userManager.GetRolesAsync(user);
			var claims = roles.Select(role => new Claim(ClaimTypes.Role, role));

			// Добавляем Claims
			context.IssuedClaims.AddRange(claims);
		}

		public Task IsActiveAsync(IsActiveContext context)
		{
			// Проверяем, активен ли пользователь
			context.IsActive = true; // или логика определения активности пользователя
			return Task.CompletedTask;
		}
	}

}
