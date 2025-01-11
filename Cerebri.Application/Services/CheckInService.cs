using Cerebri.Application.Interfaces;
using Cerebri.Domain.Entities;

namespace Cerebri.Application.Services
{
    public class CheckInService : ICheckInService
    {
        private readonly ICheckInRepository _checkInRepository;
        private readonly IMoodRepository _moodRepository;
        private readonly IUserRepository _userRepository;

        public CheckInService(ICheckInRepository checkInRepository, IMoodRepository moodRepository, IUserRepository userRepository)
        {
            _checkInRepository = checkInRepository;
            _moodRepository = moodRepository;
            _userRepository = userRepository;
        }

        public async Task CreateCheckIn(CheckInModel checkIn)
        {
            await _checkInRepository.InsertAsync(checkIn);
        }

        public async Task DeleteCheckIn(Guid checkInId)
        {
            await _checkInRepository.DeleteAsync(checkInId);
        }

        public async Task<IEnumerable<CheckInResponseModel?>> GetChecksInByUserId(Guid userId)
        {
            var checkIns = await _checkInRepository.GetByUserIdAsync(userId);

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
