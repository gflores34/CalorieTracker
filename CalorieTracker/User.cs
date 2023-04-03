using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker
{
    internal class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int DailyCalorieGoal { get; set; }
        public int GoalWeight { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsSelected { get; set; }
        public Boolean IsMale { get; set; }

    }


}
