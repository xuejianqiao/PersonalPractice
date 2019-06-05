using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Model;
using MySql.Data;
using System.Data;
using Dapper;

namespace WebApi.Repository
{
    public class CityRepository: ICityRepository
    {
        public bool CreateEntity(City entity, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string insertSql = @"INSERT INTO [dbo].[City]
                                           ([Name]
                                           ,[CountryCode]
                                           ,[District]
                                           ,[Population])
                                     VALUES
                                           (@Name
                                           ,@CountryCode
                                           ,@District
                                           ,@Population)";
                return conn.Execute(insertSql, entity) > 0;
            }
        }

        public bool DeleteEntityById(int id, string connectionString = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<City> RetriveAllEntity(string connectionString = null)
        {
            throw new NotImplementedException();
        }

        public City RetriveOneEntityById(int id, string connectionString = null)
        {
            throw new NotImplementedException();
        }

        public bool UpdateEntity(City entity, string connectionString = null)
        {
            throw new NotImplementedException();
        }
    }
}
