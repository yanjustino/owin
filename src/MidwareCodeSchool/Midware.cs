using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MidwareCodeSchool
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class Midware
    {
        private AppFunc _next;

        public Midware(AppFunc next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            await _next.Invoke(environment);
        }
    }
}
