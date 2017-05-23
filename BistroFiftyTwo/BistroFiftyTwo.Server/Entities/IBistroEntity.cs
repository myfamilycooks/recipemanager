using System;
using System.Collections.Generic;

namespace BistroFiftyTwo.Server.Entities
{
    public interface IBistroEntity
    {
        Guid ID { get; set; }
        DateTime CreatedDate { get; set; }
        string CreatedBy { get; set; }
        DateTime ModifiedDate { get; set; }
        string ModifiedBy { get; set; }

        string TableName();
        List<string> Columns();
    }
}