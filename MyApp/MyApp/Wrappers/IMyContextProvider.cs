namespace MyApp.Wrappers
{
    /// <summary>
    /// Provides instances of disposable IMyContextWrapper when needed, extracted for testability reasons.
    /// </summary>
    public interface IMyContextProvider
    {
        /// <summary>
        /// Creates an instance of disposable IMyContextWrapper.
        /// </summary>
        IMyContextWrapper CreateContext();
    }
}
