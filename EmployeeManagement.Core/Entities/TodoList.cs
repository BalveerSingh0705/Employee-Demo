﻿using EmployeeManagement.Core.Common;

namespace EmployeeManagement.Core.Entities
{
    public class TodoList : BaseEntity, IAuditedEntity
    {
        public string Title { get; set; }

        public List<TodoItem> Items { get; } = new List<TodoItem>();

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
