using ECommerce.Application.Abstractions;
using ECommerce.Infrastructure.Seeders.Scripts;

namespace ECommerce.Infrastructure.Seeders
{
    public class DatabaseScriptManager
    {
        #region Fields

        private const string CONFIG_SETTING_CODE = "DATABASE";
        private readonly AppDbContext _context;
        private readonly IPasswordHasherService _passwordHasherService;

        #endregion Fields

        #region Public Constructors

        public DatabaseScriptManager(
            AppDbContext context,
            IPasswordHasherService passwordHasherService)
        {
            _context = context;
            _passwordHasherService = passwordHasherService;
        }

        #endregion Public Constructors

        #region Public Methods

        public void RunDbScripts()
        {
            //Seed Permissions
            ModuleSeeder.Run(_context);
            UserSeeder.Run(_context, _passwordHasherService.HashPassword("password"));
            UserPermissionSeeder.Run(_context);
        }

        #endregion Public Methods
    }
}