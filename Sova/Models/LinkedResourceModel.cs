using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sova.Models
{
    public class LinkedResourceModel
    {
        public IList<LinkModel> LinkModels { get; set; } = new List<LinkModel>();
    }
}
