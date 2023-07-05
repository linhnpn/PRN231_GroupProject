using GroupProject_HRM_Library.DTOs.Bonus;

namespace GroupProject_HRM_Library.Repository.Interface
{
    public interface IBonusRepository
    {
        public Task CreateBonusAsync(BonusRequest bonus);
    }
}
