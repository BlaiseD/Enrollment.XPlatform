using System.Collections.Generic;

namespace Enrollment.Parameters.Expansions
{
    public class SelectExpandItemParameters
    {
        public SelectExpandItemParameters()
        {
        }

        public SelectExpandItemParameters(string memberName, SelectExpandItemFilterParameters filter = null, SelectExpandItemQueryFunctionParameters queryFunction = null, List<string> selects = null, List<SelectExpandItemParameters> expandedItems = null)
        {
            MemberName = memberName;
            Filter = filter;
            QueryFunction = queryFunction;
            Selects = selects ?? new List<string>();
            ExpandedItems = expandedItems ?? new List<SelectExpandItemParameters>();
        }

        public string MemberName { get; set; }
        public SelectExpandItemFilterParameters Filter { get; set; }
        public SelectExpandItemQueryFunctionParameters QueryFunction { get; set; }
        public List<string> Selects { get; set; } = new List<string>();
        public List<SelectExpandItemParameters> ExpandedItems { get; set; } = new List<SelectExpandItemParameters>();
    }
}
