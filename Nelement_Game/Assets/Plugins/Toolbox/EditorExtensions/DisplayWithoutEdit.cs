using UnityEngine;

namespace ProyectNelement.Toolbox.Editor.CustomProperties
{
    public class DisplayWithoutEdit : PropertyAttribute 
    {
        public DisplayWithoutEdit() { }
        
        public DisplayWithoutEdit(string format) { }
    }
}
