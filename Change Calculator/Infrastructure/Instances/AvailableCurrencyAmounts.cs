using System;
using System.Collections.Generic;
using System.Linq;
using ChangeCalculator.Infrastructure.Interfaces;

namespace ChangeCalculator.Infrastructure.Instances
{
    /// <summary>
    /// A class to use to work with or and hold currency amounts.<br/>
    /// It does this by implementing IAvailableCurrencyAmounts
    /// </summary>
    /// <inheritdoc/>
    public class AvailableCurrencyAmounts : IAvailableCurrencyAmounts
    {
        private readonly decimal _minimumAmount;
        private readonly IConsole _console;

        private List<decimal> AvailableAmounts { get; set; } = new List<decimal>();

        public AvailableCurrencyAmounts(List<decimal> availableAmounts = null, IConsole console = null, decimal minimumAmount = 0.01m)
        {
            _minimumAmount = minimumAmount;
            _console = console ?? new LocalConsole();
            AvailableAmounts = availableAmounts ?? AvailableAmounts;
            SetAvailableAmounts();
            SortAvailableAmounts();
        }

        private void SetAvailableAmounts()
        {
            if (!AvailableAmounts.Any())
            {
                _console.WriteLine($"{nameof(AvailableAmounts)} " +
                 $"does not contain any valid currency amounts, " +
                 $"so the default available currency amounts will be used");
                AvailableAmounts = GetDefaultAvailableCurrencyAmount();
            }

            AvailableAmounts = Verify(AvailableAmounts);
        }

        private void SortAvailableAmounts()
        {
            AvailableAmounts.Sort();
        }

        public void AddToAvailableCurrencyAmounts(decimal amount)
        {
            ValidateAmount(amount);

            amount = decimal.Round(amount, Constants.DecimalRoundUp);

            if (AvailableAmounts.Exists(x => x == amount)) return;

            AvailableAmounts.Add(amount);
            SortAvailableAmounts();
        }

        private void ValidateAmount(decimal amount)
        {
            if (amount <= _minimumAmount)
            {
                throw new Exception($"{nameof(amount)} has to be greater than zero");
            }
        }

        public void RemoveFromAvailableCurrencyAmounts(decimal amount)
        {
            AvailableAmounts.RemoveAll(x => x == amount);
            if (!AvailableAmounts.Any())
            {
                AvailableAmounts = Verify(AvailableAmounts);
            }
            SortAvailableAmounts();
        }

        public List<decimal> GetAvailableCurrencyAmounts()
        {
            Verify(AvailableAmounts);
            SortAvailableAmounts();
            AvailableAmounts.Reverse();
            return AvailableAmounts;
        }

        private List<decimal> Verify(IEnumerable<decimal> availableAmounts)
        {
            availableAmounts = availableAmounts.Where(amount => amount > _minimumAmount).ToList();
            var holder = availableAmounts.Select(item => decimal.Round(item, Constants.DecimalRoundUp)).ToList();
            return (!holder.Any() ? GetDefaultAvailableCurrencyAmount() : holder).Distinct().ToList();
        }

        private List<decimal> GetDefaultAvailableCurrencyAmount()
        {
            return new List<decimal>()
            {
                0.01m, 0.02m, 0.05m, 0.10m,
                0.20m, 0.50m, 1.00m, 2.00m,
                5.00m, 10.00m,20.00m, 50.00m
            };
        }
    }
}