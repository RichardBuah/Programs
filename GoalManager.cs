using System;
using System.Drawing;
using System.Net.NetworkInformation;
using System.IO;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;
    private List<Goal> _availableGoals = new List<Goal>();
    public GoalManager()
    {
        _goals = [];
        _score = 0;
    }
    public GoalManager(List<Goal> goals, int score)
    {
        _goals = goals;
        _score = score;
    }
    public void Start()
    {
        Console.WriteLine("Eternal Quest\n");
        DisplayPlayerInfo();
        Console.WriteLine("Choose one option from the menu below.\n1. Create Goal\n2. Record Goal\n3. List Goals\n4. Save Goals\n5. Load Goals\n6. Quit");
    }
    public void DisplayPlayerInfo()
    {
        Console.WriteLine($"You have {_score} points.");
    }
    public void ListGoalNames()
    {
        int num = 0;
        foreach(Goal item in _goals)
        {
            if (!item.IsComplete())
            {
                num++;
                Console.WriteLine($"{num}. {item.GetShortName()}");
                _availableGoals.Add(item);
            }
            else {}
        }
    }
    public void ListGoalDetails()
    {
        if (_goals.Count() > 0)
        {
            foreach(Goal item in _goals)
            {
                Console.WriteLine(item.GetDetailString());
            }
        }
        else 
        {
            Console.WriteLine("You have no listed goals.");
        }
        
    }
    public void CreateGoal()
    {
        Console.WriteLine("What type of Goal do you want to creat.\n1. Simple Goal \n2. Eternal Goal\n3. Check-List Goal\n4. Back to Menu");
        int choice = int.Parse(Console.ReadLine());
        if (choice != 4)
        {
            Console.Write("Enter the name of the Goal:    ");
            string shortName = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Write a short description of the goal:    ");
            string description = Console.ReadLine();
            Console.WriteLine();
            Console.Write("How much is this goal worth in points (eg. 50pts, 100pts etc.):    ");
            int points = int.Parse(Console.ReadLine());
            Console.WriteLine();
            if (choice == 1)
            {
                SimpleGoal simp = new SimpleGoal(shortName,description,points);
                _goals.Add(simp);
            }
            else if (choice == 2)
            {
                EternalGoal etern = new EternalGoal(shortName,description,points);
                _goals.Add(etern);
            }
            else if (choice == 3)
            {
                Console.Write("How many your target:    ");
                int target = int.Parse(Console.ReadLine());
                Console.WriteLine();
                Console.Write("Add bonus points after completion:   ");
                int bonus = int.Parse(Console.ReadLine());
                Console.WriteLine();
                ChecklistGoal check = new ChecklistGoal(shortName,description,points,target,bonus);
                _goals.Add(check);
            }
            else
            {
                Console.WriteLine("Invalid Choice. Done.");
            }
        }
        else
        {
            Start();
        }
    }
    public void RecordEvent()
    {
        _availableGoals = [];
        ListGoalNames();
        bool simu = false;
        int trueCount = 0;
        foreach (Goal item in _goals)
        {
            if (item.IsComplete() == false)
            {
                trueCount++;
                simu = true;
            }
        }
        if (_goals.Count() > 0 && simu == true)
        {
            Console.Write("Which goal do you want to record?    ");
            int choice = int.Parse(Console.ReadLine());
            if (choice <= trueCount)
            {
                _availableGoals[choice-1].RecordEvent();
                _score += _availableGoals[choice-1].GetPoints(); 
            }
            else 
            {
                Console.WriteLine("You have entered an invalid choice.");
            }           
        }
        else 
        {
            Console.WriteLine("You do not have any Incomplete goals to record.");
        }
    }
    public void SaveGoals()
    {
        var path = System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent((Environment.CurrentDirectory).ToString()).ToString()).ToString());
        if (_goals.Count <= 0){
            Console.WriteLine("You are saving an empty list to a file. The file will not be created by this command. Please choose option 1 to create a New Goal to your list, then you can proceed to store it in the same file name.");
        }
        else
        {
            Console.WriteLine("Provide the name of the file you are saving to.");
            string file = Console.ReadLine();
            TextWriter tw = new StreamWriter($@"{path}\{file}.csv");
            tw.WriteLine(_score);
            foreach(Goal goal in _goals)
            {
                tw.WriteLine(goal.GetStringRepresentation());
            }
            tw.Close();
        }
    }
    public void LoadGoals()
    {
        var path = System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent((Environment.CurrentDirectory).ToString()).ToString()).ToString());
        Console.WriteLine("Enter the name of the file you are loading from.");
        string file = Console.ReadLine();
        string score = File.ReadLines($@"{path}\{file}.csv").First();
        _score = int.Parse(score);
        string[] lines = System.IO.File.ReadAllLines($@"{path}\{file}.csv");
        lines = lines.Skip(1).ToArray();
        foreach(string line in lines)
        {
            string[] data = line.Split(",");
            string goalType = data[0];
            if (goalType == "S")
            {
                bool IsComplete = bool.Parse(data[1]);
                string shortName = data[1];
                string description = data[3];
                int points = int.Parse(data[4]);
                string date = data[5];
                SimpleGoal simp = new SimpleGoal(shortName,description,points);
                if (IsComplete)
                {
                    simp.RecordEvent();
                    simp.SetDatecompleted(date);
                }
                _goals.Add(simp);
            }
            else if (goalType == "E")
            {
                string shortName = data[1];
                string description = data[3];
                int points = int.Parse(data[4]);
                EternalGoal etern = new EternalGoal(shortName,description,points);
                _goals.Add(etern);
            }
            else 
            {
                bool IsComplete = bool.Parse(data[1]);
                string shortName = data[2];
                string description = data[3];
                int points = int.Parse(data[4]);
                int amountCompleted = int.Parse(data[5]);
                int target = int.Parse(data[6]);
                int bonus = int.Parse(data[7]);
                string date = data[8];
                ChecklistGoal check = new ChecklistGoal(shortName,description,points, target, bonus);
                check.SetAmountcompleted(amountCompleted);
                if (IsComplete)
                {
                    check.SetDatecompleted(date);
                    for (int i = 0; i < amountCompleted; i++)
                    {
                        check.RecordEvent();
                    }
                }
                _goals.Add(check);
            }
        }
    }
}