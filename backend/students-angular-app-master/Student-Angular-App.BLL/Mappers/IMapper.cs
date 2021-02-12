using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Angular_App.BLL.Mappers
{
    public interface IMapper<TEntity, TDto>
    {
        TEntity Map(TDto item);
        TDto Map(TEntity item);

        List<TEntity> MapList(List<TDto> dtos);
        List<TDto> MapList(List<TEntity> entities);
    }
}