using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace BookwormApp.Models
{
    public partial class DataEntities
    {
        [DbFunction("DataModel.Store", "ufn_GetUserId")]
        public int ufn_GetUserId(string Email)
        {
            List<ObjectParameter> parameters = new List<ObjectParameter>();
            parameters.Add(new ObjectParameter("Email", Email));
            var lObjectContext = ((IObjectContextAdapter)this).ObjectContext;
            var output = lObjectContext.CreateQuery<int>("DataModel.Store.ufn_GetUserId(@Email)", parameters.ToArray()).Execute(MergeOption.NoTracking).FirstOrDefault();
            return output;
        }
    }
}