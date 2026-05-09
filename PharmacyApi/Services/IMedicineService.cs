using PharmacyApi.Models;

namespace PharmacyApi.Services
{
    public interface IMedicineService
    {
        List<Medicine> GetAll();

        Medicine Add(Medicine medicine);
    }
}
