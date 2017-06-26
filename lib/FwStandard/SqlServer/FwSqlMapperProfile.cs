using AutoMapper;
using FwStandard.BusinessLogic;

namespace FwStandard.SqlServer
{
    public class FwSqlMapperProfile : Profile
    {
        public FwSqlMapperProfile()
        {
            CreateMap<FwBusinessLogic, FwBusinessLogic>();
        }
    }
}
