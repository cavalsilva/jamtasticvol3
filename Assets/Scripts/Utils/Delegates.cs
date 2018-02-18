using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jamtasticvol3.Utils
{
    public class Delegates
    {
        public delegate void SimpleEvent();
        public delegate void IntEvent(int i);
        public delegate void StringEvent(string s);
    }
}