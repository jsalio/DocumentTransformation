using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boundaries.PdfEngine
{
    public interface IPdfConverter
    {
        Task<string> GenerateDocument(IEnumerable<string> pages);
    }
}
