namespace TaiSim.Fumen;

public abstract class Absyn
{
    #region helpers
    
    public enum Colour
    {
        Red, // Don
        Blue // Ka
    }

    public enum Size 
    {
        Regular,
        Large
    }
    
    #region visitor

    public abstract T Accept<T>(IVisitor<T> v);
    
    public interface IVisitor<T>
    {
        T Visit(Marker a);
        T Visit(Note a);
        T Visit(Roll a);
        T Visit(Rest a);
    }
    
    #endregion
    
    #endregion
    
    #region absyn definitions
    
    public class Marker : Absyn
    {
        public enum DataType
        {
            BPM,
            Timing
        }

        public DataType Type;
        public double Value;
        
        public override T Accept<T>(IVisitor<T> v) => v.Visit(this);
    }

    public class Rest : Absyn
    {
        public int Quantity;
        public override T Accept<T>(IVisitor<T> v) => v.Visit(this);
    }
    
    public class Note : Absyn
    {
        public Size Strength;
        public Colour Type;
        
        public override T Accept<T>(IVisitor<T> v) => v.Visit(this);
    }

    public class Roll : Absyn
    {
        public Size Strength;
        public int Length;
        public int Duration;
        
        public override T Accept<T>(IVisitor<T> v) => v.Visit(this);
    }

    #endregion
}
