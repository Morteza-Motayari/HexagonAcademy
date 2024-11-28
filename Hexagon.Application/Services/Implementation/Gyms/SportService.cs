using Hexagon.Application.Generators;
using Hexagon.Application.Services.Interfaces.Gyms;
using Hexagon.Domain.Interfaces.Gyms;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.ViewModels.Gyms.Sports;
using Hexagon.Infra.Data.Repositories.Gyms;

namespace Hexagon.Application.Services.Implementation.Gyms
{
    public class SportService(ISportRepository sportRepository) : ISportService
    {
        public async Task<CreateSportResult> CreateSportAsync(CreateSportViewModel model)
        {
            if (await sportRepository.ExistSportTitle(model.Title))
                return CreateSportResult.DuplicatedTitle;

            Sport sport = new()
            {
                CreatedDate = DateTime.Now,
                Title = model.Title,
                CertificateId = model.CertificateId
            };
            string slug = model.Title.GenerateSlug();
            sport.Slug = slug;

            await sportRepository.InserAsync(sport);
            await sportRepository.SaveChangeAsync();
            return CreateSportResult.Success;
        }

        public async Task<DeleteSportResult> DeleteSportAsync(int SportId)
        {
            var Sport = await sportRepository.GetByIdAsync(SportId);
            if (Sport == null)
                return DeleteSportResult.SportNotFound;

            Sport.IsDeleted = true;
            sportRepository.Update(Sport);
            await sportRepository.SaveChangeAsync();
            return DeleteSportResult.Success;
        }

        public async Task<FilterSportViewModel> FilterSportsAsync(FilterSportViewModel filter)
        => await sportRepository.FilterSportAsync(filter);

        public async Task<UpdateSportViewModel> GetSportForEdit(int SportId)
        {
            var Sport = await sportRepository.GetByIdAsync(SportId);
            if (Sport == null)
                return null;
            return new UpdateSportViewModel()
            {
                Id = Sport.Id,
                Title= Sport.Title,
                CertificateId = Sport.CertificateId
            };
        }

        public async Task<List<SportViewModel>?> ListSportsAsync()
        => await sportRepository.GetAllSportsAsync();

        public async Task<UpdateSportResult> UpdateSportAsync(UpdateSportViewModel model)
        {
            var Sport = await sportRepository.GetByIdAsync(model.Id);
            if (Sport == null)
                return UpdateSportResult.SportNotFound;
            if (await sportRepository.ExistSportTitle(model.Title))
                return UpdateSportResult.DuplicatedTitle;

            #region Update Sport
            Sport.CertificateId = model.CertificateId;
            if (Sport.Title != model.Title)
            {
                Sport.Title = model.Title;
                string slug = model.Title.GenerateSlug();
                Sport.Slug = slug;
            }
            sportRepository.Update(Sport);
            await sportRepository.SaveChangeAsync();
            #endregion

            return UpdateSportResult.Success;
        }

    }
}
