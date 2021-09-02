using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Ray.JdTool.JdCkHistories
{
    public class JdCkHistoryDto : AuditedEntityDto<Guid>
    {
        public string PtPin { get; set; }

        public string FullStr { get; set; }
    }
}
