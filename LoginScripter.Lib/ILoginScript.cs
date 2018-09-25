using System.IO;

namespace LoginScripter.Lib
{
    public interface ILoginScript
    {
        /// <summary>
        /// Friendly name of the script to show in UI.
        /// </summary>
        string FriendlyName { get; }
        /// <summary>
        /// Whether or not to run this script.
        /// </summary>
        bool Enabled { get; }
        /// <summary>
        /// Run the script.
        /// </summary>
        /// <returns>An integer 0 when succesful and non-0 when not successful.</returns>
        int Run();
    }
}
