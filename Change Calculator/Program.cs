using ChangeCalculator.Infrastructure.Instances;
using ChangeCalculator.Infrastructure.Interfaces;

namespace ChangeCalculator
{
    public class Program
    {
        private static IConsole _console;
        private static IChangeCalculator _changeCalculator;
        private const string MenuItem1 = "1";
        private const string MenuItem2 = "2";

        public static void Main(string[] args)
        {
            _console = new LocalConsole();
            _console.WriteLine("Welcome...");
            var showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            DisplayMenuInstructions();

            switch (_console.ReadLine())
            {
                case MenuItem1:
                    RunChangeCalculator();

                    return true;

                case MenuItem2:
                    return false;

                default:
                    return true;
            }
        }

        private static void DisplayMenuInstructions()
        {
            _console.WriteLine("");
            _console.WriteLine("Choose an option then press enter to select that option:");
            _console.WriteLine($"{MenuItem1}) Use Change Calculator");
            _console.WriteLine($"{MenuItem2}) Exit");
            _console.Write("Select an option: ");
        }

        private static void RunChangeCalculator()
        {
            _changeCalculator = new Infrastructure.Instances.ChangeCalculator();

            while (true)
            {
                var itemCostResult = GetInputValue("item cost");

                if (itemCostResult.exit) { return; }

                var amountGivenResult = GetInputValue("amount given");

                if (amountGivenResult.exit) { return; }

                var change = _changeCalculator.CalculateChange
                    (amountGivenResult.value, itemCostResult.value);
                if (change < 0)
                {
                    continue;
                }
                _changeCalculator.GiveChange(change);
                break;
            }
        }

        private static (bool exit, decimal value) GetInputValue(string whatInput)
        {
            var exit = false;
            decimal value = 0.00m;

            _console.Write($"Enter the {whatInput}:");

            while (true)
            {
                var input = _console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input) && input.Trim() == MenuItem2)
                {
                    exit = true;
                    break;
                }

                if (!string.IsNullOrWhiteSpace(input) && input.Trim() != MenuItem2 &&
                    decimal.TryParse(input, out value))
                {
                    value = decimal.Round(value, Constants.DecimalRoundUp);
                    break;
                }

                _console.WriteLine($"Please enter a valid {whatInput}:");
            }

            return (exit, value);
        }
    }
}