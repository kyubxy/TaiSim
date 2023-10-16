using TaiSim.Framework.Allocation;
using TaiSim.Fumen;
using TaiSim.Ruleset.Drawables;
using TaiSim.Ruleset.Input;

namespace TaiSim.Ruleset;

public class Session 
{ 
    private readonly StatisticsTracker statisticsTracker;
    private readonly ScoreCalculator scoreCalculator;

    public PlayField DrawablePlayField { get; }
        
    private readonly InputManager inputManager;
    private readonly Clock clock;

    public Session(Chart.Map map)
    {
        clock = new Clock();
        
        statisticsTracker = new StatisticsTracker();
        scoreCalculator = new ScoreCalculator(10);
        
        DrawablePlayField = new PlayField();
        inputManager = new InputManager(map.Data);
    }

    public PollResponse PollInput(Controls c)
    {
        var frag = new InputFragment(c, clock.CurrentTime);
        var result = inputManager.Poll(frag);

        if (result.Status == PollResponse.PollStatus.Retrieved)
        {
            // update score
            statisticsTracker.AddHitResult(result.Args);
            scoreCalculator.IncrementResult(result.Args!.Judgement);
        }

        return result;
    }
}