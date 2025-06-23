using ViscoveryDemo.BLL.Models;

namespace ViscoveryDemo.DAL.Repositories
{
    public interface IUnifiedRecognitionRepository
    {
        ResponseAPIModel<UnifiedRecognitionData> UnifiedRecognition(string orderType);
    }
}
