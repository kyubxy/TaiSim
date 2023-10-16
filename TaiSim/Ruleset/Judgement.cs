using System.Runtime.CompilerServices;

namespace TaiSim.Ruleset;

public enum Judgement
{
    Max,
    Perfect,
    Great,
    Good,
    Bad,
    Miss,
}

public static class JudgementHelper
{
    public static bool CanCombo(Judgement j) 
        => j != Judgement.Good && j != Judgement.Bad && j != Judgement.Miss;
    
    public static float GetValue(Judgement j)
    {
        switch (j)
        {
            case Judgement.Max:
            case Judgement.Perfect:
                return 1.0f;
            case Judgement.Great:
                return 0.75f;
            case Judgement.Good:
                return 0.40f;
            case Judgement.Bad:
                return 0.15f;
            case Judgement.Miss:
                return 0f;
        }

        throw new SwitchExpressionException();
    }
}
