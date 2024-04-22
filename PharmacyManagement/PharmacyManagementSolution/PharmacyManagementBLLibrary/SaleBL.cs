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

        public SaleBL()
        {
            _saleRepository = new SaleRepository();
        }

        public int AddSale(Sales sale)
        {
            Sales result = _saleRepository.Add(sale);
            if (result != null)
            {
                return result.SaleId;
            }
            throw new DuplicateSaleException();
        }

        public Sales GetSaleById(int id)
        {
            Sales sale = _saleRepository.Get(id);
            if (sale != null)
            {
                return sale;
            }
            throw new SaleNotFoundException();
        }

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
