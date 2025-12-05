using UnityEngine;

public class ChainedBehaviourArgument {

    public Transform targetParameter;
    
    public Vector3 positionParameter;

    public float floatParameter;

    public EntityPath pathParameter;
    
    public ChainedBehaviourArgument() {
    }
    public ChainedBehaviourArgument(EntityPath pathParameter)
    {
        this.pathParameter = pathParameter;
    }

    public ChainedBehaviourArgument(float floatParameter)
    {
        this.floatParameter = floatParameter;
    }

    public ChainedBehaviourArgument(Vector3 positionParameter) {
        this.positionParameter = positionParameter;
    }

    public ChainedBehaviourArgument(Transform targetParameter) {
        this.targetParameter = targetParameter;
    }
}