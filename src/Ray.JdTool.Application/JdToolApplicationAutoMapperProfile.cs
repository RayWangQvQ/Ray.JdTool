using AutoMapper;
using Ray.JdTool.JdCkHistories;

namespace Ray.JdTool
{
    public class JdToolApplicationAutoMapperProfile : Profile
    {
        public JdToolApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<JdCkHistory, JdCkHistoryDto>();
            CreateMap<CreateUpdateJdCkHistoryDto, JdCkHistory>();
        }
    }
}
