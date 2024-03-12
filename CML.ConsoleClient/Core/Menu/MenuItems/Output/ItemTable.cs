using CML.ConsoleClient.Core.Menu.Interfaces;
using ConsoleTables;

namespace CML.ConsoleClient.Core.Menu.MenuItems.Output
{
    /// <summary>
    /// Represent table data. Uses <see cref="ConsoleTables"/> to display.
    /// </summary>
    public class ItemTable : IMenuItem
    {
        /// <inheritdoc/>
        public string Name { get; init; }

        /// <inheritdoc/>
        public int StrokesTaken { get; init; }

        /// <summary>
        /// Gets data to display.
        /// </summary>
        public IDictionary<string, ICollection<string>> Data { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemTable"/> class.
        /// </summary>
        /// <param name="name"> Debug name. </param>
        /// <param name="data"> Data to display. </param>
        /// <param name="pagination"> Pagination support. </param>
        /// <remarks> Pagination not supported in version 1.0. </remarks>
        public ItemTable(string name, IDictionary<string, ICollection<string>> data, int? pagination = null)
        {
            this.Name = name;
            this.Data = data;

            this.StrokesTaken = pagination ?? data.Values.Select(k => k.Count).Max();
        }

        /// <inheritdoc/>
        public void Draw()
        {
            var table = new ConsoleTable(new ConsoleTableOptions()
            {
                Columns = this.Data.Keys,
                EnableCount = false,
            });

            List<IEnumerator<string>> enumerators = this.Data.Values.Select(v => v.GetEnumerator()).ToList();
            bool ended = false;
            while (!ended)
            {
                ended = true;
                List<string> row = new();
                foreach (IEnumerator<string> e in enumerators)
                {
                    var status = e.MoveNext();
                    if (!status)
                    {
                        row.Add(null);
                        continue;
                    }

                    ended = false;
                    row.Add(e.Current);
                }

                table.Rows.Add(row.ToArray());
            }

            table.Rows.RemoveAt(table.Rows.Count - 1);
            table.Write();
        }
    }
}
