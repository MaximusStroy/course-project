//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vacancy
{
    using System;
    using System.Collections.Generic;
    
    public partial class vacancy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public vacancy()
        {
            this.responses = new HashSet<responses>();
        }
    
        public long ID_vac { get; set; }
        public long ID_employee { get; set; }
        public string post { get; set; }
        public Nullable<decimal> salary { get; set; }
        public string competencies { get; set; }
    
        public virtual employers employers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<responses> responses { get; set; }
    }
}
