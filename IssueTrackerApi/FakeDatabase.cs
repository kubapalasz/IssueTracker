using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTrackerApi
{
    public static class FakeDatabase
    {
        public static readonly List<IssueModel> Issues = new List<IssueModel>();

    }
}
