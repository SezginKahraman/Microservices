using Dapper;
using Microservices.Services.Discount.Models;
using Microservices.Shared.Core_3_1.Dtos;
using Npgsql;
using System.Data;

namespace Microservices.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            this._configuration = configuration;
            dbConnection = new NpgsqlConnection(configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var status = await dbConnection.ExecuteAsync("delete from discount where id=@Id", new { Id = id });
            if (status > 0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("an error occured while deleting data from postgreSql", 500);
        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discounts = await dbConnection.QueryAsync<Models.Discount>("Select * from discount");

            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discounts = await dbConnection.QueryAsync<Models.Discount>("select * from discount where userid=@UserId and code=@Code", new {UserId = userId, Code = code});
            var hasDiscount = discounts.FirstOrDefault();

            return hasDiscount == null ? Response<Models.Discount>.Fail("Discount not found", 404) : Response<Models.Discount>.Success(hasDiscount, 200);
        }

        public async Task<Response<Models.Discount>> GetById(int id)
        {
            var discount = (await dbConnection.QueryAsync<Models.Discount>("Select * from discount where id=@Id", new {Id = id })).SingleOrDefault();

            if (discount == null) return Response<Models.Discount>.Fail("discount not found", 404);

            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(Models.Discount discount)
        {
            var status = await dbConnection.ExecuteAsync("INSERT INTO discount {userid, rate, code} VALUES(@UserId,@Rate,@Code)", new {UserId = discount.UserId, Rate = discount.Rate, Code = discount.Code});

            if (status > 0)
            {
                return Response<NoContent>.Success(2040);
            }

            return Response<NoContent>.Fail("an error occured while adding data to postgreSql", 500);
        }

        public async Task<Response<NoContent>> Update(Models.Discount discount)
        {
            var status = await dbConnection.ExecuteAsync("update discount set userid=@UserId, rate=@Rate, code=@Code where  VALUES(@UserId,@Rate,@Code)", discount);

            if (status > 0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("an error occured while updating", 500);
        }
    }
}
