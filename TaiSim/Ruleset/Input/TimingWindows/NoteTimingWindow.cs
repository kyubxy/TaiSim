#nullable enable
using System;
using TaiSim.Framework.Structures;
using TaiSim.Fumen;

namespace TaiSim.Ruleset.Input.TimingWindows;

public class NoteTimingWindow : ITimingWindow
{
    // timings in ms
    private readonly (Judgement, double)[] timings = {
        (Judgement.Max,     16),
        (Judgement.Perfect, 33.33),
        (Judgement.Great,   50),
        (Judgement.Good,    75),
        (Judgement.Bad,     100),
    };
    
    public Time Center { get; }

    private Absyn.Note note;

    public NoteTimingWindow(Time t, Absyn.Note n)
    {
        Center = t;
        note = n;
    }

    public bool IsValidInput(Controls c)
    {
        if (note.Type == Absyn.Colour.Red)
        {
            return c.HasFlag(Controls.Primary1) ||
                   c.HasFlag(Controls.Primary2);
        }
        
        return c.HasFlag(Controls.Secondary1) ||
               c.HasFlag(Controls.Secondary2);

    }

    public HitResult? JudgeInput(Time t)
    {
        /*
        var hiterror = Center.InMilliseconds - fragment.Time.InMilliseconds;
        foreach ((Judgement j, double error) in timings)
        {
            if (Math.Abs(hiterror) <= error)
                return new HitResult(j, hiterror);
        }

        if (hiterror < 0)
            return null; // too early, ignore input
        
        // too late, miss!
        return new HitResult(Judgement.Miss, hiterror); 
        */
        return null;
    }
}