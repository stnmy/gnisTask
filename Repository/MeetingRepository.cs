using Dapper;
using gnisTask.Interface;
using gnisTask.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq; // Required for .Any()

namespace gnisTask.Repository
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly string _connectionString;

        public MeetingRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("DefaultConnection connection string is not configured in appsettings.json.");
            }
        }

        public async Task<IEnumerable<CorporateCustomer>> GetCorporateCustomers()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<CorporateCustomer>(
                "Get_Corporate_Customers_SP",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<IndividualCustomer>> GetIndividualCustomers()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<IndividualCustomer>(
                "Get_Individual_Customers_SP",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<ProductService>> GetProductsServices()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<ProductService>(
                "Get_Products_Services_SP",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> SaveMeetingMaster(MeetingMaster meetingMaster)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@CustomerType", meetingMaster.CustomerType);
            parameters.Add("@CustomerId", meetingMaster.CustomerId);
            parameters.Add("@MeetingDate", meetingMaster.MeetingDate);
            parameters.Add("@MeetingTime", meetingMaster.MeetingTime);
            parameters.Add("@MeetingPlace", meetingMaster.MeetingPlace);
            parameters.Add("@AttendsFromClientSide", meetingMaster.AttendsFromClientSide);
            parameters.Add("@AttendsFromHostSide", meetingMaster.AttendsFromHostSide);
            parameters.Add("@MeetingAgenda", meetingMaster.MeetingAgenda);
            parameters.Add("@MeetingDiscussion", meetingMaster.MeetingDiscussion);
            parameters.Add("@MeetingDecision", meetingMaster.MeetingDecision);

            var meetingId = await connection.ExecuteScalarAsync<int>(
                "Meeting_Minutes_Master_Save_SP",
                parameters,
                commandType: CommandType.StoredProcedure
            );
            return meetingId;
        }

        public async Task SaveMeetingDetails(IEnumerable<MeetingDetail> meetingDetails)
        {
            if (meetingDetails == null || !meetingDetails.Any())
            {
                return;
            }

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var transaction = await connection.BeginTransactionAsync();
            try
            {
                foreach (var detail in meetingDetails)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@MeetingId", detail.MeetingId);
                    parameters.Add("@ProductId", detail.ProductId);
                    parameters.Add("@ProductNameDisplay", detail.ProductName); 
                    parameters.Add("@Quantity", detail.Quantity);
                    parameters.Add("@Unit", detail.Unit);

                    await connection.ExecuteAsync(
                        "Meeting_Minutes_Details_Save_SP",
                        parameters,
                        transaction: transaction,
                        commandType: CommandType.StoredProcedure
                    );
                }

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw; 
            }
        }
    }
}