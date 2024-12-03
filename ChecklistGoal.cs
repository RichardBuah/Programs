using System;

public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;
    private string _dateCompleted;
    public ChecklistGoal(): base()
    {
        _amountCompleted = 0;
        _target = 0;
        _bonus = 0;
        _dateCompleted = "";
    }
    public ChecklistGoal(string shortName, string description, int points, int target, int bonus) : base(shortName, description, points) 
    {
        _target = target;
        _bonus = bonus;
    }
    public void SetAmountcompleted(int amountCompleted)
    {
        _amountCompleted = amountCompleted;
    }
    public void SetDatecompleted(string date)
    {
        _dateCompleted = date;
    }
    public override void RecordEvent()
    {
        if (!IsComplete())
        {
            _amountCompleted +=1;
            if (_amountCompleted == _target)
            {
                SetPoints(_bonus);
            }
        }
        else 
        {
            Console.WriteLine("Event Completed.");
        }
    }
    public override bool IsComplete()
    {
        if (_amountCompleted == _target)
        {
            _dateCompleted = DateTime.Now.ToShortDateString();
            return true;
        }
        else 
        {
            return false;
        }
    }
    public override string GetDetailString() 
    {
        if (IsComplete())
        {
            return $"[x] {GetShortName()} ({GetDescription()}) - {_amountCompleted}/{_target} Completed on {_dateCompleted}";
        }
        else
        {
            return $"[] {GetShortName()} ({GetDescription()}) - {_amountCompleted}/{_target}";
        }
    }
    public override string GetStringRepresentation()
    {
        return $"C, {IsComplete()}, {GetShortName()}, {GetDescription()}, {_points}, {_amountCompleted}, {_target}, {_bonus}, {_dateCompleted}";
    }
}