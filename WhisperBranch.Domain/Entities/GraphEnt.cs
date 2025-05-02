using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhisperBranch.Domain.Entities
{
    public class GraphEnt 
    {
        public Guid Id { get; set; }

        [Column(TypeName = "jsonb")]
        public string Schema { get; set; }
    }
}
