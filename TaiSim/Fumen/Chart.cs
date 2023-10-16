using System.Collections.Generic;

namespace TaiSim.Fumen;

public record Chart
{
    // NOTE: we take a convention of prepending R to mean romanised
    
    public string RTitle;
    public string RSongName;
    public string RArtist;
    public List<Map> Difficulties;
    public int Offset;
    
    public record Map
    {
        public string DifficultyName;
        public string Author;
        public float Difficulty;
        public LogicalTimeline Data;
    }
}