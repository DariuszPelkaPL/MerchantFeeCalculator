using System;
using Common.SimpleDependencyInjector;

namespace MerchantFeeCalculator
{
    using MerchantFeeCalculationEngine;
    using MerchantFeeCalculationEngine.Model;

    public class Program
    {
        public static void Main(string[] args)
        {
            var calculator = (IFeeCalculator)DependencyInjector.CreateInstance(typeof(IFeeCalculator));
            if (calculator != null)
            {
                var processedTransaction = calculator.CalculateFee(new Transaction() {Owner = new Merchant()}, 1);
            }
            Console.WriteLine("Hello World!");
        }
    }
}
