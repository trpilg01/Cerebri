using Cerebri.Application.Interfaces;
using Cerebri.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerebri.Application.Services
{
    public class MoodService : IMoodService
    {
        private readonly IMoodRepository _moodRepository;

        public MoodService(IMoodRepository moodRepository)
        {
            _moodRepository = moodRepository;
        }

        public async Task<IEnumerable<MoodModel>> GetMoods()
        {
            return await _moodRepository.GetMoods();
        }
    }
}
