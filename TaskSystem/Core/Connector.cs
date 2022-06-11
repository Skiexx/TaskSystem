namespace TaskSystem.Core;

public class Connector
{
    private static ReconstructionContext? _context;

    public static void ReloadContext()
    {
        _context = new ReconstructionContext();
    }
    
    public static ReconstructionContext GetContext()
    {
        return _context ??= new ReconstructionContext();
    }
}