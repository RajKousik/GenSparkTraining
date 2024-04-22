using PharmacyManagementModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementBLLibrary
{
    public class LoyaltyService
    {
        private Dictionary<Patient, int> customerPoints = new Dictionary<Patient, int>();
        private const int POINTS_PER_PURCHASE = 10;
        private const int DISCOUNT_THRESHOLD = 100;
        private const double DISCOUNT_PERCENTAGE = 0.1;

        public void RecordPurchase(Patient patient, double purchaseAmount)
        {
            if (!customerPoints.ContainsKey(patient))
            {
                customerPoints[patient] = 0;
            }

            int pointsEarned = (int)(purchaseAmount / DISCOUNT_THRESHOLD) * POINTS_PER_PURCHASE;
            customerPoints[patient] += pointsEarned;
        }


        public bool IsEligibleForDiscount(Patient patient)
        {
            return customerPoints.ContainsKey(patient) && customerPoints[patient] >= POINTS_PER_PURCHASE;
        }

        public double ApplyDiscount(double purchaseAmount, Patient patient)
        {
            return IsEligibleForDiscount(patient) ? purchaseAmount * (1 - DISCOUNT_PERCENTAGE) : purchaseAmount;
        }
    }
}

