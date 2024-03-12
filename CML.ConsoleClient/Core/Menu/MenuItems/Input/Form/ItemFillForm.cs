using CML.ConsoleClient.Core.Menu.Interfaces;

namespace CML.ConsoleClient.Core.Menu.MenuItems.Input.Form
{
    /// <summary>
    /// Form that displays field-by-field. Used <see cref="FillField"/>.
    /// </summary>
    public class ItemFillForm : IContextMenuItem
    {
        /// <inheritdoc/>
        public string Name { get; init; }

        /// <summary>
        /// Gets amount of strokes taken by <see cref="ItemFillForm"/>. Used in <see cref="Console.SetCursorPosition(int, int)"/>.
        /// </summary>
        public int StrokesTaken { get; init; }

        /// <summary>
        /// List of <see cref="FillField"/>, which will be displayed.
        /// </summary>
        public IList<FillField> Fields { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemFillForm"/> class.
        /// </summary>
        /// <param name="name"> Debug name. </param>
        /// <param name="fields"> Fields to display. </param>
        public ItemFillForm(string name, IList<FillField> fields)
        {
            this.Name = name;
            this.Fields = fields;
            this.StrokesTaken = fields.Select(f => f.StrokesTaken).Sum();
        }

        /// <inheritdoc/>
        public void Run()
        {
            Console.Clear();
            foreach (FillField field in this.Fields)
            {
                field.Run();
            }
        }

        /// <inheritdoc/>
        public void Restore(MenuBuilder invocationMenu)
            => invocationMenu.Run();
    }
}
