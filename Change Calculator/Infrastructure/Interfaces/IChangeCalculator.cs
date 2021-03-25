namespace ChangeCalculator.Infrastructure.Interfaces
{
    /// <summary>
    /// An interface to implement in classes that will be calculating<br/>
    /// and displaying to an output interface the change given.
    /// </summary>
    public interface IChangeCalculator
    {
        /// <summary>
        /// A method to use to calculate the change to give.
        /// </summary>
        /// <param name="cashGiven"></param>
        /// <param name="itemCost"></param>
        /// <returns>Returns the change to give as a decimal value</returns>
        decimal CalculateChange(decimal cashGiven, decimal itemCost);

        /// <summary>
        /// A method to use to out put the change given.
        /// </summary>
        /// <param name="change"></param>
        void GiveChange(decimal change);
    }
}