using ViscoveryDemo.BLL.Models;

namespace ViscoveryDemo.DAL.Repositories
{
    public interface IProductApprovalRepository
    {
        bool IsProductApproved(string orderType, int productId);
    }
}
