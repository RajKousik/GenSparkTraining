using PharmacyManagementBLLibrary.Exceptions;
using PharmacyManagementDALLibrary;
using PharmacyManagementModelLibrary;

namespace PharmacyManagementBLLibrary
{
    public class DrugBL : IDrugService
    {
        private readonly IRepository<int, Drugs> _drugRepository;

        public DrugBL()
        {
            _drugRepository = new DrugRepository();
        }

        public int AddDrug(Drugs drug)
        {
            Drugs? result = _drugRepository.Add(drug);
            if (result != null)
            {
                return result.Id;
            }
            throw new DuplicateDrugException();
        }

        public bool DeleteDrug(int id)
        {
            var deletedDrug = _drugRepository.Delete(id);
            if (deletedDrug != null)
            {
                return true;
            }
            throw new DrugNotFoundException();
        }

        public Drugs GetDrugById(int id)
        {
            var result = _drugRepository.Get(id);
            if (result != null)
            {
                return result;
            }
            throw new DrugNotFoundException();
        }

        public Drugs GetDrugByName(string name)
        {
            var drug = _drugRepository.GetAll().Find(d => d.Name == name);
            if (drug == null)
            {
                throw new DrugNotFoundException();
            }
            return drug;
        }

        public List<Drugs> GetDrugList()
        {
            var drugs = _drugRepository.GetAll();
            if (drugs == null)
            {
                throw new DrugNotFoundException();
            }
            return drugs;
        }

        public Drugs UpdateDrug(Drugs drug)
        {
            var result = _drugRepository.Update(drug);
            if (result != null)
            {
                return result;
            }
            throw new DrugNotFoundException();
        }

        public bool RemoveExpiredDrugs()
        {
            bool drugsRemoved = false;

            // Get the current date
            DateTime currentDate = DateTime.Now;

            // Get all drugs from the repository
            List<Drugs> allDrugs = _drugRepository.GetAll();

            // Iterate through each drug
            foreach (var drug in allDrugs)
            {
                // Check if the drug is expired
                if (drug.ExpiryDate < currentDate)
                {
                    // Remove the expired drug from the repository
                    _drugRepository.Delete(drug.Id);
                    drugsRemoved = true;
                }
            }

            return drugsRemoved;
        }

        public bool RemoveOutOfStockDrugs()
        {
            bool drugsRemoved = false;

            // Get all drugs from the repository
            List<Drugs> allDrugs = _drugRepository.GetAll();

            // Iterate through each drug
            foreach (var drug in allDrugs)
            {
                // Check if the drug is out of stock
                if (drug.InStock == 0)
                {
                    // Remove the out-of-stock drug from the repository
                    _drugRepository.Delete(drug.Id);
                    drugsRemoved = true;
                }
            }

            return drugsRemoved;
        }
    }
}
