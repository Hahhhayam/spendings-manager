using CML.ConsoleClient.Core.Menu.Interfaces;

namespace CML.ConsoleClient.Core.Menu.MenuItems.Output
{
    /// <summary>
    /// Simple divider. Current length is hardcoded and is 20.
    /// </summary>
    public class ItemDivider : IMenuItem
    {
        /// <inheritdoc/>
        public string Name { get; init; }

        /// <inheritdoc/>
        public int StrokesTaken { get; init; } = 1;

        /// <summary>
        /// Gets or sets symbol to write a divider.
        /// </summary>
        public char Symbol { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemDivider"/> class.
        /// </summary>
        /// <param name="name"> Debug name. </param>
        /// <param name="symbol"> Symbol to display a divider. </param>
        public ItemDivider(string name, char symbol)
        {
            this.Name = name;
            this.Symbol = symbol;
        }

        /// <inheritdoc/>
        public void Draw()
        {
            Console.WriteLine(new string(this.Symbol, 20));
        }
    }
}
