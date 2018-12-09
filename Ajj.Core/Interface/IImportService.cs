using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace Ajj.Core.Interface
{
    public interface IImportService
    {
        Task<DataTable> ImportExcelAsync(Stream file);
    }
}