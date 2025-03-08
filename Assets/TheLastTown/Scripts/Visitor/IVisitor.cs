using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVisitor
{
    protected void Visit(Soldier soldier);

    protected void Visit(Jack jack);
}
