using DataMseAPI.Model;

namespace DataMseAPI.Repository.Interface
{
    public interface IMseDataRepository
    {
        Task<IEnumerable<MseData>> GetAllDataAsync();
        Task<IEnumerable<String>> GetCodesAsync();
        Task<IEnumerable<MseData>> GetDataByCodeAsync(String code);
        Task<IEnumerable<DateClosePrice>> GetClosePrice(string code);
    }
}
