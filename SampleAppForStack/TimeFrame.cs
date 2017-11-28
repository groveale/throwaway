using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAppForStack
{
    class TimeFrame
    {
        public DateTime FrameBegin { get; set; }
        public DateTime FrameEnd { get; set; }
        public bool Busy { get; set; }

        public TimeFrame(DateTime start, DateTime end)
        {
            FrameBegin = start;
            FrameEnd = end;
        }
    }
}
