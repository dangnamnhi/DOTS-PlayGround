using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Nam.Zomebies
{
    public class ZombieMono : MonoBehaviour
    {
        public float RiseRate;
    }
    public class ZombieBaker : Baker<ZombieMono>
    {
        public override void Bake(ZombieMono authoring)
        {
            //Đây là một IComponentData sẽ chứa dữ liệu RiseRate của ZombieMono
            AddComponent(new ZombieRiseRate { Value = authoring.RiseRate });
        }
    }
}
