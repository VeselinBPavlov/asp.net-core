namespace Panda.Services.Interfaces
{
    using Panda.Domain.Models;
    using System.Linq;
    
    public interface IReceiptsService
    {
        void CreateFromPackage(decimal weight, string packageId, string recipientId);

        IQueryable<Receipt> GetAll();
    }
}
