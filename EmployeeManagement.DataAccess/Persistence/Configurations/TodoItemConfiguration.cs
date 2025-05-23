﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EmployeeManagement.Core.Entities;

namespace EmployeeManagement.DataAccess.Persistence.Configurations;

public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.Property(ti => ti.Title)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(ti => ti.Body)
            .HasMaxLength(1000)
            .IsRequired();
    }
}
