using gnisTask.Interface;
using gnisTask.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace gnisTask.Repository
{
    public class AddRepository :IAddRepository
    {
        private readonly string _connectionString;

        public AddRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("DefaultConnection connection string is not configured in appsettings.json.");
            }
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<int> InsertCorporateCustomer(CorporateCustomer customer)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CustomerName", customer.CustomerName);
                parameters.Add("@Email", customer.Email);
                parameters.Add("@PhoneNumber", customer.PhoneNumber);

                return await connection.ExecuteScalarAsync<int>(
                    "Insert_Corporate_Customer_SP",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<int> InsertIndividualCustomer(IndividualCustomer customer)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CustomerName", customer.CustomerName);
                parameters.Add("@Email", customer.Email);
                parameters.Add("@PhoneNumber", customer.PhoneNumber);

                return await connection.ExecuteScalarAsync<int>(
                    "Insert_Individual_Customer_SP",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<int> InsertProductService(ProductService product)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ProductName", product.ProductName);
                parameters.Add("@Unit", product.Unit);

                return await connection.ExecuteScalarAsync<int>(
                    "Insert_Product_Service_SP",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }
    }
}
