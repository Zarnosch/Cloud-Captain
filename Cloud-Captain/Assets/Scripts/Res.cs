using UnityEngine;
using System.Collections;

public class Res
{
    public int Matter;
    public int Energy;
    public int Engine;

    public Res(int matter, int energy, int engine)
    {
        Matter = matter;
        Energy = energy;
        Engine = engine;
    }

    public static Res operator + (Res r1, Res r2)
    {
        return new Res(r1.Matter + r2.Matter, r1.Energy + r2.Energy, r1.Engine + r2.Engine);
    }

    public static Res operator -(Res r1, Res r2)
    {
        return new Res(r1.Matter - r2.Matter, r1.Energy - r2.Energy, r1.Engine - r2.Engine);
    }
    public static Res operator *(Res r1, Res r2)
    {
        return new Res(r1.Matter * r2.Matter, r1.Energy * r2.Energy, r1.Engine * r2.Engine);
    }
    public static Res operator /(Res r1, Res r2)
    {
        return new Res(r1.Matter / r2.Matter, r1.Energy / r2.Energy, r1.Engine / r2.Engine);
    }

}
