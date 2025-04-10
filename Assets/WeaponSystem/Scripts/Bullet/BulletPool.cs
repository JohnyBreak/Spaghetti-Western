using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem.Bullet
{
    public class BulletPool : BaseObjectPool<Bullet>
    {
        private List<Vector3> _lastPositions = new List<Vector3>();
        public List<Vector3> LastPositions => _lastPositions;

        protected override Bullet CreateNewObject()
        {
            Bullet obj = Instantiate(_objectPrefab, transform.position, Quaternion.identity);

            BackObjectToPool(obj);
            _pooledObjects.Add(obj);
            _lastPositions.Add(obj.transform.position);
            return obj;
        }
    }
}