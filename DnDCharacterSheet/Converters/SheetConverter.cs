using DTOs;
using Interfaces;
using Models;

namespace Mappers
{
    public class SheetConverter : IConverter<Sheet, SheetDTO>
    {
        public SheetDTO Convert(Sheet source)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SheetDTO> Convert(IEnumerable<Sheet> source)
        {
            throw new NotImplementedException();
        }
    }
}
