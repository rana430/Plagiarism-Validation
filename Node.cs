using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plagiarism_Validation
{
    public class Node
    {
        public string path;
        public int id;
        public string idString;
        
        public Node(string Path, String Id)
        {
            path = Path;
            idString = Id;
            id = StringIdAssigner.GetId(Id);
        }
       
    }
}
