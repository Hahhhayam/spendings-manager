using DAL.Context;

namespace BLL.Services.Abstracts
{
    public abstract class ContextAccess
    {
        protected readonly SMDbContext context;

        protected ContextAccess(SMDbContext context)
        {
            this.context = context;
        }
    }
}
