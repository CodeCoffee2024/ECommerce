﻿using ECommerce.Application.CommandQueries.Common.Mapping;
using ECommerce.Domain.Dtos.UserManagement.UserPermission;

namespace ECommerce.Application.CommandQueries.UserManagement.User.GetOneUser
{
    public sealed class GetOneUserResponse
    {
        #region Properties

        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Img { get; set; } = string.Empty;
        public Guid? Id { get; set; }
        public List<OneUserPermissionDTO> Permissions { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public UserFragmentResponse CreatedBy { get; set; } = new();
        public UserFragmentResponse ModifiedBy { get; set; } = new();

        #endregion Properties

        #region Methods

        internal static GetOneUserResponse MapToResponse(ECommerce.Domain.Entities.UserManagement.User user, bool canUpdate, bool canDelete)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return new GetOneUserResponse()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedDate = user.CreatedDate,
                MiddleName = user.MiddleName,
                ModifiedDate = user.ModifiedDate,
                Img = user.Img,
                Email = user.Email,
                BirthDate = user.BirthDate,
                CanUpdate = !user.isSuperAdmin() && canUpdate,
                CanDelete = !user.isSuperAdmin() && canDelete,
                UserName = user.Username,
                Permissions = user.UserUserPermissions
                .Select(it => new OneUserPermissionDTO
                {
                    Id = it.UserPermission.Id,
                    Name = it.UserPermission.Name
                })
                .ToList(),
                CreatedBy = new UserFragmentResponse()
                {
                    Id = user.CreatedBy != null ? user.CreatedBy!.Id.ToString() : "",
                    Name = user.CreatedBy != null ? $"{user.CreatedBy?.FirstName ?? "Unknown"} {user.CreatedBy?.LastName ?? ""}".Trim() : ""
                },
                ModifiedBy = new UserFragmentResponse()
                {
                    Id = user.ModifiedBy != null ? user.ModifiedBy!.Id.ToString() : "",
                    Name = user.ModifiedBy != null ? $"{user.ModifiedBy?.FirstName ?? "Unknown"} {user.ModifiedBy?.LastName ?? ""}".Trim() : ""
                }
            };
        }

        #endregion Methods
    }
}