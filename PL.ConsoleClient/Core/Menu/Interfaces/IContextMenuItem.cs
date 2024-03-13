namespace CML.ConsoleClient.Core.Menu.Interfaces
{
    /// <summary>
    /// Reserved interface. If no input features will be added, this method belongs to be deleted.
    /// </summary>
    public interface IContextMenuItem
    {
        /// <summary>
        /// Gets Pseudo-private Name to debug and other purposes. Not indented to output this name.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Clears console and runs context menu.
        /// </summary>
        public void Run();

        /// <summary>
        /// Restores menu after clearing.
        /// </summary>
        /// <param name="invocationMenu"> Menu to restore. </param>
        public void Restore(MenuBuilder invocationMenu);
    }
}
