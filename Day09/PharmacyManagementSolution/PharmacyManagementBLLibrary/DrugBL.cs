using PharmacyManagementBLLibrary.Exceptions;
using PharmacyManagementDALLibrary;
using PharmacyManagementModelLibrary;

namespace PharmacyManagementBLLibrary
{
    public class DrugBL : IDrugService
    {
        private readonly IRepository<int, Drugs> _drugRepository;

        
        /// /// <summary>
        /// Initializes a new instance of the <see cref="DrugBL"/> class.
        /// </summary>
        public DrugBL()
        {
            _drugRepository = new DrugRepository();
        }

        /// <summary>
        /// Adds a new drug to the repository.
        /// </summary>
        /// <param name="drug">The drug object to add.</param>
        /// <returns>Returns the ID of the newly added drug.</returns>
        public int AddDrug(Drugs drug)
        {
            Drugs? result = _drugRepository.Add(drug);
            if (result != null)
            {
                return result.Id;
            }
            throw new DuplicateDrugException();
        }

        /// <summary>
        /// Deletes a drug from the repository.
        /// </summary>
        /// <param name="id">The ID of the drug to delete.</param>
        /// <returns>Returns true if the drug is successfully deleted; otherwise, false.</returns>
        public bool DeleteDrug(int id)
        {
            var deletedDrug = _drugRepository.Delete(id);
            if (deletedDrug != null)
            {
                return true;
            }
            throw new DrugNotFoundException();
        }

        /// <summary>
        /// Retrieves a drug by its ID.
        /// </summary>
        /// <param name="id">The ID of the drug to retrieve.</param>
        /// <returns>Returns the drug with the specified ID.</returns>
        public Drugs GetDrugById(int id)
        {
            var result = _drugRepository.Get(id);
            if (result != null)
            {
                return result;
            }
            throw new DrugNotFoundException();
        }

        /// <summary>
        /// Retrieves a drug by its name.
        /// </summary>
        /// <param name="name">The name of the drug to retrieve.</param>
        /// <returns>Returns the drug with the specified name.</returns>
        public Drugs GetDrugByName(string name)
        {
            var drug = _drugRepository.GetAll().Find(d => d.Name == name);
            if (drug == null)
            {
                throw new DrugNotFoundException();
            }
            return drug;
        }

        /// <summary>
        /// Retrieves a list of all drugs stored in the repository.
        /// </summary>
        /// <returns>Returns a list of all drugs.</returns>
        public List<Drugs> GetDrugList()
        {
            var drugs = _drugRepository.GetAll();
            if (drugs == null)
            {
                throw new DrugNotFoundException();
            }
            return drugs;
        }

        /// <summary>
        /// Updates the information of a drug in the repository.
        /// </summary>
        /// <param name="drug">The updated drug object.</param>
        /// <returns>Returns the updated drug.</returns>
        public Drugs UpdateDrug(Drugs drug)
        {
            var result = _drugRepository.Update(drug);
            if (result != null)
            {
                return result;
            }
            throw new DrugNotFoundException();
        }

        /// <summary>
        /// Removes expired drugs from the repository.
        /// </summary>
        /// <returns>Returns true if expired drugs are successfully removed; otherwise, false.</returns>
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

        /// <summary>
        /// Removes out-of-stock drugs from the repository.
        /// </summary>
        /// <returns>Returns true if out-of-stock drugs are successfully removed; otherwise, false.</returns>
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

        /// <summary>
        /// Reduces the quantity of a drug in stock after it's been sold.
        /// </summary>
        /// <param name="drugName">The name of the drug to reduce stock quantity.</param>
        public void ReduceStockQuantity(string drugName)
        {
            var drug = _drugRepository.GetAll().FirstOrDefault(d => d.Name.Equals(drugName, StringComparison.OrdinalIgnoreCase));

            if (drug != null)
            {
                if (drug.InStock > 0)
                {
                    drug.InStock--;
                    _drugRepository.Update(drug);
                }
                else
                {
                    throw new OutOfStockException();
                }
            }
            else
            {
                throw new DrugNotFoundException();
            }
        }
    }
}
