using NetCore.Common.Base;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetCore.Common.Utils
{
    public class PatternValidator : InitializeObject
    {
        private IList<Regex> allowPatterns = new List<Regex>();

        protected override void Init()
        {
            AssertUtils.AssertHasElements(this.allowPatterns);
            this.allowPatterns = ((List<Regex>)allowPatterns).AsReadOnly();
        }

        public PatternValidator AllowPatterns(params string[] patterns)
        {
            AssertNotInitialized();
            foreach (var pattern in patterns)
            {
                this.allowPatterns.Add(new Regex(pattern));
            }
            return this;
        }

        public PatternValidator AllowPatterns(params Regex[] patterns)
        {
            AssertNotInitialized();
            foreach (var pattern in patterns)
            {
                this.allowPatterns.Add(pattern);
            }
            return this;
        }

        public bool Validate(string value)
        {
            Initialize();

            AssertUtils.AssertNotNull(value);
            foreach (var pattern in this.allowPatterns)
            {
                if (pattern.IsMatch(value))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
