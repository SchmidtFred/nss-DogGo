using System;
using System.Linq;
using System.Collections.Generic;

namespace DogGo.Models.ViewModels
{
    public class WalkerViewModel
    {
        public Walker Walker { get; set; }
        public List<Walk> Walks { get; set; }
        public String TotalTime
        {
            get
            {
                int total = Walks.Sum(walk => walk.Duration);
                int seconds = total % 60;
                int minutes = total / 60;
                return $"{minutes/60}: {minutes % 60} : {seconds}";
            }
        }
    }
}
