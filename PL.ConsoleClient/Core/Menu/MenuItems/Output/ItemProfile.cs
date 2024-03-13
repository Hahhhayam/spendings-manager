using CML.ConsoleClient.Core.Menu.Interfaces;

namespace CML.ConsoleClient.Core.Menu.MenuItems.Output
{
    /// <summary>
    /// Represents a profile, that displays data field-by-field.
    /// </summary>
    public class ItemProfile : IMenuItem
    {
        /// <inheritdoc/>
        public string Name { get; init; }

        /// <inheritdoc/>
        public int StrokesTaken { get; init; }

        /// <summary>
        /// Gets data to display.
        /// </summary>
        public IDictionary<string, string> Data { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemProfile"/> class.
        /// </summary>
        /// <param name="name"> Debug name. </param>
        /// <param name="data"> Data to display. </param>
        public ItemProfile(string name, IDictionary<string, string> data)
        {
            this.Name = name;
            this.Data = data;

            this.StrokesTaken = data.Count;
        }

        /// <inheritdoc/>
        public void Draw()
        {
            foreach (KeyValuePair<string, string> row in this.Data)
            {
                Console.WriteLine($"{row.Key}: {row.Value}");
            }
        }
    }
}
