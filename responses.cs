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
    
    public partial class responses
    {
        public long ID_respone { get; set; }
        public long ID_vacancy { get; set; }
        public long ID_resume { get; set; }
        public Nullable<bool> flag { get; set; }
    
        public virtual resume_tab resume_tab { get; set; }
        public virtual vacancy vacancy { get; set; }
    }
}
