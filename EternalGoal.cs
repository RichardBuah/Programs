using System;

public class EternalGoal : Goal
{
    public EternalGoal() : base()
    {}
    public EternalGoal(string shortName, string description, int points) : base(shortName, description, points) 
    {}
    public override void RecordEvent()
    {
        
    }
    public override bool IsComplete()
    {
        return false;
    }
    public override string GetStringRepresentation()
    {
        return $"E, {GetShortName()}, {GetDescription()}, {_points}";
    }
}