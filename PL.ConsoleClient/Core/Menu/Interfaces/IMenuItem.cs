namespace CML.ConsoleClient.Core.Menu.Interfaces
{
    /// <summary>
    /// Represents a block from which menu builds.
    /// </summary>
    public interface IMenuItem
    {
        /// <summary>
        /// Gets Pseudo-private Name to debug and other purposes. Not indented to output this name.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Gets amount of strokes taken by <see cref="IMenuItem"/>. Used in <see cref="Console.SetCursorPosition(int, int)"/>.
        /// </summary>
        public int StrokesTaken { get; init; }

        /// <summary>
        /// Method to output <see cref="IMenuItem"/> contents.
        /// </summary>
        public void Draw();
    }
}