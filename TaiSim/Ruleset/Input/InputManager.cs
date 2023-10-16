#nullable enable
using TaiSim.Fumen;
using TaiSim.Ruleset.Input.TimingWindows;

namespace TaiSim.Ruleset.Input;

public class InputManager 
{
    private readonly InputTimeline inputTimeline;
    private ITimingWindow? currentWindow;

    public InputManager(LogicalTimeline tl)
    {
        inputTimeline = new InputTimeline(tl);
        currentWindow = inputTimeline.GetNext();
    }

    public PollResponse Poll(InputFragment input)
    {
        if (currentWindow == null)
            return new PollResponse(PollResponse.PollStatus.Suspended);
        
        // block non matching input
        if (!currentWindow.IsValidInput(input.Control))
            return new PollResponse(PollResponse.PollStatus.Pending);
        
        var result = currentWindow?.JudgeInput(input.Time);
        
        // only move onto the next note if we actually hit the current note
        if (result != null) 
            currentWindow = inputTimeline.GetNext();

        return new PollResponse(PollResponse.PollStatus.Retrieved, result);
    }

}

public record PollResponse (PollResponse.PollStatus Status, HitResult? Args = null)
{
    public enum PollStatus
    {
        Pending,
        Retrieved,
        Suspended
    }
}
