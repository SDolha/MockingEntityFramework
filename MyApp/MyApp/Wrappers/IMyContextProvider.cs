namespace MyApp.Wrappers
{
    /// <summary>
    /// Provides instances of disposable IMyContext when needed, extracted for testability reasons.
    /// </summary>
    public interface IMyContextProvider
    {
        /// <summary>
        /// Creates an instance of disposable IMyContext.
        /// </summary>
        IMyContext CreateContext();
    }
}
