using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAppForStack
{
    class Program
    {


        static void Main(string[] args)
        {
            var dateTime = new DateTime(2017, 11, 28, 0, 0, 0);
            var plana = new PlanItem { StartTime = dateTime.AddHours(6), EndTime = dateTime.AddHours(9) };
            var planb = new PlanItem { StartTime = dateTime.AddHours(14), EndTime = dateTime.AddHours(18) };
            var planc = new PlanItem { StartTime = dateTime.AddHours(16), EndTime = dateTime.AddHours(30) };
            var pland = new PlanItem { StartTime = dateTime.AddHours(2), EndTime = dateTime.AddHours(30) };

            var planList = new List<PlanItem> { plana, planb, planc };

            var hours = HoursBusy(planList, dateTime);

            Console.ReadKey();
        }


        public static double HoursBusy(List<PlanItem> plansInDay, DateTime day)
        {

            List<TimeFrame> timesInFrame = GetTimeFrames(day);

            foreach (var plan in plansInDay)
            {
                // only check free times
                foreach(var freeFrame in timesInFrame.Where(f => !(f.Busy)).ToList())
                {
                    // should never be busy
                    if (freeFrame.Busy) { continue; }

                    // if plan finish before frame start then break and go to next plan
                    if (plan.EndTime < freeFrame.FrameBegin) { break; }
                    
                    // is start in frame
                    freeFrame.Busy = plan.StartTime >= freeFrame.FrameBegin && plan.StartTime < freeFrame.FrameEnd;
                    if (freeFrame.Busy) { continue; }

                    // is end in frame
                    freeFrame.Busy = plan.EndTime >= freeFrame.FrameBegin && plan.EndTime < freeFrame.FrameEnd;
                    if (freeFrame.Busy) { continue; }

                    // is date within frame (starts before and ends after)
                    freeFrame.Busy = plan.StartTime < freeFrame.FrameBegin && plan.EndTime > freeFrame.FrameEnd;
                    if (freeFrame.Busy) { continue; }
                }
            }

            return timesInFrame.Where(f => f.Busy).Count();
        }

        private static List<TimeFrame> GetTimeFrames(DateTime date)
        {
            DateTime frameStart = date.Date;
            DateTime frameEnd = date.Date.AddHours(1);

            List<TimeFrame> framesInDay = new List<TimeFrame>();

            for (int i = 0; i < 24; i++)
            {
                framesInDay.Add(new TimeFrame(frameStart.AddHours(i), frameEnd.AddHours(i)));
            }

            return framesInDay;
        }
    }
}
