using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using System.Text;
using System.Threading.Tasks;

namespace Plagiarism_Validation
{
    public class TestFile
    {
        public string testType;
        public string testLevel;
        public string testFilePaths;
        public Stopwatch totalTimeStopwatch;
        public Stopwatch MSTTimeStopwatch;
        public Stopwatch GroupingTimeStopwatch;
        public TestFile(string TestType, string TestLevel, string TestFilePath)
        {
            totalTimeStopwatch = new Stopwatch();
            MSTTimeStopwatch = new Stopwatch();
            GroupingTimeStopwatch = new Stopwatch();
            testFilePaths = TestFilePath;
            testType = TestType;
            testLevel = TestLevel;
        }
        
    }
}
