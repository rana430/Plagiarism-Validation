using System.Collections.Generic;

namespace Plagiarism_Validation
{
    public static class StringIdAssigner
    {
        private static Dictionary<string, int> idMap = new Dictionary<string, int>();
        private static Dictionary<int, string> reverseIdMap = new Dictionary<int, string>();
        public static int currentId = 1;

        public static int GetId(string str)
        {
            if (!idMap.ContainsKey(str))
            {
                idMap[str] = currentId;
                reverseIdMap[currentId] = str;
                currentId++;
            }
            return idMap[str];
        }

        public static string GetString(int id)
        {
            if (reverseIdMap.ContainsKey(id))
            {
                return reverseIdMap[id];
            }
            else
            {
                return null; // ID not found
            }
        }

        public static void Reset()
        {
            idMap.Clear();
            reverseIdMap.Clear();
            currentId = 1;
        }
    }
}
