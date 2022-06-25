using ConsoleLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;
using VRC.Animation;
using VRC.SDKBase;
using xButtonAPI.Controls;
using xButtonAPI.Controls.Grouping;

namespace EXO.Modules
{
    internal class Movment : BaseModule
    {
        private static bool FlyToggle;
        private static bool FastFlyToggle;
        private static bool CameraFlyToggle;
        private static bool SpeedToggle;
        private static bool JetPackToggle = true;
        internal static VRCMotionState _motionState; //For Flys
        public override void OnQuickMenuInit()
        {            
            //Button Group
            var Movement = new ButtonGroup(MainModule.Movement, "<color=#9b0000>Movement</color>");                       
            //Buttons
            new ToggleButton(Movement, "Speed", "Enable Speed", "Disable Speed", (value) =>
            {
                SpeedToggle = value;
            });
            new ToggleButton(Movement, "Comfort Fly", "Enable Comfort Flying", "Disable Comfort Flying", (value) => 
            {               
                Physics.gravity = (value ? new Vector3(0f, 0f, 0f) : new Vector3(0f, -9.81f, 0f));                
                FlyToggle = value;
            });
            new ToggleButton(Movement, "Camera Fly", "Enable Camera Directional Flying", "Disable Camera Directional Flying", (value) =>
            {
                Physics.gravity = (value ? new Vector3(0f, 0f, 0f) : new Vector3(0f, -9.81f, 0f));
                CameraFlyToggle = value;
            });
            new ToggleButton(Movement, "Fast Fly", "Enable Fast Flying", "Disable Fast Flying", (value) => 
            {
                Physics.gravity = (value ? new Vector3(0f, 0f, 0f) : new Vector3(0f, -9.81f, 0f));
                FastFlyToggle = value;
            });
            new ToggleButton(Movement, "JetPack", "Toggle On JetPack", "Toggle Off JetPack", (value) =>
            {
                JetPackToggle = value;
            }).SetToggleState(true);
        }
        public override void OnUpdate()
        {
            ComfortFly();
            FastFly();
            CameraFly();
            Speed();
            JetPack();
        }

        //Comfort Fly        
        public static float FlySpeed = 2f;        
        private void ComfortFly()
        {
            var player = VRCPlayer.field_Internal_Static_VRCPlayer_0;
            if (player == null)
                return;

            if (_motionState == null)
                _motionState = player.GetComponent<VRCMotionState>();

            if (!FlyToggle) return;

            var playerTransform = player.transform;

            if (VRCInputManager.Method_Public_Static_VRCInput_String_0("Jump").prop_Single_2 == 1f)
            {
                Vector3 velocity = Networking.LocalPlayer.GetVelocity();
                velocity.y = Networking.LocalPlayer.GetJumpImpulse();
                Networking.LocalPlayer.SetVelocity(velocity);
            }

            if (XRDevice.isPresent)
            {                
                playerTransform.position += playerTransform.forward * Time.deltaTime * Input.GetAxis("Vertical") * FlySpeed;
                playerTransform.position += playerTransform.right * Time.deltaTime * Input.GetAxis("Horizontal") * FlySpeed;
                playerTransform.position += new Vector3(0f, Time.deltaTime * Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") * FlySpeed);
            }
            else
            {
                var speed = Input.GetKey(KeyCode.LeftShift) ? FlySpeed * 2 : FlySpeed;                
                playerTransform.position += playerTransform.forward * Time.deltaTime * Input.GetAxis("Vertical") * speed;
                playerTransform.position += playerTransform.right * Time.deltaTime * Input.GetAxis("Horizontal") * speed;                

                if (Input.GetKey(KeyCode.Q))
                    playerTransform.position -= new Vector3(0f, Time.deltaTime * speed, 0f);

                if (!Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.LeftControl))
                    playerTransform.position -= new Vector3(0f, Time.deltaTime * speed, 0f);

                if (Input.GetKey(KeyCode.E))
                    playerTransform.position += new Vector3(0f, Time.deltaTime * speed, 0f);                
            }
            _motionState?.Reset();
        }
               
        //Camera Fly
        private void CameraFly()
        {
            var player = VRCPlayer.field_Internal_Static_VRCPlayer_0;
            if (player == null)
                return;

            if (_motionState == null)
                _motionState = player.GetComponent<VRCMotionState>();

            if (!CameraFlyToggle) return;

            var playerTransform = player.transform;
            var camera = Camera.main.transform;

            if (VRCInputManager.Method_Public_Static_VRCInput_String_0("Jump").prop_Single_2 == 1f)
            {
                Vector3 velocity = Networking.LocalPlayer.GetVelocity();
                velocity.y = Networking.LocalPlayer.GetJumpImpulse();
                Networking.LocalPlayer.SetVelocity(velocity);
            }
            if (XRDevice.isPresent)
            {                
                playerTransform.position += camera.forward * Time.deltaTime * Input.GetAxis("Vertical") * FlySpeed;
                playerTransform.position += camera.right * Time.deltaTime * Input.GetAxis("Horizontal") * FlySpeed;
                playerTransform.position += new Vector3(0f, Time.deltaTime * Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") * FlySpeed);
            }
            else
            {
                var speed = Input.GetKey(KeyCode.LeftShift) ? FlySpeed * 2 : FlySpeed;                
                playerTransform.position += camera.forward * Time.deltaTime * Input.GetAxis("Vertical") * speed;
                playerTransform.position += camera.right * Time.deltaTime * Input.GetAxis("Horizontal") * speed;

                if (Input.GetKey(KeyCode.Q))
                    playerTransform.position -= new Vector3(0f, Time.deltaTime * speed, 0f);

                if (!Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.LeftControl))
                    playerTransform.position -= new Vector3(0f, Time.deltaTime * speed, 0f);

                if (Input.GetKey(KeyCode.E))
                    playerTransform.position += new Vector3(0f, Time.deltaTime * speed, 0f);
            }
            _motionState?.Reset();
        }

        //Fast Fly
        public static float FastFlySpeed = 10f;
        private void FastFly()
        {
            var player = VRCPlayer.field_Internal_Static_VRCPlayer_0;
            if (player == null)
                return;

            if (_motionState == null)
                _motionState = player.GetComponent<VRCMotionState>();

            if (!FastFlyToggle) return;

            var playerTransform = player.transform;

            if (VRCInputManager.Method_Public_Static_VRCInput_String_0("Jump").prop_Single_2 == 1f)
            {
                Vector3 velocity = Networking.LocalPlayer.GetVelocity();
                velocity.y = Networking.LocalPlayer.GetJumpImpulse();
                Networking.LocalPlayer.SetVelocity(velocity);
            }

            if (XRDevice.isPresent)
            {
                playerTransform.position += playerTransform.forward * Time.deltaTime * Input.GetAxis("Vertical") * FastFlySpeed;
                playerTransform.position += playerTransform.right * Time.deltaTime * Input.GetAxis("Horizontal") * FastFlySpeed;
                playerTransform.position += new Vector3(0f, Time.deltaTime * Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") * FastFlySpeed);
            }
            else
            {
                var speed = Input.GetKey(KeyCode.LeftShift) ? FastFlySpeed * 2 : FastFlySpeed;
                playerTransform.position += playerTransform.forward * Time.deltaTime * Input.GetAxis("Vertical") * speed;
                playerTransform.position += playerTransform.right * Time.deltaTime * Input.GetAxis("Horizontal") * speed;

                if (Input.GetKey(KeyCode.Q))
                    playerTransform.position -= new Vector3(0f, Time.deltaTime * speed, 0f);

                if (!Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.LeftControl))
                    playerTransform.position -= new Vector3(0f, Time.deltaTime * speed, 0f);

                if (Input.GetKey(KeyCode.E))
                    playerTransform.position += new Vector3(0f, Time.deltaTime * speed, 0f);
            }
            _motionState?.Reset();
        }

        //Speed
        public static float SpeedVal = 3f;
        private void Speed()
        {
            var player = VRCPlayer.field_Internal_Static_VRCPlayer_0;
            if (player == null)
                return;

            if (_motionState == null)
                _motionState = player.GetComponent<VRCMotionState>();

            if (!SpeedToggle) return;

            var playerTransform = player.transform;
            var speed = Input.GetKey(KeyCode.LeftShift) ? SpeedVal * 2 : SpeedVal;
            playerTransform.position += playerTransform.forward * Time.deltaTime * Input.GetAxis("Vertical") * speed;
            playerTransform.position += playerTransform.right * Time.deltaTime * Input.GetAxis("Horizontal") * speed;            
        }        

        //JetPack
        private void JetPack()
        {
            if (!JetPackToggle) return;
            if (VRCPlayer.field_Internal_Static_VRCPlayer_0 == null) return;

            if (VRCInputManager.Method_Public_Static_VRCInput_String_0("Jump").prop_Single_2 == 1f)
            {
                Vector3 velocity = Networking.LocalPlayer.GetVelocity();
                velocity.y = Networking.LocalPlayer.GetJumpImpulse();
                Networking.LocalPlayer.SetVelocity(velocity);
            }
        }

        /* Edward Fast Fly
        private Transform camera()
        {
        return GameObject.Find("Camera (eye)").transform;
        }
        same as above, (below is more compact)
        private static Transform camera() =>
        GameObject.Find("Camera (eye)").transform;
        private void NoctFly()
        {                   
            if (!FastFlyToggle) return;
            if (VRC.Player.prop_Player_0 == null) return;

            float flyspeed = Input.GetKey(KeyCode.LeftShift) ? Time.deltaTime * 60 : Time.deltaTime * 30;
            if (VRC.Player.prop_Player_0.field_Private_VRCPlayerApi_0.IsUserInVR())
            {
                if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") < 0f)
                    VRC.Player.prop_Player_0.transform.position += camera().up * flyspeed;
                if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") > 0f)
                    VRC.Player.prop_Player_0.transform.position -= camera().up * flyspeed;

                if (Input.GetAxis("Vertical") != 0f)
                    VRC.Player.prop_Player_0.transform.position += camera().forward * (flyspeed * Input.GetAxis("Vertical"));

                if (Input.GetAxis("Horizontal") != 0f)
                    VRC.Player.prop_Player_0.transform.position += camera().transform.right * (flyspeed * Input.GetAxis("Horizontal"));
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                    VRC.Player.prop_Player_0.transform.position += camera().forward * flyspeed;

                if (Input.GetKey(KeyCode.S))
                    VRC.Player.prop_Player_0.transform.position -= camera().forward * flyspeed;

                if (Input.GetKey(KeyCode.A))
                    VRC.Player.prop_Player_0.transform.position -= camera().right * (flyspeed / 2);

                if (Input.GetKey(KeyCode.D))
                    VRC.Player.prop_Player_0.transform.position += camera().right * (flyspeed / 2);

                if (Input.GetKey(KeyCode.Q))
                    VRC.Player.prop_Player_0.transform.position -= camera().up * (flyspeed / 2);

                if (Input.GetKey(KeyCode.E))
                    VRC.Player.prop_Player_0.transform.position += camera().up * (flyspeed / 2);
            }
        } */
    }
}
