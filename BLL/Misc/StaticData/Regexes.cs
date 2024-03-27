using System.Text.RegularExpressions;

namespace BLL.Misc.StaticData
{
    internal static class Regexes
    {
        internal static readonly Regex Phone
            = new Regex(@"^(\+([0-9]{1,3})[-. ]?)?\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{2})[-. ]?([0-9]{2})$");

        internal static readonly Regex Email
            = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

        internal static readonly Regex Link
            = new Regex(@"^(https?:\/\/)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b(?:[-a-zA-Z0-9()@:%_\+.~#?&//=]*)$");
    }
}
