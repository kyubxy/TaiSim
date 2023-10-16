using TaiSim.Framework.Structures;

namespace TaiSim.Ruleset.Input;

public enum Controls
{
    Primary1,
    Primary2,
    Secondary1,
    Secondary2
}

public record InputFragment(Controls Control, Time Time);
public record HitResult(Judgement Judgement, double HitError);
