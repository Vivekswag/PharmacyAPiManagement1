using PharmacyApi.Models;
using Newtonsoft.Json;

namespace PharmacyApi.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly string _filePath;

        public MedicineService()
        {
            _filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "Data",
                "medicines.json");
        }

        public List<Medicine> GetAll()
        {
            if (!File.Exists(_filePath))
                return new List<Medicine>();

            var json = File.ReadAllText(_filePath);

            return JsonConvert.DeserializeObject<List<Medicine>>(json)
                   ?? new List<Medicine>();
        }

        public Medicine Add(Medicine medicine)
        {
            var medicines = GetAll();
            Medicine exitingMedicine = medicines.FirstOrDefault(m => m.FullName == medicine.FullName && m.ExpiryDate == medicine.ExpiryDate
            && m.Price == medicine.Price && m.Brand == medicine.Brand);
            if (exitingMedicine!=null)
            {
                medicines.Find(m=>m.Id == exitingMedicine.Id).Quantity = exitingMedicine.Quantity + medicine.Quantity;
                SaveData(medicines);
                return medicine;
            }
            medicine.Id = medicines.Count > 0
                ? medicines.Max(x => x.Id) + 1
                : 1;

            medicines.Add(medicine);

            SaveData(medicines);

            return medicine;
        }

        private void SaveData(List<Medicine> medicines)
        {
            var json = JsonConvert.SerializeObject(medicines, Formatting.Indented);

            File.WriteAllText(_filePath, json);
        }

    }
}
