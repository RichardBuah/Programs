using System;

public class SimpleGoal : Goal
{
    private bool _isComplete;
    private string _dateCompleted;
    public SimpleGoal(): base()
    {
        _isComplete = false;
        _dateCompleted = "";
    }
    public SimpleGoal(string shortName, string description, int points) : base(shortName, description, points)
    {
        _isComplete = false;
    }
    public void SetDatecompleted(string date)
    {
        _dateCompleted = date;
    }
    public override void RecordEvent()
    {
        _dateCompleted = DateTime.Now.ToShortDateString();
        _isComplete = true;
    }
    public override bool IsComplete()
    {
        return _isComplete;
    }
    public override string GetDetailString()
    {
        if (IsComplete())
        {
            return $"[x] {GetShortName()} ({GetDescription()}) Completed on {_dateCompleted}";
        }
        else
        {
            return $"[] {GetShortName()} ({GetDescription()})";
        }
    }
    public override string GetStringRepresentation()
    {
        return $"S, {IsComplete()}, {GetShortName()}, {GetDescription()}, {_points}, {_dateCompleted}";
    }
}