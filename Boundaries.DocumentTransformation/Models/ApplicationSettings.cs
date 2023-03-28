using System.Collections.Generic;

namespace Boundaries.DocumentTransformation
{
    public sealed class ApplicationSettings
    {
        public Settings Settings { get; set; }
        public IEnumerable<EngineView> Engines { get; set; }
        public ApplicationRules Rules { get; set; }
    }
}
