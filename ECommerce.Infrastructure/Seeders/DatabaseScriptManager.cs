using ECommerce.Application.Abstractions;
using ECommerce.Infrastructure.Seeders.Scripts;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Infrastructure.Seeders
{
    public class DatabaseScriptManager
    {
        #region Fields

        private readonly AppDbContext _context;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IConfiguration _configuration;

        #endregion Fields

        #region Public Constructors

        public DatabaseScriptManager(
            AppDbContext context,
            IPasswordHasherService passwordHasherService,
            IConfiguration configuration
        )
        {
            _configuration = configuration;
            _context = context;
            _passwordHasherService = passwordHasherService;
        }

        #endregion Public Constructors

        #region Public Methods

        public void RunDbScripts()
        {
            //Seed Permission
            var isSeedingEnabled = bool.Parse(_configuration["DatabaseSeeding:Enabled"] ?? "true");
            if (!isSeedingEnabled)
            {
                return;
            }
            ModuleSeeder.Run(_context);
            UserSeeder.Run(_context, _passwordHasherService.HashPassword("password"));
            UserPermissionSeeder.Run(_context);
        }

        #endregion Public Methods
    }
}