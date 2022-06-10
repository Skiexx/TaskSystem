namespace TaskSystem.Core;

public class Connector
{
    private static ReconstructionContext? _context;

    public static ReconstructionContext GetContext()
    {
        return _context ??= new ReconstructionContext();
    }
}