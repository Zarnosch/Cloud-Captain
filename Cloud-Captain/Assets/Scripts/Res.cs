using UnityEngine;
using System.Collections;
using System;

//enables unity to show variable of Res in the inspector:
/// <summary>Order: Matter, Energy, Engine</summary>
[Serializable]
public struct Res
{
    public static Res Zero { get { return new Res(0, 0, 0); } }


    public int Matter;
    public int Energy;
    public int Engine;

    public Res(int matter, int energy, int engine)
    {
        Matter = matter;
        Energy = energy;
        Engine = engine;
    }


    public override string ToString()
    {
        return string.Concat("Matter: ", Matter, ", Energy: ", Energy, ", Engine: ", Engine);
    }



    public static Res operator *(Res left, int right)
    {
        return new Res(left.Matter * right, left.Energy * right, left.Engine * right);
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
