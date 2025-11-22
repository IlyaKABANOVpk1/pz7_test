using pz7_test;

namespace pz7test
{
    public class UnitTest1
    {
    
        private readonly FinancialCalculator _calculator = new FinancialCalculator();

        // ТЕСТ 1: Проверка расчета аннуитетного платежа (Кредит)
        [Fact]
        public void CalculateLoan_StandardValues_ReturnsCorrectPayment()
        {
   
            double amount = 100000;
            int months = 12;
            double rate = 10; // 10%

     
            var result = _calculator.CalculateLoan(amount, months, rate);

           
            Assert.Equal(8791.59, result.MonthlyPayment);
            Assert.Equal(105499.06, result.TotalAmount);
        }

        // ТЕСТ 2: Проверка беспроцентного кредита (деление на 0)
        [Fact]
        public void CalculateLoan_ZeroInterest_ReturnsSimpleDivision()
        {
      
            double amount = 120000;
            int months = 12;
            double rate = 0; 

          
            var result = _calculator.CalculateLoan(amount, months, rate);

            
            Assert.Equal(10000.00, result.MonthlyPayment);
            Assert.Equal(0, result.Overpayment);
        }

        // ТЕСТ 3: Проверка конвертации валюты (USD -> RUB)
        [Fact]
        public void ConvertCurrency_UsdToRub_ReturnsCorrectAmount()
        {
           
            double amount = 100;
            string from = "USD";
            string to = "RUB";

            double result = _calculator.ConvertCurrency(amount, from, to);

            Assert.Equal(9000.00, result);
        }

        // ТЕСТ 4: Проверка кросс-курса (EUR -> USD)
        [Fact]
        public void ConvertCurrency_EurToUsd_ReturnsCorrectAmount()
        {
            // Arrange
            double amount = 100;
            string from = "EUR";
            string to = "USD";

            // Act
            double result = _calculator.ConvertCurrency(amount, from, to);

            // Assert
            // 100 * 1.09 = 109.00
            Assert.Equal(109.00, result);
        }

        // ТЕСТ 5: Проверка вклада с капитализацией
        [Fact]
        public void CalculateDeposit_WithCapitalization_ReturnsCompoundInterest()
        {
            // Arrange
            double amount = 100000;
            int months = 12;
            double rate = 12; // 12% годовых (1% в месяц)
            bool capitalize = true;

            // Act
            var result = _calculator.CalculateDeposit(amount, months, rate, capitalize);

            // Assert
            // Формула: 100000 * (1.01)^12 ? 112682.50
            Assert.Equal(112682.50, result.TotalAmount);
            Assert.Equal(12682.50, result.Income);
        }
    }
}