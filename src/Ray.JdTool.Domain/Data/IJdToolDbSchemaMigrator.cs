using System.Threading.Tasks;

namespace Ray.JdTool.Data
{
    public interface IJdToolDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
