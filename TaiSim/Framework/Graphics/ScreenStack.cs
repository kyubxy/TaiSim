using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TaiSim.Framework.Graphics;

public class ScreenStack : Node
{
    private Stack<Screen> stack;

    public ScreenStack()
    {
        stack = new Stack<Screen>();
    }
    
    ContentManager cm;

    public override void Load(ContentManager cm)
    { 
        this.cm = cm;   
        
        // run any screens that were pending load
        Screen scr;
        if (!stack.TryPeek(out scr))
            return;
        scr.Load(cm);
        scr.OnEnter();
    }
    
    public void Push(Screen s)
    {
        // suspend the existing screen
        Screen currentTop;
        if (stack.TryPeek(out currentTop))
            currentTop.OnSuspend();
        
        // push the new screen
        if (stack.Count > 1)
            throw new Exception(
                "the screen stack hasn't loaded yet, the stack can at maximum leave one screen pending" +
                "for load, please load the stack first before attempting to push more screens.");
        stack.Push(s);
        if (cm != null)
        {
            s.Load(cm);
            s.OnEnter();
        }
    }

    public void Pop()
    {
        Screen scr;
        
        // exit the old screen
        if (stack.TryPop(out scr))
            scr.OnExit();
        
        // resume the existing screen
        if (stack.TryPeek(out scr))
            scr.OnResume();
    }

    public override void Update(GameTime gt)
    {
        base.Update(gt);

        if (ExecutionMode.HasFlag(Execution.NoUpdate))
            return;
        
        Screen scr;
        if (stack.TryPeek(out scr))
            scr.Update(gt);
    }

    public override void Draw(GameTime gt, SpriteBatch sb)
    {
        base.Draw(gt, sb);
        
        if (ExecutionMode.HasFlag(Execution.NoDraw))
            return;
        
        Screen scr;
        if (stack.TryPeek(out scr))
            scr.Draw(gt, sb);
    }
}