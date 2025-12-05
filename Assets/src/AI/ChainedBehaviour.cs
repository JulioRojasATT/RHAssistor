using System.Collections;
using System.Collections.Generic;

public class ChainedBehaviour {

    public delegate IEnumerator ChainedBehaviourFunction(GeneralHumanBehaviours entity, ChainedBehaviourArgument arguments);

    public List<ChainedBehaviourFunction> functionList = new List<ChainedBehaviourFunction>();
    
    public List<ChainedBehaviourArgument> argumentsList = new List<ChainedBehaviourArgument>();
}