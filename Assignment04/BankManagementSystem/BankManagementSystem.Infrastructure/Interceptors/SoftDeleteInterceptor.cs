using BankManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankManagementSystem.Application.Interceptors;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
            return new(result);

        SoftDeleteEntities(eventData.Context);
        return new(result);
    }

    private void SoftDeleteEntities(DbContext context)
    {
        var entityEntries = context.ChangeTracker.Entries();

        foreach (var entityEntry in entityEntries)
        { 
            if(entityEntry is not null && entityEntry.State == EntityState.Deleted && entityEntry.Entity is BaseEntity entity)
            {
                entityEntry.State = EntityState.Modified;
                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow;
                entity.DeletedBy = "System";
            }
        }
    }
}