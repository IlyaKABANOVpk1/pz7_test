using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz7_test
{
    public class LoanResult
    {
        public double MonthlyPayment { get; set; }
        public double TotalAmount { get; set; }
        public double Overpayment { get; set; }
    }

    public class DepositResult
    {
        public double Income { get; set; }
        public double TotalAmount { get; set; }
    }


    public class FinancialCalculator
    {
        // --- 1. КРЕДИТНЫЙ КАЛЬКУЛЯТОР ---
        public LoanResult CalculateLoan(double amount, int months, double annualRatePercent)
        {
            double monthlyRate = annualRatePercent / 12 / 100;
            double monthlyPayment;

            if (monthlyRate == 0)
            {
                monthlyPayment = amount / months;
            }
            else
            {
                double k = Math.Pow(1 + monthlyRate, months);
                monthlyPayment = amount * (monthlyRate * k) / (k - 1);
            }

            double totalAmount = monthlyPayment * months;

            return new LoanResult
            {
                MonthlyPayment = Math.Round(monthlyPayment, 2),
                TotalAmount = Math.Round(totalAmount, 2),
                Overpayment = Math.Round(totalAmount - amount, 2)
            };
        }


        public double ConvertCurrency(double amount, string fromCurrency, string toCurrency)
        {
      
            fromCurrency = fromCurrency.ToUpper();
            toCurrency = toCurrency.ToUpper();

            if (fromCurrency == toCurrency) return amount;

          
            double usdToRub = 90.0;
            double eurToRub = 98.5;
            double eurToUsd = 1.09;

            if (fromCurrency == "RUB")
            {
                if (toCurrency == "USD") return Math.Round(amount / usdToRub, 2);
                if (toCurrency == "EUR") return Math.Round(amount / eurToRub, 2);
            }
            else if (fromCurrency == "USD")
            {
                if (toCurrency == "RUB") return Math.Round(amount * usdToRub, 2);
                if (toCurrency == "EUR") return Math.Round(amount / eurToUsd, 2); 
            }
            else if (fromCurrency == "EUR")
            {
                if (toCurrency == "RUB") return Math.Round(amount * eurToRub, 2);
                if (toCurrency == "USD") return Math.Round(amount * eurToUsd, 2);
            }

            throw new ArgumentException("Неподдерживаемая валютная пара");
        }


        public DepositResult CalculateDeposit(double amount, int months, double annualRatePercent, bool isCapitalized)
        {
            double totalAmount;
            double income;

            if (isCapitalized)
            {
              
                double monthlyRate = annualRatePercent / 100 / 12;
                totalAmount = amount * Math.Pow(1 + monthlyRate, months);
            }
            else
            {
         
                double simpleIncome = (amount * annualRatePercent * months) / (12 * 100);
                totalAmount = amount + simpleIncome;
            }

            income = totalAmount - amount;

            return new DepositResult
            {
                Income = Math.Round(income, 2),
                TotalAmount = Math.Round(totalAmount, 2)
            };
        }
    }
}
