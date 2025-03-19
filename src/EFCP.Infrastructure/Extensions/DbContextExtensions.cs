using EFCP.Infrastructure.Common;
using EFCP.Infrastructure.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace EFCP.Infrastructure.Extensions
{
    public static class DbContextExtensions
    {
        public static bool CreateTempTable(this ImdbDbContext dbContext, string tempTable, List<int> listWhereInIds)
        {
            bool result = false;
            if (listWhereInIds.IsNullOrEmpty()) return result;

            var simpleLookups = new List<TempTable>(listWhereInIds.Distinct().Select(x => new TempTable { Id = x }).ToList());
            var columns = new List<string> { nameof(TempTable.Id) };
            try
            {
                var sqlCon = (SqlConnection)dbContext.Database.GetDbConnection();
                var sqlCommand = $@"IF OBJECT_ID(N'tempdb..{tempTable}') IS NOT NULL DROP TABLE {tempTable} ; 
                                    CREATE TABLE {tempTable} ({nameof(TempTable.Id)} int, PRIMARY KEY ({nameof(TempTable.Id)}))";

                if (sqlCon.State != ConnectionState.Open)
                {
                    dbContext.Database.OpenConnection();
                }
                dbContext.Database.ExecuteSqlRaw(sqlCommand);
                using (var bulkcopy = new SqlBulkCopy(sqlCon))
                {
                    bulkcopy.BulkCopyTimeout = DatabaseConstants.BULK_COPY_TIMEOUT;
                    bulkcopy.DestinationTableName = tempTable;
                    var dataTable = DataTableHelpers.GetDataTableFromFields(simpleLookups, bulkcopy);
                    bulkcopy.WriteToServer(dataTable);
                    bulkcopy.Close();
                }
                result = true;
            }
            catch (Exception)
            {
                dbContext.Database.CloseConnection();
                result = false;
            }

            return result;
        }

        public static bool DeleteTempTable(this ImdbDbContext dbContext, string tempTable)
        {
            bool result;
            try
            {
                SqlConnection sqlCon = (SqlConnection)dbContext.Database.GetDbConnection();
                var sqlCommand = $"IF OBJECT_ID(N'tempdb..{tempTable}') IS NOT NULL DROP TABLE {tempTable}";
                if (sqlCon.State != ConnectionState.Open)
                {
                    dbContext.Database.OpenConnection();
                }
                dbContext.Database.ExecuteSqlRaw(sqlCommand);
                if (sqlCon.State == ConnectionState.Open)
                {
                    dbContext.Database.CloseConnection();
                }
                result = true;
            }
            catch (Exception)
            {
                dbContext.Database.CloseConnection();
                result = false;
            }

            return result;
        }

        public static async Task<bool> CreateTempTableAsync(this ImdbDbContext dbContext, string tempTable, List<int> listWhereInIds)
        {
            bool result = false;
            if (listWhereInIds.IsNullOrEmpty()) return result;

            var simpleLookups = new List<TempTable>(listWhereInIds.Distinct().Select(x => new TempTable { Id = x }).ToList());
            var columns = new List<string> { nameof(TempTable.Id) };
            try
            {
                var sqlCon = (SqlConnection)dbContext.Database.GetDbConnection();
                var sqlCommand = $@"IF OBJECT_ID(N'tempdb..{tempTable}') IS NOT NULL DROP TABLE {tempTable} ; 
                                    CREATE TABLE {tempTable} ({nameof(TempTable.Id)} int, PRIMARY KEY ({nameof(TempTable.Id)}))";

                if (sqlCon.State != ConnectionState.Open)
                {
                    await dbContext.Database.OpenConnectionAsync();
                }
                await dbContext.Database.ExecuteSqlRawAsync(sqlCommand);
                using (var bulkcopy = new SqlBulkCopy(sqlCon))
                {
                    bulkcopy.BulkCopyTimeout = DatabaseConstants.BULK_COPY_TIMEOUT;
                    bulkcopy.DestinationTableName = tempTable;
                    var dataTable = DataTableHelpers.GetDataTableFromFields(simpleLookups, bulkcopy);
                    await bulkcopy.WriteToServerAsync(dataTable);
                    bulkcopy.Close();
                }
                result = true;
            }
            catch (Exception)
            {
                await dbContext.Database.CloseConnectionAsync();
                result = false;
            }

            return result;
        }

        public static async Task<bool> DeleteTempTableAsync(this ImdbDbContext dbContext, string tempTable)
        {
            bool result;
            try
            {
                SqlConnection sqlCon = (SqlConnection)dbContext.Database.GetDbConnection();
                var sqlCommand = $"IF OBJECT_ID(N'tempdb..{tempTable}') IS NOT NULL DROP TABLE {tempTable}";
                if (sqlCon.State != ConnectionState.Open)
                {
                    await dbContext.Database.OpenConnectionAsync();
                }
                await dbContext.Database.ExecuteSqlRawAsync(sqlCommand);
                if (sqlCon.State == ConnectionState.Open)
                {
                    await dbContext.Database.CloseConnectionAsync();
                }
                result = true;
            }
            catch (Exception)
            {
                await dbContext.Database.CloseConnectionAsync();
                result = false;
            }

            return result;
        }
    }
}
