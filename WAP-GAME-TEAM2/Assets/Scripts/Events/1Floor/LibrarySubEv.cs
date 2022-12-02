using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibrarySubEv : DefaultEvent
{
    // Start is called before the first frame update
    protected override IEnumerator ExtraEventCo() 
    {
        if (!theEvent.switches[(int)theSwitch])
            theEvent.switches[(int)theSwitch] = true;
        yield break;
    }
}
