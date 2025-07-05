using gnisTask.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gnisTask.Interface
{
    public interface IMeetingRepository
    {
        Task<IEnumerable<CorporateCustomer>> GetCorporateCustomers();
        Task<IEnumerable<IndividualCustomer>> GetIndividualCustomers();
        Task<int> SaveMeetingMaster(MeetingMaster meetingMaster);
        Task<IEnumerable<ProductService>> GetProductsServices();
        Task SaveMeetingDetails(IEnumerable<MeetingDetail> meetingDetails);
    }
}