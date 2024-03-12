using CML.ConsoleClient.Core.Menu.Interfaces;

namespace CML.ConsoleClient.Core.Menu.MenuItems.Input
{
    /// <summary>
    /// Represents a button.
    /// </summary>
    public class ItemButton : IMenuItem
    {
        /// <inheritdoc/>
        public string Name { get; init; }

        /// <summary>
        /// Gets or sets key to react for.
        /// </summary>
        public ConsoleKey Key { get; set; }

        /// <summary>
        /// Gets or sets action to do after pressing a key.
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        /// Gets key caption.
        /// </summary>
        public string Caption { get; init; }

        /// <inheritdoc/>
        public int StrokesTaken { get; init; } = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemButton"/> class.
        /// </summary>
        /// <param name="name"> Debug name. </param>
        /// <param name="key"> Key to react. </param>
        /// <param name="caption"> Key caption. </param>
        public ItemButton(string name, ConsoleKey key, string caption)
        {
            this.Name = name;
            this.Key = key;
            this.Caption = caption;
        }

        /// <inheritdoc/>
        public void Draw()
        {
            Console.WriteLine($"<{this.Key}> {this.Caption};");
        }
    }
}
