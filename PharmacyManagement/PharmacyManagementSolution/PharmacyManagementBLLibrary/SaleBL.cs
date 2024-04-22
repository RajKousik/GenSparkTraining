using PharmacyManagementBLLibrary.Exceptions;
using PharmacyManagementDALLibrary;
using PharmacyManagementModelLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementBLLibrary
{
    public class SaleBL : ISaleService
    {
        private readonly IRepository<int, Sales> _saleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaleBL"/> class.
        /// </summary>
        public SaleBL()
        {
            _saleRepository = new SaleRepository();
        }

        /// <summary>
        /// Adds a new sale transaction to the repository.
        /// </summary>
        /// <param name="sale">The sale object to add.</param>
        /// <returns>Returns the ID of the newly added sale.</returns>
        public int AddSale(Sales sale)
        {
            Sales result = _saleRepository.Add(sale);
            if (result != null)
            {
                return result.SaleId;
            }
            throw new DuplicateSaleException();
        }

        /// <summary>
        /// Retrieves a sale transaction by its ID.
        /// </summary>
        /// <param name="id">The ID of the sale transaction to retrieve.</param>
        /// <returns>Returns the sale transaction with the specified ID.</returns>
        public Sales GetSaleById(int id)
        {
            Sales sale = _saleRepository.Get(id);
            if (sale != null)
            {
                return sale;
            }
            throw new SaleNotFoundException();
        }

        /// <summary>
        /// Retrieves a list of all sale transactions stored in the repository.
        /// </summary>
        /// <returns>Returns a list of all sale transactions.</returns>
        public List<Sales> GetAllSales()
        {
            List<Sales> sales = _saleRepository.GetAll();
            if (sales != null)
            {
                return sales;
            }
            throw new SaleNotFoundException();
        }
    }
}
