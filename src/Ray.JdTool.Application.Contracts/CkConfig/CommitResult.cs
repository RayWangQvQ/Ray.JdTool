using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.JdTool.CkConfig
{
    public class CommitResult
    {
        public bool Success { get; set; }

        public CommitResultType CommitResultType { get; set; }

        public string ResultStr { get; set; }
    }
}
