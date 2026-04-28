using BankManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Application.Interceptors;

public sealed class AuditInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
            return new(result);

        UpdateAuditColumns(eventData.Context);
        return new(result);
    }

    private void UpdateAuditColumns(DbContext context)
    {
        var entityEntries = context.ChangeTracker.Entries();

        foreach(var entityEntry in entityEntries)
        {
            if (entityEntry is null || 
                entityEntry.Entity is not BaseEntity entity || 
                entityEntry.State is EntityState.Deleted)
            {
                continue;
            }

            switch(entityEntry.State)
            {
                case EntityState.Added:
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.CreatedBy = "system";
                    break;
                case EntityState.Modified:
                    entity.ModifiedAt = DateTime.UtcNow;
                    entity.ModifiedBy = "system";
                    break;
                default:
                     break;
            }
        }
    }
}