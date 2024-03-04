using DAL.Context;

namespace BLL.Services.Abstracts
{
    public abstract class ContextAccess
    {
        protected readonly SMDbContext _context;
        protected ContextAccess(SMDbContext context)
        {
            _context = context;
        }
    }
}
