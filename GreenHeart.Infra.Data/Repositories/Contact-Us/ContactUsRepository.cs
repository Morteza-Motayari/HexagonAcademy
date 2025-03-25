using GreenHeart.Domain.Enums.Filter;
using GreenHeart.Domain.Interfaces.Contact_Us;
using GreenHeart.Domain.Models.Contact_Us;
using GreenHeart.Domain.ViewModels.Contact_Us;
using GreenHeart.Domain.ViewModels.Gyms.Sports;
using GreenHeart.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Infra.Data.Repositories.Contact_Us
{
    public class ContactUsRepository:GenericRepository<ContactUs>, IContactUsRepository
    {
        private readonly GreenHeartContext _db;

        public ContactUsRepository(GreenHeartContext db):base(db)
        {
            _db = db;
        }

        public async Task<FilterContactUsViewModel> FilterContactUsAsync(FilterContactUsViewModel filter)
        {
            var query = _db.ContactUs.Include(x => x.AnsweredUser).AsQueryable();

            #region Filter Search
            switch (filter.Status)
            {
                case ExistingStatus.All:
                    break;
                case ExistingStatus.Deleted:
                    query = query.Where(u => u.IsDeleted == true);
                    break;
                case ExistingStatus.NotDeleted:
                    query = query.Where(u => !u.IsDeleted);
                    break;
            }

            switch (filter.AnswerStatus)
            {
                case FilterContactUsStatus.All:
                    break;
                case FilterContactUsStatus.Answered:
                    query = query.Where(u => u.IsAnswered == true);
                    break;
                case FilterContactUsStatus.UnAnswered:
                    query = query.Where(u => !u.IsAnswered);
                    break;
            }

            if (filter.Subject != null)
            {
                query = query.Where(r => r.Subject.Contains(filter.Subject));
            }
            if (filter.FullName != null)
            {
                query = query.Where(r => r.FullName.Contains(filter.FullName));
            }
            if (filter.AnsweredUser != null)
            {
                query = query.Where(r => r.AnsweredUser.FirstName.Contains(filter.AnsweredUser)|| r.AnsweredUser.LastName.Contains(filter.AnsweredUser)).Distinct();
            }
            if (filter.Email != null)
            {
                query = query.Where(r => r.Email.Contains(filter.Email));
            }
            #endregion

            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u => new ContactUsViewModel
            {
                Id = u.Id,
                CreatedDate = u.CreatedDate,
                IsDeleted = u.IsDeleted,
                Subject = u.Subject,
                FullName = u.FullName,
                Email = u.Email,
                AnsweredDate = u.CreatedDate,
                IsAnswered = u.IsAnswered,
                Phone = u.Phone,
                UserAnswered=u.AnsweredUser.FirstName+" "+u.AnsweredUser.LastName
            }));
            return filter;
        }

        public async Task<DateTime> GetLastModifiedDate(int id)
        => await _db.ContactUs.Where(d=>d.Id == id).Select(s=>(DateTime)s.LastModifiedDate).FirstAsync();
    }
}
