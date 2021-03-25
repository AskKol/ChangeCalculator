using System;
using System.Collections.Generic;
using System.Linq;
using ChangeCalculator.Infrastructure.Interfaces;

namespace ChangeCalculator.Infrastructure.Instances
{
    /// <summary>
    /// A class to use to work out how much change to give and to display<br/>
    /// the change given to a display interface.<br/>
    /// It does this by implementing IChangeCalculator
    /// </summary>
    /// <inheritdoc/>
    public class ChangeCalculator : IChangeCalculator
    {
        private readonly IAvailableCurrencyAmounts _currencyAmounts;
        private readonly IConsole _console;

        public ChangeCalculator(IAvailableCurrencyAmounts currencyAmounts = null, IConsole console = null)
        {
            _currencyAmounts = currencyAmounts ?? new AvailableCurrencyAmounts();
            _console = console ?? new LocalConsole();
        }

        public decimal CalculateChange(decimal cashGiven, decimal itemCost)
        {
            if (AreValuesValid(cashGiven, itemCost)) return -1m;

            cashGiven = RoundupValues(cashGiven, ref itemCost);

            var result = GetChange(cashGiven, itemCost);

            if (result >= 0) return result;

            _console.WriteLine($"{nameof(cashGiven)} is less than the {nameof(itemCost)}");

            return -1;
        }

        private bool AreValuesValid(decimal cashGiven, decimal itemCost)
        {
            var invalidReturn = 0;
            invalidReturn += ValidateValue(cashGiven, nameof(itemCost));
            invalidReturn += ValidateValue(itemCost, nameof(itemCost));

            return invalidReturn > 0;
        }

        private int ValidateValue(decimal value, string valueName)
        {
            if (value >= 0) return 0;
            _console.WriteLine($"Please provide a valid {valueName}. I.e. a value equal to or greater than zero ");
            return 1;
        }

        private decimal RoundupValues(decimal cashGiven, ref decimal itemCost)
        {
            cashGiven = decimal.Round(cashGiven, Constants.DecimalRoundUp);
            itemCost = decimal.Round(itemCost, Constants.DecimalRoundUp);
            return cashGiven;
        }

        private decimal GetChange(decimal cashGiven, decimal itemCost)
        {
            var result = decimal.Round(cashGiven - itemCost, Constants.DecimalRoundUp);
            return result;
        }

        public void GiveChange(decimal change)
        {
            var currencyAmounts = _currencyAmounts.GetAvailableCurrencyAmounts();
            _console.WriteLine("Your change is:");
            if (IsChangeZero(change)) return;
            while (change > 0)
            {
                var closest = GetClosestAmount(change, currencyAmounts);
                var howMany = GetHowMany(change, closest);
                var amountToDisplay = GetAmountToDisplay(closest);
                _console.WriteLine($"{howMany} X £{amountToDisplay}");
                change = ResetChange(change, howMany, closest);
            }
        }

        private bool IsChangeZero(decimal change)
        {
            if (change != 0) return false;
            _console.WriteLine("You do not have any change");
            return true;
        }

        private static decimal GetClosestAmount(decimal change, List<decimal> currencyAmounts)
        {
            decimal closest = currencyAmounts.Aggregate((x, y) => Math.Abs(x - change) < Math.Abs(y - change) ? x : y);
            if (closest > change && change < currencyAmounts[0])
            {
                var index = currencyAmounts.FindIndex(x => x == closest);
                if (index == currencyAmounts.Count - 1)
                {
                    throw new Exception("Due to no smaller amount available, full change cannot be given");
                }
                closest = currencyAmounts[index + 1];
            }
            return closest;
        }

        private decimal GetHowMany(decimal change, decimal closest)
        {
            var howMany = change / closest;
            return Math.Truncate(howMany);
        }

        private string GetAmountToDisplay(decimal closest)
        {
            return closest < 1 ? (closest * 100).ToString("N0") + "p" : closest.ToString("N0");
        }

        private decimal ResetChange(decimal change, decimal howMany, decimal closest)
        {
            change -= howMany * closest;
            return change;
        }
    }
}