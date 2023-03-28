using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public sealed class ApplicationSettings
    {
        public ServiceSettings Settings { get; }
        public IEnumerable<EngineView> Engines { get; }
        public ApplicationRules Rules { get; }

        public ApplicationSettings(ServiceSettings settings, IEnumerable<EngineView> engines, ApplicationRules rules)
        {
            Settings = settings;
            Engines = engines;
            Rules = rules;
        }

        public override bool Equals(object obj)
        {
            return obj is ApplicationSettings other &&
                   EqualityComparer<ServiceSettings>.Default.Equals(Settings, other.Settings) &&
                   EqualityComparer<IEnumerable<EngineView>>.Default.Equals(Engines, other.Engines) &&
                   EqualityComparer<ApplicationRules>.Default.Equals(Rules, other.Rules);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Settings, Engines, Rules);
        }
    }
}
