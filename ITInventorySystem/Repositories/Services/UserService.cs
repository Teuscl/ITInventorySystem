﻿using ITInventorySystem.Data;
using ITInventorySystem.DTO.User;
using ITInventorySystem.Models;
using ITInventorySystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITInventorySystem.Repositories.Services;

public class UserService(AppDbContext context) : IUserInterface
{
    public async Task<User> AddAsync(UserCreateDto user)
    {
        if (await context.Users.AnyAsync(userDb => userDb.Email == user.Email))
            throw new InvalidOperationException("Email already in use!");

        HashPassword.Hash(user.Password, out var passwordHash, out var passwordSalt);

        var newUser = new User
        {
            Name = user.Name,
            Email = user.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Type = user.Type
        };

        context.Users.Add(newUser);
        await context.SaveChangesAsync();

        return newUser;
    }

    public async Task<IEnumerable<User>> GetAllAsync() => await context.Users.ToListAsync();

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await context.Users.FirstOrDefaultAsync(userDb => userDb.Id == id);

        if (user == null)
            throw new KeyNotFoundException("User not found!");

        return user;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        var user = await context.Users.FirstOrDefaultAsync(userDb => userDb.Email == email);

        if (user == null)
            throw new KeyNotFoundException("User not found!");

        return user;
    }

    public async Task<User> UpdateAsync(UserUpdateDto user)
    {
        if (await context.Users.AnyAsync(userDb => userDb.Email == user.Email && userDb.Id != user.Id))
            throw new InvalidOperationException("Email already in use!");

        var userDb = await context.Users.FirstOrDefaultAsync(userDb => userDb.Id == user.Id);

        if (userDb == null)
            throw new KeyNotFoundException("User not found!");

        userDb.Name = user.Name;
        userDb.Email = user.Email;
        userDb.Type = user.Type;
        userDb.UpdatedAt = DateTime.Now;
        if (user.Password != "")
        {
            HashPassword.Hash(user.Password, out var passwordHash, out var passwordSalt);
            userDb.PasswordHash = passwordHash;
            userDb.PasswordSalt = passwordSalt;
        }

        context.Users.Update(userDb);
        await context.SaveChangesAsync();

        return userDb;
    }

    public async Task UpdatePasswordAsync(UserUpdatePasswordDto user)
    {
        var userDb = await context.Users.FirstOrDefaultAsync(userDb => userDb.Id == user.Id);

        if (userDb == null)
            throw new KeyNotFoundException("User not found!");

        if (!HashPassword.Verify(user.Password, userDb.PasswordHash, userDb.PasswordSalt))
            throw new InvalidOperationException("Incorrect password!");

        HashPassword.Hash(user.NewPassword, out var passwordHash, out var passwordSalt);

        userDb.PasswordHash = passwordHash;
        userDb.PasswordSalt = passwordSalt;

        context.Users.Update(userDb);
        await context.SaveChangesAsync();
    }

    public async Task<User> UpdateStatusAsync(UserUpdateStatusDto user)
    {
        var userDb = await context.Users.FirstOrDefaultAsync(userDb => userDb.Id == user.Id);

        if (userDb == null)
            throw new KeyNotFoundException("User not found!");

        userDb.Status = user.Status;

        context.Users.Update(userDb);
        await context.SaveChangesAsync();

        return userDb;
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(userDb => userDb.Id == id);

            if (user == null)
                throw new KeyNotFoundException("User not found!");

            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}