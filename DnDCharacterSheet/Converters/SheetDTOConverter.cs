using DTOs;
using Interfaces;
using Models;

namespace Mappers
{
    public class SheetDTOConverter : IConverter<SheetDTO, Sheet>
    {
        public Sheet Convert(SheetDTO source)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sheet> Convert(IEnumerable<SheetDTO> source)
        {
            throw new NotImplementedException();
        }
    }
}
