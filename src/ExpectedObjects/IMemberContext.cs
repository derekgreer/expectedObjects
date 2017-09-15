namespace ExpectedObjects
{
    public interface IMemberContext
    {
        /// <summary>
        ///     Configures a member to use a custom <see cref="IComparison" />.
        /// </summary>
        /// <param name="comparison"></param>
        void UsesComparison(IComparison comparison);
    }
}