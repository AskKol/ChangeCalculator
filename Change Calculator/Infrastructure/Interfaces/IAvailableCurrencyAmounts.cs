using System.Collections.Generic;

namespace ChangeCalculator.Infrastructure.Interfaces
{
    /// <summary>
    /// An interface to use for working with or extending the available currency amount(s).
    /// </summary>
    public interface IAvailableCurrencyAmounts
    {
        /// <summary>
        /// A method to use to add a single currency amount.
        /// </summary>
        /// <param name="amount"></param>
        void AddToAvailableCurrencyAmounts(decimal amount);

        /// <summary>
        /// A method to use to remove a single currency amount.<br/>
        /// </summary>
        /// <param name="amount"></param>
        void RemoveFromAvailableCurrencyAmounts(decimal amount);

        /// <summary>
        /// A method to use to get the available currency amount(s).
        /// </summary>
        /// <returns></returns>
        List<decimal> GetAvailableCurrencyAmounts();
    }
}