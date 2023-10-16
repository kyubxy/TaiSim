#nullable enable
using System;
using System.Collections.Generic;
using TaiSim.Framework.Structures;
using TaiSim.Fumen;
using TaiSim.Ruleset.Input.TimingWindows;

namespace TaiSim.Ruleset.Input;

public class InputTimeline : ITimeline<ITimingWindow>
{
    private class AbsynInputInterpreter : Absyn.IVisitor<ITimingWindow?>
    {
        private Time t = Time.Zero;
        private double unit = -1;
        
        public ITimingWindow? Visit(Absyn.Marker a)
        {
            throw new NotImplementedException();
        }

        public ITimingWindow Visit(Absyn.Note a)
        {
            throw new NotImplementedException();
        }

        public ITimingWindow Visit(Absyn.Roll a)
        {
            throw new NotImplementedException();
        }

        public ITimingWindow? Visit(Absyn.Rest a)
        {
            throw new NotImplementedException();
        }
    }
    
    private readonly List<ITimingWindow> inputTimeline;
    private int position;

    public InputTimeline(LogicalTimeline t)
    {
        inputTimeline = new List<ITimingWindow>();
        var interpreter = new AbsynInputInterpreter();
        Absyn? buf;
        t.Seek(Time.Zero);
        while ((buf = t.GetNext()) != null)
        {
            var res = buf.Accept(interpreter);
            if (res != null)
                inputTimeline.Add(res);
        }
    }

    public ITimingWindow? GetNext()
    {
        if (position > inputTimeline.Count)
            return null;
        return inputTimeline[position++];
    }

    public bool Seek(Time t)
    {
        int i = 0;
        foreach (var w in inputTimeline)
        {
            if (w.JudgeInput(t)!.Judgement != Judgement.Miss)
            {
                position = i;
                break;
            }

            i++;
        }

        return i <= inputTimeline.Count;
    }
    
}