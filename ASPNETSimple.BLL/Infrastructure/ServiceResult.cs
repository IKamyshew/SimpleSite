using System.Collections.Generic;

namespace ASPNETSimple.BLL.Infrastructure
{
    public class ServiceResult
    {
        private static readonly ServiceResult _success = new ServiceResult(true);

        /// <summary>
        /// True if the operation was successful
        /// 
        /// </summary>
        public bool Succeeded { get; private set; }

        /// <summary>
        /// List of errors
        /// 
        /// </summary>
        public IEnumerable<string> Errors { get; private set; }

        /// <summary>
        /// Static success result
        /// 
        /// </summary>
        /// 
        /// <returns/>
        public static ServiceResult Success
        {
            get
            {
                return ServiceResult._success;
            }
        }

        /// <summary>
        /// Failure constructor that takes error messages
        /// 
        /// </summary>
        /// <param name="errors"/>
        public ServiceResult(params string[] errors)
            : this((IEnumerable<string>)errors)
        {
        }

        /// <summary>
        /// Failure constructor that takes error messages
        /// 
        /// </summary>
        /// <param name="errors"/>
        public ServiceResult(IEnumerable<string> errors)
        {
            if (errors == null)
                errors = (IEnumerable<string>)new string[1]
                {
                    "Default Service Error"
                };
            this.Succeeded = false;
            this.Errors = errors;
        }

        /// <summary>
        /// Constructor that takes whether the result is successful
        /// 
        /// </summary>
        /// <param name="success"/>
        protected ServiceResult(bool success)
        {
            this.Succeeded = success;
            this.Errors = (IEnumerable<string>)new string[0];
        }

        /// <summary>
        /// Failed helper method
        /// 
        /// </summary>
        /// <param name="errors"/>
        /// <returns/>
        public static ServiceResult Failed(params string[] errors)
        {
            return new ServiceResult(errors);
        }
    }
}
