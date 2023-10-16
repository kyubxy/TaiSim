#nullable enable
using System;
using System.Collections.Generic;
using TaiSim.Framework.Structures;

namespace TaiSim.Fumen;

public class LogicalTimeline : ITimeline<Absyn>
{
    private int index;
    private List<Absyn> data;

    public LogicalTimeline(List<Absyn> data)
    {
        this.data = data;
    }

    public Absyn? GetNext()
    {
        if (index > data.Count)
            return null;
        return data[++index];
    }

    public bool Seek(Time t)
    {
        // set index based on measure and value
        double unit = -1;
        double bpm;
        var i = 0;
        
        foreach (var a in data)
        {
            if (a.GetType() == typeof(Absyn.Marker))
            {
                var m = (Absyn.Marker)a;
                switch (m.Type)
                {
                    case Absyn.Marker.DataType.BPM:
                        bpm = m.Value;
                        break;
                    case Absyn.Marker.DataType.Timing:
                        unit = m.Value;
                        break;
                }
            } 
            else if (a.GetType() == typeof(Absyn.Rest))
            {
                if (unit < 0)
                    throw new Exception("rest given before unit specified");
                var r = (Absyn.Rest)a;
                t -= new Time(r.Quantity, unit);
            }

            if (t.Value <= 0)
                break;
            
            i++;
        }

        if (i > data.Count)
            return false;
        
        index = i;
        return true;
    }
}