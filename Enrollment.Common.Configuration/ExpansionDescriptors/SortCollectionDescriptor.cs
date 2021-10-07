﻿using System.Collections.Generic;

namespace Enrollment.Common.Configuration.ExpansionDescriptors
{
    public class SortCollectionDescriptor
    {
        public ICollection<SortDescriptionDescriptor> SortDescriptions { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
