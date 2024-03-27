namespace BLL.Tests.Comparers
{
    internal interface IComparer<in T, in K>
    {
        public bool EqualsTo(T x, K y);
    }
}
