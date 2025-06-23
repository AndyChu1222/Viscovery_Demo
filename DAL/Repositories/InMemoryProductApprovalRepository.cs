namespace ViscoveryDemo.DAL.Repositories
{
    public class InMemoryProductApprovalRepository : IProductApprovalRepository
    {
        public bool IsProductApproved(string orderType, int productId)
        {
            switch (orderType)
            {
                case "1":
                    return true; // all pass
                case "2":
                    return false; // all fail
                case "3":
                    return productId != 3; // partial success
                default:
                    return false;
            }
        }
    }
}
