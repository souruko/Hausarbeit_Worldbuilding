//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hausarbeit_Worldbuilding
{
    using System;
    using System.Collections.Generic;
    
    public partial class Character_Group
    {
        public int GroupID { get; set; }
        public int CharacterID { get; set; }
        public string Description { get; set; }
    
        public virtual Character Character { get; set; }
        public virtual Gruppe Gruppe { get; set; }
    }
}
