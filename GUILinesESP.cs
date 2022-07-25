using UnityEngine;
using Wrapper.PlayerWrapper;

namespace EXO
{
    internal class GUILinesESP
    {        
        public static void OnGUI()
        {            
            foreach (VRC.Player player in PlayerWrapper.GetAllButSelf())
            {
                //In-Game Positio
                Vector3 pivotPos = player.transform.position; //Pivot point NOT at the feet, at the center
                Vector3 playerFootPos; playerFootPos.x = pivotPos.x; playerFootPos.z = pivotPos.z; playerFootPos.y = pivotPos.y - 2f; //At the feet
                Vector3 playerHeadPos; playerHeadPos.x = pivotPos.x; playerHeadPos.z = pivotPos.z; playerHeadPos.y = pivotPos.y + 2f; //At the head
                Vector3 playerChestPos; playerChestPos.x = pivotPos.x; playerChestPos.z = pivotPos.z; playerChestPos.y = pivotPos.y + 1f; //At the chest

                //Screen Position
                Vector3 w2s_playerpos = Camera.main.WorldToScreenPoint(pivotPos);
                Vector3 w2s_footpos = Camera.main.WorldToScreenPoint(playerFootPos);
                Vector3 w2s_headpos = Camera.main.WorldToScreenPoint(playerHeadPos);
                Vector3 w2s_chestpos = Camera.main.WorldToScreenPoint(playerChestPos);

                if (w2s_footpos.z > 0f)
                {
                    DrawLineESP(w2s_playerpos, w2s_footpos, w2s_headpos, Color.red);
                }
            }
        }

        public static void DrawLineESP(Vector3 playerpos, Vector3 footpos, Vector3 headpos, Color color) //Rendering the ESP
        {
            float height = headpos.y - footpos.y;
            float widthOffset = 2f;
            float width = height / widthOffset;
            
            //Snapline
            Render.Render.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(playerpos.x, (float)Screen.height - playerpos.y), color, 2f);
        }
    }
}
