using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class BehaviourInterpreter : MonoBehaviour {

    private ChainedBehaviour _currentChainedBehaviour;

    [SerializeField] private List<string> behaviourNames;

    [SerializeField] private List<UnityEvent> commandList;

    public ChainedBehaviour GetBehaviourByName(string behaviourName) {
        _currentChainedBehaviour = new ChainedBehaviour();
        commandList[behaviourNames.IndexOf(behaviourName)].Invoke();
        return FinishChainedBehaviour();
    }

    public void AddDelay(float delay)
    {
        _currentChainedBehaviour.functionList.Add(Delay);
        _currentChainedBehaviour.argumentsList.Add(new ChainedBehaviourArgument(delay));
    }

    public IEnumerator Delay(GeneralHumanBehaviours entity, ChainedBehaviourArgument arguments)
    {
        yield return new WaitForSeconds(arguments.floatParameter);
        
    }

    public void AddPatrol(EntityPath pathToPatrol)
    {
        _currentChainedBehaviour.functionList.Add(Patrol);
        _currentChainedBehaviour.argumentsList.Add(new ChainedBehaviourArgument(pathToPatrol));
    }

    public IEnumerator Patrol(GeneralHumanBehaviours entity, ChainedBehaviourArgument arguments)
    {
        yield return entity.FollowPath(arguments.pathParameter, true);
    }

    public void AddFollow(Transform target) {
        _currentChainedBehaviour.functionList.Add(Follow);
        _currentChainedBehaviour.argumentsList.Add(new ChainedBehaviourArgument(target));
    }

    public IEnumerator Follow(GeneralHumanBehaviours entity, ChainedBehaviourArgument arguments) {
        yield return entity.FollowCor(arguments.targetParameter);
    }
    
    public void AddFollowUntilTouched(Transform target) {
        _currentChainedBehaviour.functionList.Add(FollowUntilTouched);
        _currentChainedBehaviour.argumentsList.Add(new ChainedBehaviourArgument(target));
    }

    public IEnumerator FollowUntilTouched(GeneralHumanBehaviours entity, ChainedBehaviourArgument arguments) {
        yield return entity.FollowCor(arguments.targetParameter,true);
    }
    
    public void AddGoTo(Transform target) {
        _currentChainedBehaviour.functionList.Add(GoTo);
        _currentChainedBehaviour.argumentsList.Add(new ChainedBehaviourArgument(target));
    }
    
    public IEnumerator GoTo(GeneralHumanBehaviours entity, ChainedBehaviourArgument arguments) {
        yield return entity.GoToCor(arguments.targetParameter);
    }
    
    public void AddWalk() {
        _currentChainedBehaviour.functionList.Add(Walk);
        _currentChainedBehaviour.argumentsList.Add(null);
    }
    
    public IEnumerator Walk(GeneralHumanBehaviours entity, ChainedBehaviourArgument arguments) {
        entity.Walk();
        yield return null;
    }
    
    public void AddRun() {
        _currentChainedBehaviour.functionList.Add(Run);
        _currentChainedBehaviour.argumentsList.Add(null);
    }
    
    public IEnumerator Run(GeneralHumanBehaviours entity, ChainedBehaviourArgument arguments) {
        entity.Run();
        yield return null;
    }
    
    public void AddDestroy() {
        _currentChainedBehaviour.functionList.Add(Destroy);
        _currentChainedBehaviour.argumentsList.Add(null);
    }
    
    public IEnumerator Destroy(GeneralHumanBehaviours entity, ChainedBehaviourArgument arguments) {
        yield return new WaitForEndOfFrame();
        Destroy(entity.gameObject);
    }
    
    public ChainedBehaviour FinishChainedBehaviour() {
        return _currentChainedBehaviour;
    }
}




