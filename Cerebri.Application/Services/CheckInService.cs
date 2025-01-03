using Cerebri.Application.Interfaces;
using Cerebri.Domain.Entities;

namespace Cerebri.Application.Services
{
    public class CheckInService : ICheckInService
    {
        private readonly ICheckInRepository _checkInRepository;
        private readonly IMoodRepository _moodRepository;
        
        public CheckInService(ICheckInRepository checkInRepository, IMoodRepository moodRepository)
        {
            _checkInRepository = checkInRepository;
            _moodRepository = moodRepository;
        }

        public async Task CreateCheckInAsync(CheckInModel checkIn)
        {
            await _checkInRepository.InsertAsync(checkIn);
        }

        public async Task DeleteCheckInAsync(Guid checkInId)
        {
            await _checkInRepository.DeleteAsync(checkInId);
        }

        public async Task<IEnumerable<CheckInResponseModel?>> GetCheckInByUserIdAsync(Guid userId)
        {
            var checkIns = await _checkInRepository.GetByUserIdAsync(userId);

            if (checkIns == null)
            {
                return new List<CheckInResponseModel>();
            }

            List<CheckInResponseModel> responseModels = new List<CheckInResponseModel>();

            foreach (var checkIn in checkIns)
            {
                if (checkIn != null)
                {
                    var moodIds = checkIn.MoodTags?.Select(x => x.MoodId).ToList() ?? new List<int>();
                    var moods = await _moodRepository.GetMoodsByIdAsync(moodIds);
                    CheckInResponseModel responseModel = new CheckInResponseModel();

                    responseModel.Id = checkIn.Id;
                    responseModel.Content = checkIn.Content;
                    responseModel.Moods = moods;
                    responseModel.CreatedAt = checkIn.CreatedAt;

                    responseModels.Add(responseModel);
                }
            }

            return responseModels;
        }
    }
}
