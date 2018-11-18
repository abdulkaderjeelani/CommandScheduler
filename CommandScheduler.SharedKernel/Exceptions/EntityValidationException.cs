using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.SharedKernel.Exceptions
{
    [Serializable]
    public class EntityValidationException : Exception
    {
        public IEnumerable<string> ValidationErrors;

        private List<string> _validationErrors = null;

        public EntityValidationException(List<string> errors)
        {
            _validationErrors = errors;
        }

    }
}
