using SL.Utils.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Mapper
{
    public class DefaultMapper : IMapper
    {
        
        private readonly AutoMapper.IMapper _mapper;

        public DefaultMapper(AutoMapper.IMapper mapper)
        {
            _mapper = mapper;
        }

        public T Map<T>(object source)
        {
            return _mapper.Map<T>(source);
        }

        public void Map<TSource, TTarget>(TSource source, TTarget target)
        {
            _mapper.Map(source, target);
        }
    }
}
