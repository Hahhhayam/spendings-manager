namespace CML.ConsoleClient.Core.Menu.MenuItems.Input.Form
{
    /// <summary>
    /// Supporting class for <see cref="ItemFillForm"/>. Not intended to use directly.
    /// </summary>
    public class FillField
    {
        /// <summary>
        /// Gets Pseudo-private Name to debug and other purposes. Not indented to output this name.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Gets a message, if exists, witch will be write before reading field.
        /// </summary>
        public string? Description { get; init; }

        /// <summary>
        /// Gets the output after <see cref="Run"/> method.
        /// </summary>
        public string? Output { get; private set; }

        /// <summary>
        /// Gets amount of strokes taken by <see cref="FillField"/>. Used in <see cref="Console.SetCursorPosition(int, int)"/>.
        /// </summary>
        public int StrokesTaken { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FillField"/> class.
        /// </summary>
        /// <param name="name"> Debug name. </param>
        /// <param name="description"> Message to show. </param>
        public FillField(string name, string description)
        {
            this.Name = name;
            this.Description = description;

            this.StrokesTaken = string.IsNullOrWhiteSpace(this.Description) ? 1 : 2;
        }

        /// <summary>
        /// Displays a message and allows to get a value.
        /// </summary>
        public void Run()
        {
            if (!string.IsNullOrWhiteSpace(this.Description))
            {
                Console.WriteLine(this.Description);
            }

            this.Output = Console.ReadLine();
        }
    }
}