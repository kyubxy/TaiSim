using System;

namespace TaiSim.Ruleset;

public class ScoreCalculator
{
    public int Score { get; private set; }
    
    // TODO: compute score delta by counting notes in chart
    public int ScoreDelta { get; private set; }

    public ScoreCalculator(int d) => ScoreDelta = d;

    public void IncrementResult(Judgement j)
    {
        Score += (int)Math.Round(JudgementHelper.GetValue(j)*ScoreDelta);
    }
}