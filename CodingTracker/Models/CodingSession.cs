using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTracker.Models
{
    public class CodingSession
    {
        internal int Id { get; set; }
        internal DateTime StartTime { get; set; }
        internal DateTime EndTime { get; set; }
        internal int Duration
        {
            get
            {
                return (int)(EndTime - StartTime).TotalMinutes;
            }
        }

        internal CodingSession(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

    }
}