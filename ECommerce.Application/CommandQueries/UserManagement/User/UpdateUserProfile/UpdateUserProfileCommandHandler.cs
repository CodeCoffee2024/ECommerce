using ECommerce.Application.Abstractions;
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Entities.UserManagement.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Application.CommandQueries.UserManagement.User.UpdateUserProfile
{
    internal sealed class UpdateUserProfileCommandHandler : ICommandHandler<UpdateUserProfileCommand>
    {
        #region Fields

        private readonly IDbService _dbService;

        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IConfiguration _configuration;
        private readonly UpdateUserProfileValidator _validator;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string FolderPath = "";

        #endregion Fields

        #region Public Constructors

        public UpdateUserProfileCommandHandler(IUserRepository userRepository, IDbService dbService, IPasswordHasherService passwordHasherService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _passwordHasherService = passwordHasherService;
            _dbService = dbService;
            _userRepository = userRepository;
            _webHostEnvironment = webHostEnvironment;
            _validator = new UpdateUserProfileValidator(userRepository);
            _configuration = configuration;
            FolderPath = _configuration["UploadSettings:UploadPath"]!;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            // Validate request
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return Result.Failure<Result>(Error.Validation, validation.Errors);

            var current = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            // Create user
            var user = current.Update(
                request.LastName, request.FirstName, request.MiddleName,
                request.BirthDate, current.Email, request.UserName, DateTime.Now, request.Id
            );

            if (request.Img != null && request.Img.Length > 0)
            {
                var uploadsPath = Path.Combine(FolderPath);
                if (!Directory.Exists(uploadsPath))
                    Directory.CreateDirectory(uploadsPath);

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + request.Img.FileName;
                var filePath = Path.Combine(uploadsPath, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await request.Img.CopyToAsync(stream);
                }
                user.UpdateImage(uniqueFileName);
            }

            _userRepository.Update(user);
            await _dbService.SaveChangesAsync();

            return Result.Success("user");
        }

        #endregion Public Methods
    }
}