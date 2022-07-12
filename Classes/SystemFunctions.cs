using Windows.ApplicationModel.DataTransfer;

namespace WinUI3DevTools.Classes
{
    /// <summary>
    /// Custom system functions class.
    /// </summary>
    internal class SystemFunctions
    {
        /// <summary>
        /// Adds supplied text to clipboard.
        /// </summary>
        /// <param name="str">The str.</param>
        internal static void CopyText(string str)
        {
            DataPackage dataPackage = new() { RequestedOperation = DataPackageOperation.Copy };
            dataPackage.SetText(str);
            Clipboard.SetContent(dataPackage);
        }
    }
}
