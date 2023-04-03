using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker
{
    internal class Meal
    {
        public int Id { get; set; }
        public string MealName { get; set; }
        public int Calories { get; set; }
        public int UserID { get; set; }
        public string Restaurant { get; set; }

    }
}
