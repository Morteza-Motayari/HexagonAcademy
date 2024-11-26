using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Common
{
    public class BasePaging<T>
    {
        public BasePaging()
        {
            Page = 1;
            TakeEntity = 10;
            HowManyShowPageAfterAndBefore = 5;
            Entities = new List<T>();
        }

        public int Page { get; set; }

        public int PageCount { get; set; }

        public int AllEntitiesCount { get; set; }

        public int StartPage { get; set; }

        public int EndPage { get; set; }

        public virtual int TakeEntity { get; set; }

        public int SkipEntity { get; set; }

        public int HowManyShowPageAfterAndBefore { get; set; }
        public int StartItemFromList {  get; set; }
        public int EndItemFromList {  get; set; }

        public List<T> Entities { get; set; }

        public PagingViewModel GetCurrentPaging()
        {
            return new PagingViewModel
            {
                EndPage = EndPage,
                Page = Page,
                StartPage = StartPage,
                PageCount = PageCount,
                StartItemFromList = StartItemFromList,
                EndItemFromList= EndItemFromList
            };
        }


        public async Task<BasePaging<T>> Paging(IQueryable<T> queryable)
        {
            TakeEntity = TakeEntity;

            var allEntitiesCount = queryable.Count();

            var pageCount = 0;
            try
            {
                pageCount = Convert.ToInt32(Math.Ceiling(allEntitiesCount / (double)TakeEntity));
            }
            catch (Exception)
            {
            }


            Page = Page > pageCount ? pageCount : Page;
            if (Page <= 0) Page = 1;

            AllEntitiesCount = allEntitiesCount;
            HowManyShowPageAfterAndBefore = HowManyShowPageAfterAndBefore;

            SkipEntity = (Page - 1) * TakeEntity;

            StartPage = Page - HowManyShowPageAfterAndBefore <= 0 ? 1 : Page - HowManyShowPageAfterAndBefore;
            EndPage = Page + HowManyShowPageAfterAndBefore > pageCount ? pageCount : Page + HowManyShowPageAfterAndBefore;

            PageCount = pageCount;

            Entities = await Task.Run(() => queryable.Skip(SkipEntity).Take(TakeEntity).ToList());
            StartItemFromList = Page == 1 ? 1 : (Page - 1) * TakeEntity+1;
            EndItemFromList= Page == 1 ? Entities.Count() : (Page - 1) * TakeEntity + Entities.Count();
            return this;
        }

        public BasePaging<T> Paging()
        {
            var allEntitiesCount = Entities.Count;

            var pageCount = 0;
            try
            {
                pageCount = Convert.ToInt32(Math.Ceiling(allEntitiesCount / (double)TakeEntity));
            }
            catch (Exception)
            {
            }

            Page = Page > pageCount ? pageCount : Page;
            if (Page <= 0) Page = 1;
            AllEntitiesCount = allEntitiesCount;
            HowManyShowPageAfterAndBefore = HowManyShowPageAfterAndBefore;
            StartPage = Page - HowManyShowPageAfterAndBefore <= 0 ? 1 : Page - HowManyShowPageAfterAndBefore;
            EndPage = Page + HowManyShowPageAfterAndBefore > pageCount ? pageCount : Page + HowManyShowPageAfterAndBefore;
            PageCount = pageCount;
            StartItemFromList=Page==1?1:(Page -1)*TakeEntity;
            EndItemFromList = Page == 1 ? Entities.Count() : (Page - 1) * TakeEntity + Entities.Count();
            return this;
        }
    }

    public class PagingViewModel
    {
        public int Page { get; set; }

        public int StartPage { get; set; }

        public int EndPage { get; set; }

        public int PageCount { get; set; }
        public int StartItemFromList { get; set; }
        public int EndItemFromList { get; set; }
    }
}
