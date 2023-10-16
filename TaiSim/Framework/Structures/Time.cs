using System;

namespace TaiSim.Framework.Structures;

/// <summary>
/// A discrete, atomic point in time
/// </summary>
public class Time
{
    public static readonly Time Zero = new (0,0);
    
    public double Value { get; private set; } 
    public double Unit { get; private set; } 

    public Time(double value, double unit)
    {
        Value = value;
        Unit = unit;
    }

    public void SetUnit(double u)
    {
        Value = u * (Value / Unit);
        Unit = u;
    }
    
    public Time InUnit(double u)
    {
        Time t = new Time(Value, Unit);
        t.SetUnit(u);
        return t;
    }

    public double ToMilliseconds(int bpm, int beat)
    {
        var barpos = InUnit(beat).Value;
        return (60 / bpm) * beat * barpos;
    }
    
    public double ToFrames(int bpm, int beat, int fps)
    {
        var sec = ToMilliseconds(bpm, beat) * 1000;
        return sec * fps;
    }

    public static Time operator +(Time a, Time b)
    {
        commoniseUnits(ref a, ref b);
        return new Time(a.Value + b.Value, a.Unit);
    }

    public static Time operator -(Time a, Time b)
    {
        commoniseUnits(ref a, ref b);
        return new Time(a.Value - b.Value, a.Unit);
    }
    
    private static void commoniseUnits(ref Time a, ref Time b)
    {
        if (Math.Abs(a.Unit - b.Unit) < Double.Epsilon)
            return; // units already the same, nothing to do here
        var commonUnit = Math.Min(a.Unit, b.Unit);
        var c = Math.Abs(a.Unit - commonUnit) > Double.Epsilon ? a : b;
        c.SetUnit(commonUnit);
    }

    protected bool Equals(Time other)
    {
        Time a = new(Value, Unit), b = new(other.Value, other.Unit);
        commoniseUnits(ref a, ref b);
        return Math.Abs(a.Value - b.Value) < Double.Epsilon && Math.Abs(a.Unit - b.Unit) < Double.Epsilon;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ToMilliseconds(60,4));
    }
}
