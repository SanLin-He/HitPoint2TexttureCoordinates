using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
    public class DragRigidbody : MonoBehaviour
    {
        const float k_Spring = 50.0f;
        const float k_Damper = 5.0f;
        const float k_Drag = 10.0f;
        const float k_AngularDrag = 5.0f;
        const float k_Distance = 0.2f;
        const bool k_AttachToCenterOfMass = false;
        int i = 0;
        private SpringJoint[] m_SpringJoint = new SpringJoint[2];


        private void Update()
        {
            // Make sure the user pressed the mouse down
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            var mainCamera = FindCamera();

            // We need to actually hit an object

            RaycastHit[] hits = Physics.RaycastAll(mainCamera.ScreenPointToRay(Input.mousePosition).origin,
                            mainCamera.ScreenPointToRay(Input.mousePosition).direction, 100,
                            Physics.DefaultRaycastLayers);
            // We need to hit a rigidbody that is not kinematic

            foreach (RaycastHit hit in hits)
            {
                if (!hit.rigidbody || hit.rigidbody.isKinematic)
                {
                    return;
                }

                if (!m_SpringJoint[i])
                {
                    var go = new GameObject("Rigidbody dragger");
                    Rigidbody body = go.AddComponent<Rigidbody>();
                    m_SpringJoint[i] = go.AddComponent<SpringJoint>();
                    body.isKinematic = true;
                }

                m_SpringJoint[i].transform.position = hit.point;
                m_SpringJoint[i].anchor = Vector3.zero;

                m_SpringJoint[i].spring = k_Spring;
                m_SpringJoint[i].damper = k_Damper;
                m_SpringJoint[i].maxDistance = k_Distance;
                m_SpringJoint[i].connectedBody = hit.rigidbody;
                if (i == 0)
                    StartCoroutine("DragObject1", hit.distance);
                else if (i == 1)
                    StartCoroutine("DragObject2", hit.distance);
                i = ++i % 2;
            }
        }


        private IEnumerator DragObject1(float distance)
        {
            var oldDrag = m_SpringJoint[0].connectedBody.drag;
            var oldAngularDrag = m_SpringJoint[0].connectedBody.angularDrag;
            m_SpringJoint[0].connectedBody.drag = k_Drag;
            m_SpringJoint[0].connectedBody.angularDrag = k_AngularDrag;
            var mainCamera = FindCamera();
            while (Input.GetMouseButton(0))
            {
                var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                m_SpringJoint[0].transform.position = ray.GetPoint(distance);
                yield return null;
            }
            if (m_SpringJoint[0].connectedBody)
            {
                m_SpringJoint[0].connectedBody.drag = oldDrag;
                m_SpringJoint[0].connectedBody.angularDrag = oldAngularDrag;
                m_SpringJoint[0].connectedBody = null;
            }
        }
        private IEnumerator DragObject2(float distance)
        {
            var oldDrag = m_SpringJoint[1].connectedBody.drag;
            var oldAngularDrag = m_SpringJoint[1].connectedBody.angularDrag;
            m_SpringJoint[1].connectedBody.drag = k_Drag;
            m_SpringJoint[1].connectedBody.angularDrag = k_AngularDrag;
            var mainCamera = FindCamera();
            while (Input.GetMouseButton(0))
            {
                var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                m_SpringJoint[1].transform.position = ray.GetPoint(distance);
                yield return null;
            }
            if (m_SpringJoint[1].connectedBody)
            {
                m_SpringJoint[1].connectedBody.drag = oldDrag;
                m_SpringJoint[1].connectedBody.angularDrag = oldAngularDrag;
                m_SpringJoint[1].connectedBody = null;
            }
        }

        private Camera FindCamera()
        {
            if (GetComponent<Camera>())
            {
                return GetComponent<Camera>();
            }

            return Camera.main;
        }
    }
}
