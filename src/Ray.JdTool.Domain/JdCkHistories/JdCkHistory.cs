using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ray.JdTool.JdCkHistories
{
    public class JdCkHistory : FullAuditedAggregateRoot<Guid>
    {
        public JdCkHistory(string fullStr)
        {
            FullStr = fullStr;
            PtPin = GetPtPinByFull();
        }

        public string PtPin { get; private set; }

        public string FullStr { get; private set; }

        public string SimpleStr => PtPin + GetPtKeyByFull();

        private string GetPtPinByFull()
        {
            var match = new Regex("pt_pin=.+?;")
                .Match(FullStr);
            return match.Success ? match.Value : "";
        }

        private string GetPtKeyByFull()
        {
            var match = new Regex("pt_key=.+?;")
                .Match(FullStr);
            return match.Success ? match.Value : "";
        }
    }
}
