using System;

public abstract class Goal
{
    private string _shortName;
    private string _description;
    protected int _points;
    public string GetShortName()
    {
        return _shortName;
    }
    public string GetDescription()
    {
        return _description;
    }
    public int GetPoints()
    {
        return _points;
    }
    public void SetPoints(int points)
    {
        _points += points;
    }
    public Goal()
    {
        _shortName = "Undefined";
        _description = "None";
        _points = 0;
    }
    public Goal(string shortName, string description, int points)
    {
        _shortName = shortName;
        _description = description;
        _points = points;
    }
    public abstract void RecordEvent();
    public abstract bool IsComplete();
    public virtual string GetDetailString()
    {
        if (IsComplete())
        {
            return $"[x] {_shortName} ({_description})";
        }
        else
        {
            return $"[] {_shortName} ({_description})";
        }
    }
    public abstract string GetStringRepresentation();
}