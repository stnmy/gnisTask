using gnisTask.Models;

namespace gnisTask.Interface
{
    public interface IAddRepository
    {
        Task<int> InsertCorporateCustomer(CorporateCustomer customer);
        Task<int> InsertIndividualCustomer(IndividualCustomer customer);
        Task<int> InsertProductService(ProductService product);
    }
}
