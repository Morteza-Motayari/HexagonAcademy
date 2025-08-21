using GreenHeart.Domain.ViewModels.Essays;
using GreenHeart.Domain.ViewModels.Essays.Essays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Services.Interfaces.Essays
{
    public interface IEssayService
    {
        #region Admin
        Task<CreateEssayResult> CreateEssayAsync(CreateEssayViewModel model);
        Task<UpdateEssayViewModel> GetEssayForEdit(int EssayId);
        Task<UpdateEssayResult> UpdateEssayAsync(UpdateEssayViewModel model);
        Task<DeleteEssayResult> DeleteEssayAsync(int EssayId);
        Task<FilterEssayViewModel> FilterEssaysAsync(FilterEssayViewModel filter);
        Task<AdminSideDetailEssayViewModel?> AdminSideDetailEssayAsync(int EssayId);
        Task<DeleteForeverEssayResult> DeleteEssayForever(int EssayId);
        Task<string> CantDeleteEssayForeverNowMessage(int EssayId);
        #endregion

        #region Client
        Task<ClientSideFilterEssayViewModel> FilterClientSideEssayAsync(ClientSideFilterEssayViewModel Filter);
        Task<ClientSideEssayDetaillViewModel> GetClientSideEssayBySlugAsync(string slug);

        #endregion

    }
}
