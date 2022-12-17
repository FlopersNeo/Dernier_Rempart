namespace GSGD1
{
    using UnityEngine;

    public interface ICellChild
    {
        Transform GetTransform();
        void OnSetChild();
    }

    public class Cell : MonoBehaviour
    {
        private ICellChild _towerChild = null;

        public bool HasChild
        {
            get
            {
                MonoBehaviour concreteObj = _towerChild as MonoBehaviour;
                if (concreteObj)
                {
                    return true;
                }
                return false;
            }
        }

        public bool SetChild(ICellChild cellChild)
        {
            ATower tower = cellChild as ATower;
            int cost = tower._cost;
            if (ThunasseManager.Instance._currentMoney >= cost)
            {
                if (cellChild == null)
                {
                    return false;
                }
                var childTransform = cellChild.GetTransform();
                childTransform.SetParent(transform);
                childTransform.localPosition = Vector3.zero;
                cellChild.OnSetChild();
                _towerChild = cellChild;
            }

            return true;
        }
    }
}