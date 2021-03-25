using System;
using ChangeCalculator.Infrastructure.Interfaces;

namespace ChangeCalculator.Infrastructure.Instances
{
    /// <summary>
    /// A class to use to display text to and read text from a display interface.<br/>
    /// It does this by implementing IConsole
    /// </summary>
    /// <inheritdoc/>
    public class LocalConsole : IConsole
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public void Write(string text)
        {
            Console.Write(text);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}