#nullable enable
using TaiSim.Framework.Structures;

namespace TaiSim.Ruleset.Input.TimingWindows;

public interface ITimingWindow
{
    bool IsValidInput(Controls c);
    HitResult? JudgeInput(Time t);
}