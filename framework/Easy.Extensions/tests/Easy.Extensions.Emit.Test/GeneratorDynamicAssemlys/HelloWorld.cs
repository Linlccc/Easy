namespace Easy.Extensions.Emit.Test.GeneratorDynamicAssemlys;

/// <summary>
/// 以类代码是通过Emit 生成的
/// </summary>
public class HelloWorld
{
    private int _num;

    private string _text;

    private string Txtk__BackingField;

    public string Txt
    {
        get
        {
            return Txtk__BackingField;
        }
        set
        {
            Txtk__BackingField = value;
        }
    }

    public HelloWorld(int P_0, string P_1)
    {
        _num = P_0;
        _text = P_1;
        Txt = P_1 + " - " + P_0;
    }

    public HelloWorld(int P_0) : this(P_0, "aa") { }


    public HelloWorld() : this(100) { }


    public static void WriteHello()
    {
        Console.WriteLine("Hello World!");
    }

    public void WriteTxt()
    {
        Console.WriteLine(Txt);
    }

    public int Mul(int P_0)
    {
        return _num * P_0;
    }
}
