using System;
using UnityEngine;

namespace Script
{
    public class movement : MonoBehaviour
    {
        public float velocity = 0.2f;
        public float angularVelocity = 0.2f;

        public void Update()
        {
            #region input
            Vector2 movement= Vector2.zero;
            if(Input.GetKey(KeyCode.D))
                movement += new Vector2(1,0);
            if(Input.GetKey(KeyCode.A))
                movement -= new Vector2(1,0);
            if(Input.GetKey(KeyCode.W))
                movement += new Vector2(0,1);
            if(Input.GetKey(KeyCode.S))
                movement -= new Vector2(0,1);

            float rotation = 0;
            if(Input.GetKey(KeyCode.E))
                rotation += 1;
            if(Input.GetKey(KeyCode.Q))
                rotation -= 1;
            #endregion

            
        }
        
        
    }
}