using CML.ConsoleClient.Core.Menu.Interfaces;

namespace CML.ConsoleClient.Core.Menu.MenuItems.Output
{
    /// <summary>
    /// Single stroke of data.
    /// </summary>
    public class ItemStroke : IMenuItem
    {
        /// <inheritdoc/>
        public string Name { get; init; }

        /// <inheritdoc/>
        public int StrokesTaken { get; init; }

        /// <summary>
        /// Gets message to display.
        /// </summary>
        public string Content { get; init; }

        public ItemStroke(string name, string content)
        {
            this.Name = name;
            this.Content = content;
            this.StrokesTaken = 1;
        }

        /// <inheritdoc/>
        public void Draw()
        {
            Console.WriteLine(this.Content);
        }
    }
}
