namespace ChangeCalculator.Infrastructure.Interfaces
{
    /// <summary>
    /// An interface to implement to be able to write to and read from a display interface.
    /// </summary>
    public interface IConsole
    {
        /// <summary>
        /// A method to use to write a line to a display interface and sets the return <br/>
        /// carriage to the next line afterwards.
        /// </summary>
        /// <param name="text"></param>
        void WriteLine(string text);

        /// <summary>
        /// A method to use to write a line to a display interface but does not set<br/>
        /// the return carriage to the next line.
        /// </summary>
        /// <param name="text"></param>
        void Write(string text);

        /// <summary>
        /// A method to use to read a line from a display interface.
        /// </summary>
        /// <returns></returns>
        string ReadLine();
    }
}