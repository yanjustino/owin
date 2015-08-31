using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MidwareCodeSchool
{
    using System.IO;
    using System.Text;
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class Midware
    {
        ///The primary interface in OWIN is called the application delegate or AppFunc. 
        /// An application delegate takes the IDictionary<string, object> environment and returns a Task 
        /// when it has finished processing.
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
