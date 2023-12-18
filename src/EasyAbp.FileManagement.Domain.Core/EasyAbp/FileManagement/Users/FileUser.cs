﻿using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Users;

namespace EasyAbp.FileManagement.Users;

public class FileUser : AggregateRoot<Guid>, IUser, IUpdateUserData
{
    #region IUser

    public virtual Guid? TenantId { get; protected set; }

    public virtual string UserName { get; protected set; }

    public virtual string Email { get; protected set; }

    public virtual string Name { get; protected set; }

    public virtual string Surname { get; protected set; }

    public virtual bool IsActive { get; protected set; }

    public virtual bool EmailConfirmed { get; protected set; }

    public virtual string PhoneNumber { get; protected set; }

    public virtual bool PhoneNumberConfirmed { get; protected set; }

    #endregion

    protected FileUser()
    {
    }

    public FileUser(IUserData user) : base(user.Id)
    {
        TenantId = user.TenantId;

        UpdateInternal(user);
    }

    public virtual bool Update(IUserData user)
    {
        if (Id != user.Id)
        {
            throw new ArgumentException($"Given User's Id '{user.Id}' does not match to this User's Id '{Id}'");
        }

        if (TenantId != user.TenantId)
        {
            throw new ArgumentException(
                $"Given User's TenantId '{user.TenantId}' does not match to this User's TenantId '{TenantId}'");
        }

        if (Equals(user))
        {
            return false;
        }

        UpdateInternal(user);
        return true;
    }

    protected virtual bool Equals(IUserData user)
    {
        return Id == user.Id &&
               TenantId == user.TenantId &&
               UserName == user.UserName &&
               Name == user.Name &&
               Surname == user.Surname &&
               Email == user.Email &&
               EmailConfirmed == user.EmailConfirmed &&
               PhoneNumber == user.PhoneNumber &&
               PhoneNumberConfirmed == user.PhoneNumberConfirmed;
    }

    protected virtual void UpdateInternal(IUserData user)
    {
        Email = user.Email;
        Name = user.Name;
        Surname = user.Surname;
        EmailConfirmed = user.EmailConfirmed;
        PhoneNumber = user.PhoneNumber;
        PhoneNumberConfirmed = user.PhoneNumberConfirmed;
        UserName = user.UserName;
    }

    public virtual BriefFileUserInfoModel ToBriefUserInfoModel()
    {
        return new BriefFileUserInfoModel(Id, UserName);
    }
}