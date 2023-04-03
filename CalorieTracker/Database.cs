using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker
{

    internal class Database
    {
        ILiteCollection<User> UserCollection;
        ILiteCollection<Meal> MealCollection;
        User currentUser;
        public Database()
        {
            LiteDatabase db = new LiteDatabase(@"Filename = CalorieTracker.db;connection=shared");

            //User setup
            UserCollection = db.GetCollection<User>("users");
            MealCollection = db.GetCollection<Meal>("meals");
        }

        public ILiteCollection<User> FindAll()
        {
            return UserCollection;
        }

        /*
         * ********************USER DATABASE***************************
         */

        //Inserts a user to the database user collection
        public void InsertUser(User user)
        {
            currentUser = user;
            UserCollection.Insert(user);
        }

        //Updates user in the user database collection
        public void UpdateUser(User user)
        {
            var UserUpdate = UserCollection.FindById(user.Id);
            UserCollection.Update(user.Id, UserUpdate);
        }

        //Deletes the user
        public void DeleteUser(User user) 
        {
            UserCollection.Delete(user.Id);
        }

        //Find User
        public List<User> FindUser(int id)
        {
            var UserQuery = UserCollection.Find(x => x.Id == id);
            List<User> result = UserQuery.ToList();
            return result;
        }

        //Find all Users
        public List<User> FindAllUsers()
        {
            var UserQuery = UserCollection.FindAll();
            List<User> result = UserQuery.ToList();
            return result;
        }

        /*
         * ********************MEAL DATABASE***************************
         */

        public void InsertMeal(Meal meal)
        {
            MealCollection.Insert(meal);
        }

        public void UpdateMeal(Meal meal)
        {
            MealCollection.Update(meal);
        }

        public void DeleteMeal(Meal meal)
        {
            var MealUpdate = MealCollection.FindById(meal.Id);
            MealCollection.Delete(meal.Id);
        }


    }


}

