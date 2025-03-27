using ECommerce.Application.Abstractions;
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.User.AddUser.AddUser;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Entities.UserManagement;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.UserManagement.User.AddUser
{
    internal sealed class AddUserCommandHandler : ICommandHandler<AddUserCommand>
    {
        #region Fields

        private readonly IDbService _dbService;

        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IUserPermissionRepository _userPermissionRepository;
        private readonly AddUserCommandValidator _validator;

        #endregion Fields

        #region Public Constructors

        public AddUserCommandHandler(IUserRepository userRepository, IUserPermissionRepository userPermissionRepository, IPasswordHasherService passwordHasherService, IDbService dbService)
        {
            _passwordHasherService = passwordHasherService;
            _dbService = dbService;
            _userRepository = userRepository;
            _userPermissionRepository = userPermissionRepository;
            _validator = new AddUserCommandValidator(userRepository);
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            // Validate request
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return Result.Failure<Result>(Error.Validation, validation.Errors);

            // Fetch permissions
            var permissions = (await _userPermissionRepository.GetAllPermissions(cancellationToken))
                .Where(it => request.UserPermissions.Contains(it.Id!.ToString()!))
                .ToList(); // ✅ Convert to List to avoid multiple enumerations

            Console.WriteLine($"Permissions: {string.Join(", ", permissions.Select(p => p.Id))}"); // ✅ Improved logging

            // Create user
            var user = ECommerce.Domain.Entities.UserManagement.User.Create(
                request.LastName, request.FirstName, request.MiddleName,
                request.BirthDate, request.Email, request.UserName,
                _passwordHasherService.HashPassword(request.Password), DateTime.Now, request.Id
            );


            // Set permissions & save
            _userRepository.Add(user);
            // Create user-permission mappings
            var userUserPermissions = new List<UserUserPermission>(); // ✅ Use List<T>
            foreach (var permission in permissions)
            {
                userUserPermissions.Add(
                    UserUserPermission.Create(Guid.Parse(user.Id.ToString()!), Guid.Parse(permission.Id.ToString()!))
                );
            }
            user.SetUserUserPermissions(userUserPermissions);
            await _dbService.SaveChangesAsync();

            return Result.Success("user");
        }

        #endregion Public Methods
    }
}