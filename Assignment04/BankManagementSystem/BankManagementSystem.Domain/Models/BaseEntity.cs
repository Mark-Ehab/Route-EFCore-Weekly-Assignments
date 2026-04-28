using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Domain.Models;

public abstract class BaseEntity
{
    /* Properties */
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = String.Empty;
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}