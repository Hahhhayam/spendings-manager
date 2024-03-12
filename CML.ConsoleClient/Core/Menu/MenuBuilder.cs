using CML.ConsoleClient.Core.Menu.Interfaces;
using CML.ConsoleClient.Core.Menu.MenuItems.Input;

namespace CML.ConsoleClient.Core.Menu
{
    /// <summary>
    /// <see cref="MenuBuilder"/> ver 1.0.
    /// Class, that represents menu with it contents.
    /// </summary>
    public class MenuBuilder
    {
        /// <summary>
        /// Gets debug name.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Gets or sets a value indicating whether cancel or not cancel reading keys.
        /// </summary>
        public bool ExitToken { get; set; } = false;

        /// <summary>
        /// Gets list of items, which will be displayed.
        /// </summary>
        public IList<IMenuItem> Items { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuBuilder"/> class.
        /// </summary>
        /// <param name="name"> Sets debug name. </param>
        /// <param name="items"> Sets items to display. </param>
        public MenuBuilder(string name, IList<IMenuItem> items)
        {
            this.Name = name;
            this.Items = items;
        }

        /// <summary>
        /// Displays all <see cref="IMenuItem"/>s, listed in <see cref="Items"/>.
        /// Afterall, waits for a key.
        /// </summary>
        public void Run()
        {
            Console.Clear();
            foreach (var item in this.Items)
            {
                item.Draw();
            }

            this.ReactOnKeys();
        }

        private void ReactOnKeys()
        {
            IEnumerable<ItemButton> keys = this.Items.OfType<ItemButton>().ToList();
            ConsoleKey key = ConsoleKey.None;
            while (!this.ExitToken)
            {
                key = Console.ReadKey(intercept: true).Key;
                keys.SingleOrDefault(x => x.Key == key)?.Action();
            }
        }
    }
}
