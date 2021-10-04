using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Ray.JdTool.Identity
{
    public class IdentityUserEto : EntityEto
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }
    }
}
