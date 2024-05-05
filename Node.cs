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

        public Node(string Path, int Id)
        {
            path = Path;
            id = Id;
        }
    }
}
