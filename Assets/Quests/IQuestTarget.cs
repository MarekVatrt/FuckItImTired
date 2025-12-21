using UnityEngine;
//This interface just says:
// If you are a quest target, you must provide a Transform

public interface IQuestTarget
{
    Transform TargetTransform { get; }
}
