namespace Panda.Services.Interfaces
{
    using Panda.Domain.Models;
    using System.Linq;

    public interface IPackagesService
    {
        void Create(string description, decimal weight, string shippingAddress, string recipientName);

        IQueryable<Package> GetAllByStatus(PackageStatus status);
        void Deliver(string id);
    }
}
