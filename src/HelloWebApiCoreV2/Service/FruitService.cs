using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HelloWebApiCoreV2.Context;
using HelloWebApiCoreV2.Models;

namespace HelloWebApiCoreV2.Service
{
    public interface IFruitService
    {
        Task<IEnumerable<FruitDto>> FruitQuery();
        Task<FruitDto> FruitQuery(string name);
        void FruitAdd(List<FruitDto> fruitDtoList);
        void FruitUpdate(FruitDto fruitDto);
        void FruitDelete(string name);
    }
    public class FruitService : IFruitService
    {
        string connStr = DapperContext.Current
            .Configuration["Data:DefaultConnection:ConnectionString"];
        public async Task<IEnumerable<FruitDto>> FruitQuery()
        {
            string sqlText = @"SELECT * FROM  Fruit";
            using (var conn = new SqlConnection(connStr))
            {
                return await conn.QueryAsync<FruitDto>(sqlText);
            }
        }

        public async Task<FruitDto> FruitQuery(string name)
        {
            string sqlText = "SELECT * FROM Fruit WHERE Name = @Name";
            using (var conn = new SqlConnection(connStr))
            {
                return (await conn.QueryAsync<FruitDto>(sqlText, new { Name=name})).FirstOrDefault();
            }
        }

        public void FruitAdd(List<FruitDto> fruitDtoList)
        {
            string sqlText = @"INSERT INTO Fruit(Name,Price,Color,Code,StoreCode) 
                                VALUES(@Name,@Price,@Color,@Code,@StoreCode)";
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        conn.Execute(sqlText, fruitDtoList, tran);
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

        public void FruitUpdate(FruitDto fruitDto)
        {
            string sqlText = @"UPDATE Fruit 
                SET Price = @Price,Color=@Color,Code=@Code,StoreCode=@StoreCode
                WHERE Name=@Name";
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        conn.Execute(sqlText, fruitDto, tran);
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

        public void FruitDelete(string name)
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
                        conn.Execute(sqlText, new { Name=name}, tran);
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
