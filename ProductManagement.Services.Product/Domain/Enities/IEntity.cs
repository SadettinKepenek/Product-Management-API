using System;

namespace ProductManagement.Services.Product.Domain.Enities
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}