using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HelloWebApiCoreV2.Context;
using HelloWebApiCoreV2.Models;
using HelloApiWithCoreDapper.Common;
using Newtonsoft.Json;
using HelloWebApiCoreV2.Common.ApiPack;
using HelloApiWithCoreDapper.Context;

namespace HelloWebApiCoreV2.Service
{
    public interface IFruitService
    {
        Task<ApiQuery> FruitQuery(string fields, string sortedColumnint, int skipCount, int maxResultCount);
        Task<FruitDto> FruitQuery(string name);
        Task FruitAdd(List<FruitDto> fruitDtoList);
        Task FruitUpdate(FruitDto fruitDto);
        Task FruitDelete(string name);
    }
    public class FruitService : IFruitService
    {
        string connStr = ApiContext.Current
            .Configuration["Data:DefaultConnection:ConnectionString"];
        private JsonSerializerSettings jsonFormatSettings = new JsonSerializerSettings
        {
            MaxDepth = new int?(1),
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
        };
        public async Task<ApiQuery> FruitQuery(string fields, string sortedColumn, int skipCount, int maxResultCount)
        {
            string fieldSelect = fields ?? "*";
            string sql = $"SELECT {fieldSelect} FROM  Fruit";
            //only if DapperSql2008ResultString
            maxResultCount = skipCount + maxResultCount;
            ++skipCount;
            //end
            string sqlText = string.Format(PagingHelper.DapperSql2008ResultString, sql, sortedColumn, skipCount, maxResultCount);
            SqlConnection conn = await ConnectContext.Current.GetOpenConnection();
            var read = await conn.QueryMultipleAsync(sqlText);
            IEnumerable<dynamic> result = read.Read();
            return new ApiQuery { TotalCount = result.Count(), Items = JsonConvert.SerializeObject(result, Formatting.None, jsonFormatSettings) };
            //  return await Task.Factory.StartNew(()=> JsonConvert.SerializeObject(read.ReadAsync(), Formatting.None, jsonFormatSettings));

        }

        public async Task<FruitDto> FruitQuery(string name)
        {
            string sqlText = "SELECT * FROM Fruit WHERE Name = @Name";
            SqlConnection conn = await ConnectContext.Current.GetOpenConnection();
            return (await conn.QueryAsync<FruitDto>(sqlText, new { Name = name })).FirstOrDefault();

        }

        public async Task FruitAdd(List<FruitDto> fruitDtoList)
        {
            string sqlText = @"INSERT INTO Fruit(Name,Price,Color,Code,StoreCode) 
                                VALUES(@Name,@Price,@Color,@Code,@StoreCode)";
            SqlConnection conn = await ConnectContext.Current.GetOpenConnection();
            using (var tran = conn.BeginTransaction())
            {
                try
                {
                    await conn.ExecuteAsync(sqlText, fruitDtoList, tran);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally
                {
                    tran.Dispose();
                }
            }
        }

        public async Task FruitUpdate(FruitDto fruitDto)
        {
            string sqlText = @"UPDATE Fruit 
                SET Price = @Price,Color=@Color,Name=@Name,StoreCode=@StoreCode
                WHERE Code=@Code";
            SqlConnection conn = await ConnectContext.Current.GetOpenConnection();
            using (var tran = conn.BeginTransaction())
            {
                try
                {
                  await  conn.ExecuteAsync(sqlText, fruitDto, tran);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally
                {
                    tran.Dispose();
                }
            }

        }

        public async Task FruitDelete(string name)
        {
            string sqlText = @"DELETE Fruit 
                WHERE Name =@Name";
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        await conn.ExecuteAsync(sqlText, new { Name = name }, tran);
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        tran.Dispose();
                    }
                }
            }
        }

    }
}
