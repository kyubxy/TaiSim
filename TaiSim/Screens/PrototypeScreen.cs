using System;
using System.Collections.Generic;
using TaiSim.Framework;
using TaiSim.Framework.Graphics;
using TaiSim.Fumen;
using TaiSim.Ruleset;

namespace TaiSim.Screens;

public class PrototypeScreen : Screen
{
    private Session session;
    
    public PrototypeScreen()
    {
        Chart testchart = new Chart()
        {
            RSongName = "sandstorm",
            RArtist = "darude",
            Difficulties = new List<Chart.Map>(new Chart.Map[]
            {
                new()
                {
                    Author = "kyubey",
                    Difficulty = 69,
                    DifficultyName = "Another",
                    //Data = new LogicalTimeline()
                }
            })
        };
        
        session = new Session(testchart.Difficulties[0]);
        
        Add(session.DrawablePlayField);
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
        Console.WriteLine("entered");
    }
}