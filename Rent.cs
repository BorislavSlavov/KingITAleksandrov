//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KingITAleksandrov
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rent
    {
        public int rentNumber { get; set; }
        public Nullable<int> tenantNumber { get; set; }
        public Nullable<int> shopCenterNumber { get; set; }
        public string pavilionNumber { get; set; }
        public Nullable<int> emloyeeNumber { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> rentDateStart { get; set; }
        public Nullable<System.DateTime> rentDateStop { get; set; }
    
        public virtual Employees Employees { get; set; }
        public virtual Pavilions Pavilions { get; set; }
        public virtual Tenants Tenants { get; set; }
    }
}
