using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp.Services
{
    public interface ICosmosService
    {
        Task AddItemAsync(Quotes item);
        Task DeleteItemAsync(string id);
        Task<Quotes> GetItemAsync(string id);
        Task<IEnumerable<Quotes>> GetAllItem(string queryString);
        Task UpdateItem(string id, Quotes item);
    }
}
