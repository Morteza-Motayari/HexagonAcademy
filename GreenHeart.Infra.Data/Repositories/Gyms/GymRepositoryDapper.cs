using Dapper;
using GreenHeart.Domain.Interfaces.Gyms;
using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.ViewModels.Gyms.Gyms;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Infra.Data.Repositories.Gyms
{
    public class GymRepositoryDapper : IGymRepository
    {
        private readonly IDbConnection db;
        public GymRepositoryDapper(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("AcademyConnectionStrings"));
        }
        public void Delete(Gym entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistConstantPhoneNumberAsync(string constantphone)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistConstantPhoneNumberAsync(string constantphone, int gymId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistGymAsync(int gymId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistSpecificSlug(string slug)
        {
            throw new NotImplementedException();
        }

        public Task<FilterGymViewModel> FilterGymAsync(FilterGymViewModel filter)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Gym>?> GetAllAsync()
        {
            string query = "SELECT * FROM Gyms";
            return (await db.QueryAsync<Gym>(query)).ToList();
        }

        public Task<List<GymViewModel>?> GetAllGymItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<GymViewModel>?> GetAllGymsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Gym?> GetByIdAsync(int id)
        {
            string query = "Select * From Gyms Where Id=@id";
            return (await db.QueryAsync<Gym>(query, new { @id = id })).Single();
        }

        public string GetGymName(int gymId)
        {
            string query = "Select Name From Gyms Where Id=@id";
            return db.Query<string>(query, new { @id = gymId }).Single();
        }

        public Task<DateTime> GetLastModifiedDate(int id)
        {
            throw new NotImplementedException();
        }

        public async Task InserAsync(Gym entity)
        {
            string query = "Insert Into Gyms (Name,Slug,Address,Area,ConstantPhone,ImageUrl,IsDeleted) Values (@Name,@Slug,@Address,@Area,@ConstantPhone,@ImageUrl,@IsDeleted)"
                + "Selsect Cast(Scope_Identity() as int)";
            var id =(await db.QueryAsync<int>(query, new
            {
                entity.Name,
                entity.Slug,
                entity.Address,
                entity.Area,
                entity.ConstantPhone,
                entity.ImageUrl,
                entity.IsDeleted,
            })).Single();
            entity.Id = id;

        }

        public Task<string> PutSpecificSlug(string slug)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangeAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(Gym entity)
        {
            throw new NotImplementedException();
        }
    }
}
