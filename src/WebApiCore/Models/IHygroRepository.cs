using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiCore.Models.HygroApi.Models;

namespace WebApiCore.Models
{
    public interface IHygroRepository
    {
        void Add(HygroItem item);
        Task<List<HygroItem>> GetAll();
        Task<HygroItem> Find();
    }
}