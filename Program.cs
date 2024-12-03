/*
// EXCEED REQUIREMENT
// I added the Try and Catch to handle exception errors
// I added a date stamp on completed goals. Because eternal goals are never completed I excluded it from that functionality.
*/

using System;
using System.Security.Cryptography;

class Program
{
    static void Main(string[] args)
    {
        // Console.WriteLine("Hello Develop06 World!");
        GoalManager manage = new GoalManager();
        bool start = true;
        try
        {
            while (start)
            {
                manage.Start();
                int choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    manage.CreateGoal();
                }
                else if (choice == 2)
                {
                    manage.RecordEvent();
                }
                else if (choice == 3)
                {
                    Console.WriteLine("Goal Summary");
                    manage.ListGoalDetails();
                }
                else if (choice == 4)
                {
                    manage.SaveGoals();
                }
                else if (choice == 5)
                {
                    manage.LoadGoals();
                }
                else
                {
                    start = false;
                }
            }
        }
        catch (FileNotFoundException){
            Console.WriteLine("The file name does not exist.");
        }
        catch (ArgumentException){
            Console.WriteLine("Invalid argument input.");
        }
        catch (FormatException){
            Console.WriteLine("Invalid type. Check input for string or integer.");
        }
        catch (Exception){
            Console.WriteLine("Something Went wrong. Try again.");
        }
    }
}