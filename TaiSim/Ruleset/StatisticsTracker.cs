using System.Collections.Generic;
using System.Linq;
using TaiSim.Ruleset.Input;

namespace TaiSim.Ruleset;

public enum Achievement
{
    Clear,
    FullCombo,
    AllPerfect,
}

public class StatisticsTracker
{
    public int Combo { get; private set; }
    public double AverageHitError => errors.Average();

    public bool FullCombo => ratios[Judgement.Miss] + 
        ratios[Judgement.Bad] + ratios[Judgement.Good] == 0;
    
    public bool AllPerfect => ratios[Judgement.Miss] + 
        ratios[Judgement.Bad] + ratios[Judgement.Good] + 
        ratios[Judgement.Great] == 0;
    
    private Dictionary<Judgement, int> ratios = new();
    private List<double> errors = new();

    public void AddHitResult(HitResult hr)
    {
        var j = hr.Judgement;
        ratios.Add(j, ratios[j]++);
        errors.Add(hr.HitError);
        if (JudgementHelper.CanCombo(j))
            Combo++;
        else
            Combo = 0;
    }

    public int GetJudgementRatio(Judgement j) => ratios[j];

    public Achievement GetFinalAchievement()
    {
        if (AllPerfect)
            return Achievement.AllPerfect;
        if (FullCombo)
            return Achievement.FullCombo;
        
        return Achievement.Clear;
    }
}