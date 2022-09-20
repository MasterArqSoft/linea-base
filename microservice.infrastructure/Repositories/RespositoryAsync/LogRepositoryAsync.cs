using microservice.domain.Entities;
using microservice.infrastructure.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microservice.infrastructure.Repositories.RespositoryAsync;

public class LogRepositoryAsync : GenericRepository<LogDetail>
{
    public LogRepositoryAsync(MicroServiceContext microServiceContext) : base(microServiceContext)
    {

    }
}
