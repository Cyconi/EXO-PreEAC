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
    internal class Movement : BaseModule
    {        
        internal static bool FlyToggle;
        internal static bool FastFlyToggle;        
        internal static bool CameraFlyToggle;
        internal static bool SpeedToggle;
        internal static bool NoClipToggle = false;
        private static bool JetPackToggle = true;
        private static bool SpaceCope;
        private static ToggleButton SpaceToggleBtn;
        internal static VRCMotionState _motionState;
        private static readonly List<int> _disabledColliders = new List<int>();
        public override void OnQuickMenuInit()
        {            
            //Button Group
            var Movement = new ButtonGroup(MainModule.Movement, "<color=#9b0000>Movement</color>");                       
            //Buttons
            new ToggleButton(Movement, "Speed", "Enable Speed", "Disable Speed", (value) =>
            {
                SpeedToggle = value;
            });
            new ToggleButton(Movement, "NoClip", "No Clip", "Disable No Clip", (value) =>
            {
                if (FlyToggle || FastFlyToggle || CameraFlyToggle)
                {
                    NoClipToggle = value;
                    NoClip();
                }
                else
                    CLog.L("Turn On Fly First");                
            });            
            new ToggleButton(Movement, "Comfort Fly", "Enable Comfort Flying", "Disable Comfort Flying", (value) => 
            {
                if (NoClipToggle)
                    CLog.L("Turn Off No Clip");
                else
                {
                    Physics.gravity = (value ? new Vector3(0f, 0f, 0f) : new Vector3(0f, -9.81f, 0f));
                    FlyToggle = value;
                }
            });
            new ToggleButton(Movement, "Camera Fly", "Enable Camera Directional Flying", "Disable Camera Directional Flying", (value) =>
            {
                if (NoClipToggle)
                    CLog.L("Turn Off No Clip");
                else
                {
                    Physics.gravity = (value ? new Vector3(0f, 0f, 0f) : new Vector3(0f, -9.81f, 0f));
                    CameraFlyToggle = value;
                }                               
            });
            new ToggleButton(Movement, "Fast Fly", "Enable Fast Flying", "Disable Fast Flying", (value) => 
            {
                if (NoClipToggle)
                    CLog.L("Turn Off No Clip");
                else
                {
                    Physics.gravity = (value ? new Vector3(0f, 0f, 0f) : new Vector3(0f, -9.81f, 0f));
                    FastFlyToggle = value;
                }               
            });
            new ToggleButton(Movement, "JetPack", "Toggle On JetPack", "Toggle Off JetPack", (value) =>
            {
                JetPackToggle = value;
            }).SetToggleState(true);
            new ToggleButton(Movement, "Space Jump", "Are you sure?", "nah nvm", (value) =>
            {
                SpaceToggleBtn.SetActive(value);
            });
            SpaceToggleBtn = new ToggleButton(Movement, "Death Space Jump", "Bruh", "Its Not to Late...", (value) =>
            {
                SpaceCope = value;
            });
            SpaceToggleBtn.SetActive(false);
        }
        public override void OnUpdate()
        {
            ComfortFly();
            FastFly();
            CameraFly();
            Speed();            
            JetPack();
            ToSpace();
        }
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (NoClipToggle)
                NoClip();
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

            if (FlyToggle)
                Physics.gravity = new Vector3(0f, 0f, 0f);

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

            if (CameraFlyToggle)
                Physics.gravity = new Vector3(0f, 0f, 0f);

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

            if (FastFlyToggle)
                Physics.gravity = new Vector3(0f, 0f, 0f);

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

                if (Input.GetKey(KeyCode.E))
                    playerTransform.position += new Vector3(0f, Time.deltaTime * speed, 0f);
            }
            _motionState?.Reset();
        }
        private static readonly List<Il2CppSystem.Type> DontAntiClip = new List<Il2CppSystem.Type>
        {
            UnhollowerRuntimeLib.Il2CppType.Of<PlayerSelector>(),
            UnhollowerRuntimeLib.Il2CppType.Of<VRC_Pickup>(),
            UnhollowerRuntimeLib.Il2CppType.Of<QuickMenu>(),
            UnhollowerRuntimeLib.Il2CppType.Of<VRCSDK2.VRC_Station>(),
            UnhollowerRuntimeLib.Il2CppType.Of<VRC_AvatarPedestal>(),
            UnhollowerRuntimeLib.Il2CppType.Of<VRC_UiShape>(),
            UnhollowerRuntimeLib.Il2CppType.Of<VRC.SDK3.Components.VRCUiShape>()
        };
        internal static void NoClip()
        {
            var player = VRCPlayer.field_Internal_Static_VRCPlayer_0;
            if (player == null)
                return;

            var colliders = UnityEngine.Object.FindObjectsOfType<Collider>();
            var ownCollider = player.GetComponents<Collider>().FirstOrDefault();

            foreach (var collider in colliders)
            {
                if (DontAntiClip.Any(comp => collider.GetComponent(comp) != null))
                    continue;


                if (collider == ownCollider)
                    continue;

                if (!(NoClipToggle && collider.enabled || !NoClipToggle && _disabledColliders.Contains(collider.GetInstanceID())))
                    continue;

                collider.enabled = !NoClipToggle;
                if (NoClipToggle)
                    _disabledColliders.Add(collider.GetInstanceID());

            }
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
            
            if (XRDevice.isPresent)
            {
                playerTransform.position += playerTransform.forward * Time.deltaTime * Input.GetAxis("Vertical") * FastFlySpeed;
                playerTransform.position += playerTransform.right * Time.deltaTime * Input.GetAxis("Horizontal") * FastFlySpeed;
                if (FlyToggle || FastFlyToggle || CameraFlyToggle)
                {
                    playerTransform.position += new Vector3(0f, Time.deltaTime * Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") * FastFlySpeed);
                }
            }
            else
            {
                var speed = Input.GetKey(KeyCode.LeftShift) ? SpeedVal * 2 : SpeedVal;
                playerTransform.position += playerTransform.forward * Time.deltaTime * Input.GetAxis("Vertical") * speed;
                playerTransform.position += playerTransform.right * Time.deltaTime * Input.GetAxis("Horizontal") * speed;
                if (FlyToggle || FastFlyToggle || CameraFlyToggle)
                {
                    if (Input.GetKey(KeyCode.Q))
                        playerTransform.position -= new Vector3(0f, Time.deltaTime * speed, 0f);

                    if (Input.GetKey(KeyCode.E))
                        playerTransform.position += new Vector3(0f, Time.deltaTime * speed, 0f);
                }                
            }
        }        
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
        public static float Space = 340282300000000000000000000000000000000f;
        private void ToSpace()
        {
            var player = VRCPlayer.field_Internal_Static_VRCPlayer_0;
            if (player == null)
                return;

            if (_motionState == null)
                _motionState = player.GetComponent<VRCMotionState>();

            if (!SpaceCope) return;

            var playerTransform = player.transform;

            if (VRCInputManager.Method_Public_Static_VRCInput_String_0("Jump").prop_Single_2 == 1f)
            {
                Vector3 velocity = Networking.LocalPlayer.GetVelocity();
                velocity.y = Networking.LocalPlayer.GetJumpImpulse();
                Networking.LocalPlayer.SetVelocity(velocity);
            }

            if (XRDevice.isPresent)
            {
                playerTransform.position += playerTransform.forward * Time.deltaTime * Input.GetAxis("Vertical") * Space;
                playerTransform.position += playerTransform.right * Time.deltaTime * Input.GetAxis("Horizontal") * Space;
                playerTransform.position += new Vector3(0f, Time.deltaTime * Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") * Space);
                if (VRCInputManager.Method_Public_Static_VRCInput_String_0("Jump").prop_Single_2 == 1f)
                {                    
                    playerTransform.position += new Vector3(0f, Time.deltaTime * Space, 0f);
                }
            }
            else
            {
                var speed = Input.GetKey(KeyCode.LeftShift) ? Space * 9999999999999999999 : Space;
                playerTransform.position += playerTransform.forward * Time.deltaTime * Input.GetAxis("Vertical") * speed;
                playerTransform.position += playerTransform.right * Time.deltaTime * Input.GetAxis("Horizontal") * speed;

                if (Input.GetKey(KeyCode.Space))
                    playerTransform.position += new Vector3(0f, Time.deltaTime * speed, 0f);

                if (VRCInputManager.Method_Public_Static_VRCInput_String_0("Jump").prop_Single_2 == 1f)
                {
                    playerTransform.position += new Vector3(0f, Time.deltaTime * speed, 0f);
                    playerTransform.position += new Vector3(0f, Time.deltaTime * Space, 0f);
                }
            }
            _motionState?.Reset();
        }

    }
}
